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
using UnityEngine.UI;
using Wave.Native;

namespace Wave.Essence.Samples.NotifyDeviceActions
{
	[RequireComponent(typeof(Button))]
	public class NotifyDeviceActions : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Sample.NotifyDeviceActions";
		static void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		[SerializeField]
		private WVR_DeviceType m_DeviceType = WVR_DeviceType.WVR_DeviceType_HMD;
		public WVR_DeviceType DeviceType { get { return m_DeviceType; } set { m_DeviceType = value; } }

		public void StartNotify()
		{
			var result = NotifyDevice.Start(m_DeviceType);
			DEBUG("StartNotify() " + m_DeviceType + ", result: " + result);
		}
		public void StopNotify()
		{
			DEBUG("StopNotify() " + m_DeviceType);
			NotifyDevice.Stop(m_DeviceType);
		}
		public void SendNotify(string info)
		{
			DEBUG("SendNotify() " + m_DeviceType + ", " + info);
			NotifyDevice.Send(m_DeviceType, info);
		}
	}
}
