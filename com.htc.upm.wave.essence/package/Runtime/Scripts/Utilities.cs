// "Wave SDK
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;
using Wave.Native;
using Wave.XR;
using Wave.XR.Function;
using System.Collections.Generic;
using UnityEngine.Profiling;

#if UNITY_EDITOR
using Wave.Essence.Editor;
#endif

namespace Wave.Essence
{
	public static class ClientInterface
	{
		const string LOG_TAG = "Wave.Essence.ClientInterface";
		static List<InputDevice> m_Devices = new List<InputDevice>();
		public static void DeviceInfo()
		{
			InputDevices.GetDevices(m_Devices);
			foreach (var dev in m_Devices)
			{
				bool connected = false;
				if (dev.TryGetFeatureValue(UnityEngine.XR.CommonUsages.isTracked, out bool value))
					connected = value;
				Log.d(LOG_TAG, "DeviceInfo() dev: " + dev.name + ", valid? " + dev.isValid + ", connected? " + connected, true);
			}
		}

		public static bool SetOrigin(WVR_PoseOriginModel wvrOrigin)
		{
			TrackingOriginModeFlags originMode = TrackingOriginModeFlags.Unknown;
			if (wvrOrigin == WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnGround)
				originMode = TrackingOriginModeFlags.Floor;
			if (wvrOrigin == WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnHead)
				originMode = TrackingOriginModeFlags.Device;
			if (wvrOrigin == WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnTrackingObserver)
				originMode = TrackingOriginModeFlags.TrackingReference;

			if (originMode == TrackingOriginModeFlags.Unknown)
				return false;

			return Utils.InputSubsystem.TrySetTrackingOriginMode(originMode);
		}
		public static bool GetOrigin(ref WVR_PoseOriginModel wvrOrigin)
		{
#if UNITY_EDITOR
			if (Application.isEditor)
			{
				wvrOrigin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnHead;
				return true;
			}
			else
#endif
			{
				return GetOrigin(Utils.InputSubsystem.GetTrackingOriginMode(), ref wvrOrigin);
			}
		}
		public static bool GetOrigin(in TrackingOriginModeFlags xrOrigin, ref WVR_PoseOriginModel wvrOrigin)
		{
			bool result = true;
			switch (xrOrigin)
			{
				case TrackingOriginModeFlags.Device:
					wvrOrigin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnHead;
					break;
				case TrackingOriginModeFlags.Floor:
					wvrOrigin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnGround;
					break;
				case TrackingOriginModeFlags.TrackingReference:
					wvrOrigin = WVR_PoseOriginModel.WVR_PoseOriginModel_OriginOnTrackingObserver;
					break;
				default:
					result = false;
					break;
			}

			return result;
		}

		public static string GetCurrentRenderModelName(WVR_DeviceType type)
		{
			Profiler.BeginSample("GetCurrentRenderModelName");
			string parameterName = "GetRenderModelName";
			IntPtr ptrParameterName = Marshal.StringToHGlobalAnsi(parameterName);
			IntPtr ptrResult = Marshal.AllocHGlobal(64);
			uint resultVertLength = 64;
			string tmprenderModelName = "";
			uint retOfRenderModel = Interop.WVR_GetParameters(type, ptrParameterName, ptrResult, resultVertLength);

			if (retOfRenderModel > 0)
				tmprenderModelName = Marshal.PtrToStringAnsi(ptrResult);

			Log.d(LOG_TAG, "Type: " + type + ", current render model name: " + tmprenderModelName);

			Marshal.FreeHGlobal(ptrParameterName);
			Marshal.FreeHGlobal(ptrResult);
			Profiler.EndSample();
			return tmprenderModelName;
		}

		static int m_ForegroundFrameCount = 0;
		private static bool m_IsFocused = false;
		/// <summary> Means the system focus is captured by current scene or not. </summary>
		public static bool IsFocused
		{
			get
			{
				if (m_ForegroundFrameCount != Time.frameCount)
				{
					m_ForegroundFrameCount = Time.frameCount;
					bool focused = (Interop.WVR_IsInputFocusCapturedBySystem() ? false : true);
					if (m_IsFocused != focused)
					{
						m_IsFocused = focused;
						Log.d(LOG_TAG, "IsFocused() " + m_IsFocused, true);
					}
				}
				return m_IsFocused;
			}
		}

		public static XR_InteractionMode InteractionMode
		{
			get { return WaveEssence.Instance.GetInteractionMode(); }
			set { }
		}
	} // class ClientInterface

	public static class Numeric
	{
		public static bool IsBoolean(string value)
		{
			try
			{
				bool i = Convert.ToBoolean(value);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool IsFloat(string value)
		{
			try
			{
				float i = Convert.ToSingle(value);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool IsNumeric(string value)
		{
			try
			{
				int i = Convert.ToInt32(value);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	} // class Numeric

	public class RenderFunctions
	{
		public delegate void SetPoseUsedOnSubmitDelegate([In, Out] WVR_PoseState_t[] poseState);
		private static SetPoseUsedOnSubmitDelegate setPoseUsedOnSubmit = null;
		public static SetPoseUsedOnSubmitDelegate SetPoseUsedOnSubmit
		{
			get
			{
				if (setPoseUsedOnSubmit == null)
					setPoseUsedOnSubmit = FunctionsHelper.GetFuncPtr<SetPoseUsedOnSubmitDelegate>("SetPoseUsedOnSubmit");
				return setPoseUsedOnSubmit;
			}
		}

		public delegate void NotifyQualityLevelChangeDelegate();
		private static NotifyQualityLevelChangeDelegate notifyQualityLevelChange = null;
		public static NotifyQualityLevelChangeDelegate NotifyQualityLevelChange
		{
			get
			{
				if (notifyQualityLevelChange == null)
					notifyQualityLevelChange = FunctionsHelper.GetFuncPtr<NotifyQualityLevelChangeDelegate>("NotifyQualityLevelChange");
				return notifyQualityLevelChange;
			}
		}
	} // class RenderFunctions

	public static class WXRDevice
	{
		public static bool IsLeftHanded {
			get {
				return WaveEssence.Instance.IsLeftHanded;
			}
		}

		public static bool IsConnected(WVR_DeviceType deviceType)
		{
			return WaveEssence.Instance.IsConnected(deviceType);
		}
		/// <summary> Wave Head Mounted Device Characteristics </summary>
		const InputDeviceCharacteristics kHMDCharacteristics = (
			InputDeviceCharacteristics.HeadMounted |
			InputDeviceCharacteristics.Camera |
			InputDeviceCharacteristics.TrackedDevice
		);
		/// <summary> Wave Left Controller Characteristics </summary>
		const InputDeviceCharacteristics kControllerLeftCharacteristics = (
			InputDeviceCharacteristics.Left |
			InputDeviceCharacteristics.TrackedDevice |
			InputDeviceCharacteristics.Controller |
			InputDeviceCharacteristics.HeldInHand
		);
		/// <summary> Wave Right Controller Characteristics </summary>
		const InputDeviceCharacteristics kControllerRightCharacteristics = (
			InputDeviceCharacteristics.Right |
			InputDeviceCharacteristics.TrackedDevice |
			InputDeviceCharacteristics.Controller |
			InputDeviceCharacteristics.HeldInHand
		);
		public static bool IsConnected(XR_Device device, bool adaptiveHanded = false)
		{
			bool connected = false;
#if UNITY_EDITOR
			if (Application.isEditor)
				return true;
			else
#endif
			{
				var inputDevices = new List<InputDevice>();
				InputDevices.GetDevices(inputDevices);

				foreach (InputDevice id in inputDevices)
				{
					if ((device == XR_Device.Head && id.characteristics.Equals(kHMDCharacteristics)) ||
						(device == XR_Device.Right && id.characteristics.Equals(kControllerRightCharacteristics)) ||
						(device == XR_Device.Left && id.characteristics.Equals(kControllerLeftCharacteristics)))
					{
						connected = true;
						break;
					}
				}
			}

			return connected;
		}

		public static WVR_DeviceType GetRoleType(this XR_Device device, bool adaptiveHanded = false)
		{
			switch (device)
			{
				case XR_Device.Head:
					return WVR_DeviceType.WVR_DeviceType_HMD;
				case XR_Device.Dominant:
					if (adaptiveHanded)
						return IsLeftHanded ? WVR_DeviceType.WVR_DeviceType_Controller_Left : WVR_DeviceType.WVR_DeviceType_Controller_Right;
					else
						return WVR_DeviceType.WVR_DeviceType_Controller_Right;
				case XR_Device.NonDominant:
					if (adaptiveHanded)
						return IsLeftHanded ? WVR_DeviceType.WVR_DeviceType_Controller_Right : WVR_DeviceType.WVR_DeviceType_Controller_Left;
					else
						return WVR_DeviceType.WVR_DeviceType_Controller_Left;
				default:
					break;
			}

			return WVR_DeviceType.WVR_DeviceType_Invalid;
		}

		public static InputDevice GetRoleDevice(XR_Device device, bool adaptiveHanded = false)
		{
			if (device == XR_Device.Dominant)
			{
				if (adaptiveHanded)
					return IsLeftHanded ? InputDevices.GetDeviceAtXRNode(XRNode.LeftHand) : InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
				else
					return InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
			}
			if (device == XR_Device.NonDominant)
			{
				if (adaptiveHanded)
					return IsLeftHanded ? InputDevices.GetDeviceAtXRNode(XRNode.RightHand) : InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
				else
					return InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
			}

			return InputDevices.GetDeviceAtXRNode(XRNode.Head);
		}
		static WVR_DeviceType GetRoleDevice(WVR_DeviceType device, bool adaptiveHanded = false)
		{
			if (adaptiveHanded && IsLeftHanded)
			{
				if (device == WVR_DeviceType.WVR_DeviceType_Controller_Right)
					return WVR_DeviceType.WVR_DeviceType_Controller_Left;
				if (device == WVR_DeviceType.WVR_DeviceType_Controller_Left)
					return WVR_DeviceType.WVR_DeviceType_Controller_Right;

			}
			return device;
		}

		#region Unity XR Buttons
		public static bool KeyDown(XR_Device device, InputFeatureUsage<bool> button, bool adaptiveHanded = false)
		{
			bool isDown = false;
			InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
			if (input_device.isValid)
			{
				if (input_device.TryGetFeatureValue(button, out bool value))
					isDown = value;
			}

			return isDown;
		}
		public static float KeyAxis1D(XR_Device device, InputFeatureUsage<float> button, bool adaptiveHanded = false)
		{
			float axis = 0;

			if (KeyAxis1D(device, button, out float value)) { axis = value; }

			return axis;
		}
		public static bool KeyAxis1D(XR_Device device, InputFeatureUsage<float> button, out float axis1d, bool adaptiveHanded = false)
		{
			InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
			if (input_device.isValid)
			{
				if (input_device.TryGetFeatureValue(button, out float value))
				{
					axis1d = value;
					return true;
				}
			}

			axis1d = 0;
			return false;
		}
		public static Vector2 KeyAxis2D(XR_Device device, InputFeatureUsage<Vector2> button, bool adaptiveHanded = false)
		{
			Vector2 axis = Vector2.zero;

			if (KeyAxis2D(device, button, out Vector2 value)) { axis = value; }

			return axis;
		}
		public static bool KeyAxis2D(XR_Device device, InputFeatureUsage<Vector2> button, out Vector2 axis2d, bool adaptiveHanded = false)
		{
			InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
			if (input_device.isValid)
			{
				if (input_device.TryGetFeatureValue(button, out Vector2 value))
				{
					axis2d = value;
					return true;
				}
			}

			axis2d = Vector2.zero;
			return false;
		}
		#endregion
		#region Wave Buttons
		public static bool ButtonPress(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonPress(adaptive_device, id);
		}
		public static bool ButtonHold(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonHold(adaptive_device, id);
		}
		public static bool ButtonRelease(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonRelease(adaptive_device, id);
		}
		public static bool ButtonTouch(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonTouch(adaptive_device, id);
		}
		public static bool ButtonTouching(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonTouching(adaptive_device, id);
		}
		public static bool ButtonUntouch(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonUntouch(adaptive_device, id);
		}
		public static Vector2 ButtonAxis(WVR_DeviceType device, WVR_InputId id, bool adaptiveHanded = false)
		{
			WVR_DeviceType adaptive_device = GetRoleDevice(device);
			return WaveEssence.Instance.ButtonAxis(adaptive_device, id);
		}
		#endregion

		#region XR Device Vibration
		static readonly HapticCapabilities emptyHapticCapabilities = new HapticCapabilities();
		public static bool TryGetHapticCapabilities(XR_Device device, out HapticCapabilities hapticCaps, bool adaptiveHanded = false)
		{
			hapticCaps = emptyHapticCapabilities;

			InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
			if (input_device.isValid)
				return input_device.TryGetHapticCapabilities(out hapticCaps);

			return false;
		}

		static HapticCapabilities m_HapticCaps = new HapticCapabilities();
		public static bool SendHapticImpulse(XR_Device device, float amplitude, float duration, bool adaptiveHanded = false)
		{
			if (TryGetHapticCapabilities(device, out m_HapticCaps, adaptiveHanded))
			{
				if (m_HapticCaps.supportsImpulse)
				{
					InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
					if (input_device.isValid)
					{
						amplitude = Mathf.Clamp(amplitude, 0, 1);
						return input_device.SendHapticImpulse(0, amplitude, duration);
					}
				}
			}

			return false;
		}
		#endregion

		public static bool IsTracked(XR_Device device, bool adaptiveHanded = false)
		{
#if UNITY_EDITOR
			if (Application.isEditor)
				return true;
			else
#endif
			{
				bool tracked = false;

				InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
				if (input_device.isValid)
				{
					if (input_device.TryGetFeatureValue(XR_Feature.ValidPose, out bool value))
						tracked = value;
				}

				return tracked;
			}
		}

		#region Controller Pose Mode
		static int[,] positionFrame = new int[Enum.GetNames(typeof(WVR_DeviceType)).Length, Enum.GetNames(typeof(WVR_ControllerPoseMode)).Length];
		static bool AllowGetPosition(XR_Hand hand, XR_ControllerPoseMode mode)
		{
			if (Time.frameCount != positionFrame[(int)hand, (int)mode])
			{
				positionFrame[(int)hand, (int)mode] = Time.frameCount;
				return true;
			}
			return false;
		}
		static int[,] rotationFrame = new int[Enum.GetNames(typeof(WVR_DeviceType)).Length, Enum.GetNames(typeof(WVR_ControllerPoseMode)).Length];
		static bool AllowGetRotation(XR_Hand hand, XR_ControllerPoseMode mode)
		{
			if (Time.frameCount != rotationFrame[(int)hand, (int)mode])
			{
				rotationFrame[(int)hand, (int)mode] = Time.frameCount;
				return true;
			}
			return false;
		}

		static Vector3 triggerPos_L = Vector3.zero, panelPos_L = Vector3.zero, handlePos_L = Vector3.zero;
		static Quaternion triggerRot_L = Quaternion.identity, panelRot_L = Quaternion.identity, handleRot_L = Quaternion.identity;
		static Vector3 triggerPos_R = Vector3.zero, panelPos_R = Vector3.zero, handlePos_R = Vector3.zero;
		static Quaternion triggerRot_R = Quaternion.identity, panelRot_R = Quaternion.identity, handleRot_R = Quaternion.identity;
		public static bool GetControllerPosition(XR_Hand hand, XR_ControllerPoseMode mode, ref Vector3 position, bool adaptiveHanded = false)
		{
			if (!IsTracked((XR_Device)hand, adaptiveHanded))
				return false;

			Vector3 pos = Vector3.zero;
#if UNITY_EDITOR
			if (Application.isEditor)
			{
				pos = DummyPose.GetPosition(GetRoleDevice((WVR_DeviceType)hand));
			} else
#endif
			{
				InputDevice input_device = GetRoleDevice((XR_Device)hand, adaptiveHanded);
				if (!input_device.isValid)
					return false;

				if (input_device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 value))
				{
					pos = value;
				}
			}

			Vector3 offset = WaveEssence.Instance.GetControllerPositionOffset(GetRoleDevice((WVR_DeviceType)hand), (WVR_ControllerPoseMode)mode);

			if (AllowGetPosition(hand, mode))
			{
				position = pos + offset;
				if (hand == XR_Hand.Dominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						triggerPos_R = position;
					if (mode == XR_ControllerPoseMode.Panel)
						panelPos_R = position;
					if (mode == XR_ControllerPoseMode.Handle)
						handlePos_R = position;
				}
				if (hand == XR_Hand.NonDominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						triggerPos_L = position;
					if (mode == XR_ControllerPoseMode.Panel)
						panelPos_L = position;
					if (mode == XR_ControllerPoseMode.Handle)
						handlePos_L = position;
				}
			}
			else
			{
				if (hand == XR_Hand.Dominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						position = triggerPos_R;
					if (mode == XR_ControllerPoseMode.Panel)
						position = panelPos_R;
					if (mode == XR_ControllerPoseMode.Handle)
						position = handlePos_R;
				}
				if (hand == XR_Hand.NonDominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						position = triggerPos_L;
					if (mode == XR_ControllerPoseMode.Panel)
						position = panelPos_L;
					if (mode == XR_ControllerPoseMode.Handle)
						position = handlePos_L;
				}
			}

			return true;
		}
		public static bool GetControllerRotation(XR_Hand hand, XR_ControllerPoseMode mode, ref Quaternion rotation, bool adaptiveHanded = false)
		{
			if (!IsTracked((XR_Device)hand, adaptiveHanded))
				return false;

			Quaternion rot = Quaternion.identity;

#if UNITY_EDITOR
			if (Application.isEditor)
			{
				rot = DummyPose.GetRotation(GetRoleDevice((WVR_DeviceType)hand));
			}
			else
#endif
			{
				InputDevice input_device = GetRoleDevice((XR_Device)hand, adaptiveHanded);
				if (!input_device.isValid)
					return false;

				if (input_device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion value))
				{
					rot = value;
				}
			}

			Quaternion offset = WaveEssence.Instance.GetControllerRotationOffset(GetRoleDevice((WVR_DeviceType)hand), (WVR_ControllerPoseMode)mode);

			if (AllowGetRotation(hand, mode))
			{
				rotation = rot * offset;
				if (hand == XR_Hand.Dominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						triggerRot_R = rotation;
					if (mode == XR_ControllerPoseMode.Panel)
						panelRot_R = rotation;
					if (mode == XR_ControllerPoseMode.Handle)
						handleRot_R = rotation;
				}
				if (hand == XR_Hand.NonDominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						triggerRot_L = rotation;
					if (mode == XR_ControllerPoseMode.Panel)
						panelRot_L = rotation;
					if (mode == XR_ControllerPoseMode.Handle)
						handleRot_L = rotation;
				}
			}
			else
			{
				if (hand == XR_Hand.Dominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						rotation = triggerRot_R;
					if (mode == XR_ControllerPoseMode.Panel)
						rotation = panelRot_R;
					if (mode == XR_ControllerPoseMode.Handle)
						rotation = handleRot_R;
				}
				if (hand == XR_Hand.NonDominant)
				{
					if (mode == XR_ControllerPoseMode.Trigger)
						rotation = triggerRot_L;
					if (mode == XR_ControllerPoseMode.Panel)
						rotation = panelRot_L;
					if (mode == XR_ControllerPoseMode.Handle)
						rotation = handleRot_L;
				}
			}

			return true;
		}
#endregion

		/// <summary>
		/// Retrieves a device's battery life with a valid value between 0~1 where 1 means full capacity or an invalid value of -1.
		/// </summary>
		public static float GetBatteryLevel(XR_Device device, bool adaptiveHanded = false)
		{
			float level = 0;

			InputDevice input_device = GetRoleDevice(device, adaptiveHanded);
			if (input_device.isValid)
			{
				if (input_device.TryGetFeatureValue(XR_Feature.batteryLevel, out float value))
				{
					level = value;
				}
			}

			return level;
		}

		/// <summary>
		/// When user wears the head mounted device, CommonUsages.userPresence is true.
		/// </summary>
		public static bool IsUserPresence()
		{
			bool userPresence = false;

			InputDevice input_device = GetRoleDevice(XR_Device.Head);
			if (input_device.isValid)
			{
				if (input_device.TryGetFeatureValue(XR_Feature.userPresence, out bool value))
				{
					userPresence = value;
				}
			}
			return userPresence;
		}
	} // class WXRDevice

	[Obsolete("Deprecated.")]
	public static class ApplicationScene
	{
		public static bool IsFocused { get { return ClientInterface.IsFocused; } }
	}
}
