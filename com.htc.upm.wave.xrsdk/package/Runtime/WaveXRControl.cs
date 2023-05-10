// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Wave.OpenXR
{
	public static class InputDeviceControl
	{
		const string LOG_TAG = "Wave.OpenXR.InputDeviceControl";
		static void DEBUG(string msg)
		{
			Debug.Log(LOG_TAG + " " + msg);
		}

		/// <summary> Wave Head Mounted Device Characteristics </summary>
		public const InputDeviceCharacteristics kHMDCharacteristics = (
			InputDeviceCharacteristics.HeadMounted |
			InputDeviceCharacteristics.Camera |
			InputDeviceCharacteristics.TrackedDevice
		);
		/// <summary> Wave Left Controller Characteristics </summary>
		public const InputDeviceCharacteristics kControllerLeftCharacteristics = (
			InputDeviceCharacteristics.Left |
			InputDeviceCharacteristics.TrackedDevice |
			InputDeviceCharacteristics.Controller |
			InputDeviceCharacteristics.HeldInHand
		);
		/// <summary> Wave Right Controller Characteristics </summary>
		public const InputDeviceCharacteristics kControllerRightCharacteristics = (
			InputDeviceCharacteristics.Right |
			InputDeviceCharacteristics.TrackedDevice |
			InputDeviceCharacteristics.Controller |
			InputDeviceCharacteristics.HeldInHand
		);

		internal static List<InputDevice> m_InputDevices = new List<InputDevice>();

		/// Tracking state
		public static bool IsConnected(InputDeviceCharacteristics device)
		{
			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				if (id.characteristics.Equals(device))
					return true;
			}

			return false;
		}
		public static bool IsTracked(InputDeviceCharacteristics device)
		{
			bool isTracked = false;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(CommonUsages.isTracked, out bool value))
						isTracked = value;
				}
			}

			return isTracked;
		}

		/// Button
		public static bool KeyDown(InputDeviceCharacteristics device, InputFeatureUsage<bool> button)
		{
			bool isDown = false;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(button, out bool value))
						isDown = value;
				}
			}

			return isDown;
		}
		public static bool KeyAxis1D(InputDeviceCharacteristics device, InputFeatureUsage<float> button, out float axis1d)
		{
			axis1d = 0;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(button, out float value))
					{
						axis1d = value;
						return true;
					}
				}
			}

			return false;
		}
		public static bool KeyAxis2D(InputDeviceCharacteristics device, InputFeatureUsage<Vector2> button, out Vector2 axis2d)
		{
			axis2d = Vector2.zero;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(button, out Vector2 value))
					{
						axis2d = value;
						return true;
					}
				}
			}

			return false;
		}

		/// Haptic
		static readonly HapticCapabilities emptyHapticCapabilities = new HapticCapabilities();
		public static bool TryGetHapticCapabilities(InputDeviceCharacteristics device, out HapticCapabilities hapticCaps)
		{
			hapticCaps = emptyHapticCapabilities;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetHapticCapabilities(out HapticCapabilities value))
					{
						hapticCaps = value;
						return true;
					}
				}
			}

			return false;
		}
		public static bool SendHapticImpulse(InputDeviceCharacteristics device, float amplitude, float duration)
		{
			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetHapticCapabilities(out HapticCapabilities value))
					{
						if (value.supportsImpulse)
						{
							amplitude = Mathf.Clamp(amplitude, 0, 1);
							return id.SendHapticImpulse(0, amplitude, duration);
						}
					}
				}
			}

			return false;
		}

		/// Pose
		public static bool GetPosition(InputDeviceCharacteristics device, out Vector3 position)
		{
			position = Vector3.zero;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(CommonUsages.isTracked, out bool tracked))
					{
						if (id.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 value))
						{
							position = value;
							return true;
						}
					}
				}
			}

			return false;
		}
		public static bool GetRotation(InputDeviceCharacteristics device, out Quaternion rotation)
		{
			rotation = Quaternion.identity;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(CommonUsages.isTracked, out bool tracked))
					{
						if (id.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion value))
						{
							rotation = value;
							return true;
						}
					}
				}
			}

			return false;
		}

		/// Battery
		public static float GetBatteryLevel(InputDeviceCharacteristics device)
		{
			float level = 0;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(device))
				{
					if (id.TryGetFeatureValue(CommonUsages.batteryLevel, out float value))
						level = value;
				}
			}

			return level;
		}

		public static bool IsUserPresence()
		{
			bool userPresence = false;

			InputDevices.GetDevices(m_InputDevices);
			foreach (InputDevice id in m_InputDevices)
			{
				// The device is connected.
				if (id.characteristics.Equals(kHMDCharacteristics))
				{
					if (id.TryGetFeatureValue(CommonUsages.userPresence, out bool value))
						userPresence = value;
				}
			}

			return userPresence;
		}
	}
}