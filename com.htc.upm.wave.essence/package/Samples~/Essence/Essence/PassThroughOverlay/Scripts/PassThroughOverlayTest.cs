using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Native;
using Wave.Essence;

namespace Wave.Essence.Samples.PassThroughOverlayTest
{
	public class PassThroughOverlayTest : MonoBehaviour
	{
		private static string LOG_TAG = "PassThroughOverlayTest";

		private bool passThroughOverlayFlag = false;
		private bool showPassThroughOverlay = false;
		// Start is called before the first frame update
		void Start()
		{
			Log.i(LOG_TAG, "PassThroughOverlay start: " + passThroughOverlayFlag);
			showPassThroughOverlay = Interop.WVR_ShowPassthroughOverlay(passThroughOverlayFlag);
			Log.i(LOG_TAG, "ShowPassThroughOverlay start: " + showPassThroughOverlay);
		}

		// Update is called once per frame
		void Update()
		{
			if (WXRDevice.ButtonPress(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Trigger)|| WXRDevice.ButtonPress(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Trigger))
			{
				passThroughOverlayFlag = !passThroughOverlayFlag;
				Log.i(LOG_TAG, "PassThroughOverlay: " + passThroughOverlayFlag);
				showPassThroughOverlay = Interop.WVR_ShowPassthroughOverlay(passThroughOverlayFlag);
				Log.i(LOG_TAG, "ShowPassThroughOverlay: " + showPassThroughOverlay);
			}

		}

		private void OnApplicationPause()
		{
			showPassThroughOverlay = Interop.WVR_ShowPassthroughOverlay(false);
			Log.i(LOG_TAG, "ShowPassThroughOverlay Pause: " + showPassThroughOverlay);
		}

		private void OnApplicationQuit()
		{
			showPassThroughOverlay = Interop.WVR_ShowPassthroughOverlay(false);
			Log.i(LOG_TAG, "ShowPassThroughOverlay Quit: " + showPassThroughOverlay);
		}
	}
}
