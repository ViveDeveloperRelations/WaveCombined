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
using Wave.XR.Settings;

namespace Wave.OpenXR
{
	public static class InputDeviceHand
	{
		#region Wave XR Interface
		public static void ActivateNaturalHand(bool active)
		{
			WaveXRSettings settings = WaveXRSettings.GetInstance();
			if (settings != null && settings.EnableNaturalHand != active)
			{
				settings.EnableNaturalHand = active;
				Debug.Log("InputDeviceHand::ActivateNaturalHand() " + (settings.EnableNaturalHand ? "Activate." : "Deactivate."));
				SettingsHelper.SetBool(WaveXRSettings.EnableNaturalHandText, settings.EnableNaturalHand);
			}
		}
		public static void ActivateElectronicHand(bool active)
		{
			WaveXRSettings settings = WaveXRSettings.GetInstance();
			if (settings != null && settings.EnableElectronicHand != active)
			{
				settings.EnableElectronicHand = active;
				Debug.Log("InputDeviceHand::ActivateElectronicHand() " + (settings.EnableElectronicHand ? "Activate." : "Deactivate."));
				SettingsHelper.SetBool(WaveXRSettings.EnableElectronicHandText, settings.EnableElectronicHand);
			}
		}
		#endregion

		#region Unity XR Hand definitions
		public const string kLeftHandName = "Wave Left Hand";
		public const string kRightHandName = "Wave Right Hand";
		public const string kLeftHandSN = "HTC-211116-LeftHand";
		public const string kRightHandSN = "HTC-211116-RightHand";
		/// <summary> Right Tracker Characteristics </summary>
		public const InputDeviceCharacteristics kRightHandCharacteristics = (
			InputDeviceCharacteristics.HandTracking |
			InputDeviceCharacteristics.Right
		);
		/// <summary> Left Tracker Characteristics </summary>
		public const InputDeviceCharacteristics kLeftHandCharacteristics = (
			InputDeviceCharacteristics.HandTracking |
			InputDeviceCharacteristics.Left
		);

		public const int kUnityXRFingerCount = 5;

		/**
		 * UnityEngine.XR Enumeration describing the AR rendering mode used with XR.Hand.
		 * public enum HandFinger
		 * {
		 *   Thumb = 0,
		 *   Index = 1,
		 *   Middle = 2,
		 *   Ring = 3,
		 *   Pinky = 4
		 * }
		 **/
		public const int kUnityXRMaxFingerBoneCount = 5;
		#endregion

		public static string GetName(bool isLeft)
		{
			return (isLeft ? kLeftHandName : kRightHandName);
		}
		public static string GetSerialNumber(bool isLeft)
		{
			return (isLeft ? kLeftHandSN : kRightHandSN);
		}
		public static InputDeviceCharacteristics GetCharacteristic(bool isLeft)
		{
			return (isLeft ? kLeftHandCharacteristics : kRightHandCharacteristics);
		}

		public static bool IsHandDevice(InputDevice input, bool isLeft)
		{
			if (input.name.Equals(GetName(isLeft)) &&
				input.serialNumber.Equals(GetSerialNumber(isLeft)) &&
				input.characteristics.Equals(GetCharacteristic(isLeft))
				)
			{
				return true;
			}

			return false;
		}

		public static bool IsAvailable()
		{
			InputDevices.GetDevices(s_InputDevices);
			for (int i = 0; i < s_InputDevices.Count; i++)
			{
				if (!s_InputDevices[i].isValid) { continue; }

				if (IsHandDevice(s_InputDevices[i], true) || IsHandDevice(s_InputDevices[i], false))
				{
					return true;
				}
			}
			return false;
		}
		public static bool IsAvailable(bool isLeft)
		{
			InputDevices.GetDevices(s_InputDevices);
			for (int i = 0; i < s_InputDevices.Count; i++)
			{
				if (!s_InputDevices[i].isValid) { continue; }

				if (IsHandDevice(s_InputDevices[i], isLeft)) { return true; }
			}
			return false;
		}

		internal static List<InputDevice> s_InputDevices = new List<InputDevice>();
		public static bool IsTracked(bool isLeft)
		{
			InputDevices.GetDevices(s_InputDevices);
			for (int i = 0; i < s_InputDevices.Count; i++)
			{
				if (!s_InputDevices[i].isValid) { continue; }

				if (!IsHandDevice(s_InputDevices[i], isLeft)) { continue; }

				if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.isTracked, out bool isTracked))
				{
					return isTracked;
				}
			}
			return false;
		}

		internal static Bone m_Palm, m_Wrist;
		public static Bone GetPalm(bool isLeft)
		{
			InputDevices.GetDevices(s_InputDevices);
			for (int i = 0; i < s_InputDevices.Count; i++)
			{
				if (!s_InputDevices[i].isValid) { continue; }

				if (!IsHandDevice(s_InputDevices[i], isLeft)) { continue; }

				if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.handData, out Hand value))
				{
					if (value.TryGetRootBone(out Bone bone))
					{
						m_Palm = bone;
					}
				}
			}
			return m_Palm;
		}
		public static Bone GetWrist(bool isLeft)
		{
			if (GetPalm(isLeft).TryGetParentBone(out Bone value))
				m_Wrist = value;

			return m_Wrist;
		}

		internal static Dictionary<HandFinger, List<Bone>> s_FingerBones = new Dictionary<HandFinger, List<Bone>>()
		{
			{ HandFinger.Thumb, new List<Bone>() },
			{ HandFinger.Index, new List<Bone>() },
			{ HandFinger.Middle, new List<Bone>() },
			{ HandFinger.Ring, new List<Bone>() },
			{ HandFinger.Pinky, new List<Bone>() }
		};
		/// <summary>
		/// Retrieves the bone list of a finger.
		/// The list length will be zero if cannot find a finger's bone list.
		/// </summary>
		public static List<Bone> GetFingerBones(bool isLeft, HandFinger finger)
		{
			InputDevices.GetDevices(s_InputDevices);
			for (int i = 0; i < s_InputDevices.Count; i++)
			{
				if (!s_InputDevices[i].isValid) { continue; }

				if (!IsHandDevice(s_InputDevices[i], isLeft)) { continue; }

				if (s_InputDevices[i].TryGetFeatureValue(CommonUsages.handData, out Hand handData))
				{
					if (!handData.TryGetFingerBones(finger, s_FingerBones[finger]))
					{
						s_FingerBones[finger].Clear();
					}
				}
			}
			return s_FingerBones[finger];
		}
	}
}