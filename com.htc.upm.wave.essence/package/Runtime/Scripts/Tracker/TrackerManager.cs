// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC\u2019s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using Wave.Native;
using Wave.Essence.Events;
using Wave.XR.Settings;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Diagnostics;
using UnityEngine.XR;

namespace Wave.Essence.Tracker
{
	public class TrackerManager : MonoBehaviour
	{
		private const string LOG_TAG = "Wave.Essence.Tracker.TrackerManager";
		private static void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		[SerializeField]
		private bool m_UseXRDevice = false;
		public bool UseXRDevice { get { return m_UseXRDevice; } set { m_UseXRDevice = value; } }

		public enum TrackerStatus
		{
			// Initial, can call Start API in this state.
			NotStart,
			StartFailure,

			// Processing, should NOT call API in this state.
			Starting,
			Stopping,

			// Running, can call Stop API in this state.
			Available,

			// Do nothing.
			NoSupport
		}

		private static TrackerManager instance = null;
		public static TrackerManager Instance { get { return instance; } }

		[SerializeField]
		private bool m_InitialStartTracker = false;
		public bool InitialStartTracker { get { return m_InitialStartTracker; } set { m_InitialStartTracker = value; } }

		readonly TrackerId[] s_TrackerIds = new TrackerId[]
		{
			TrackerId.Tracker0,
			TrackerId.Tracker1,
			TrackerId.Tracker2,
			TrackerId.Tracker3,
		};

		#region Monobehaviour overrides
		private void Awake()
		{
			instance = this;

			/// Checks the feature support.
			var supportedFeature = Interop.WVR_GetSupportedFeatures();

			if ((supportedFeature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_Tracker) == 0)
				m_TrackerStatus = TrackerStatus.NoSupport;
			else
				m_TrackerStatus = TrackerStatus.NotStart;

			Log.i(LOG_TAG, "Awake() tracker status: " + m_TrackerStatus);

			/// Initializes the tracker attributes.
			s_TrackerCaps = new WVR_TrackerCapabilities[s_TrackerIds.Length];
			for (int i = 0; i < s_TrackerIds.Length; i++)
			{
				s_TrackerConnection.Add(s_TrackerIds[i], false);

				s_TrackerRole.Add(s_TrackerIds[i], TrackerRole.Undefined);

				ResetTrackerCapability(s_TrackerIds[i]);

				s_TrackerPoses.Add(s_TrackerIds[i], new TrackerPose());

				s_TrackerButtonBits.Add(s_TrackerIds[i], 0);
				s_TrackerTouchBits.Add(s_TrackerIds[i], 0);
				s_TrackerAnalogBits.Add(s_TrackerIds[i], 0);

				s_TrackerButtonStates.Add(s_TrackerIds[i], new TrackerButtonStates());

				s_ButtonAxisType.Add(s_TrackerIds[i], new AxisType[(int)WVR_InputId.WVR_InputId_Max]);

				ss_TrackerPress.Add(s_TrackerIds[i], new bool[(int)WVR_InputId.WVR_InputId_Max]);
				ss_TrackerPressEx.Add(s_TrackerIds[i], new bool[(int)WVR_InputId.WVR_InputId_Max]);
				ss_TrackerTouch.Add(s_TrackerIds[i], new bool[(int)WVR_InputId.WVR_InputId_Max]);
				ss_TrackerTouchEx.Add(s_TrackerIds[i], new bool[(int)WVR_InputId.WVR_InputId_Max]);

				for (int id = 0; id < (int)WVR_InputId.WVR_InputId_Max; id++)
				{
					s_ButtonAxisType[s_TrackerIds[i]][id] = AxisType.None;

					ss_TrackerPress[s_TrackerIds[i]][id] = false;
					ss_TrackerPressEx[s_TrackerIds[i]][id] = false;
					ss_TrackerTouch[s_TrackerIds[i]][id] = false;
					ss_TrackerTouchEx[s_TrackerIds[i]][id] = false;
				}

				s_TrackerBattery.Add(s_TrackerIds[i], 0);
			}
		}

		private void OnEnable()
		{
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerConnected, OnTrackerConnected);
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerDisconnected, OnTrackerDisconnected);
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerBatteryLevelUpdate, OnTrackerBatteryLevelUpdate);
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerButtonPressed, OnTrackerButtonPressed);
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerButtonUnpressed, OnTrackerButtonUnpressed);
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerTouchTapped, OnTrackerTouchTapped);
			SystemEvent.Listen(WVR_EventType.WVR_EventType_TrackerTouchUntapped, OnTrackerTouchUntapped);

			if (m_InitialStartTracker) { StartTracker(); }
		}
		private void OnDisable()
		{
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerConnected, OnTrackerConnected);
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerDisconnected, OnTrackerDisconnected);
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerBatteryLevelUpdate, OnTrackerBatteryLevelUpdate);
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerButtonPressed, OnTrackerButtonPressed);
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerButtonUnpressed, OnTrackerButtonUnpressed);
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerTouchTapped, OnTrackerTouchTapped);
			SystemEvent.Remove(WVR_EventType.WVR_EventType_TrackerTouchUntapped, OnTrackerTouchUntapped);
		}

		static List<InputDevice> s_InputDevices = new List<InputDevice>();
		private void Update()
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				InputDevices.GetDevices(s_InputDevices);
				for (int i = 0; i < s_TrackerIds.Length; i++)
				{
					CheckXRDeviceTrackerConnection(s_TrackerIds[i]);
					CheckXRDeviceTrackerButtons(s_TrackerIds[i]);
				}
			}
			CheckAllTrackerPoseStates();
		}

		private void OnApplicationPause(bool pause)
		{
			DEBUG("OnApplicationPause() " + pause);
			if (m_UseXRDevice && !Application.isEditor) { return; }
			if (GetTrackerStatus() != TrackerStatus.Available) { return; }
			if (!pause)
			{
				for (int i = 0; i < s_TrackerIds.Length; i++)
				{
					DEBUG("Resume() 1.check " + s_TrackerIds[i] + " connection.");
					CheckTrackerConnection(s_TrackerIds[i]);

					DEBUG("Resume() 2.check " + s_TrackerIds[i] + " role.");
					CheckTrackerRole(s_TrackerIds[i]);

					DEBUG("Resume() 3.check " + s_TrackerIds[i] + " capability.");
					CheckTrackerCapbility(s_TrackerIds[i]);

					DEBUG("Resume() 4. check " + s_TrackerIds[i] + " input capability.");
					CheckTrackerInputs(s_TrackerIds[i]);

					DEBUG("Resume() 5. check " + s_TrackerIds[i] + " button analog type.");
					CheckTrackerButtonAnalog(s_TrackerIds[i]);

					DEBUG("Resume() 6. check " + s_TrackerIds[i] + " buttons.");
					CheckAllTrackerButtons(s_TrackerIds[i]);

					DEBUG("Resume() 7.check " + s_TrackerIds[i] + " battery.");
					CheckTrackerBattery(s_TrackerIds[i]);
				}
			}
		}

		private void Start()
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }
			if (GetTrackerStatus() != TrackerStatus.Available) { return; }
			for (int i = 0; i < s_TrackerIds.Length; i++)
			{
				DEBUG("Start() 1.check " + s_TrackerIds[i] + " connection.");
				CheckTrackerConnection(s_TrackerIds[i]);

				DEBUG("Start() 2.check " + s_TrackerIds[i] + " role.");
				CheckTrackerRole(s_TrackerIds[i]);

				DEBUG("Start() 3.check " + s_TrackerIds[i] + " capability.");
				CheckTrackerCapbility(s_TrackerIds[i]);

				// For WVR_TrackerCapabilities.supportsInputDevice
				DEBUG("Start() 4. check " + s_TrackerIds[i] + " input capability.");
				CheckTrackerInputs(s_TrackerIds[i]);

				// Depends on IsTrackerInputAvailable
				DEBUG("Start() 5. check " + s_TrackerIds[i] + " button analog type.");
				CheckTrackerButtonAnalog(s_TrackerIds[i]);

				// Depends on IsTrackerInputAvailable
				DEBUG("Start() 6. check " + s_TrackerIds[i] + " buttons.");
				CheckAllTrackerButtons(s_TrackerIds[i]);

				// For WVR_TrackerCapabilities.supportsBatteryLevel
				DEBUG("Start() 7.check " + s_TrackerIds[i] + " battery.");
				CheckTrackerBattery(s_TrackerIds[i]);
			}
		}
		#endregion

		#region Life Cycle
		private TrackerStatus m_TrackerStatus = TrackerStatus.NotStart;
		private static ReaderWriterLockSlim m_TrackerStatusRWLock = new ReaderWriterLockSlim();
		private void SetTrackerStatus(TrackerStatus status)
		{
			try
			{
				m_TrackerStatusRWLock.TryEnterWriteLock(2000);
				m_TrackerStatus = status;
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "SetTrackerStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				m_TrackerStatusRWLock.ExitWriteLock();
			}
		}

		private bool CanStartTracker()
		{
			TrackerStatus status = GetTrackerStatus();
			if (status == TrackerStatus.Available ||
				status == TrackerStatus.NoSupport ||
				status == TrackerStatus.Starting ||
				status == TrackerStatus.Stopping)
			{
				return false;
			}

			return true;
		}
		private bool CanStopTracker()
		{
			TrackerStatus status = GetTrackerStatus();
			if (status == TrackerStatus.Available) { return true; }
			return false;
		}

		private uint m_TrackerRefCount = 0;
		public delegate void TrackerResultDelegate(object sender, bool result);
		private event TrackerResultDelegate trackerResultCB = null;
		private void StartTrackerLock()
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				WaveXRSettings settings = WaveXRSettings.GetInstance();
				if (settings != null && settings.EnableTracker == false)
				{
					DEBUG("StartTrackerLock() Activate WaveXRSettings.EnableTracker.");
					settings.EnableTracker = true;
					SettingsHelper.SetBool(WaveXRSettings.EnableTrackerText, true);
					SetTrackerStatus((settings.EnableTracker ? TrackerStatus.Available : TrackerStatus.NotStart));
					return;
				}
			}

			if (!CanStartTracker()) { return; }

			SetTrackerStatus(TrackerStatus.Starting);
			WVR_Result result = Interop.WVR_StartTracker();
			switch(result)
			{
				case WVR_Result.WVR_Success:
					SetTrackerStatus(TrackerStatus.Available);
					break;
				case WVR_Result.WVR_Error_FeatureNotSupport:
					SetTrackerStatus(TrackerStatus.NoSupport);
					break;
				default:
					SetTrackerStatus(TrackerStatus.StartFailure);
					break;
			}
			DEBUG("StartTrackerLock() result: " + result);
			if (result == WVR_Result.WVR_Success)
			{
				for (int i = 0; i < s_TrackerIds.Length; i++)
					CheckTrackerConnection(s_TrackerIds[i]);
			}

			if (trackerResultCB != null)
			{
				trackerResultCB(this, result == WVR_Result.WVR_Success ? true : false);
				trackerResultCB = null;
			}
		}

		private object trackerThreadLocker = new object();
		private void StartTrackerThread()
		{
			lock (trackerThreadLocker)
			{
				DEBUG("StartTrackerThread()");
				StartTrackerLock();
			}
		}
		public void StartTracker(TrackerResultDelegate callback)
		{
			if (trackerResultCB == null)
			{
				trackerResultCB = callback;
			}
			else
			{
				trackerResultCB += callback;
			}

			StartTracker();
		}
		public void StartTracker()
		{
			//string caller = new StackFrame(1, true).GetMethod().Name;
			m_TrackerRefCount++;
			//Log.i(LOG_TAG, "StartTracker(" + m_TrackerRefCount + ") from " + caller, true);

			if (!CanStartTracker())
			{
				DEBUG("StartTracker() can NOT start tracker.");
				if (trackerResultCB != null) { trackerResultCB = null; }
				return;
			}

			Thread tracker_t = new Thread(StartTrackerThread);
			tracker_t.Name = "StartTrackerThread";
			tracker_t.Start();
		}

		private void StopTrackerLock()
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				WaveXRSettings settings = WaveXRSettings.GetInstance();
				if (settings != null && settings.EnableTracker == true)
				{
					DEBUG("StopTrackerLock() Deactivate WaveXRSettings.EnableTracker.");
					settings.EnableTracker = false;
					SettingsHelper.SetBool(WaveXRSettings.EnableTrackerText, false);
					SetTrackerStatus((settings.EnableTracker ? TrackerStatus.Available : TrackerStatus.NotStart));
					return;
				}
			}

			if (!CanStopTracker()) { return; }

			SetTrackerStatus(TrackerStatus.Stopping);
			Interop.WVR_StopTracker();
			SetTrackerStatus(TrackerStatus.NotStart);

			// Reset all tracker status.
			for (int i = 0; i < s_TrackerIds.Length; i++)
			{
				TrackerId trackerId = s_TrackerIds[i];
				s_TrackerConnection[trackerId] = false;

				CheckTrackerRole(trackerId);
				CheckTrackerCapbility(trackerId);
				CheckTrackerInputs(trackerId);
				CheckTrackerButtonAnalog(trackerId);
			}
		}
		private void StopTrackerThread()
		{
			lock (trackerThreadLocker)
			{
				DEBUG("StopTrackerThread()");
				StopTrackerLock();
			}
		}
		public void StopTracker()
		{
			string caller = new StackFrame(1, true).GetMethod().Name;
			m_TrackerRefCount--;
			Log.i(LOG_TAG, "StopTracker(" + m_TrackerRefCount + ") from " + caller, true);
			if (m_TrackerRefCount > 0) { return; }

			if (!CanStopTracker())
			{
				DEBUG("CanStopTracker() can NOT stop tracker.");
				return;
			}

			Thread tracker_t = new Thread(StopTrackerThread);
			tracker_t.Name = "StopTrackerThread";
			tracker_t.Start();
		}
		#endregion

		#region Unity XR Tracker definitions
		/// <summary> Standalone Tracker Characteristics </summary>
		public const InputDeviceCharacteristics kAloneTrackerCharacteristics = (
			InputDeviceCharacteristics.TrackedDevice
		);
		/// <summary> Right Tracker Characteristics </summary>
		public const InputDeviceCharacteristics kRightTrackerCharacteristics = (
			InputDeviceCharacteristics.TrackedDevice |
			InputDeviceCharacteristics.Right
		);
		/// <summary> Left Tracker Characteristics </summary>
		public const InputDeviceCharacteristics kLeftTrackerCharacteristics = (
			InputDeviceCharacteristics.TrackedDevice |
			InputDeviceCharacteristics.Left
		);

		public static bool IsTrackerDevice(InputDevice input, TrackerId trackerId)
		{
			if (input.name.Equals(trackerId.Name()) && input.serialNumber.Equals(trackerId.SerialNumber()))
				return true;

			return false;
		}
		#endregion

		#region Connection
		Dictionary<TrackerId, bool> s_TrackerConnection = new Dictionary<TrackerId, bool>();
		private void CheckTrackerConnection(TrackerId trackerId)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			bool connected = Interop.WVR_IsTrackerConnected(trackerId.Id());
			if (s_TrackerConnection[trackerId] != connected)
			{
				s_TrackerConnection[trackerId] = connected;
				DEBUG("CheckTrackerConnection() " + trackerId + ": " + s_TrackerConnection[trackerId]);
				CheckStatusWhenConnectionChanges(trackerId);
			}
		}
		void CheckStatusWhenConnectionChanges(TrackerId trackerId)
		{
			CheckTrackerRole(trackerId);
			CheckTrackerCapbility(trackerId);
			CheckTrackerInputs(trackerId);
			CheckTrackerButtonAnalog(trackerId);
			CheckAllTrackerButtons(trackerId);
			CheckTrackerBattery(trackerId);
		}
		private void OnTrackerConnected(WVR_Event_t systemEvent)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			TrackerId trackerId = systemEvent.tracker.trackerId.Id();
			DEBUG("OnTrackerConnected() " + trackerId);

			if (s_TrackerConnection[trackerId] != true)
			{
				s_TrackerConnection[trackerId] = true;
				CheckStatusWhenConnectionChanges(trackerId);
			}
		}
		private void OnTrackerDisconnected(WVR_Event_t systemEvent)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			TrackerId trackerId = systemEvent.tracker.trackerId.Id();
			DEBUG("OnTrackerDisconnected() " + trackerId);

			if (s_TrackerConnection[trackerId] != false)
			{
				s_TrackerConnection[trackerId] = false;
				CheckStatusWhenConnectionChanges(trackerId);
			}
		}
		private void CheckXRDeviceTrackerConnection(TrackerId trackerId)
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				bool foundTracker = false;
				for (int i = 0; i < s_InputDevices.Count; i++)
				{
					if (!s_InputDevices[i].isValid) { continue; }

					if (IsTrackerDevice(s_InputDevices[i], trackerId))
					{
						foundTracker = true;
						break;
					}
				}

				if (s_TrackerConnection[trackerId] != foundTracker)
				{
					s_TrackerConnection[trackerId] = foundTracker;
					DEBUG("CheckXRDeviceTrackerConnection() " + trackerId + " is " + (s_TrackerConnection[trackerId] ? "connected." : "disconnected."));
					CheckStatusWhenConnectionChanges(trackerId);
				}
			}
		}
		#endregion

		#region Role
		Dictionary<TrackerId, TrackerRole> s_TrackerRole = new Dictionary<TrackerId, TrackerRole>();
		private void CheckTrackerRole(TrackerId trackerId)
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				// Default value.
				s_TrackerRole[trackerId] = TrackerRole.Undefined;

				for (int i = 0; i < s_InputDevices.Count; i++)
				{
					if (!s_InputDevices[i].isValid) { continue; }

					// Found the tracker.
					if (IsTrackerDevice(s_InputDevices[i], trackerId))
					{
						DEBUG("CheckTrackerRole() characteristics: " + s_InputDevices[i].characteristics);
						if (s_InputDevices[i].characteristics.Equals(kLeftTrackerCharacteristics))
							s_TrackerRole[trackerId] = TrackerRole.Pair1_Left;
						else if (s_InputDevices[i].characteristics.Equals(kRightTrackerCharacteristics))
							s_TrackerRole[trackerId] = TrackerRole.Pair1_Right;
						else
							s_TrackerRole[trackerId] = TrackerRole.Standalone;
					}
				}

				DEBUG("CheckTrackerRole() " + trackerId
					+ "[" + trackerId.Name() + "]"
					+ "[" + trackerId.SerialNumber() + "]"
					+ ": " + s_TrackerRole[trackerId]);
				return;
			}

			if (s_TrackerConnection[trackerId])
			{
				s_TrackerRole[trackerId] = (Interop.WVR_GetTrackerRole(trackerId.Id())).Id();
				DEBUG("CheckTrackerRole() " + trackerId + " role: " + s_TrackerRole[trackerId]);
			}
			else
			{
				s_TrackerRole[trackerId] = TrackerRole.Undefined;
			}
		}
		#endregion

		#region Capability
		WVR_TrackerCapabilities[] s_TrackerCaps = null;
		private void ResetTrackerCapability(TrackerId trackerId)
		{
			s_TrackerCaps[trackerId.Num()].supportsOrientationTracking = false;
			s_TrackerCaps[trackerId.Num()].supportsPositionTracking = false;
			s_TrackerCaps[trackerId.Num()].supportsInputDevice = false;
			s_TrackerCaps[trackerId.Num()].supportsHapticVibration = false;
			s_TrackerCaps[trackerId.Num()].supportsBatteryLevel = false;

		}
		private void CheckTrackerCapbility(TrackerId trackerId)
		{
			if (s_TrackerConnection[trackerId])
			{
				WVR_Result result = Interop.WVR_GetTrackerCapabilities(trackerId.Id(), ref s_TrackerCaps[trackerId.Num()]);
				if (result != WVR_Result.WVR_Success) { ResetTrackerCapability(trackerId); }

				DEBUG("CheckTrackerCapbility() " + trackerId + ", result: " + result
					+ "\n\tsupportsOrientationTracking: " + s_TrackerCaps[trackerId.Num()].supportsOrientationTracking
					+ "\n\tsupportsPositionTracking: " + s_TrackerCaps[trackerId.Num()].supportsPositionTracking
					+ "\n\tsupportsInputDevice: " + s_TrackerCaps[trackerId.Num()].supportsInputDevice
					+ "\n\tsupportsHapticVibration: " + s_TrackerCaps[trackerId.Num()].supportsHapticVibration
					+ "\n\tsupportsBatteryLevel: " + s_TrackerCaps[trackerId.Num()].supportsBatteryLevel);
			}
			else
			{
				ResetTrackerCapability(trackerId);
			}
		}
		#endregion

		#region Pose State
		class TrackerPose
		{
			public bool valid = false;
			public RigidTransform rigid = RigidTransform.identity;

			public TrackerPose()
			{
				valid = false;
				rigid = RigidTransform.identity;
			}
		}
		Dictionary<TrackerId, TrackerPose> s_TrackerPoses = new Dictionary<TrackerId, TrackerPose>();
		private void CheckTrackerPoseState(TrackerId trackerId)
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				// Default value.
				s_TrackerPoses[trackerId].valid = false;
				s_TrackerPoses[trackerId].rigid = RigidTransform.identity;

				for (int i = 0; i < s_InputDevices.Count; i++)
				{
					if (!s_InputDevices[i].isValid) { continue; }

					// Found the tracker.
					if (IsTrackerDevice(s_InputDevices[i], trackerId))
					{
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.isTracked, out bool validPose))
						{
							s_TrackerPoses[trackerId].valid = validPose;
						}
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position))
						{
							s_TrackerPoses[trackerId].rigid.pos = position;
						}
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation))
						{
							s_TrackerPoses[trackerId].rigid.rot = rotation;
						}
					}
				}

				return;
			}

			if (s_TrackerConnection[trackerId] && s_TrackerCaps[trackerId.Num()].supportsOrientationTracking)
			{
				WVR_PoseOriginModel origin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnHead_3DoF;
				if (s_TrackerCaps[trackerId.Num()].supportsPositionTracking)
				{
					origin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnHead;
				}

				WVR_PoseState_t pose = new WVR_PoseState_t();
				WVR_Result result = Interop.WVR_GetTrackerPoseState(trackerId.Id(), origin, 0, ref pose);
				if (result == WVR_Result.WVR_Success)
				{
					s_TrackerPoses[trackerId].valid = pose.IsValidPose;
					s_TrackerPoses[trackerId].rigid.update(pose.PoseMatrix);
				}
				else
				{
					s_TrackerPoses[trackerId].valid = false;
				}
			}
		}
		private void CheckAllTrackerPoseStates()
		{
			for (int i = 0; i < s_TrackerIds.Length; i++)
			{
				CheckTrackerPoseState(s_TrackerIds[i]);
			}
		}
		#endregion

		#region Input Capability
		Dictionary<TrackerId, Int32> s_TrackerButtonBits = new Dictionary<TrackerId, Int32>();
		Dictionary<TrackerId, Int32> s_TrackerTouchBits = new Dictionary<TrackerId, Int32>();
		Dictionary<TrackerId, Int32> s_TrackerAnalogBits = new Dictionary<TrackerId, Int32>();
		private void CheckTrackerInputs(TrackerId trackerId)
		{
			s_TrackerButtonBits[trackerId] = s_TrackerConnection[trackerId] ?
				(s_TrackerCaps[trackerId.Num()].supportsInputDevice ?
					Interop.WVR_GetTrackerInputDeviceCapability(trackerId.Id(), WVR_InputType.WVR_InputType_Button) : 0
				) : 0;

			s_TrackerTouchBits[trackerId] = s_TrackerConnection[trackerId] ?
				(s_TrackerCaps[trackerId.Num()].supportsInputDevice ?
					Interop.WVR_GetTrackerInputDeviceCapability(trackerId.Id(), WVR_InputType.WVR_InputType_Touch) : 0
				) : 0;

			s_TrackerAnalogBits[trackerId] = s_TrackerConnection[trackerId] ?
				(s_TrackerCaps[trackerId.Num()].supportsInputDevice ?
					Interop.WVR_GetTrackerInputDeviceCapability(trackerId.Id(), WVR_InputType.WVR_InputType_Analog) : 0
				) : 0;

			DEBUG("CheckTrackerInputs() " + trackerId
				+ ", button: " + s_TrackerButtonBits[trackerId]
				+ ", touch: " + s_TrackerTouchBits[trackerId]
				+ ", analog: " + s_TrackerAnalogBits[trackerId]);
		}
		private bool IsTrackerInputAvailable(TrackerId trackerId, WVR_InputType inputType, uint id)
		{
			bool ret = false;

			Int32 input = 1 << (Int32)id;
			switch (inputType)
			{
				case WVR_InputType.WVR_InputType_Button:
					ret = ((s_TrackerButtonBits[trackerId] & input) == input);
					break;
				case WVR_InputType.WVR_InputType_Touch:
					ret = ((s_TrackerTouchBits[trackerId] & input) == input);
					break;
				case WVR_InputType.WVR_InputType_Analog:
					ret = ((s_TrackerAnalogBits[trackerId] & input) == input);
					break;
				default:
					break;
			}

			return ret;
		}
		#endregion

		#region Button Analog
		Dictionary<TrackerId, AxisType[]> s_ButtonAxisType = new Dictionary<TrackerId, AxisType[]>();
		private void CheckTrackerButtonAnalog(TrackerId trackerId)
		{
			for (uint id = 0; id < (uint)WVR_InputId.WVR_InputId_Max; id++)
			{
				s_ButtonAxisType[trackerId][id] = s_TrackerConnection[trackerId] ?
					(IsTrackerInputAvailable(trackerId, WVR_InputType.WVR_InputType_Analog, id) ?
						(Interop.WVR_GetTrackerInputDeviceAnalogType(trackerId.Id(), (WVR_InputId)id)).Id() : AxisType.None
					) : AxisType.None;
			}
			DEBUG("CheckTrackerButtonAnalog() " + trackerId
				+ ", system: " + s_ButtonAxisType[trackerId][WVR_InputId.WVR_InputId_Alias1_System.Num()]
				+ ", menu: " + s_ButtonAxisType[trackerId][WVR_InputId.WVR_InputId_Alias1_Menu.Num()]
				+ ", A: " + s_ButtonAxisType[trackerId][WVR_InputId.WVR_InputId_Alias1_A.Num()]
				+ ", B: " + s_ButtonAxisType[trackerId][WVR_InputId.WVR_InputId_Alias1_B.Num()]);
		}
		#endregion

		#region Button State
		class TrackerButtonStates
		{
			public bool[] s_ButtonPress = new bool[(int)WVR_InputId.WVR_InputId_Max];
			public int[] s_ButtonPressFrame = new int[(int)WVR_InputId.WVR_InputId_Max];
			public bool[] s_ButtonTouch = new bool[(int)WVR_InputId.WVR_InputId_Max];
			public int[] s_ButtonTouchFrame = new int[(int)WVR_InputId.WVR_InputId_Max];
			public Vector2[] s_ButtonAxis = new Vector2[(int)WVR_InputId.WVR_InputId_Max];
			public int[] s_ButtonAxisFrame = new int[(int)WVR_InputId.WVR_InputId_Max];

			public TrackerButtonStates()
			{
				for (int i = 0; i < s_ButtonPress.Length; i++)
				{
					s_ButtonPress[i] = false;
					s_ButtonPressFrame[i] = 0;
					s_ButtonTouch[i] = false;
					s_ButtonTouchFrame[i] = 0;
					s_ButtonAxis[i].x = 0;
					s_ButtonAxis[i].y = 0;
					s_ButtonAxisFrame[i] = 0;
				}
			}
		};
		Dictionary<TrackerId, TrackerButtonStates> s_TrackerButtonStates = new Dictionary<TrackerId, TrackerButtonStates>();
		/// <summary> Checks all buttons' states of a TrackerId. Do NOT call this function every frame. </summary>
		private void CheckAllTrackerButtons(TrackerId trackerId)
		{
			if (!s_TrackerConnection[trackerId]) { return; }

			CheckAllTrackerButton(trackerId, WVR_InputType.WVR_InputType_Button);
			CheckAllTrackerButton(trackerId, WVR_InputType.WVR_InputType_Touch);
			CheckAllTrackerButton(trackerId, WVR_InputType.WVR_InputType_Analog);
		}
		/// <summary> Checks all buttons' states of a TrackerId and WVR_InputType. Do NOT call this function every frame. </summary>
		private void CheckAllTrackerButton(TrackerId trackerId, WVR_InputType cap)
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				DEBUG("CheckAllTrackerButton() " + trackerId + " is not updated.");
				return;
			}

			for (uint id = 0; id < (uint)WVR_InputId.WVR_InputId_Max; id++)
			{
				switch (cap)
				{
					case WVR_InputType.WVR_InputType_Button:
						s_TrackerButtonStates[trackerId].s_ButtonPress[id] = s_TrackerConnection[trackerId] ?
							(IsTrackerInputAvailable(trackerId, cap, id) ?
								Interop.WVR_GetTrackerInputButtonState(trackerId.Id(), (WVR_InputId)id) : false
							) : false;
						break;
					case WVR_InputType.WVR_InputType_Touch:
						s_TrackerButtonStates[trackerId].s_ButtonTouch[id] = s_TrackerConnection[trackerId] ?
							(IsTrackerInputAvailable(trackerId, cap, id) ?
								Interop.WVR_GetTrackerInputTouchState(trackerId.Id(), (WVR_InputId)id) : false
							) : false;
						break;
					case WVR_InputType.WVR_InputType_Analog:
						if (s_TrackerConnection[trackerId] && IsTrackerInputAvailable(trackerId, cap, id))
						{
							WVR_Axis_t axis = Interop.WVR_GetTrackerInputAnalogAxis(trackerId.Id(), (WVR_InputId)id);
							s_TrackerButtonStates[trackerId].s_ButtonAxis[id].x = axis.x;
							s_TrackerButtonStates[trackerId].s_ButtonAxis[id].y = axis.y;
						}
						else
						{
							s_TrackerButtonStates[trackerId].s_ButtonAxis[id] = Vector2.zero;
						}
						break;
					default:
						break;
				}
			}
		}
		private void CheckXRDeviceTrackerButtons(TrackerId trackerId)
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				// Default value.
				for (uint id = 0; id < (uint)WVR_InputId.WVR_InputId_Max; id++)
				{
					s_TrackerButtonStates[trackerId].s_ButtonPress[id] = false;
					s_TrackerButtonStates[trackerId].s_ButtonTouch[id] = false;
				}

				for (int i = 0; i < s_InputDevices.Count; i++)
				{
					if (!s_InputDevices[i].isValid) { continue; }
					if (!IsTrackerDevice(s_InputDevices[i], trackerId)) { continue; }

					/**
					 * Button press
					 **/
					{
						// ------------------------ A or X ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.primaryButton, out bool pressAX))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_A.Num()] = pressAX;
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_X.Num()] = pressAX;
						}
						// ------------------------ B or Y ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.secondaryButton, out bool pressBY))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_B.Num()] = pressBY;
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_Y.Num()] = pressBY;
						}
						// ------------------------ Grip ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.gripButton, out bool pressGrip))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_Grip.Num()] = pressGrip;
						}
						// ------------------------ Trigger ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.triggerButton, out bool pressTrigger))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_Trigger.Num()] = pressTrigger;
						}
						// ------------------------ Menu ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.menuButton, out bool pressMenu))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_Menu.Num()] = pressMenu;
						}
						// ------------------------ Touchpad ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool pressTouchpad))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_Touchpad.Num()] = pressTouchpad;
						}
						// ------------------------ Thumbstick ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool pressThumbstick))
						{
							s_TrackerButtonStates[trackerId].s_ButtonPress[WVR_InputId.WVR_InputId_Alias1_Thumbstick.Num()] = pressThumbstick;
						}
					}

					/**
					 * Button touch
					 **/
					{
						// ------------------------ A or X ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.primaryTouch, out bool touchAX))
						{
							s_TrackerButtonStates[trackerId].s_ButtonTouch[WVR_InputId.WVR_InputId_Alias1_A.Num()] = touchAX;
							s_TrackerButtonStates[trackerId].s_ButtonTouch[WVR_InputId.WVR_InputId_Alias1_X.Num()] = touchAX;
						}
						// ------------------------ B or Y ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.secondaryTouch, out bool touchBY))
						{
							s_TrackerButtonStates[trackerId].s_ButtonTouch[WVR_InputId.WVR_InputId_Alias1_B.Num()] = touchBY;
							s_TrackerButtonStates[trackerId].s_ButtonTouch[WVR_InputId.WVR_InputId_Alias1_Y.Num()] = touchBY;
						}
						// ------------------------ Touchpad ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool touchTouchpad))
						{
							s_TrackerButtonStates[trackerId].s_ButtonTouch[WVR_InputId.WVR_InputId_Alias1_Touchpad.Num()] = touchTouchpad;
						}
						// ------------------------ Thumbstick ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out bool touchThumbstick))
						{
							s_TrackerButtonStates[trackerId].s_ButtonTouch[WVR_InputId.WVR_InputId_Alias1_Thumbstick.Num()] = touchThumbstick;
						}
					}

					/**
					 * Button axis
					 **/
					{
						// ------------------------ Trigger ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.trigger, out float axisTrigger))
						{
							s_TrackerButtonStates[trackerId].s_ButtonAxis[WVR_InputId.WVR_InputId_Alias1_Trigger.Num()].x = axisTrigger;
							s_TrackerButtonStates[trackerId].s_ButtonAxis[WVR_InputId.WVR_InputId_Alias1_Trigger.Num()].y = 0;
						}
						// ------------------------ Grip ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.grip, out float axisGrip))
						{
							s_TrackerButtonStates[trackerId].s_ButtonAxis[WVR_InputId.WVR_InputId_Alias1_Grip.Num()].x = axisGrip;
							s_TrackerButtonStates[trackerId].s_ButtonAxis[WVR_InputId.WVR_InputId_Alias1_Grip.Num()].y = 0;
						}
						// ------------------------ Touchpad ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axisTouchpad))
						{
							s_TrackerButtonStates[trackerId].s_ButtonAxis[WVR_InputId.WVR_InputId_Alias1_Touchpad.Num()] = axisTouchpad;
						}
						// ------------------------ Thumbstick ------------------------
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 axisThumbstick))
						{
							s_TrackerButtonStates[trackerId].s_ButtonAxis[WVR_InputId.WVR_InputId_Alias1_Thumbstick.Num()] = axisThumbstick;
						}
					}
				}
			}
		}

		private void OnTrackerButtonPressed(WVR_Event_t systemEvent)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			TrackerId trackerId = systemEvent.trackerInput.tracker.trackerId.Id();
			WVR_InputId id = systemEvent.trackerInput.inputId;
			DEBUG("OnTrackerButtonPressed() " + trackerId + ", " + id);

			s_TrackerButtonStates[trackerId].s_ButtonPress[id.Num()] = true;
		}
		private void OnTrackerButtonUnpressed(WVR_Event_t systemEvent)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			TrackerId trackerId = systemEvent.trackerInput.tracker.trackerId.Id();
			WVR_InputId id = systemEvent.trackerInput.inputId;
			DEBUG("OnTrackerButtonUnpressed() " + trackerId + ", " + id);

			s_TrackerButtonStates[trackerId].s_ButtonPress[id.Num()] = false;
		}
		private void OnTrackerTouchTapped(WVR_Event_t systemEvent)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			TrackerId trackerId = systemEvent.trackerInput.tracker.trackerId.Id();
			WVR_InputId id = systemEvent.trackerInput.inputId;
			DEBUG("OnTrackerTouchTapped() " + trackerId + ", " + id);

			s_TrackerButtonStates[trackerId].s_ButtonTouch[id.Num()] = true;
		}
		private void OnTrackerTouchUntapped(WVR_Event_t systemEvent)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			TrackerId trackerId = systemEvent.trackerInput.tracker.trackerId.Id();
			WVR_InputId id = systemEvent.trackerInput.inputId;
			DEBUG("OnTrackerTouchUntapped() " + trackerId + ", " + id);

			s_TrackerButtonStates[trackerId].s_ButtonTouch[id.Num()] = false;
		}

		bool AllowUpdateTrackerButton(TrackerId trackerId, WVR_InputId id)
		{
			if (s_TrackerButtonStates[trackerId].s_ButtonPressFrame[id.Num()] != Time.frameCount)
			{
				s_TrackerButtonStates[trackerId].s_ButtonPressFrame[id.Num()] = Time.frameCount;
				return true;
			}
			return false;
		}
		Dictionary<TrackerId, bool[]> ss_TrackerPress = new Dictionary<TrackerId, bool[]>();
		Dictionary<TrackerId, bool[]> ss_TrackerPressEx = new Dictionary<TrackerId, bool[]>();
		private void UpdateTrackerPress(TrackerId trackerId, WVR_InputId id)
		{
			if (AllowUpdateTrackerButton(trackerId, id))
			{
				ss_TrackerPressEx[trackerId][id.Num()] = ss_TrackerPress[trackerId][id.Num()];
				ss_TrackerPress[trackerId][id.Num()] = s_TrackerButtonStates[trackerId].s_ButtonPress[id.Num()];
			}
		}

		bool AllowUpdateTrackerTouch(TrackerId trackerid, WVR_InputId id)
		{
			if (s_TrackerButtonStates[trackerid].s_ButtonTouchFrame[id.Num()] != Time.frameCount)
			{
				s_TrackerButtonStates[trackerid].s_ButtonTouchFrame[id.Num()] = Time.frameCount;
				return true;
			}
			return false;
		}
		Dictionary<TrackerId, bool[]> ss_TrackerTouch = new Dictionary<TrackerId, bool[]>();
		Dictionary<TrackerId, bool[]> ss_TrackerTouchEx = new Dictionary<TrackerId, bool[]>();
		private void UpdateTrackerTouch(TrackerId trackerId, WVR_InputId id)
		{
			if (AllowUpdateTrackerTouch(trackerId, id))
			{
				ss_TrackerTouchEx[trackerId][id.Num()] = ss_TrackerTouch[trackerId][id.Num()];
				ss_TrackerTouch[trackerId][id.Num()] = s_TrackerButtonStates[trackerId].s_ButtonTouch[id.Num()];
			}
		}

		bool AllowUpdateTrackerAxis(TrackerId trackerId, WVR_InputId id)
		{
			if (s_TrackerButtonStates[trackerId].s_ButtonAxisFrame[id.Num()] != Time.frameCount)
			{
				s_TrackerButtonStates[trackerId].s_ButtonAxisFrame[id.Num()] = Time.frameCount;
				return true;
			}

			return false;
		}
		private void UpdateTrackerAxis(TrackerId trackerId, WVR_InputId id)
		{
			if (m_UseXRDevice && !Application.isEditor) { return; }

			if (IsTrackerInputAvailable(trackerId, WVR_InputType.WVR_InputType_Analog, (uint)id))
			{
				if (AllowUpdateTrackerAxis(trackerId, id))
				{
					WVR_Axis_t axis = Interop.WVR_GetTrackerInputAnalogAxis(trackerId.Id(), id);
					s_TrackerButtonStates[trackerId].s_ButtonAxis[id.Num()].x = axis.x;
					s_TrackerButtonStates[trackerId].s_ButtonAxis[id.Num()].y = axis.y;
				}
			}
			else
			{
				s_TrackerButtonStates[trackerId].s_ButtonAxis[id.Num()] = Vector2.zero;
			}
		}
		#endregion

		#region Battery Life
		Dictionary<TrackerId, float> s_TrackerBattery = new Dictionary<TrackerId, float>();
		private void CheckTrackerBattery(TrackerId trackerId)
		{
			if (m_UseXRDevice && !Application.isEditor)
			{
				// Default value.
				s_TrackerBattery[trackerId] = 0;

				for (int i = 0; i < s_InputDevices.Count; i++)
				{
					if (!s_InputDevices[i].isValid) { continue; }

					// Found the tracker.
					if (IsTrackerDevice(s_InputDevices[i], trackerId))
					{
						if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.batteryLevel, out float batteryLife))
						{
							s_TrackerBattery[trackerId] = batteryLife;
						}
					}
				}

				DEBUG("CheckTrackerBattery() " + trackerId
					+ "[" + trackerId.Name() + "]"
					+ "[" + trackerId.SerialNumber() + "]"
					+ ": " + s_TrackerBattery[trackerId]);
				return;
			}

			s_TrackerBattery[trackerId] = s_TrackerConnection[trackerId] ?
				(s_TrackerCaps[trackerId.Num()].supportsBatteryLevel
					? Interop.WVR_GetTrackerBatteryLevel(trackerId.Id()) : 0
				) : 0;
			DEBUG("CheckTrackerBattery() " + trackerId + ": " + s_TrackerBattery[trackerId]);
		}
		private void OnTrackerBatteryLevelUpdate(WVR_Event_t systemEvent)
		{
			TrackerId trackerId = systemEvent.tracker.trackerId.Id();
			DEBUG("OnTrackerBatteryLevelUpdate() " + trackerId);
			CheckTrackerBattery(trackerId);
		}
		#endregion

		#region Vibration
		public bool TriggerTrackerVibration(TrackerId trackerId, UInt32 durationMicroSec = 500000, UInt32 frequency = 0, float amplitude = 0.5f)
		{
			amplitude = Mathf.Clamp(amplitude, 0, 1);
			float durationSec = durationMicroSec / 1000000;

			if (m_UseXRDevice && !Application.isEditor)
			{
				// Default value.
				s_TrackerBattery[trackerId] = 0;

				for (int i = 0; i < s_InputDevices.Count; i++)
				{
					if (!s_InputDevices[i].isValid) { continue; }

					// Found the tracker.
					if (IsTrackerDevice(s_InputDevices[i], trackerId))
					{
						DEBUG("CheckTrackerRole() " + trackerId
							+ "[" + trackerId.Name() + "]"
							+ "[" + trackerId.SerialNumber() + "]"
							+ ": " + durationSec.ToString() + ", " + amplitude);
						return s_InputDevices[i].SendHapticImpulse(0, amplitude, durationSec);
					}
				}

				return false;
			}

			if (s_TrackerConnection[trackerId] && s_TrackerCaps[trackerId.Num()].supportsHapticVibration)
			{
				WVR_Result result = Interop.WVR_TriggerTrackerVibration(trackerId.Id(), durationMicroSec, frequency, amplitude);
				DEBUG("TriggerTrackerVibration() " + trackerId);

				return (result == WVR_Result.WVR_Success);
			}

			return false;
		}
		#endregion

		#region Public Interface
		public TrackerStatus GetTrackerStatus()
		{
			try
			{
				m_TrackerStatusRWLock.TryEnterReadLock(2000);
				return m_TrackerStatus;
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "GetTrackerStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				m_TrackerStatusRWLock.ExitReadLock();
			}
		}

		public bool IsTrackerConnected(TrackerId trackerId)
		{
			return s_TrackerConnection[trackerId];
		}

		public TrackerRole GetTrackerRole(TrackerId trackerId)
		{
			return s_TrackerRole[trackerId];
		}

		public bool GetTrackerPosition(TrackerId trackerId, out Vector3 position)
		{
			position = s_TrackerPoses[trackerId].rigid.pos;
			return s_TrackerPoses[trackerId].valid;
		}
		public Vector3 GetTrackerPosition(TrackerId trackerId)
		{
			return s_TrackerPoses[trackerId].rigid.pos;
		}
		public bool GetTrackerRotation(TrackerId trackerId, out Quaternion rotation)
		{
			rotation = s_TrackerPoses[trackerId].rigid.rot;
			return s_TrackerPoses[trackerId].valid;
		}
		public Quaternion GetTrackerRotation(TrackerId trackerId)
		{
			return s_TrackerPoses[trackerId].rigid.rot;
		}

		public AxisType GetTrackerButtonAxisType(TrackerId trackerId, TrackerButton id)
		{
			return s_ButtonAxisType[trackerId][id.Num()];
		}

		public bool TrackerButtonPress(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerPress(trackerId, id.Id());
			return (!ss_TrackerPressEx[trackerId][id.Num()] && ss_TrackerPress[trackerId][id.Num()]);
		}
		public bool TrackerButtonHold(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerPress(trackerId, id.Id());
			return (ss_TrackerPressEx[trackerId][id.Num()] && ss_TrackerPress[trackerId][id.Num()]);
		}
		public bool TrackerButtonRelease(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerPress(trackerId, id.Id());
			return (ss_TrackerPressEx[trackerId][id.Num()] && !ss_TrackerPress[trackerId][id.Num()]);
		}
		public bool TrackerButtonTouch(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerTouch(trackerId, id.Id());
			return (!ss_TrackerTouchEx[trackerId][id.Num()] && ss_TrackerTouch[trackerId][id.Num()]);
		}
		public bool TrackerButtonTouching(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerTouch(trackerId, id.Id());
			return (ss_TrackerTouchEx[trackerId][id.Num()] && ss_TrackerTouch[trackerId][id.Num()]);
		}
		public bool TrackerButtonUntouch(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerTouch(trackerId, id.Id());
			return (ss_TrackerTouchEx[trackerId][id.Num()] && !ss_TrackerTouch[trackerId][id.Num()]);
		}
		public Vector2 TrackerButtonAxis(TrackerId trackerId, TrackerButton id)
		{
			UpdateTrackerAxis(trackerId, id.Id());
			return s_TrackerButtonStates[trackerId].s_ButtonAxis[id.Num()];
		}

		public float GetTrackerBatteryLife(TrackerId trackerId)
		{
			return s_TrackerBattery[trackerId];
		}
		#endregion
	}
}