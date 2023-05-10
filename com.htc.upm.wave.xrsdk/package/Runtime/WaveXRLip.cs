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
using Wave.XR.Settings;

namespace Wave.OpenXR
{
    public static class InputDeviceLip
    {
        const string LOG_TAG = "Wave.OpenXR.InputDeviceLip";

		#region Wave XR Interface
		public static void ActivateLipExp(bool active)
		{
			WaveXRSettings settings = WaveXRSettings.GetInstance();
			if (settings != null && settings.EnableLipExp != active)
			{
				settings.EnableLipExp = active;
				Debug.Log(LOG_TAG + " ActivateLipExp() " + (settings.EnableLipExp ? "Activate." : "Deactivate."));
				SettingsHelper.SetBool(WaveXRSettings.EnableLipExpText, settings.EnableLipExp);
			}
		}

		const string kLipExpStatus = "kLipExpStatus";
		internal static uint m_LipExpStatus = 0;
		public static uint GetLipExpStatus()
		{
			SettingsHelper.GetInt(kLipExpStatus, ref m_LipExpStatus);
			return m_LipExpStatus;
		}
		#endregion
	}
}