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
using Wave.Native;
using UnityEngine.XR;

namespace Wave.Essence
{
	public class ArenaSetter : MonoBehaviour
	{
		// A binary value representing the Touchpad press state.
		public static InputFeatureUsage<bool> TouchpadPressFeature = new InputFeatureUsage<bool>("Primary2DAxisClick");
		// A binary value representing the Trigger press state.
		public static InputFeatureUsage<bool> TriggerPressFeature = new InputFeatureUsage<bool>("TriggerButton");
		// A binary value representing the Menu press state.
		public static InputFeatureUsage<bool> MenuPressFeature = new InputFeatureUsage<bool>("MenuButton");

		public enum ArenaShape
		{
			Rectangle = WVR_ArenaShape.WVR_ArenaShape_Rectangle,
			Round = WVR_ArenaShape.WVR_ArenaShape_Round
		}

		private const string LOG_TAG = "Wave.Essence.ArenaSetter";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		#region External variables.
		public ArenaShape Shape;
		[Tooltip("Length of rectangle arena (meter)")]
		[Range(0.5f, 2.0f)]
		public float RectangleLength = 0.5f;
		[Tooltip("Width of rectangle arena (meter)")]
		[Range(0.5f, 2.0f)]
		public float RectangleWidth = 0.5f;
		[Tooltip("RoundDiameter of round arena (meter)")]
		[Range(1.0f, 4.0f)]
		public float RoundDiameter = 1.0f;
		#endregion

		private WVR_Arena_t arena;
		private WVR_ArenaArea_t arenaArea;
		private bool overRange = false;
		WVR_ArenaVisible arenaVisible = WVR_ArenaVisible.WVR_ArenaVisible_Auto;

		private bool binaryValue = false;
		private bool touchpadPressed = false, triggerPressed = false, menuPressed = false, preTouchpadPressed = false, preTriggerPressed = false, preMenuPressed = false;
		private InputDevice inputDeviceRight = new InputDevice(), inputDeviceLeft = new InputDevice();

		private void OnEnable()
		{
			inputDeviceRight = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
			inputDeviceLeft = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

			arena.shape = (WVR_ArenaShape)Shape;
			switch (Shape)
			{
				case ArenaShape.Rectangle:
					arenaArea.rectangle.length = RectangleLength;
					arenaArea.rectangle.width = RectangleWidth;
					break;
				case ArenaShape.Round:
					arenaArea.round.diameter = RoundDiameter;
					break;
				default:
					break;
			}
			arena.area = arenaArea;

			bool ret = Interop.WVR_SetArena(ref arena);
			if (!ret)
				Log.e(LOG_TAG, "WVR_SetArena() failed.");
		}

		private void UpdateMenuStates()
		{
			preMenuPressed = menuPressed;
			menuPressed = false;

			if (!inputDeviceRight.TryGetFeatureValue(MenuPressFeature, out binaryValue))
				binaryValue = false;
			menuPressed |= binaryValue;

			if (!inputDeviceLeft.TryGetFeatureValue(MenuPressFeature, out binaryValue))
				binaryValue = false;
			menuPressed |= binaryValue;
		}

		private void UpdateTouchpadStates()
		{
			preTouchpadPressed = touchpadPressed;
			touchpadPressed = false;

			if (!inputDeviceRight.TryGetFeatureValue(TouchpadPressFeature, out binaryValue))
				binaryValue = false;
			touchpadPressed |= binaryValue;

			if (!inputDeviceLeft.TryGetFeatureValue(TouchpadPressFeature, out binaryValue))
				binaryValue = false;
			touchpadPressed |= binaryValue;
		}

		private void UpdateTriggerStates()
		{
			preTriggerPressed = triggerPressed;
			triggerPressed = false;

			if (!inputDeviceRight.TryGetFeatureValue(TriggerPressFeature, out binaryValue))
				binaryValue = false;
			triggerPressed |= binaryValue;

			if (!inputDeviceLeft.TryGetFeatureValue(TriggerPressFeature, out binaryValue))
				binaryValue = false;
			triggerPressed |= binaryValue;
		}

		void Update()
		{
			arena = Interop.WVR_GetArena();
			overRange = Interop.WVR_IsOverArenaRange();
			arenaVisible = Interop.WVR_GetArenaVisible();

			UpdateMenuStates();
			if (!preMenuPressed && menuPressed)
				Interop.WVR_SetArenaVisible(WVR_ArenaVisible.WVR_ArenaVisible_ForceOff);

			UpdateTouchpadStates();
			if (!preTouchpadPressed && touchpadPressed)
				Interop.WVR_SetArenaVisible(WVR_ArenaVisible.WVR_ArenaVisible_ForceOn);

			UpdateTriggerStates();
			if (!preTriggerPressed && triggerPressed)
				Interop.WVR_SetArenaVisible(WVR_ArenaVisible.WVR_ArenaVisible_Auto);
		}
	}
}