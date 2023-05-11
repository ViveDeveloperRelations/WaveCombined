// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using System.Threading;
using System;
using System.Runtime.InteropServices;
using Wave.Native;
using Wave.XR.Function;
using Wave.Essence.Events;
using System.Diagnostics;
using UnityEngine.Profiling;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Wave.Essence.Hand
{
	[DisallowMultipleComponent]
	public sealed class HandManager : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Hand.HandManager";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}
		private void INFO(string msg) { Log.i(LOG_TAG, msg, true); }
		private void WARNING(string msg) { Log.w(LOG_TAG, msg, true); }

		#region Global Declaration
		public static readonly string HAND_STATIC_GESTURE = "HAND_STATIC_GESTURE";
		public static readonly string HAND_DYNAMIC_GESTURE_LEFT = "HAND_DYNAMIC_GESTURE_LEFT";
		public static readonly string HAND_DYNAMIC_GESTURE_RIGHT = "HAND_DYNAMIC_GESTURE_RIGHT";
		public static readonly string HAND_GESTURE_STATUS = "HAND_GESTURE_STATUS";
		public static readonly string HAND_TRACKER_STATUS = "HAND_TRACKER_STATUS";

		public enum HandType
		{
			Right = 0,
			Left = 1
		};
		public enum HandJoint
		{
			Palm = WVR_HandJoint.WVR_HandJoint_Palm,
			Wrist = WVR_HandJoint.WVR_HandJoint_Wrist,
			Thumb_Joint0 = WVR_HandJoint.WVR_HandJoint_Thumb_Joint0,
			Thumb_Joint1 = WVR_HandJoint.WVR_HandJoint_Thumb_Joint1,
			Thumb_Joint2 = WVR_HandJoint.WVR_HandJoint_Thumb_Joint2,
			Thumb_Tip = WVR_HandJoint.WVR_HandJoint_Thumb_Tip,
			Index_Joint0 = WVR_HandJoint.WVR_HandJoint_Index_Joint0,
			Index_Joint1 = WVR_HandJoint.WVR_HandJoint_Index_Joint1,
			Index_Joint2 = WVR_HandJoint.WVR_HandJoint_Index_Joint2,
			Index_Joint3 = WVR_HandJoint.WVR_HandJoint_Index_Joint3,
			Index_Tip = WVR_HandJoint.WVR_HandJoint_Index_Tip,
			Middle_Joint0 = WVR_HandJoint.WVR_HandJoint_Middle_Joint0,
			Middle_Joint1 = WVR_HandJoint.WVR_HandJoint_Middle_Joint1,
			Middle_Joint2 = WVR_HandJoint.WVR_HandJoint_Middle_Joint2,
			Middle_Joint3 = WVR_HandJoint.WVR_HandJoint_Middle_Joint3,
			Middle_Tip = WVR_HandJoint.WVR_HandJoint_Middle_Tip,
			Ring_Joint0 = WVR_HandJoint.WVR_HandJoint_Ring_Joint0,
			Ring_Joint1 = WVR_HandJoint.WVR_HandJoint_Ring_Joint1,
			Ring_Joint2 = WVR_HandJoint.WVR_HandJoint_Ring_Joint2,
			Ring_Joint3 = WVR_HandJoint.WVR_HandJoint_Ring_Joint3,
			Ring_Tip = WVR_HandJoint.WVR_HandJoint_Ring_Tip,
			Pinky_Joint0 = WVR_HandJoint.WVR_HandJoint_Pinky_Joint0,
			Pinky_Joint1 = WVR_HandJoint.WVR_HandJoint_Pinky_Joint1,
			Pinky_Joint2 = WVR_HandJoint.WVR_HandJoint_Pinky_Joint2,
			Pinky_Joint3 = WVR_HandJoint.WVR_HandJoint_Pinky_Joint3,
			Pinky_Tip = WVR_HandJoint.WVR_HandJoint_Pinky_Tip,
		};
		public enum HandModel
		{
			WithoutController = WVR_HandModelType.WVR_HandModelType_WithoutController,
			WithController = WVR_HandModelType.WVR_HandModelType_WithController,
		}
		public enum HandMotion
		{
			None = WVR_HandPoseType.WVR_HandPoseType_Invalid,
			Pinch = WVR_HandPoseType.WVR_HandPoseType_Pinch,
			Hold = WVR_HandPoseType.WVR_HandPoseType_Hold,
		}
		public enum HandHoldRole
		{
			None = WVR_HandHoldRoleType.WVR_HandHoldRoleType_None,
			Main = WVR_HandHoldRoleType.WVR_HandHoldRoleType_MainHold,
			Side = WVR_HandHoldRoleType.WVR_HandHoldRoleType_SideHold,
		}
		public enum HandHoldType
		{
			None = WVR_HandHoldObjectType.WVR_HandHoldObjectType_None,
			Gun = WVR_HandHoldObjectType.WVR_HandHoldObjectType_Gun,
			OCSpray = WVR_HandHoldObjectType.WVR_HandHoldObjectType_OCSpray,
			LongGun = WVR_HandHoldObjectType.WVR_HandHoldObjectType_LongGun,
			Baton = WVR_HandHoldObjectType.WVR_HandHoldObjectType_Baton,
			FlashLight = WVR_HandHoldObjectType.WVR_HandHoldObjectType_FlashLight,
		}

		public delegate void HandGestureResultDelegate(object sender, bool result);
		public enum GestureStatus
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
		public enum GestureType
		{
			Invalid = WVR_HandGestureType.WVR_HandGestureType_Invalid,
			Unknown = WVR_HandGestureType.WVR_HandGestureType_Unknown,
			Fist = WVR_HandGestureType.WVR_HandGestureType_Fist,
			Five = WVR_HandGestureType.WVR_HandGestureType_Five,
			OK = WVR_HandGestureType.WVR_HandGestureType_OK,
			ThumbUp = WVR_HandGestureType.WVR_HandGestureType_ThumbUp,
			IndexUp = WVR_HandGestureType.WVR_HandGestureType_IndexUp,
			Inverse = WVR_HandGestureType.WVR_HandGestureType_Inverse,
		}
		[Serializable]
		public class GestureSetter
		{
			private bool Unknown = true;
			public bool Fist = false;
			public bool Five = false;
			public bool OK = false;
			public bool ThumbUp = false;
			public bool IndexUp = false;
			public bool Inverse = false;

			public ulong optionValue { get; private set; }
			public void UpdateOptionValue()
			{
				optionValue = 0;
				if (Unknown)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Unknown;
				if (Fist)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Fist;
				if (Five)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Five;
				if (OK)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_OK;
				if (ThumbUp)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_ThumbUp;
				if (IndexUp)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_IndexUp;
				if (Inverse)
					optionValue |= 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Inverse;
			}
		}
		[Serializable]
		public class GestureOption
		{
			public bool Enable = false;
			public GestureSetter Gesture = new GestureSetter();
		}

		public delegate void HandTrackerResultDelegate(object sender, bool result);
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
		public enum TrackerType
		{
			Natural = WVR_HandTrackerType.WVR_HandTrackerType_Natural,
			Electronic = WVR_HandTrackerType.WVR_HandTrackerType_Electronic,
		}
		public enum TrackerSelector
		{
			[Tooltip("Uses electronic hand first.")]
			ElectronicPrior,
			[Tooltip("Uses natural hand first.")]
			NaturalPrior,
		}
		[Serializable]
		public class NaturalTrackerOption
		{
			public bool InitialStart = false;
		}
		[Serializable]
		public class ElectronicTrackerOption
		{
			public bool InitialStart = false;
			[Tooltip("Selects the hand model: with or without a controller stick.")]
			public HandModel Model = HandModel.WithoutController;
		}
		[Serializable]
		public class TrackerOption
		{
			[Tooltip("Selects the prefer tracker.")]
			public TrackerSelector Tracker = TrackerSelector.NaturalPrior;
			public NaturalTrackerOption Natural = new NaturalTrackerOption();
			public ElectronicTrackerOption Electronic = new ElectronicTrackerOption();
		}
		#endregion

		#region Customized Settings
		[Tooltip("Selects supported gesture types.")]
		[SerializeField]
		private GestureOption m_GestureOptions = new GestureOption();
		public GestureOption GestureOptions { get { return m_GestureOptions; } set { m_GestureOptions = value; } }

		[SerializeField]
		private TrackerOption m_TrackerOptions = new TrackerOption();
		public TrackerOption TrackerOptions { get { return m_TrackerOptions; } set { m_TrackerOptions = value; } }
		#endregion

		private static HandManager m_Instance = null;
		public static HandManager Instance { get { return m_Instance; } }
		[Obsolete("Do NOT use! Use HandInputSwitch.Instance.PrimaryInput instead.")]
		public static HandType PrimaryInput = HandType.Right;

		private XR_InteractionMode interactionMode = XR_InteractionMode.Default;

		#region MonoBehaviour Overrides
		private ulong supportedFeature = 0;
		private ulong m_GestureOptionValue = 0;

		private delegate void ConvertHandTrackingDataToUnityDelegate(ref WVR_HandJointData_t src, ref HandJointData26 dest);

		private ConvertHandTrackingDataToUnityDelegate ConvertHandTrackingDataToUnity = null;

		void Awake()
		{
			DEBUG("Awake()");
			if (m_Instance == null)
			{
				m_Instance = this;
				// TODO Cant use DontDestroyOnLoad to an object which is not put in transform root.
				// TODO how to kill self?
				// TODO how to spawn when access?
				DontDestroyOnLoad(m_Instance);
			}

			supportedFeature = Interop.WVR_GetSupportedFeatures();
			// 1. Checks if Hand Gesture is supported.
			if ((supportedFeature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_HandGesture) == 0)
			{
				Log.w(LOG_TAG, "WVR_SupportedFeature_HandGesture is not enabled.", true);
				SetHandGestureStatus(GestureStatus.NoSupport);
			}
			// 2. Checks if Natural Hand is supported.
			if ((supportedFeature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_HandTracking) == 0)
			{
				Log.w(LOG_TAG, "Awake() WVR_SupportedFeature_HandTracking is not enabled.", true);
				SetHandTrackerStatus(TrackerType.Natural, TrackerStatus.NoSupport);
			}
			// 3. Checks if WVR_SupportedFeature electronic hand is supported.
			if ((supportedFeature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_ElectronicHand) == 0)
			{
				Log.w(LOG_TAG, "Awake() WVR_SupportedFeature_ElectronicHand is not enabled.", true);
				SetHandTrackerStatus(TrackerType.Electronic, TrackerStatus.NoSupport);
			}

			interactionMode = ClientInterface.InteractionMode;

			m_GestureOptions.Gesture.UpdateOptionValue();
			m_GestureOptionValue = m_GestureOptions.Gesture.optionValue;

			if (HandInputSwitch.Instance != null)
				Log.i(LOG_TAG, "Awake() Loaded HandInputSwitch.", true);
		}
		void Start()
		{
			if (m_GestureOptions.Enable)
			{
				DEBUG("Start() Starts hand gesture.");
				StartHandGesture();
			}

			if (m_TrackerOptions.Natural.InitialStart)
			{
				DEBUG("Start() Starts the natural hand tracker.");
				StartHandTracker(TrackerType.Natural);
			}
			if (m_TrackerOptions.Electronic.InitialStart)
			{
				DEBUG("Start() Starts the electronic hand tracker.");
				StartHandTracker(TrackerType.Electronic);
			}

			var ptr = FunctionsHelper.GetFuncPtr("ConvertHandTrackingDataToUnity");
			if (ptr != IntPtr.Zero)
				ConvertHandTrackingDataToUnity = Marshal.GetDelegateForFunctionPointer<ConvertHandTrackingDataToUnityDelegate>(ptr);
			else
				ConvertHandTrackingDataToUnity = null;
		}
		private bool toRestartGesture = false;
		void Update()
		{
			interactionMode = ClientInterface.InteractionMode;

			/// ------------- Gesture -------------
			Profiler.BeginSample("Gesture");
			m_GestureOptions.Gesture.UpdateOptionValue();
			if (m_GestureOptionValue != m_GestureOptions.Gesture.optionValue)
			{
				m_GestureOptionValue = m_GestureOptions.Gesture.optionValue;
				toRestartGesture = true;
			}
			if (m_GestureOptions.Enable)
			{
				if (GetHandGestureStatus() == GestureStatus.Available)
				{
					// Hand Gesture is already started.
					if (toRestartGesture)
					{
						toRestartGesture = false;
						RestartHandGesture();
					}
					else
					{
						GetHandGestureData();
						if (hasHandGestureData)
						{
							UpdateHandGestureDataLeft();
							UpdateHandGestureDataRight();
						}
					}
				}
				else
				{
					// Even calls to the function, StartHandGesture still confirms the start conditions.
					StartHandGesture();
				}
			}
			else
			{
				if (GetHandGestureStatus() == GestureStatus.Available)
					StopHandGesture();
			}
			Profiler.EndSample();

			GetHandTrackingData(TrackerType.Natural);
			GetHandTrackingData(TrackerType.Electronic);

			if (Log.gpl.Print)
			{
				DEBUG("Update() Interaction Mode: " + interactionMode
					+ ", gesture: " + m_GestureOptions.Enable
					+ ", gesture value: " + m_GestureOptions.Gesture.optionValue
					+ ", tracker: " + m_TrackerOptions.Tracker
					+ ", natural count: " + refCountNatural
					+ ", natural joint: " + m_NaturalHandJointCount
					+ ", electronic count: " + refCountElectronic
					+ ", electronic model: " + m_TrackerOptions.Electronic.Model
					+ ", electronic joint: " + m_ElectronicHandJointCount);
			}
		}
		#endregion

		public bool GetPreferTracker(ref TrackerType tracker)
		{
			TrackerStatus natural_status = GetHandTrackerStatus(TrackerType.Natural);
			TrackerStatus electronic_status = GetHandTrackerStatus(TrackerType.Electronic);
			if (TrackerOptions.Tracker == TrackerSelector.NaturalPrior)
			{
				if (natural_status == TrackerStatus.Available)
				{
					tracker = TrackerType.Natural;
					return true;
				}
				else if (electronic_status == TrackerStatus.Available)
				{
					tracker = TrackerType.Electronic;
					return true;
				}
			}
			else // (m_TrackerOptions.Tracker == TrackerSelector.ElectronicPrior)
			{
				if (electronic_status == TrackerStatus.Available)
				{
					tracker = TrackerType.Electronic;
					return true;
				}
				else if (natural_status == TrackerStatus.Available)
				{
					tracker = TrackerType.Natural;
					return true;
				}
			}
			return false;
		}

		#region Hand Gesture Lifecycle
		private GestureStatus m_HandGestureStatus = GestureStatus.NotStart;
		private static ReaderWriterLockSlim m_HandGestureStatusRWLock = new ReaderWriterLockSlim();
		private void SetHandGestureStatus(GestureStatus status)
		{
			try
			{
				m_HandGestureStatusRWLock.TryEnterWriteLock(2000);
				m_HandGestureStatus = status;
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "SetHandGestureStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				m_HandGestureStatusRWLock.ExitWriteLock();
			}
		}

		private bool CanStartHandGesture()
		{
			GestureStatus status = GetHandGestureStatus();
			if (!m_GestureOptions.Enable ||
				status == GestureStatus.Available ||
				status == GestureStatus.Starting ||
				status == GestureStatus.Stopping ||
				status == GestureStatus.NoSupport)
			{
				return false;
			}

			/*if (!WaveEssence.Instance.IsHandDeviceConnected(XR_HandDevice.GestureLeft) &&
				!WaveEssence.Instance.IsHandDeviceConnected(XR_HandDevice.GestureRight))
			{
				return false;
			}*/

			return true;
		}
		private bool CanStopHandGesture()
		{
			GestureStatus status = GetHandGestureStatus();
			if (m_GestureOptions.Enable ||
				status == GestureStatus.NoSupport ||
				status == GestureStatus.NotStart ||
				status == GestureStatus.StartFailure ||
				status == GestureStatus.Starting ||
				status == GestureStatus.Stopping)
			{
				return false;
			}

			return true;
		}

		private object handGestureThreadLock = new object();
		private event HandGestureResultDelegate handGestureResultCB = null;
		private void StartHandGestureLock()
		{
			if (!CanStartHandGesture())
				return;

			bool result = false;

			SetHandGestureStatus(GestureStatus.Starting);
			result = Interop.WVR_StartHandGesture(m_GestureOptionValue) == WVR_Result.WVR_Success ? true : false;
			if (!result)
			{
				m_GestureOptions.Enable = false;
			}
			SetHandGestureStatus(result ? GestureStatus.Available : GestureStatus.StartFailure);

			GestureStatus status = GetHandGestureStatus();
			DEBUG("StartHandGestureLock() " + result + ", status: " + status);
			GeneralEvent.Send(HAND_GESTURE_STATUS, status);

			if (handGestureResultCB != null)
			{
				handGestureResultCB(this, result);
				handGestureResultCB = null;
			}
		}
		private void StartHandGestureThread()
		{
			lock (handGestureThreadLock)
			{
				DEBUG("StartHandGestureThread()");
				StartHandGestureLock();
			}
		}
		private void StartHandGesture()
		{
			if (!CanStartHandGesture())
				return;

			Thread hand_gesture_t = new Thread(StartHandGestureThread);
			hand_gesture_t.Name = "StartHandGestureThread";
			hand_gesture_t.Start();
		}

		private void StopHandGestureLock()
		{
			GestureStatus status = GetHandGestureStatus();
			if (status == GestureStatus.Available)
			{
				DEBUG("StopHandGestureLock()");
				SetHandGestureStatus(GestureStatus.Stopping);
				Interop.WVR_StopHandGesture();
				SetHandGestureStatus(GestureStatus.NotStart);
				hasHandGestureData = false;
			}

			status = GetHandGestureStatus();
			GeneralEvent.Send(HAND_GESTURE_STATUS, status);
		}
		private void StopHandGestureThread()
		{
			lock (handGestureThreadLock)
			{
				DEBUG("StopHandGestureThread()");
				StopHandGestureLock();
			}
		}
		private void StopHandGesture()
		{
			Thread hand_gesture_t = new Thread(StopHandGestureThread);
			hand_gesture_t.Name = "StopHandGestureThread";
			hand_gesture_t.Start();
		}

		private void RestartHandGestureThread()
		{
			lock (handGestureThreadLock)
			{
				DEBUG("RestartHandGestureThread()");
				StopHandGestureLock();
				StartHandGestureLock();
			}
		}
		#endregion

		#region Hand Gesture Interface
		public GestureStatus GetHandGestureStatus()
		{
			try
			{
				m_HandGestureStatusRWLock.TryEnterReadLock(2000);
				return m_HandGestureStatus;
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "GetHandGestureStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				m_HandGestureStatusRWLock.ExitReadLock();
			}
		}
		public void RestartHandGesture()
		{
			GestureStatus status = GetHandGestureStatus();
			if (status == GestureStatus.Starting || status == GestureStatus.Stopping)
				return;
			Thread hand_gesture_t = new Thread(RestartHandGestureThread);
			hand_gesture_t.Name = "RestartHandGestureThread";
			hand_gesture_t.Start();
		}
		public void RestartHandGesture(HandGestureResultDelegate callback)
		{
			if (handGestureResultCB == null)
				handGestureResultCB = callback;
			else
				handGestureResultCB += callback;

			RestartHandGesture();
		}

		private bool hasHandGestureData = false;
		private WVR_HandGestureData_t handGestureData = new WVR_HandGestureData_t();
		private void GetHandGestureData()
		{
			GestureStatus status = GetHandGestureStatus();
			if (status == GestureStatus.Available)
				hasHandGestureData = Interop.WVR_GetHandGestureData(ref handGestureData) == WVR_Result.WVR_Success ? true : false;
		}

		private GestureType m_HandGestureLeftEx = GestureType.Invalid;
		private GestureType m_HandGestureLeft = GestureType.Invalid;
		private void UpdateHandGestureDataLeft()
		{
			m_HandGestureLeftEx = m_HandGestureLeft;
			switch (handGestureData.left)
			{
				case WVR_HandGestureType.WVR_HandGestureType_Unknown:
					m_HandGestureLeft = GestureType.Unknown;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_Fist:
					m_HandGestureLeft = GestureType.Fist;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_Five:
					m_HandGestureLeft = GestureType.Five;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_OK:
					m_HandGestureLeft = GestureType.OK;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_ThumbUp:
					m_HandGestureLeft = GestureType.ThumbUp;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_IndexUp:
					m_HandGestureLeft = GestureType.IndexUp;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_Inverse:
					m_HandGestureLeft = GestureType.Inverse;
					break;
				default:
					m_HandGestureLeft = GestureType.Invalid;
					break;
			}

			if (m_HandGestureLeft != m_HandGestureLeftEx)
			{
				DEBUG("UpdateHandGestureDataLeft() Receives " + m_HandGestureLeft);
				GeneralEvent.Send(HAND_STATIC_GESTURE, HandType.Left, m_HandGestureLeft);
#pragma warning disable 0618
				GeneralEvent.Send(HAND_STATIC_GESTURE_LEFT, (WVR_HandGestureType)m_HandGestureLeft);
#pragma warning restore 0618
			}
		}

		private GestureType m_HandGestureRightEx = GestureType.Invalid;
		private GestureType m_HandGestureRight = GestureType.Invalid;
		private void UpdateHandGestureDataRight()
		{
			m_HandGestureRightEx = m_HandGestureRight;
			switch (handGestureData.right)
			{
				case WVR_HandGestureType.WVR_HandGestureType_Unknown:
					m_HandGestureRight = GestureType.Unknown;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_Fist:
					m_HandGestureRight = GestureType.Fist;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_Five:
					m_HandGestureRight = GestureType.Five;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_OK:
					m_HandGestureRight = GestureType.OK;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_ThumbUp:
					m_HandGestureRight = GestureType.ThumbUp;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_IndexUp:
					m_HandGestureRight = GestureType.IndexUp;
					break;
				case WVR_HandGestureType.WVR_HandGestureType_Inverse:
					m_HandGestureRight = GestureType.Inverse;
					break;
				default:
					m_HandGestureRight = GestureType.Invalid;
					break;
			}

			if (m_HandGestureRight != m_HandGestureRightEx)
			{
				DEBUG("UpdateHandGestureDataLeft() Receives " + m_HandGestureRight);
				GeneralEvent.Send(HAND_STATIC_GESTURE, HandType.Right, m_HandGestureRight);
#pragma warning disable 0618
				GeneralEvent.Send(HAND_STATIC_GESTURE_RIGHT, (WVR_HandGestureType)m_HandGestureRight);
#pragma warning restore 0618
			}
		}

		public GestureType GetHandGesture(bool isLeft)
		{
			return isLeft ? m_HandGestureLeft : m_HandGestureRight;
		}
		public GestureType GetHandGesture(HandType hand)
		{
			return GetHandGesture(hand == HandType.Left ? true : false);
		}
		#endregion

		#region Hand Tracking Lifecycle
		private TrackerStatus m_NaturalTrackerStatus = TrackerStatus.NotStart, m_ElectronicTrackerStatus = TrackerStatus.NotStart;
		private static ReaderWriterLockSlim m_NaturalTrackerStatusRWLock = new ReaderWriterLockSlim(), m_ElectronicTrackerStatusRWLock = new ReaderWriterLockSlim();
		private void SetHandTrackerStatus(TrackerType tracker, TrackerStatus status)
		{
			try
			{
				if (tracker == TrackerType.Electronic)
				{
					m_ElectronicTrackerStatusRWLock.TryEnterWriteLock(2000);
					m_ElectronicTrackerStatus = status;
				}
				if (tracker == TrackerType.Natural)
				{
					m_NaturalTrackerStatusRWLock.TryEnterWriteLock(2000);
					m_NaturalTrackerStatus = status;
				}
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "SetHandTrackerStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				if (tracker == TrackerType.Natural)
					m_NaturalTrackerStatusRWLock.ExitWriteLock();
				if (tracker == TrackerType.Electronic)
					m_ElectronicTrackerStatusRWLock.ExitWriteLock();
			}
		}

		private bool CanStartHandTracker(TrackerSelector selector)
		{
			if (selector == TrackerSelector.ElectronicPrior)
			{
				if (!CanStartHandTracker(TrackerType.Electronic))
				{
					TrackerStatus electronic_status = GetHandTrackerStatus(TrackerType.Electronic);
					switch (electronic_status)
					{
						case TrackerStatus.NoSupport:		// Electronic tracker is not supported.
						case TrackerStatus.NotStart:		// Electronic tracker is supported but no electronic hand connected.
						case TrackerStatus.StartFailure:	// Electronic tracker is supported but has been started failed.
							if (!CanStartHandTracker(TrackerType.Natural))
								return false;
							// else return true; // The natural tracker is able to start.
							break;
						default:
							break;
					}
				}
				// else return true; // The electronic tracker is able to start.
			}
			if (selector == TrackerSelector.NaturalPrior)
			{
				if (!CanStartHandTracker(TrackerType.Natural))
				{
					TrackerStatus natural_status = GetHandTrackerStatus(TrackerType.Natural);
					switch (natural_status)
					{
						case TrackerStatus.NoSupport:	   // Natural tracker is not supported.
						case TrackerStatus.NotStart:		// Natural tracker is supported but no electronic hand connected.
						case TrackerStatus.StartFailure:	// Natural tracker is supported but has been started failed.
							if (!CanStartHandTracker(TrackerType.Electronic))
								return false;
							// else return true; // The natural tracker is able to start.
							break;
						default:
							break;
					}
				}
				// else return true; // The natural tracker is able to start.
			}

			return true;
		}
		private bool CanStartHandTracker(TrackerType tracker)
		{
			if (tracker == TrackerType.Natural)
			{
				TrackerStatus status = GetHandTrackerStatus(TrackerType.Natural);
				if (status == TrackerStatus.Available ||
					status == TrackerStatus.Starting ||
					status == TrackerStatus.Stopping ||
					status == TrackerStatus.NoSupport)
				{
					return false;
				}

				/*if (!WaveEssence.Instance.IsHandDeviceConnected(XR_HandDevice.NaturalLeft) &&
					!WaveEssence.Instance.IsHandDeviceConnected(XR_HandDevice.NaturalRight))
				{
					return false;
				}*/
			}
			if (tracker == TrackerType.Electronic)
			{
				TrackerStatus status = GetHandTrackerStatus(TrackerType.Electronic);
				if (status == TrackerStatus.Available ||
					status == TrackerStatus.Starting ||
					status == TrackerStatus.Stopping ||
					status == TrackerStatus.NoSupport)
				{
					return false;
				}

				/*if (!WaveEssence.Instance.IsHandDeviceConnected(XR_HandDevice.ElectronicLeft) &&
					!WaveEssence.Instance.IsHandDeviceConnected(XR_HandDevice.ElectronicRight))
				{
					return false;
				}*/
			}

			return true;
		}
		private bool CanStopHandTracker(TrackerType tracker)
		{
			if (tracker == TrackerType.Natural)
			{
				if (GetHandTrackerStatus(TrackerType.Natural) == TrackerStatus.Available)
					return true;
			}
			if (tracker == TrackerType.Electronic)
			{
				if (GetHandTrackerStatus(TrackerType.Electronic) == TrackerStatus.Available)
					return true;
			}

			return false;
		}

		private uint refCountNatural = 0, refCountElectronic = 0;
		private object handTrackerThreadLocker = new object();
		private event HandTrackerResultDelegate handTrackerResultCB = null;
		private void StartHandTrackerLock(TrackerType tracker)
		{
			if (!CanStartHandTracker(tracker))
				return;

			SetHandTrackerStatus(tracker, TrackerStatus.Starting);
			WVR_Result result = Interop.WVR_StartHandTracking((WVR_HandTrackerType)tracker);
			switch (result)
			{
				case WVR_Result.WVR_Success:
					SetHandTrackerStatus(tracker, TrackerStatus.Available);
					GetHandJointCount(tracker);
					GetHandTrackerInfo(tracker);
					break;
				case WVR_Result.WVR_Error_FeatureNotSupport:
					SetHandTrackerStatus(tracker, TrackerStatus.NoSupport);
					break;
				default:
					SetHandTrackerStatus(tracker, TrackerStatus.StartFailure);
					break;
			}

			TrackerStatus status = GetHandTrackerStatus(tracker);

			if (status != TrackerStatus.Available)
			{
				if (tracker == TrackerType.Natural)
				{
					refCountNatural = refCountNatural > 0 ? refCountNatural - 1 : 0;
					INFO("StartHandTrackerLock() " + tracker + " failed. (" + refCountNatural + ")");
				}
				if (tracker == TrackerType.Electronic)
				{
					refCountElectronic = refCountElectronic > 0 ? refCountElectronic - 1 : 0;
					INFO("StartHandTrackerLock() " + tracker + " failed. (" + refCountElectronic + ")");
				}
			}

			DEBUG("StartHandTrackerLock() " + tracker + ", " + result + ", status: " + status);
			GeneralEvent.Send(HAND_TRACKER_STATUS, tracker, status);

			if (handTrackerResultCB != null)
			{
				handTrackerResultCB(this, result == WVR_Result.WVR_Success ? true : false);
				handTrackerResultCB = null;
			}
		}
		private void StartHandTrackerThread(object tracker)
		{
			lock (handTrackerThreadLocker)
			{
				DEBUG("StartHandTrackerThread() " + (TrackerType)tracker);
				StartHandTrackerLock((TrackerType)tracker);
			}
		}
		public void StartHandTracker(TrackerType tracker)
		{
			string caller = new StackFrame(1, true).GetMethod().Name;
			if (tracker == TrackerType.Natural)
			{
				refCountNatural++;
				INFO("StartHandTracker() " + tracker + "(" + refCountNatural + ") from " + caller);
			}
			if (tracker == TrackerType.Electronic)
			{
				refCountElectronic++;
				INFO("StartHandTracker() " + tracker + "(" + refCountElectronic + ") from " + caller);
			}

			if (!CanStartHandTracker(tracker))
				return;

			INFO("StartHandTracker() " + tracker);
			Thread hand_tracker_t = new Thread(StartHandTrackerThread);
			hand_tracker_t.Name = "StartHandTrackerThread";
			hand_tracker_t.Start(tracker);
		}

		private void StopHandTrackerLock(TrackerType tracker)
		{
			if (!CanStopHandTracker(tracker))
				return;

			DEBUG("StopHandTrackerLock() " + tracker);
			SetHandTrackerStatus(tracker, TrackerStatus.Stopping);
			Interop.WVR_StopHandTracking((WVR_HandTrackerType)tracker);
			SetHandTrackerStatus(tracker, TrackerStatus.NotStart);
			if (tracker == TrackerType.Natural)
				hasNaturalHandTrackerData = false;
			if (tracker == TrackerType.Electronic)
				hasElectronicHandTrackerData = false;

			TrackerStatus status = GetHandTrackerStatus(tracker);
			GeneralEvent.Send(HAND_TRACKER_STATUS, tracker, status);
		}
		private void StopHandTrackerThread(object tracker)
		{
			lock (handTrackerThreadLocker)
			{
				DEBUG("StopHandTrackerThread() " + (TrackerType)tracker);
				StopHandTrackerLock((TrackerType)tracker);
			}
		}
		public void StopHandTracker(TrackerType tracker)
		{
			string caller = new StackFrame(1, true).GetMethod().Name;
			if (tracker == TrackerType.Natural)
			{
				refCountNatural = refCountNatural > 0 ? refCountNatural - 1 : 0;
				INFO("StopHandTracker() " + tracker + "(" + refCountNatural + ") from " + caller);
				if (refCountNatural > 0) return;
			}
			if (tracker == TrackerType.Electronic)
			{
				refCountElectronic = refCountElectronic > 0 ? refCountElectronic - 1 : 0;
				INFO("StopHandTracker() " + tracker + "(" + refCountElectronic + ") from " + caller);
				if (refCountElectronic > 0) return;
			}

			if (!CanStopHandTracker(tracker))
				return;

			INFO("StopHandTracker() " + tracker);
			Thread hand_tracker_t = new Thread(StopHandTrackerThread);
			hand_tracker_t.Name = "StopHandTrackerThread";
			hand_tracker_t.Start(tracker);
		}

		private void RestartHandTrackerThread(object tracker)
		{
			lock (handTrackerThreadLocker)
			{
				DEBUG("RestartHandTrackerThread() " + (TrackerType)tracker);
				StopHandTrackerLock((TrackerType)tracker);
				StartHandTrackerLock((TrackerType)tracker);
			}
		}
		#endregion

		#region Hand Tracking Interface
		public TrackerStatus GetHandTrackerStatus(TrackerType tracker)
		{
			try
			{
				if (tracker == TrackerType.Electronic)
				{
					m_ElectronicTrackerStatusRWLock.TryEnterReadLock(2000);
					return m_ElectronicTrackerStatus;
				}
				else // TrackerType.Natural
				{
					m_NaturalTrackerStatusRWLock.TryEnterReadLock(2000);
					return m_NaturalTrackerStatus;
				}
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "GetHandTrackerStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				if (tracker == TrackerType.Electronic)
					m_ElectronicTrackerStatusRWLock.ExitReadLock();
				else // TrackerType.Natural
					m_NaturalTrackerStatusRWLock.ExitReadLock();
			}
		}
		public TrackerStatus GetHandTrackerStatus()
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetHandTrackerStatus(tracker);
			else
				return TrackerStatus.NotStart;
		}

		public void RestartHandTracker(TrackerType tracker)
		{
			TrackerStatus status = GetHandTrackerStatus();
			if (status == TrackerStatus.Starting || status == TrackerStatus.Stopping)
				return;
			INFO("RestartHandTracker() " + tracker);
			Thread hand_tracker_t = new Thread(RestartHandTrackerThread);
			hand_tracker_t.Name = "RestartHandTrackerThread";
			hand_tracker_t.Start(tracker);
		}
		public void RestartHandTracker(TrackerType tracker, HandTrackerResultDelegate callback)
		{
			if (handTrackerResultCB == null)
				handTrackerResultCB = callback;
			else
				handTrackerResultCB += callback;

			RestartHandTracker(tracker);
		}
		public void RestartHandTracker()
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				RestartHandTracker(tracker);
		}

		public bool IsHandPoseValid(TrackerType tracker, bool isLeft)
		{
			if (tracker == TrackerType.Natural)
			{
				if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
				{
					if (isLeft)
						return m_NaturalHandTrackerData.left.isValidPose;
					else
						return m_NaturalHandTrackerData.right.isValidPose;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
				{
					if (isLeft)
						return m_ElectronicHandTrackerData.left.isValidPose;
					else
						return m_ElectronicHandTrackerData.right.isValidPose;
				}
			}

			return false;
		}
		public bool IsHandPoseValid(TrackerType tracker, HandType hand)
		{
			return IsHandPoseValid(tracker, hand == HandType.Left ? true : false);
		}
		public bool IsHandPoseValid(bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return IsHandPoseValid(tracker, isLeft);
			return false;
		}
		public bool IsHandPoseValid(HandType hand)
		{
			return IsHandPoseValid(hand == HandType.Left ? true : false);
		}

		public float GetHandConfidence(TrackerType tracker, bool isLeft)
		{
			if (tracker == TrackerType.Natural)
			{
				if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
				{
					if (isLeft)
						return m_NaturalHandTrackerData.left.confidence;
					else
						return m_NaturalHandTrackerData.right.confidence;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
				{
					if (isLeft)
						return m_ElectronicHandTrackerData.left.confidence;
					else
						return m_ElectronicHandTrackerData.right.confidence;
				}
			}

			return 0;
		}
		public float GetHandConfidence(TrackerType tracker, HandType hand)
		{
			return GetHandConfidence(tracker, hand == HandType.Left ? true : false);
		}
		public float GetHandConfidence(bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetHandConfidence(tracker, isLeft);
			return 0;
		}
		public float GetHandConfidence(HandType hand)
		{
			return GetHandConfidence(hand == HandType.Left ? true : false);
		}

		/// <summary> @position will not be updated when no position. </summary>
		public bool GetJointPosition(TrackerType tracker, HandJoint joint, ref Vector3 position, bool isLeft)
		{
			if (!IsHandPoseValid(tracker, isLeft))
			{
				if (Log.gpl.Print)
					DEBUG("GetJointPosition() tracker " + tracker + (isLeft ? "left" : "right") + " joint " + joint + " has invalid pose.");
				return false;
			}

			bool ret = false;

			int joint_index = -1;

			if (tracker == TrackerType.Natural)
			{
				// Checks the WVR_HandTrackerInfo_t to get the valid joint index.
				for (int i = 0; i < m_NaturalTrackerInfo.jointCount; i++)
				{
					if (s_NaturalHandJoints[i] == (WVR_HandJoint)joint &&
						(s_NaturalHandJointsFlag[i] & (ulong)WVR_HandJointValidFlag.WVR_HandJointValidFlag_PositionValid) != 0)
					{
						joint_index = i;
						break;
					}
				}

				// Gets the WVR_HandJointData_t with the valid index.
				if (joint_index >= 0)
				{
					if (isLeft && joint_index < s_NaturalHandJointsPoseLeft.Length)
					{
						Coordinate.GetVectorFromGL(s_NaturalHandJointsPoseLeft[joint_index].position, out position);
						ret = true;
					}
					if (!isLeft && joint_index < s_NaturalHandJointsPoseRight.Length)
					{
						Coordinate.GetVectorFromGL(s_NaturalHandJointsPoseRight[joint_index].position, out position);
						ret = true;
					}
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				// Checks the WVR_HandTrackerInfo_t to get the valid joint index.
				for (int i = 0; i < m_ElectronicTrackerInfo.jointCount; i++)
				{
					if (s_ElectronicHandJoints[i] == (WVR_HandJoint)joint &&
						(s_ElectronicHandJointsFlag[i] & (ulong)WVR_HandJointValidFlag.WVR_HandJointValidFlag_PositionValid) != 0)
					{
						joint_index = i;
						break;
					}
				}

				// Gets the WVR_HandJointData_t with the valid index.
				if (joint_index >= 0)
				{
					if (isLeft && joint_index < s_ElectronicHandJointsPoseLeft.Length)
					{
						Coordinate.GetVectorFromGL(s_ElectronicHandJointsPoseLeft[joint_index].position, out position);
						ret = true;
					}
					if (!isLeft && joint_index < s_ElectronicHandJointsPoseRight.Length)
					{
						Coordinate.GetVectorFromGL(s_ElectronicHandJointsPoseRight[joint_index].position, out position);
						ret = true;
					}
				}
			}

			if (Log.gpl.Print)
			{
				DEBUG("GetJointPosition() "
					+ "tracker: " + tracker
					+ ", " + (isLeft ? "Left" : "Right") + " joint " + joint
					+ ", position {" + position.x.ToString() + ", " + position.y.ToString() + ", " + position.z.ToString() + ")");
			}

			return ret;
		}
		/// <summary> @position will not be updated when no position. </summary>
		public bool GetJointPosition(TrackerType tracker, HandJoint joint, ref Vector3 position, HandType hand)
		{
			return GetJointPosition(tracker, joint, ref position, hand == HandType.Left ? true : false);
		}
		public bool GetJointPosition(HandJoint joint, ref Vector3 position, bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetJointPosition(tracker, joint, ref position, isLeft);
			return false;
		}
		public bool GetJointPosition(HandJoint joint, ref Vector3 position, HandType hand)
		{
			return GetJointPosition(joint, ref position, hand == HandType.Left ? true : false);
		}

		/// <summary> @rotation will not be updated when no rotation. </summary>
		public bool GetJointRotation(TrackerType tracker, HandJoint joint, ref Quaternion rotation, bool isLeft)
		{
			if (!IsHandPoseValid(tracker, isLeft))
				return false;

			bool ret = false;

			int joint_index = -1;

			if (tracker == TrackerType.Natural)
			{
				// Checks the WVR_HandTrackerInfo_t to get the valid joint index.
				for (int i = 0; i < m_NaturalTrackerInfo.jointCount; i++)
				{
					if (s_NaturalHandJoints[i] == (WVR_HandJoint)joint &&
						(s_NaturalHandJointsFlag[i] & (ulong)WVR_HandJointValidFlag.WVR_HandJointValidFlag_RotationValid) != 0)
					{
						joint_index = i;
						break;
					}
				}

				// Gets the WVR_HandJointData_t with the valid index.
				if (joint_index >= 0)
				{
					if (isLeft && joint_index < s_NaturalHandJointsPoseLeft.Length)
					{
						Coordinate.GetQuaternionFromGL(s_NaturalHandJointsPoseLeft[joint_index].rotation, out rotation);
						ret = true;
					}
					if (!isLeft && joint_index < s_NaturalHandJointsPoseRight.Length)
					{
						Coordinate.GetQuaternionFromGL(s_NaturalHandJointsPoseRight[joint_index].rotation, out rotation);
						ret = true;
					}
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				// Checks the WVR_HandTrackerInfo_t to get the valid joint index.
				for (int i = 0; i < m_ElectronicTrackerInfo.jointCount; i++)
				{
					if (s_ElectronicHandJoints[i] == (WVR_HandJoint)joint &&
						(s_ElectronicHandJointsFlag[i] & (ulong)WVR_HandJointValidFlag.WVR_HandJointValidFlag_RotationValid) != 0)
					{
						joint_index = i;
						break;
					}
				}

				// Gets the WVR_HandJointData_t with the valid index.
				if (joint_index >= 0)
				{
					if (isLeft && joint_index < s_ElectronicHandJointsPoseLeft.Length)
					{
						Coordinate.GetQuaternionFromGL(s_ElectronicHandJointsPoseLeft[joint_index].rotation, out rotation);
						ret = true;
					}
					if (!isLeft && joint_index < s_ElectronicHandJointsPoseRight.Length)
					{
						Coordinate.GetQuaternionFromGL(s_ElectronicHandJointsPoseRight[joint_index].rotation, out rotation);
						ret = true;
					}
				}
			}

			if (Log.gpl.Print)
			{
				DEBUG("GetJointRotation()"
					+ " tracker: " + tracker
					+ ", " + (isLeft ? "Left" : "Right") + " joint " + joint
					+ ", rotation {" + rotation.x.ToString() + ", " + rotation.y.ToString() + ", " + rotation.z.ToString() + ", " + rotation.w.ToString() + ")");
			}

			return ret;
		}
		/// <summary> @rotation will not be updated when no rotation. </summary>
		public bool GetJointRotation(TrackerType tracker, HandJoint joint, ref Quaternion rotation, HandType hand)
		{
			return GetJointRotation(tracker, joint, ref rotation, hand == HandType.Left ? true : false);
		}
		public bool GetJointRotation(HandJoint joint, ref Quaternion rotation, bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetJointRotation(tracker, joint, ref rotation, isLeft);
			return false;
		}
		public bool GetJointRotation(HandJoint joint, ref Quaternion rotation, HandType hand)
		{
			return GetJointRotation(joint, ref rotation, hand == HandType.Left ? true : false);
		}

		/// <summary> @scale will not be updated when no scale. </summary>
		public bool GetHandScale(TrackerType tracker, ref Vector3 scale, bool isLeft)
		{
			if (!IsHandPoseValid(tracker, isLeft))
				return false;

			bool ret = false;

			if (tracker == TrackerType.Natural)
			{
				var scaleGL = isLeft ? m_NaturalHandJointDataLeft.scale : m_NaturalHandJointDataRight.scale;
				scale = new Vector3(scaleGL.v0, scaleGL.v1, scaleGL.v2);
				ret = true;
			}
			if (tracker == TrackerType.Electronic)
			{
				var scaleGL = isLeft ? m_ElectronicHandJointDataLeft.scale : m_ElectronicHandJointDataRight.scale;
				scale = new Vector3(scaleGL.v0, scaleGL.v1, scaleGL.v2);
				ret = true;
			}

			if (Log.gpl.Print)
			{
				DEBUG("GetHandScale()"
					+ " tracker: " + tracker
					+ ", " + (isLeft ? "Left" : "Right")
					+ ", scale {" + scale.x.ToString() + ", " + scale.y.ToString() + ", " + scale.z.ToString() + ")");
			}

			return ret;
		}
		/// <summary> @scale will not be updated when no scale. </summary>
		public bool GetHandScale(TrackerType tracker, ref Vector3 scale, HandType hand)
		{
			return GetHandScale(tracker, ref scale, hand == HandType.Left ? true : false);
		}
		public bool GetHandScale(ref Vector3 scale, bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetHandScale(tracker, ref scale, isLeft);
			return false;
		}
		public bool GetHandScale(ref Vector3 scale, HandType hand)
		{
			return GetHandScale(ref scale, hand == HandType.Left ? true : false);
		}

		/// <summary> Checks if the player in taking a motion, e.g. Pinch, Hold. </summary>
		public bool GetHandMotion(TrackerType tracker, out HandMotion motion, bool isLeft)
		{
			if (tracker == TrackerType.Natural)
			{
				if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
				{
					motion = isLeft ?
						(HandMotion)m_NaturalHandPoseData.left.state.type :
						(HandMotion)m_NaturalHandPoseData.right.state.type;

					return true;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
				{
					motion = isLeft ?
						(HandMotion)m_ElectronicHandPoseData.left.state.type :
						(HandMotion)m_ElectronicHandPoseData.right.state.type;

					return true;
				}
			}

			motion = HandMotion.None;
			return false;
		}
		public HandMotion GetHandMotion(TrackerType tracker, bool isLeft)
		{
			HandMotion motion = HandMotion.None;

			if (GetHandMotion(tracker, out HandMotion value, isLeft))
				motion = value;

			return motion;
		}
		public HandMotion GetHandMotion(TrackerType tracker, HandType hand)
		{
			return GetHandMotion(tracker, hand == HandType.Left ? true : false);
		}
		public HandMotion GetHandMotion(bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetHandMotion(tracker, isLeft);
			return HandMotion.None;
		}
		public HandMotion GetHandMotion(HandType hand)
		{
			return GetHandMotion(hand == HandType.Left ? true : false);
		}

		/// <summary> Checks if the player is holding or side holding an equipment. </summary>
		public bool GetHandHoldRole(TrackerType tracker, out HandHoldRole role, bool isLeft)
		{
			if (GetHandMotion(tracker, isLeft) == HandMotion.Hold)
			{
				if (tracker == TrackerType.Natural)
				{
					if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
					{
						role = isLeft ?
							(HandHoldRole)m_NaturalHandPoseData.left.hold.role :
							(HandHoldRole)m_NaturalHandPoseData.right.hold.role;

						return true;
					}
				}
				if (tracker == TrackerType.Electronic)
				{
					if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
					{
						role = isLeft ?
							(HandHoldRole)m_ElectronicHandPoseData.left.hold.role :
							(HandHoldRole)m_ElectronicHandPoseData.right.hold.role;

						return true;
					}
				}
			}

			role = HandHoldRole.None;
			return false;
		}
		public HandHoldRole GetHandHoldRole(TrackerType tracker, bool isLeft)
		{
			HandHoldRole role = HandHoldRole.None;

			if (GetHandHoldRole(tracker, out HandHoldRole value, isLeft))
				role = value;

			return role;
		}
		public HandHoldRole GetHandHoldRole(TrackerType tracker, HandType hand)
		{
			return GetHandHoldRole(tracker, hand == HandType.Left ? true : false);

		}
		public bool GetHandHoldRole(out HandHoldRole role, bool isLeft)
		{
			TrackerType tracker = TrackerType.Natural;
			if (GetPreferTracker(ref tracker))
			{
				return GetHandHoldRole(tracker, out role, isLeft);
			}

			role = HandHoldRole.None;
			return false;
		}
		public HandHoldRole GetHandHoldRole(bool isLeft)
		{
			HandHoldRole role = HandHoldRole.None;

			if (GetHandHoldRole(out HandHoldRole value, isLeft))
				role = value;

			return role;
		}

		/// <summary> Retrieves the type of equipment hold by the player. </summary>
		public bool GetHandHoldType(TrackerType tracker, out HandHoldType type, bool isLeft)
		{
			if (GetHandMotion(tracker, isLeft) == HandMotion.Hold)
			{
				if (tracker == TrackerType.Natural)
				{
					if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
					{
						type = isLeft ?
							(HandHoldType)m_NaturalHandPoseData.left.hold.type :
							(HandHoldType)m_NaturalHandPoseData.right.hold.type;

						return true;
					}
				}
				if (tracker == TrackerType.Electronic)
				{
					if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
					{
						type = isLeft ?
							(HandHoldType)m_ElectronicHandPoseData.left.hold.type :
							(HandHoldType)m_ElectronicHandPoseData.right.hold.type;

						return true;
					}
				}
			}

			type = HandHoldType.None;
			return false;
		}
		public HandHoldType GetHandHoldType(TrackerType tracker, bool isLeft)
		{
			HandHoldType type = HandHoldType.None;

			if (GetHandHoldType(tracker, out HandHoldType value, isLeft))
				type = value;

			return type;
		}
		public HandHoldType GetHandHoldType(TrackerType tracker, HandType hand)
		{
			return GetHandHoldType(tracker, hand == HandType.Left ? true : false);
		}
		public bool GetHandHoldType(out HandHoldType type, bool isLeft)
		{
			TrackerType tracker = TrackerType.Natural;
			if (GetPreferTracker(ref tracker))
			{
				return GetHandHoldType(tracker, out type, isLeft);
			}

			type = HandHoldType.None;
			return false;
		}
		public HandHoldType GetHandHoldType(bool isLeft)
		{
			HandHoldType type = HandHoldType.None;

			if (GetHandHoldType(out HandHoldType value, isLeft))
				type = value;

			return type;
		}

		public bool GetPinchOrigin(TrackerType tracker, ref Vector3 origin, bool isLeft)
		{
			if (GetHandMotion(tracker, isLeft) != HandMotion.Pinch)
				return false;

			if (tracker == TrackerType.Natural)
			{
				if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
				{
					if (isLeft)
						Coordinate.GetVectorFromGL(m_NaturalHandPoseData.left.pinch.origin, out origin);
					else
						Coordinate.GetVectorFromGL(m_NaturalHandPoseData.right.pinch.origin, out origin);

					return true;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
				{
					if (isLeft)
						Coordinate.GetVectorFromGL(m_ElectronicHandPoseData.left.pinch.origin, out origin);
					else
						Coordinate.GetVectorFromGL(m_ElectronicHandPoseData.right.pinch.origin, out origin);

					return true;
				}
			}

			return false;
		}
		public bool GetPinchOrigin(TrackerType tracker, ref Vector3 origin, HandType hand)
		{
			return GetPinchOrigin(tracker, ref origin, hand == HandType.Left ? true : false);
		}
		public bool GetPinchOrigin(ref Vector3 origin, bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetPinchOrigin(tracker, ref origin, isLeft);
			return false;
		}
		public bool GetPinchOrigin(ref Vector3 origin, HandType hand)
		{
			return GetPinchOrigin(ref origin, hand == HandType.Left ? true : false);
		}

		public bool GetPinchDirection(TrackerType tracker, ref Vector3 direction, bool isLeft)
		{
			if (GetHandMotion(tracker, isLeft) != HandMotion.Pinch)
				return false;

			if (tracker == TrackerType.Natural)
			{
				if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
				{
					if (isLeft)
						Coordinate.GetVectorFromGL(m_NaturalHandPoseData.left.pinch.direction, out direction);
					else
						Coordinate.GetVectorFromGL(m_NaturalHandPoseData.right.pinch.direction, out direction);

					return true;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
				{
					if (isLeft)
						Coordinate.GetVectorFromGL(m_ElectronicHandPoseData.left.pinch.direction, out direction);
					else
						Coordinate.GetVectorFromGL(m_ElectronicHandPoseData.right.pinch.direction, out direction);

					return true;
				}
			}

			return false;
		}
		public bool GetPinchDirection(TrackerType tracker, ref Vector3 direction, HandType hand)
		{
			return GetPinchDirection(tracker, ref direction, hand == HandType.Left ? true : false);
		}
		public bool GetPinchDirection(ref Vector3 direction, bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetPinchDirection(tracker, ref direction, isLeft);
			return false;
		}
		public bool GetPinchDirection(ref Vector3 direction, HandType hand)
		{
			return GetPinchDirection(ref direction, hand == HandType.Left ? true : false);
		}

		public float GetPinchStrength(TrackerType tracker, bool isLeft)
		{
			if (GetHandMotion(tracker, isLeft) != HandMotion.Pinch)
				return 0;

			if (tracker == TrackerType.Natural)
			{
				if (hasNaturalHandTrackerData && hasNaturalTrackerInfo)
				{
					if (isLeft)
						return m_NaturalHandPoseData.left.pinch.strength;
					else
						return m_NaturalHandPoseData.right.pinch.strength;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (hasElectronicHandTrackerData && hasElectronicTrackerInfo)
				{
					if (isLeft)
						return m_ElectronicHandPoseData.left.pinch.strength;
					else
						return m_ElectronicHandPoseData.right.pinch.strength;
				}
			}

			return 0;
		}
		public float GetPinchStrength(TrackerType tracker, HandType hand)
		{
			return GetPinchStrength(tracker, hand == HandType.Left ? true : false);
		}
		public float GetPinchStrength(bool isLeft)
		{
			TrackerType tracker = TrackerType.Electronic;
			if (GetPreferTracker(ref tracker))
				return GetPinchStrength(tracker, isLeft);
			return 0;
		}
		public float GetPinchStrength(HandType hand)
		{
			return GetPinchStrength(hand == HandType.Left ? true : false);
		}

		public float GetPinchThreshold(TrackerType tracker)
		{
			if ((tracker == TrackerType.Natural) && hasNaturalTrackerInfo)
			{
				return m_NaturalTrackerInfo.pinchThreshold;
			}
			if ((tracker == TrackerType.Electronic) && hasElectronicTrackerInfo)
			{
				return m_ElectronicTrackerInfo.pinchThreshold;
			}
			return 0;
		}
		public float GetPinchThreshold()
		{
			TrackerType tracker = TrackerType.Natural;
			if (GetPreferTracker(ref tracker))
				return GetPinchThreshold(tracker);
			return 0;
		}
		#endregion

		private uint m_NaturalHandJointCount = 0;
		private WVR_HandTrackingData_t m_NaturalHandTrackerData = new WVR_HandTrackingData_t();
		private WVR_HandJointData_t m_NaturalHandJointDataLeft = new WVR_HandJointData_t();
		private WVR_Pose_t[] s_NaturalHandJointsPoseLeft;
		private WVR_HandJointData_t m_NaturalHandJointDataRight = new WVR_HandJointData_t();
		private WVR_Pose_t[] s_NaturalHandJointsPoseRight;

		private uint m_ElectronicHandJointCount;
		private WVR_HandTrackingData_t m_ElectronicHandTrackerData = new WVR_HandTrackingData_t();
		private WVR_HandJointData_t m_ElectronicHandJointDataLeft = new WVR_HandJointData_t();
		private WVR_Pose_t[] s_ElectronicHandJointsPoseLeft;
		private WVR_HandJointData_t m_ElectronicHandJointDataRight = new WVR_HandJointData_t();
		private WVR_Pose_t[] s_ElectronicHandJointsPoseRight;
		private HandJointData26 jointData26 = new HandJointData26();

		private void GetHandJointCount(TrackerType tracker)
		{
			if (GetHandTrackerStatus(tracker) != TrackerStatus.Available)
			{
				if (tracker == TrackerType.Natural)
					m_NaturalHandJointCount = 0;
				if (tracker == TrackerType.Electronic)
					m_ElectronicHandJointCount = 0;
				return;
			}

			uint count = 0;
			WVR_Result result = Interop.WVR_GetHandJointCount((WVR_HandTrackerType)tracker, ref count);
			if (tracker == TrackerType.Natural)
			{
				if (result != WVR_Result.WVR_Success)
				{
					m_NaturalHandJointCount = 0;
					return;
				}
				if (m_NaturalHandJointCount != count)
				{
					DEBUG("GetHandJointCount() " + tracker);
					m_NaturalHandJointCount = count;
					InitializeHandTrackerInfo(ref m_NaturalTrackerInfo, ref s_NaturalHandJoints, ref s_NaturalHandJointsFlag, m_NaturalHandJointCount);
					InitializeHandTrackerData(
						ref m_NaturalHandTrackerData,
						ref m_NaturalHandJointDataLeft,
						ref m_NaturalHandJointDataRight,
						ref s_NaturalHandJointsPoseLeft,
						ref s_NaturalHandJointsPoseRight,
						m_NaturalHandJointCount);
					DEBUG("GetHandJointCount() " + tracker
						+ ", joint count: " + m_NaturalTrackerInfo.jointCount
						+ ", right (" + m_NaturalHandTrackerData.right.isValidPose + ", " + m_NaturalHandTrackerData.right.confidence + ", " + m_NaturalHandTrackerData.right.jointCount + ")"
						+ ", left (" + m_NaturalHandTrackerData.left.isValidPose + ", " + m_NaturalHandTrackerData.left.confidence + ", " + m_NaturalHandTrackerData.left.jointCount + ")");
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (result != WVR_Result.WVR_Success)
				{
					m_ElectronicHandJointCount = 0;
					return;
				}
				if (m_ElectronicHandJointCount != count)
				{
					DEBUG("GetHandJointCount() " + tracker);
					m_ElectronicHandJointCount = count;
					InitializeHandTrackerInfo(ref m_ElectronicTrackerInfo, ref s_ElectronicHandJoints, ref s_ElectronicHandJointsFlag, m_ElectronicHandJointCount);
					InitializeHandTrackerData(
						ref m_ElectronicHandTrackerData,
						ref m_ElectronicHandJointDataLeft,
						ref m_ElectronicHandJointDataRight,
						ref s_ElectronicHandJointsPoseLeft,
						ref s_ElectronicHandJointsPoseRight,
						m_ElectronicHandJointCount);
					DEBUG("GetHandJointCount() " + tracker
						+ ", joint count: " + m_ElectronicTrackerInfo.jointCount
						+ ", right (" + m_ElectronicHandTrackerData.right.isValidPose + ", " + m_ElectronicHandTrackerData.right.confidence + ", " + m_ElectronicHandTrackerData.right.jointCount
						+ ", left (" + m_ElectronicHandTrackerData.left.isValidPose + ", " + m_ElectronicHandTrackerData.left.confidence + ", " + m_ElectronicHandTrackerData.left.jointCount);
				}
			}
		}

		#region Hand Tracker Info
		int[] intJointMappingArray;
		byte[] jointValidFlagArrayBytes;
		private void InitializeHandTrackerInfo(ref WVR_HandTrackerInfo_t handTrackerInfo, ref WVR_HandJoint[] jointMappingArray, ref ulong[] jointValidFlagArray, uint count)
		{
			DEBUG("InitializeHandTrackerInfo() count: " + count);
			handTrackerInfo.jointCount = count;
			handTrackerInfo.handModelTypeBitMask = 0;

			/// WVR_HandTrackerInfo_t.jointMappingArray
			jointMappingArray = new WVR_HandJoint[count];
			intJointMappingArray = new int[jointMappingArray.Length];
			intJointMappingArray = Array.ConvertAll(jointMappingArray, delegate (WVR_HandJoint value) { return (int)value; });
			handTrackerInfo.jointMappingArray = Marshal.AllocHGlobal(sizeof(int) * intJointMappingArray.Length);
			Marshal.Copy(intJointMappingArray, 0, handTrackerInfo.jointMappingArray, intJointMappingArray.Length);
			/*unsafe
			{
				fixed (WVR_HandJoint* pJointMappingArray = jointMappingArray)
				{
					handTrackerInfo.jointMappingArray = pJointMappingArray;
				}
			}*/

			/// WVR_HandTrackerInfo_t.jointValidFlagArray
			jointValidFlagArray = new ulong[count];
			int jointValidFlagArrayByteLength = Buffer.ByteLength(jointValidFlagArray);
			jointValidFlagArrayBytes = new byte[jointValidFlagArrayByteLength];
			Buffer.BlockCopy(jointValidFlagArray, 0, jointValidFlagArrayBytes, 0, jointValidFlagArrayBytes.Length);

			handTrackerInfo.jointValidFlagArray = Marshal.AllocHGlobal(sizeof(byte) * jointValidFlagArrayBytes.Length);
			Marshal.Copy(jointValidFlagArrayBytes, 0, handTrackerInfo.jointValidFlagArray, jointValidFlagArrayBytes.Length);
			/*unsafe
			{
				fixed (ulong* pHandJointsFlag = jointValidFlagArray)
				{
					handTrackerInfo.jointValidFlagArray = pHandJointsFlag;
				}
			}*/

			handTrackerInfo.pinchThreshold = 0;
		}
		private bool ExtractHandTrackerInfo(WVR_HandTrackerInfo_t handTrackerInfo, ref WVR_HandJoint[] jointMappingArray, ref ulong[] jointValidFlagArray)
		{
			if (handTrackerInfo.jointCount == 0)
			{
				WARNING("ExtractHandTrackerInfo() WVR_GetHandTrackerInfo WVR_HandTrackerInfo_t jointCount SHOULD NOT be 0!!");
				return false;
			}

			// WVR_HandTrackerInfo_t.jointMappingArray
			if (jointMappingArray.Length != handTrackerInfo.jointCount)
			{
				WARNING("ExtractHandTrackerInfo() The WVR_GetHandJointCount count (jointMappingArray) " + jointMappingArray.Length
					+ " differs from WVR_GetHandTrackerInfo WVR_HandTrackerInfo_t jointCount " + handTrackerInfo.jointCount);
				jointMappingArray = new WVR_HandJoint[handTrackerInfo.jointCount];
				intJointMappingArray = new int[jointMappingArray.Length];
			}

			Marshal.Copy(handTrackerInfo.jointMappingArray, intJointMappingArray, 0, intJointMappingArray.Length);
			jointMappingArray = Array.ConvertAll(intJointMappingArray, delegate (int value) { return (WVR_HandJoint)value; });
			/*unsafe
			{
				for (int i = 0; i < jointMappingArray.Length; i++)
				{
					jointMappingArray[i] = *(handTrackerInfo.jointMappingArray + i);
				}
			}*/

			// WVR_HandTrackerInfo_t.jointValidFlagArray
			if (jointValidFlagArray.Length != handTrackerInfo.jointCount)
			{
				WARNING("ExtractHandTrackerInfo() The WVR_GetHandJointCount count (jointValidFlagArray) " + jointValidFlagArray.Length
					+ " differs from WVR_GetHandTrackerInfo WVR_HandTrackerInfo_t jointCount " + handTrackerInfo.jointCount);
				jointValidFlagArray = new ulong[handTrackerInfo.jointCount];
				int jointValidFlagArrayByteLength = Buffer.ByteLength(jointValidFlagArray);
				jointValidFlagArrayBytes = new byte[jointValidFlagArrayByteLength];
			}

			Marshal.Copy(handTrackerInfo.jointValidFlagArray, jointValidFlagArrayBytes, 0, jointValidFlagArrayBytes.Length);
			for (int byteIndex = 0; byteIndex < jointValidFlagArrayBytes.Length; byteIndex = byteIndex+8)
			{
				int i = (byteIndex / 8);
				jointValidFlagArray[i] = BitConverter.ToUInt64(jointValidFlagArrayBytes, byteIndex);
			}
			/*unsafe
			{
				for (int i = 0; i < jointValidFlagArray.Length; i++)
				{
					jointValidFlagArray[i] = *(handTrackerInfo.jointValidFlagArray + i);
				}
			}*/

			return true;
		}

		private bool hasNaturalTrackerInfo = false;
		private WVR_HandTrackerInfo_t m_NaturalTrackerInfo = new WVR_HandTrackerInfo_t();
		private WVR_HandJoint[] s_NaturalHandJoints;
		private ulong[] s_NaturalHandJointsFlag;

		private bool hasElectronicTrackerInfo;
		private WVR_HandTrackerInfo_t m_ElectronicTrackerInfo = new WVR_HandTrackerInfo_t();
		private WVR_HandJoint[] s_ElectronicHandJoints;
		private ulong[] s_ElectronicHandJointsFlag;
		private void GetHandTrackerInfo(TrackerType tracker)
		{
			INFO("GetHandTrackerInfo() " + tracker);
			if (tracker == TrackerType.Natural)
			{
				if (GetHandTrackerStatus(TrackerType.Natural) == TrackerStatus.Available && m_NaturalHandJointCount != 0)
				{
					hasNaturalTrackerInfo = Interop.WVR_GetHandTrackerInfo((WVR_HandTrackerType)tracker, ref m_NaturalTrackerInfo) == WVR_Result.WVR_Success ? true : false;
					if (hasNaturalTrackerInfo)
					{
						hasNaturalTrackerInfo = ExtractHandTrackerInfo(m_NaturalTrackerInfo, ref s_NaturalHandJoints, ref s_NaturalHandJointsFlag);
						if (hasNaturalTrackerInfo)
						{
							for (int i = 0; i < m_NaturalTrackerInfo.jointCount; i++)
							{
								DEBUG("GetHandTrackerInfo() " + tracker
									+ ", joint count: " + m_NaturalTrackerInfo.jointCount
									+ ", s_NaturalHandJoints[" + i + "] = " + s_NaturalHandJoints[i]
									+ ", s_NaturalHandJointsFlag[" + i + "] = " + s_NaturalHandJointsFlag[i]);
							}
						}
					}
				}
				else
				{
					if (Log.gpl.Print)
						Log.e(LOG_TAG, "GetHandTrackerInfo() Natural, failed!!", true);
					hasNaturalTrackerInfo = false;
				}
			}
			if (tracker == TrackerType.Electronic)
			{
				if (GetHandTrackerStatus(TrackerType.Electronic) == TrackerStatus.Available && m_ElectronicHandJointCount != 0)
				{
					hasElectronicTrackerInfo = Interop.WVR_GetHandTrackerInfo((WVR_HandTrackerType)tracker, ref m_ElectronicTrackerInfo) == WVR_Result.WVR_Success ? true : false;
					if (hasElectronicTrackerInfo)
					{
						hasElectronicTrackerInfo = ExtractHandTrackerInfo(m_ElectronicTrackerInfo, ref s_ElectronicHandJoints, ref s_ElectronicHandJointsFlag);
						if (hasElectronicTrackerInfo)
						{
							for (int i = 0; i < m_ElectronicTrackerInfo.jointCount; i++)
							{
								DEBUG("GetHandTrackerInfo() " + tracker
									+ ", joint count: " + m_ElectronicTrackerInfo.jointCount
									+ ", s_ElectronicHandJoints[" + i + "] = " + s_ElectronicHandJoints[i]
									+ ", s_ElectronicHandJointsFlag[" + i + "] = " + s_ElectronicHandJointsFlag[i]);
							}
						}
					}
				}
				else
				{
					if (Log.gpl.Print)
						Log.e(LOG_TAG, "GetHandTrackerInfo() Electronic, failed!!", true);
					hasElectronicTrackerInfo = false;
				}
			}
		}
		#endregion

		#region Hand Tracker Data
		private void InitializeHandJointData(ref WVR_HandJointData_t handJointData, ref WVR_Pose_t[] jointsPose, uint count)
		{
			handJointData.isValidPose = false;
			handJointData.confidence = 0;
			handJointData.jointCount = count;

			WVR_Pose_t wvr_pose_type = default(WVR_Pose_t);
			handJointData.joints = Marshal.AllocHGlobal(Marshal.SizeOf(wvr_pose_type) * (int)count);
			handJointData.scale = new WVR_Vector3f_t() {
				v0 = 1,
				v1 = 1,
				v2 = 1
			};

			jointsPose = new WVR_Pose_t[count];

			long offset = 0;
			if (IntPtr.Size == 4)
				offset = handJointData.joints.ToInt32();
			else
				offset = handJointData.joints.ToInt64();

			for (int i = 0; i < jointsPose.Length; i++)
			{
				IntPtr wvr_pose_ptr = new IntPtr(offset);
				Marshal.StructureToPtr(jointsPose[i], wvr_pose_ptr, false);
				offset += Marshal.SizeOf(wvr_pose_type);
			}
		}
		private void InitializeHandTrackerData(
			ref WVR_HandTrackingData_t handTrackerData,
			ref WVR_HandJointData_t handJointDataLeft,
			ref WVR_HandJointData_t handJointDataRight,
			ref WVR_Pose_t[] handJointsPoseLeft,
			ref WVR_Pose_t[] handJointsPoseRight,
			uint count
		)
		{
			handTrackerData.timestamp = 0;

			InitializeHandJointData(ref handJointDataLeft, ref handJointsPoseLeft, count);
			handTrackerData.left = handJointDataLeft;

			InitializeHandJointData(ref handJointDataRight, ref handJointsPoseRight, count);
			handTrackerData.right = handJointDataRight;
		}

		private bool ExtractHandJointData(WVR_HandJointData_t handJointData, ref WVR_Pose_t[] jointsPose)
		{
			if (handJointData.jointCount == 0)
			{
				WARNING("ExtractHandJointData() WVR_GetHandTrackingData WVR_HandJointData_t jointCount SHOULD NOT be 0!!");
				return false;
			}

			if (jointsPose.Length != handJointData.jointCount)
			{
				WARNING("ExtractHandJointData() The WVR_GetHandJointCount count " + jointsPose.Length
					+ " differs from WVR_GetHandTrackingData WVR_HandJointData_t jointCount " + handJointData.jointCount);
				jointsPose = new WVR_Pose_t[handJointData.jointCount];
			}

			WVR_Pose_t wvr_pose_type = default(WVR_Pose_t);

			Profiler.BeginSample("Get JointsPose");
			int offset = 0;
			for (int i = 0; i < jointsPose.Length; i++)
			{
				if (IntPtr.Size == 4)
					jointsPose[i] = (WVR_Pose_t)Marshal.PtrToStructure(new IntPtr(handJointData.joints.ToInt32() + offset), typeof(WVR_Pose_t));
				else
					jointsPose[i] = (WVR_Pose_t)Marshal.PtrToStructure(new IntPtr(handJointData.joints.ToInt64() + offset), typeof(WVR_Pose_t));

				offset += Marshal.SizeOf(wvr_pose_type);
			}
			Profiler.EndSample();

			return true;
		}

		private void ExtractHandJointData2(ref WVR_HandJointData_t jd, ref WVR_Pose_t[] poses)
		{
			Profiler.BeginSample("Get JointsPose");
			ConvertHandTrackingDataToUnity(ref jd, ref jointData26);

			poses[0] = jointData26.j00;
			poses[1] = jointData26.j01;
			poses[2] = jointData26.j02;
			poses[3] = jointData26.j03;
			poses[4] = jointData26.j04;
			poses[5] = jointData26.j05;
			poses[6] = jointData26.j06;
			poses[7] = jointData26.j07;
			poses[8] = jointData26.j08;
			poses[9] = jointData26.j09;
			poses[10] = jointData26.j10;
			poses[11] = jointData26.j11;
			poses[12] = jointData26.j12;
			poses[13] = jointData26.j13;
			poses[14] = jointData26.j14;
			poses[15] = jointData26.j15;
			poses[16] = jointData26.j16;
			poses[17] = jointData26.j17;
			poses[18] = jointData26.j18;
			poses[19] = jointData26.j19;
			poses[20] = jointData26.j20;
			poses[21] = jointData26.j21;
			poses[22] = jointData26.j22;
			poses[23] = jointData26.j23;
			poses[24] = jointData26.j24;
			poses[25] = jointData26.j25;
			Profiler.EndSample();
		}

		private bool ExtractHandTrackerData(WVR_HandTrackingData_t handTrackerData, ref WVR_Pose_t[] handJointsPoseLeft, ref WVR_Pose_t[] handJointsPoseRight)
		{
			if (handTrackerData.left.jointCount == 26 && 
				handTrackerData.right.jointCount == 26 &&
				handJointsPoseLeft.Length == 26 &&
				handJointsPoseRight.Length == 26 &&
				ConvertHandTrackingDataToUnity != null)
			{
				// A very fast way to convert data
				ExtractHandJointData2(ref handTrackerData.left, ref handJointsPoseLeft);
				ExtractHandJointData2(ref handTrackerData.right, ref handJointsPoseRight);
				return true;
			}

			if (!ExtractHandJointData(handTrackerData.left, ref handJointsPoseLeft))
				return false;
			if (!ExtractHandJointData(handTrackerData.right, ref handJointsPoseRight))
				return false;

			return true;
		}

		private WVR_PoseOriginModel originModel = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnHead;
		private WVR_HandPoseData_t m_NaturalHandPoseData = new WVR_HandPoseData_t(), m_ElectronicHandPoseData = new WVR_HandPoseData_t();
		private bool hasNaturalHandTrackerData = false, hasElectronicHandTrackerData = false;
		private void GetHandTrackingData(TrackerType tracker)
		{
			if (GetHandTrackerStatus(tracker) != TrackerStatus.Available)
				return;

			ClientInterface.GetOrigin(ref originModel);

			if (tracker == TrackerType.Natural)
			{
				Profiler.BeginSample("Natural Hand");
				WVR_Result result = WVR_Result.WVR_Error_InvalidArgument;
				if (hasNaturalTrackerInfo &&
					m_NaturalTrackerInfo.jointCount != 0 &&
					(m_NaturalTrackerInfo.handModelTypeBitMask & (ulong)WVR_HandModelType.WVR_HandModelType_WithoutController) != 0)
				{
					Profiler.BeginSample("GetHandTrackingData");
					result = Interop.WVR_GetHandTrackingData(
						WVR_HandTrackerType.WVR_HandTrackerType_Natural,
						WVR_HandModelType.WVR_HandModelType_WithoutController,
						originModel,
						ref m_NaturalHandTrackerData,
						ref m_NaturalHandPoseData
					);
					Profiler.EndSample();

					hasNaturalHandTrackerData = result == WVR_Result.WVR_Success ? true : false;
					if (hasNaturalHandTrackerData)
					{
						if (Log.gpl.Print)
						{
							DEBUG("GetHandTrackingData() Natural"
								+ ", timestamp: " + m_NaturalHandTrackerData.timestamp
								+ ", left valid? " + m_NaturalHandTrackerData.left.isValidPose
								+ ", left confidence: " + m_NaturalHandTrackerData.left.confidence.ToString()
								+ ", left count: " + m_NaturalHandTrackerData.left.jointCount
								+ ", right valid? " + m_NaturalHandTrackerData.right.isValidPose
								+ ", right confidence: " + m_NaturalHandTrackerData.right.confidence.ToString()
								+ ", right count: " + m_NaturalHandTrackerData.right.jointCount);

							if (m_NaturalHandPoseData.left.state.type == WVR_HandPoseType.WVR_HandPoseType_Pinch)
							{
								DEBUG("GetHandTrackingData() Natural, left pinch "
									+ "strength: " + m_NaturalHandPoseData.left.pinch.strength.ToString()
								);
							}
							if (m_NaturalHandPoseData.left.state.type == WVR_HandPoseType.WVR_HandPoseType_Hold)
							{
								DEBUG("GetHandTrackingData() Natural, left hold "
									+ "role: " + m_NaturalHandPoseData.left.hold.role
									+ ", type: " + m_NaturalHandPoseData.left.hold.type
								);
							}
							if (m_NaturalHandPoseData.right.state.type == WVR_HandPoseType.WVR_HandPoseType_Pinch)
							{
								DEBUG("GetHandTrackingData() Natural, right pinch "
									+ "strength: " + m_NaturalHandPoseData.right.pinch.strength.ToString()
								);
							}
							if (m_NaturalHandPoseData.right.state.type == WVR_HandPoseType.WVR_HandPoseType_Hold)
							{
								DEBUG("GetHandTrackingData() Natural, right hold "
									+ "role: " + m_NaturalHandPoseData.right.hold.role
									+ ", type: " + m_NaturalHandPoseData.right.hold.type
								);
							}
						}

						hasNaturalHandTrackerData = ExtractHandTrackerData(m_NaturalHandTrackerData, ref s_NaturalHandJointsPoseLeft, ref s_NaturalHandJointsPoseRight);
						if (hasNaturalHandTrackerData)
						{
							m_NaturalHandJointDataLeft = m_NaturalHandTrackerData.left;
							m_NaturalHandJointDataRight = m_NaturalHandTrackerData.right;
						}
					}
					else
					{
						if (Log.gpl.Print)
							Log.e(LOG_TAG, "GetHandTrackingData() Natural " + result, true);

						m_NaturalHandTrackerData.left.isValidPose = false;
						m_NaturalHandTrackerData.left.confidence = 0;
						m_NaturalHandTrackerData.right.isValidPose = false;
						m_NaturalHandTrackerData.right.confidence = 0;
					}
				}
				else
				{
					if (Log.gpl.Print)
						WARNING("GetHandTrackingData() Natural, hasNaturalTrackerInfo: " + hasNaturalTrackerInfo
						+ ", jointCount: " + m_NaturalTrackerInfo.jointCount
						+ ", handModelTypeBitMask: " + m_NaturalTrackerInfo.handModelTypeBitMask);
					hasNaturalHandTrackerData = false;
				}
				Profiler.EndSample();
			}
			if (tracker == TrackerType.Electronic)
			{
				Profiler.BeginSample("Natural Hand");
				switch (m_TrackerOptions.Electronic.Model)
				{
					case HandModel.WithController:
						if ((m_ElectronicTrackerInfo.handModelTypeBitMask & (ulong)m_TrackerOptions.Electronic.Model) == 0)
							m_TrackerOptions.Electronic.Model = HandModel.WithoutController;
						break;
					case HandModel.WithoutController:
						if ((m_ElectronicTrackerInfo.handModelTypeBitMask & (ulong)m_TrackerOptions.Electronic.Model) == 0)
							m_TrackerOptions.Electronic.Model = HandModel.WithController;
						break;
					default:
						break;
				}

				WVR_Result result = WVR_Result.WVR_Error_InvalidArgument;
				if (hasElectronicTrackerInfo &&
					m_ElectronicTrackerInfo.jointCount != 0 &&
					(m_ElectronicTrackerInfo.handModelTypeBitMask & (ulong)m_TrackerOptions.Electronic.Model) != 0)
				{
					result = Interop.WVR_GetHandTrackingData(
						WVR_HandTrackerType.WVR_HandTrackerType_Electronic,
						(WVR_HandModelType)m_TrackerOptions.Electronic.Model,
						originModel,
						ref m_ElectronicHandTrackerData,
						ref m_ElectronicHandPoseData
					);

					hasElectronicHandTrackerData = result == WVR_Result.WVR_Success ? true : false;
					if (hasElectronicHandTrackerData)
					{
						if (Log.gpl.Print)
						{
							DEBUG("GetHandTrackingData() Electronic"
								+ ", timestamp: " + m_ElectronicHandTrackerData.timestamp
								+ ", left valid? " + m_ElectronicHandTrackerData.left.isValidPose
								+ ", left confidence: " + m_ElectronicHandTrackerData.left.confidence
								+ ", left count: " + m_ElectronicHandTrackerData.left.jointCount
								+ ", right valid? " + m_ElectronicHandTrackerData.right.isValidPose
								+ ", right confidence: " + m_ElectronicHandTrackerData.right.confidence
								+ ", right count: " + m_ElectronicHandTrackerData.right.jointCount);
						}

						hasElectronicHandTrackerData = ExtractHandTrackerData(m_ElectronicHandTrackerData, ref s_ElectronicHandJointsPoseLeft, ref s_ElectronicHandJointsPoseRight);
						if (hasElectronicHandTrackerData)
						{
							m_ElectronicHandJointDataLeft = m_ElectronicHandTrackerData.left;
							m_ElectronicHandJointDataRight = m_ElectronicHandTrackerData.right;
						}
					}
					else
					{
						if (Log.gpl.Print)
							Log.e(LOG_TAG, "GetHandTrackingData() Electronic " + result, true);

						m_ElectronicHandTrackerData.left.isValidPose = false;
						m_ElectronicHandTrackerData.left.confidence = 0;
						m_ElectronicHandTrackerData.right.isValidPose = false;
						m_ElectronicHandTrackerData.right.confidence = 0;
					}
				}
				else
				{
					if (Log.gpl.Print)
						WARNING("GetHandTrackingData() Electronic, hasNaturalTrackerInfo: " + hasNaturalTrackerInfo
						+ ", jointCount: " + m_ElectronicTrackerInfo.jointCount
						+ ", handModelTypeBitMask: " + m_ElectronicTrackerInfo.handModelTypeBitMask);
					hasElectronicHandTrackerData = false;
				}
				Profiler.EndSample();
			}
		}
		#endregion

		#region Obsolete Hand v1 APIs
		[Obsolete("This parameter is deprecated.")]
		public static readonly string HAND_STATIC_GESTURE_LEFT = "HAND_STATIC_GESTURE_LEFT";
		[Obsolete("This parameter is deprecated.")]
		public static readonly string HAND_STATIC_GESTURE_RIGHT = "HAND_STATIC_GESTURE_RIGHT";
		[Obsolete("This parameter is deprecated.")]
		public bool EnableHandTracking { get { return true; } }

		[Obsolete("This enum is deprecated. Please use GestureStatus instead.")]
		public enum HandGestureStatus
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
		[Obsolete("This enum is deprecated.")]
		public enum HandTrackingStatus
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
		[Obsolete("This enum is deprecated.")]
		public enum StaticGestures
		{
			UNKNOWN = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Unknown,
			FIST = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Fist,
			FIVE = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Five,
			OK = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_OK,
			THUMBUP = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_ThumbUp,
			INDEXUP = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_IndexUp,
			INVERSE = 1 << (int)WVR_HandGestureType.WVR_HandGestureType_Inverse,
		}

		[Obsolete("This function is deprecated.")]
		public ulong GetHandGestureLeft()
		{
			ulong gesture_value = 0;
			switch (m_HandGestureLeft)
			{
				case GestureType.Fist:
					gesture_value = (ulong)StaticGestures.FIST;
					break;
				case GestureType.Five:
					gesture_value = (ulong)StaticGestures.FIVE;
					break;
				case GestureType.IndexUp:
					gesture_value = (ulong)StaticGestures.INDEXUP;
					break;
				case GestureType.ThumbUp:
					gesture_value = (ulong)StaticGestures.THUMBUP;
					break;
				case GestureType.OK:
					gesture_value = (ulong)StaticGestures.OK;
					break;
				default:
					break;
			}
			return gesture_value;
		}
		[Obsolete("This function is deprecated.")]
		public ulong GetHandGestureRight()
		{
			ulong gesture_value = 0;
			switch (m_HandGestureRight)
			{
				case GestureType.Fist:
					gesture_value = (ulong)StaticGestures.FIST;
					break;
				case GestureType.Five:
					gesture_value = (ulong)StaticGestures.FIVE;
					break;
				case GestureType.IndexUp:
					gesture_value = (ulong)StaticGestures.INDEXUP;
					break;
				case GestureType.ThumbUp:
					gesture_value = (ulong)StaticGestures.THUMBUP;
					break;
				case GestureType.OK:
					gesture_value = (ulong)StaticGestures.OK;
					break;
				default:
					break;
			}
			return gesture_value;
		}
		[Obsolete("This function is deprecated.")]
		public HandTrackingStatus GetHandTrackingStatus() { return HandTrackingStatus.NoSupport; }
		[Obsolete("This function is deprecated.")]
		public void RestartHandTracking() { }
		[Obsolete("This function is deprecated.")]
		public bool GetHandPoseData(ref WVR_HandPoseData_t data) {
			data = m_NaturalHandPoseData;
			return hasNaturalHandTrackerData;
		}
		#endregion
	}
}
