// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using System.Threading;
using System;
using Wave.Native;
using Wave.Essence.Events;
#if UNITY_EDITOR
using Wave.Essence.Editor;
#endif

namespace Wave.Essence.Eye
{
	[DisallowMultipleComponent]
	public sealed class EyeManager : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Eye.EyeManager";
		void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}
		void INFO(string msg) { Log.i(LOG_TAG, msg, true); }

		private static EyeManager m_Instance = null;
		public static EyeManager Instance { get { return m_Instance; } }

		#region Public declaration
		public const string EYE_TRACKING_STATUS = "EYE_TRACKING_STATUS";
		public enum EyeType
		{
			Combined = 0,
			Right,
			Left
		}
		public enum EyeTrackingStatus
		{
			// Initial, can call Start API in this state.
			NOT_START,
			START_FAILURE,

			// Processing, should NOT call API in this state.
			STARTING,
			STOPING,

			// Running, can call Stop API in this state.
			AVAILABLE,

			// Do nothing.
			UNSUPPORT
		}
		public enum EyeSpace
		{
			Local = WVR_CoordinateSystem.WVR_CoordinateSystem_Local,
			World = WVR_CoordinateSystem.WVR_CoordinateSystem_Global
		}
		#endregion

		#region Customized Settings
		private bool m_EnableEyeTrackingEx = true;
		[Tooltip("Enables or disables the eye tracking.")]
		[SerializeField]
		private bool m_EnableEyeTracking = true;
		public bool EnableEyeTracking { get { return m_EnableEyeTracking; } set { m_EnableEyeTracking = value; } }

		[Tooltip("Eye Tracking in local space or world space.")]
		[SerializeField]
		private EyeSpace m_LocationSpace = EyeSpace.World;
		public EyeSpace LocationSpace { get { return m_LocationSpace; } set { m_LocationSpace = value; } }
		#endregion

		#region MonoBehaviour overrides
		private ulong supportedFeature = 0;
		private void Awake()
		{
			supportedFeature = Interop.WVR_GetSupportedFeatures();
			INFO("Awake() supportedFeature: " + supportedFeature);
			// ToDo: Checks if eye tracking is supported.
			/*if ((supportedFeature & (ulong)WVR_SupportedFeature.WVR_SupportedFeature_EyeTracking) == 0)
			{
				Log.w(LOG_TAG, "WVR_SupportedFeature_HandGesture is not enabled.", true);
				SetEyeTrackingStatus(EyeTrackingStatus.UNSUPPORT);
			}*/

			m_Instance = this;
		}
		private bool mEnabled = false;
		private void OnEnable()
		{
			if (!mEnabled)
			{
				m_EnableEyeTrackingEx = m_EnableEyeTracking;
				if (m_EnableEyeTracking)
					StartEyeTracking();

				mEnabled = true;
			}
		}
		private void OnDisable()
		{
			if (mEnabled)
			{
				mEnabled = false;
			}
		}
		void Update()
		{
			if (m_EnableEyeTrackingEx != m_EnableEyeTracking)
			{
				m_EnableEyeTrackingEx = m_EnableEyeTracking;
				if (m_EnableEyeTracking)
				{
					DEBUG("Update() Start eye tracking.");
					StartEyeTracking();
				}
				if (!m_EnableEyeTracking)
				{
					DEBUG("Update() Stop eye tracking.");
					StopEyeTracking();
				}
			}

			if (m_EnableEyeTracking)
				GetEyeTrackingData();
		}
		#endregion

		#region Eye Tracking Lifecycle
		private EyeTrackingStatus m_EyeTrackingStatus = EyeTrackingStatus.NOT_START;
		private static ReaderWriterLockSlim m_EyeTrackingStatusRWLock = new ReaderWriterLockSlim();
		private void SetEyeTrackingStatus(EyeTrackingStatus status)
		{
			try
			{
				m_EyeTrackingStatusRWLock.TryEnterWriteLock(2000);
				m_EyeTrackingStatus = status;
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "SetEyeTrackingStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				m_EyeTrackingStatusRWLock.ExitWriteLock();
			}
		}

		private bool CanStartEyeTracking()
		{
			EyeTrackingStatus status = GetEyeTrackingStatus();
			if (!m_EnableEyeTracking ||
				status == EyeTrackingStatus.AVAILABLE ||
				status == EyeTrackingStatus.STARTING ||
				status == EyeTrackingStatus.STOPING ||
				status == EyeTrackingStatus.UNSUPPORT)
			{
				return false;
			}

			return true;
		}
		private bool CanStopEyeTracking()
		{
			EyeTrackingStatus status = GetEyeTrackingStatus();
			if (!m_EnableEyeTracking && status == EyeTrackingStatus.AVAILABLE)
				return true;
			return false;
		}

		public delegate void EyeTrackingResultDelegate(object sender, bool result);
		private object m_EyeTrackingThreadLock = new object();
		private event EyeTrackingResultDelegate m_EyeTrackingResultCB = null;
		private void StartEyeTrackingLock()
		{
			if (!CanStartEyeTracking())
				return;

			SetEyeTrackingStatus(EyeTrackingStatus.STARTING);
			WVR_Result result = Interop.WVR_StartEyeTracking();
			switch (result)
			{
				case WVR_Result.WVR_Success:
					SetEyeTrackingStatus(EyeTrackingStatus.AVAILABLE);
					break;
				case WVR_Result.WVR_Error_FeatureNotSupport:
					SetEyeTrackingStatus(EyeTrackingStatus.UNSUPPORT);
					break;
				default:
					SetEyeTrackingStatus(EyeTrackingStatus.START_FAILURE);
					break;
			}

			EyeTrackingStatus status = GetEyeTrackingStatus();
			INFO("StartEyeTrackingLock() result: " + result + ", status: " + status);
			GeneralEvent.Send(EYE_TRACKING_STATUS, status);

			if (m_EyeTrackingResultCB != null)
			{
				m_EyeTrackingResultCB(this, result == WVR_Result.WVR_Success ? true : false);
				m_EyeTrackingResultCB = null;
			}
		}
		private void StartEyeTrackingThread()
		{
			lock (m_EyeTrackingThreadLock)
			{
				DEBUG("StartEyeTrackingThread()");
				StartEyeTrackingLock();
			}
		}
		private void StartEyeTracking()
		{
			if (!CanStartEyeTracking())
				return;

			INFO("StartEyeTracking()");
			Thread eye_tracking_t = new Thread(StartEyeTrackingThread);
			eye_tracking_t.Name = "StartEyeTrackingThread";
			eye_tracking_t.Start();
		}

		private void StopEyeTrackingLock()
		{
			if (!CanStopEyeTracking())
				return;

			INFO("StopEyeTrackingLock()");
			SetEyeTrackingStatus(EyeTrackingStatus.STOPING);
			Interop.WVR_StopEyeTracking();
			SetEyeTrackingStatus(EyeTrackingStatus.NOT_START);
			hasEyeTrackingData = false;

			EyeTrackingStatus status = GetEyeTrackingStatus();
			GeneralEvent.Send(EYE_TRACKING_STATUS, status);
		}
		private void StopEyeTrackingThread()
		{
			lock (m_EyeTrackingThreadLock)
			{
				DEBUG("StopEyeTrackingThread()");
				StopEyeTrackingLock();
			}
		}
		private void StopEyeTracking()
		{
			if (!CanStopEyeTracking())
				return;

			INFO("StopEyeTracking()");
			Thread eye_tracking_t = new Thread(StopEyeTrackingThread);
			eye_tracking_t.Name = "StopEyeTrackingThread";
			eye_tracking_t.Start();
		}

		private void RestartEyeTrackingThread()
		{
			lock (m_EyeTrackingThreadLock)
			{
				INFO("RestartEyeTrackingThread()");
				StopEyeTrackingLock();
				StartEyeTrackingLock();
			}
		}
		/// <summary> Restarts the eye tracking service. </summary>
		public void RestartEyeTracking()
		{
			EyeTrackingStatus status = GetEyeTrackingStatus();
			if (status == EyeTrackingStatus.STARTING || status == EyeTrackingStatus.STOPING)
				return;
			Thread eye_tracking_t = new Thread(RestartEyeTrackingThread);
			eye_tracking_t.Name = "RestartEyeTrackingThread";
			eye_tracking_t.Start();
		}
		public void RestartEyeTracking(EyeTrackingResultDelegate callback)
		{
			if (m_EyeTrackingResultCB == null)
				m_EyeTrackingResultCB = callback;
			else
				m_EyeTrackingResultCB += callback;

			RestartEyeTracking();
		}
		#endregion

		#region Eye Tracking Data
		private WVR_EyeTracking_t m_EyeData = new WVR_EyeTracking_t();
		private bool m_CombinedEyeOriginValid = false, m_CombinedEyeDirectionValid = false;
		private Vector3 m_CombinedEyeOrigin = Vector3.zero, m_CombinedEyeDirection = Vector3.zero;

		private bool m_LeftEyeOriginValid = false, m_LeftEyeDirectionValid = false, m_LeftEyeOpennessValid = false, m_LeftEyePupilDiameterValid = false, m_LeftEyePupilPositionInSensorAreaValid = false;
		private Vector3 m_LeftEyeOrigin = Vector3.zero, m_LeftEyeDirection = Vector3.zero;
		private float m_LeftEyeOpenness = 0, m_LeftEyePupilDiameter = 0;
		private Vector2 m_LeftEyePupilPositionInSensorArea = Vector2.zero;

		private bool m_RightEyeOriginValid = false, m_RightEyeDirectionValid = false, m_RightEyeOpennessValid = false, m_RightEyePupilDiameterValid = false, m_RightEyePupilPositionInSensorAreaValid = false;
		private Vector3 m_RightEyeOrigin = Vector3.zero, m_RightEyeDirection = Vector3.zero;
		private float m_RightEyeOpenness = 0, m_RightEyePupilDiameter = 0;
		private Vector2 m_RightEyePupilPositionInSensorArea = Vector2.zero;

		private bool hasEyeTrackingData = false;
		private void GetEyeTrackingData()
		{
			EyeTrackingStatus status = GetEyeTrackingStatus();
			if (status == EyeTrackingStatus.AVAILABLE)
			{
				hasEyeTrackingData = Interop.WVR_GetEyeTracking(ref m_EyeData, (WVR_CoordinateSystem)m_LocationSpace) == WVR_Result.WVR_Success ? true : false;
				if (hasEyeTrackingData)
				{
					/// Combined eye data.
					m_CombinedEyeOriginValid =
						((m_EyeData.combined.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_GazeOriginValid) != 0);
					if (m_CombinedEyeOriginValid)
						Coordinate.GetVectorFromGL(m_EyeData.combined.gazeOrigin, out m_CombinedEyeOrigin);

					m_CombinedEyeDirectionValid = 
						((m_EyeData.combined.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_GazeDirectionNormalizedValid) != 0);
					if (m_CombinedEyeDirectionValid)
						Coordinate.GetVectorFromGL(m_EyeData.combined.gazeDirectionNormalized, out m_CombinedEyeDirection);

					/// Left eye data.
					m_LeftEyeOriginValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_GazeOriginValid) != 0);
					if (m_LeftEyeOriginValid)
						Coordinate.GetVectorFromGL(m_EyeData.left.gazeOrigin, out m_LeftEyeOrigin);

					m_LeftEyeDirectionValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_GazeDirectionNormalizedValid) != 0);
					Coordinate.GetVectorFromGL(m_EyeData.left.gazeDirectionNormalized, out m_LeftEyeDirection);

					m_LeftEyeOpennessValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_EyeOpennessValid) != 0);
					if (m_LeftEyeOpennessValid)
						m_LeftEyeOpenness = m_EyeData.left.eyeOpenness;

					m_LeftEyePupilDiameterValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_PupilDiameterValid) != 0);
					if (m_LeftEyePupilDiameterValid)
						m_LeftEyePupilDiameter = m_EyeData.left.pupilDiameter;

					m_LeftEyePupilPositionInSensorAreaValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_PupilPositionInSensorAreaValid) != 0);
					if (m_LeftEyePupilPositionInSensorAreaValid)
					{
						m_LeftEyePupilPositionInSensorArea.x = m_EyeData.left.pupilPositionInSensorArea.v0;
						m_LeftEyePupilPositionInSensorArea.y = m_EyeData.left.pupilPositionInSensorArea.v1;
					}

					/// Right eye data.
					m_RightEyeOriginValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_GazeOriginValid) != 0);
					if (m_RightEyeOriginValid)
						Coordinate.GetVectorFromGL(m_EyeData.left.gazeOrigin, out m_RightEyeOrigin);

					m_RightEyeDirectionValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_GazeDirectionNormalizedValid) != 0);
					Coordinate.GetVectorFromGL(m_EyeData.left.gazeDirectionNormalized, out m_RightEyeDirection);

					m_RightEyeOpennessValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_EyeOpennessValid) != 0);
					if (m_RightEyeOpennessValid)
						m_RightEyeOpenness = m_EyeData.left.eyeOpenness;

					m_RightEyePupilDiameterValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_PupilDiameterValid) != 0);
					if (m_RightEyePupilDiameterValid)
						m_RightEyePupilDiameter = m_EyeData.left.pupilDiameter;

					m_RightEyePupilPositionInSensorAreaValid =
						((m_EyeData.left.eyeTrackingValidBitMask & (ulong)WVR_EyeTrackingStatus.WVR_PupilPositionInSensorAreaValid) != 0);
					if (m_RightEyePupilPositionInSensorAreaValid)
					{
						m_RightEyePupilPositionInSensorArea.x = m_EyeData.left.pupilPositionInSensorArea.v0;
						m_RightEyePupilPositionInSensorArea.y = m_EyeData.left.pupilPositionInSensorArea.v1;
					}
				}
			}
		}
		#endregion

		#region Public API
		/// <summary> Retrieves current eye tracking service status. </summary>
		public EyeTrackingStatus GetEyeTrackingStatus()
		{
			try
			{
				m_EyeTrackingStatusRWLock.TryEnterReadLock(2000);
				return m_EyeTrackingStatus;
			}
			catch (Exception e)
			{
				Log.e(LOG_TAG, "GetEyeTrackingStatus() " + e.Message, true);
				throw;
			}
			finally
			{
				m_EyeTrackingStatusRWLock.ExitReadLock();
			}
		}
		/// <summary> Checks whether the eye tracking service is available. </summary>
		public bool IsEyeTrackingAvailable()
		{
			EyeTrackingStatus status = GetEyeTrackingStatus();
			if (status == EyeTrackingStatus.AVAILABLE && hasEyeTrackingData)
				return true;

			return false;
		}

		// ------------------------- Combined Eye -------------------------
		/// <summary> Retrieves the origin location of combined eye. </summary>
		public bool GetCombinedEyeOrigin(out Vector3 origin)
		{
			if (!IsEyeTrackingAvailable())
			{
				origin = Vector3.zero;
				return false;
			}
			origin = m_CombinedEyeOrigin;
			return m_CombinedEyeOriginValid;
		}
		/// <summary> Retrieves the looking direction (z-normalized) of combined eye. </summary>
		public bool GetCombindedEyeDirectionNormalized(out Vector3 direction)
		{
			if (!IsEyeTrackingAvailable())
			{
				direction = Vector3.zero;
				return false;
			}
			direction = m_CombinedEyeDirection;
			return m_CombinedEyeDirectionValid;
		}

		// ------------------------- Left Eye -------------------------
		/// <summary> Retrieves the origin location of left eye. </summary>
		public bool GetLeftEyeOrigin(out Vector3 origin)
		{
			if (!IsEyeTrackingAvailable())
			{
				origin = Vector3.zero;
				return false;
			}
			origin = m_LeftEyeOrigin;
			return m_LeftEyeOriginValid;
		}
		/// <summary> Retrieves the looking direction (z-normalized) of left eye. </summary>
		public bool GetLeftEyeDirectionNormalized(out Vector3 direction)
		{
			if (!IsEyeTrackingAvailable())
			{
				direction = Vector3.zero;
				return false;
			}
			direction = m_LeftEyeDirection;
			return m_LeftEyeDirectionValid;
		}
		/// <summary> Retrieves the value representing how open the left eye is. </summary>
		public bool GetLeftEyeOpenness(out float openness)
		{
			if (!IsEyeTrackingAvailable())
			{
				openness = 0;
				return false;
			}
			openness = m_LeftEyeOpenness;
			return m_LeftEyeOpennessValid;
		}
		/// <summary> Retrieves the diameter of left eye pupil in millimeters. </summary>
		public bool GetLeftEyePupilDiameter(out float diameter)
		{
			if (!IsEyeTrackingAvailable())
			{
				diameter = 0;
				return false;
			}
			diameter = m_LeftEyePupilDiameter;
			return m_LeftEyePupilDiameterValid;
		}
		/// <summary> Retrieves the normalized position of left eye pupil in [0,1]. </summary>
		public bool GetLeftEyePupilPositionInSensorArea(out Vector2 area)
		{
			if (!IsEyeTrackingAvailable())
			{
				area = Vector2.zero;
				return false;
			}
			area = m_LeftEyePupilPositionInSensorArea;
			return m_LeftEyePupilPositionInSensorAreaValid;
		}

		// ------------------------- Right Eye -------------------------
		/// <summary> Retrieves the origin location of right eye. </summary>
		public bool GetRightEyeOrigin(out Vector3 origin)
		{
			if (!IsEyeTrackingAvailable())
			{
				origin = Vector3.zero;
				return false;
			}
			origin = m_RightEyeOrigin;
			return m_RightEyeOriginValid;
		}
		/// <summary> Retrieves the looking direction (z-normalized) of right eye. </summary>
		public bool GetRightEyeDirectionNormalized(out Vector3 direction)
		{
			if (!IsEyeTrackingAvailable())
			{
				direction = Vector3.zero;
				return false;
			}
			direction = m_RightEyeDirection;
			return m_RightEyeDirectionValid;
		}
		/// <summary> Retrieves the value representing how open the right eye is. </summary>
		public bool GetRightEyeOpenness(out float openness)
		{
			if (!IsEyeTrackingAvailable())
			{
				openness = 0;
				return false;
			}
			openness = m_RightEyeOpenness;
			return m_RightEyeOpennessValid;
		}
		/// <summary> Retrieves the diameter of right eye pupil in millimeters. </summary>
		public bool GetRightEyePupilDiameter(out float diameter)
		{
			if (!IsEyeTrackingAvailable())
			{
				diameter = 0;
				return false;
			}
			diameter = m_RightEyePupilDiameter;
			return m_RightEyePupilDiameterValid;
		}
		/// <summary> Retrieves the normalized position of right eye pupil in [0,1]. </summary>
		public bool GetRightEyePupilPositionInSensorArea(out Vector2 area)
		{
			if (!IsEyeTrackingAvailable())
			{
				area = Vector2.zero;
				return false;
			}
			area = m_RightEyePupilPositionInSensorArea;
			return m_RightEyePupilPositionInSensorAreaValid;
		}
		#endregion
	}
}
