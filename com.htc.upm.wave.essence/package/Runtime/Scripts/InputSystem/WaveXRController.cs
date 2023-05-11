// "WaveVR SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Linq;
using UnityEngine;
using Wave.Native;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;

namespace Wave.Essence.HIDPlugin
{
	public struct WaveXRControllerState : IInputStateTypeInfo
	{
		public FourCC format => new FourCC('W', 'A', 'V', 'E');

		#region button press
		[InputControl(name = "leftSystemHold",		layout = "Button", bit = 0, displayName = "Press(L) System/Home")]
		[InputControl(name = "leftMenuHold",		layout = "Button", bit = 1, displayName = "Press(L) Menu")]
		[InputControl(name = "leftGripHold",		layout = "Button", bit = 2, displayName = "Press(L) Grip")]
		[InputControl(name = "leftVolumeUpHold",	layout = "Button", bit = 7, displayName = "Press(L) Volume Up")]
		[InputControl(name = "leftVolumeDownHold",	layout = "Button", bit = 8, displayName = "Press(L) Volume Down")]
		[InputControl(name = "leftBumperHold",		layout = "Button", bit = 9, displayName = "Press(L) Bumper")]
		[InputControl(name = "leftAHold",			layout = "Button", bit = 10, displayName = "Press(L) A")]
		[InputControl(name = "leftBHold",			layout = "Button", bit = 11, displayName = "Press(L) B")]
		[InputControl(name = "leftXHold",			layout = "Button", bit = 12, displayName = "Press(L) X")]
		[InputControl(name = "leftYHold",			layout = "Button", bit = 13, displayName = "Press(L) Y")]
		[InputControl(name = "leftBackHold",		layout = "Button", bit = 14, displayName = "Press(L) Back")]
		[InputControl(name = "leftEnterHold",		layout = "Button", bit = 15, displayName = "Press(L) Enter")]
		[InputControl(name = "leftTouchpadHold",	layout = "Button", bit = 16, displayName = "Press(L) Touchpad")]
		[InputControl(name = "leftTriggerHold",		layout = "Button", bit = 17, displayName = "Press(L) Trigger")]
		[InputControl(name = "leftJoystickHold",	layout = "Button", bit = 18, displayName = "Press(L) Joystick/Thumbstick")]
		public uint pressLeft;

		[InputControl(name = "rightSystemHold",		layout = "Button", bit = 0, displayName = "Press(R) System/Home")]
		[InputControl(name = "rightMenuHold",		layout = "Button", bit = 1, displayName = "Press(R) Menu")]
		[InputControl(name = "rightGripHold",		layout = "Button", bit = 2, displayName = "Press(R) Grip")]
		[InputControl(name = "rightVolumeUpHold",	layout = "Button", bit = 7, displayName = "Press(R) Volume Up")]
		[InputControl(name = "rightVolumeDownHold",	layout = "Button", bit = 8, displayName = "Press(R) Volume Down")]
		[InputControl(name = "rightBumperHold",		layout = "Button", bit = 9, displayName = "Press(R) Bumper")]
		[InputControl(name = "rightAHold",			layout = "Button", bit = 10, displayName = "Press(R) A")]
		[InputControl(name = "rightBHold",			layout = "Button", bit = 11, displayName = "Press(R) B")]
		[InputControl(name = "rightXHold",			layout = "Button", bit = 12, displayName = "Press(R) X")]
		[InputControl(name = "rightYHold",			layout = "Button", bit = 13, displayName = "Press(R) Y")]
		[InputControl(name = "rightBackHold",		layout = "Button", bit = 14, displayName = "Press(R) Back")]
		[InputControl(name = "rightEnterHold",		layout = "Button", bit = 15, displayName = "Press(R) Enter")]
		[InputControl(name = "rightTouchpadHold",	layout = "Button", bit = 16, displayName = "Press(R) Touchpad")]
		[InputControl(name = "rightTriggerHold",	layout = "Button", bit = 17, displayName = "Press(R) Trigger")]
		[InputControl(name = "rightJoystickHold",	layout = "Button", bit = 18, displayName = "Press(R) Joystick/Thumbstick")]
		public uint pressRight;
		#endregion

		#region button touch
		[InputControl(name = "leftGripTouch",		layout = "Button", bit = 2, displayName = "Touch(L) Grip")]
		[InputControl(name = "leftBumperTouch",		layout = "Button", bit = 9, displayName = "Touch(L) Bumper")]
		[InputControl(name = "leftATouch",			layout = "Button", bit = 10, displayName = "Touch(L) A")]
		[InputControl(name = "leftBTouch",			layout = "Button", bit = 11, displayName = "Touch(L) B")]
		[InputControl(name = "leftXTouch",			layout = "Button", bit = 12, displayName = "Touch(L) X")]
		[InputControl(name = "leftYTouch",			layout = "Button", bit = 13, displayName = "Touch(L) Y")]
		[InputControl(name = "leftTouchpadTouch",	layout = "Button", bit = 16, displayName = "Touch(L) Touchpad")]
		[InputControl(name = "leftTriggerTouch",	layout = "Button", bit = 17, displayName = "Touch(L) Trigger")]
		[InputControl(name = "leftJoystickTouch",	layout = "Button", bit = 18, displayName = "Touch(L) Joystick/Thumbstick")]
		[InputControl(name = "leftParkingTouch",	layout = "Button", bit = 19, displayName = "Touch(L) Parking")]
		public uint touchLeft;

		[InputControl(name = "rightGripTouch",		layout = "Button", bit = 2, displayName = "Touch(R) Grip")]
		[InputControl(name = "rightBumperTouch",	layout = "Button", bit = 9, displayName = "Touch(R) Bumper")]
		[InputControl(name = "rightATouch",			layout = "Button", bit = 10, displayName = "Touch(R) A")]
		[InputControl(name = "rightBTouch",			layout = "Button", bit = 11, displayName = "Touch(R) B")]
		[InputControl(name = "rightXTouch",			layout = "Button", bit = 12, displayName = "Touch(R) X")]
		[InputControl(name = "rightYTouch",			layout = "Button", bit = 13, displayName = "Touch(R) Y")]
		[InputControl(name = "rightTouchpadTouch",	layout = "Button", bit = 16, displayName = "Touch(R) Touchpad")]
		[InputControl(name = "rightTriggerTouch",	layout = "Button", bit = 17, displayName = "Touch(R) Trigger")]
		[InputControl(name = "rightJoystickTouch",	layout = "Button", bit = 18, displayName = "Touch(R) Joystick/Thumbstick")]
		[InputControl(name = "rightParkingTouch",	layout = "Button", bit = 19, displayName = "Touch(R) Parking")]
		public uint touchRight;
		#endregion

		#region button axis
		[InputControl(name = "leftGripAxis", layout = "Analog", format = "FLT", displayName = "Axis(L) Grip", parameters = "clamp=true,clampMin=0,clampMax=1")]
		public float leftGripAxis;
		[InputControl(name = "leftBumperAxis", layout = "Analog", format = "FLT", displayName = "Axis(L) Bumper", parameters = "clamp=true,clampMin=0,clampMax=1")]
		public float leftBumperAxis;
		[InputControl(name = "leftTouchpadAxis", layout = "Stick", format = "VEC2", displayName = "Axis(L) Touchpad")]
		public Vector2 leftTouchpadAxis;
		[InputControl(name = "leftTriggerAxis", layout = "Analog", format = "FLT", displayName = "Axis(L) Trigger", parameters = "clamp=true,clampMin=0,clampMax=1")]
		public float leftTriggerAxis;
		[InputControl(name = "leftJoystickAxis", layout = "Stick", format = "VEC2", displayName = "Axis(L) Joystick")]
		public Vector2 leftJoystickAxis;

		[InputControl(name = "rightGripAxis", layout = "Analog", format = "FLT", displayName = "Axis(R) Grip", parameters = "clamp=true,clampMin=0,clampMax=1")]
		public float rightGripAxis;
		[InputControl(name = "rightBumerAxis", layout = "Analog", format = "FLT", displayName = "Axis(R) Bumper", parameters = "clamp=true,clampMin=0,clampMax=1")]
		public float rightBumerAxis;
		[InputControl(name = "rightTouchpadAxis", layout = "Stick", format = "VEC2", displayName = "Axis(R) Touchpad")]
		public Vector2 rightTouchpadAxis;
		[InputControl(name = "rightTriggerAxis", layout = "Analog", format = "FLT", displayName = "Axis(R) Trigger", parameters = "clamp=true,clampMin=0,clampMax=1")]
		public float rightTriggerAxis;
		[InputControl(name = "rightJoystickAxis", layout = "Stick", format = "VEC2", displayName = "Axis(R) Joystick")]
		public Vector2 rightJoystickAxis;
		#endregion
	}

#if UNITY_EDITOR
	[InitializeOnLoad] // Call static class constructor in editor.
#endif
	[InputControlLayout(stateType = typeof(WaveXRControllerState))]
	public class WaveXRController : InputDevice, IInputUpdateCallbackReceiver
	{
		// [InitializeOnLoad] will ensure this gets called on every domain (re)load
		// in the editor.
#if UNITY_EDITOR
		static WaveXRController()
		{
			// Trigger our RegisterLayout code in the editor.
			Initialize();
		}
#endif

		const string kInterfaceName = "WaveController";
		// In the player, [RuntimeInitializeOnLoadMethod] will make sure our
		// initialization code gets called during startup.
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			InputSystem.RegisterLayout<WaveXRController>(
				matches: new InputDeviceMatcher()
					.WithInterface(kInterfaceName));
		}

		#region button press
		public ButtonControl leftSystemHold { get; private set; }
		public ButtonControl leftMenuHold { get; private set; }
		public ButtonControl leftGripHold { get; private set; }
		public ButtonControl leftVolumeUpHold { get; private set; }
		public ButtonControl leftVolumeDownHold { get; private set; }
		public ButtonControl leftBumperHold { get; private set; }
		public ButtonControl leftAHold { get; private set; }
		public ButtonControl leftBHold { get; private set; }
		public ButtonControl leftXHold { get; private set; }
		public ButtonControl leftYHold { get; private set; }
		public ButtonControl leftBackHold { get; private set; }
		public ButtonControl leftEnterHold { get; private set; }
		public ButtonControl leftTouchpadHold { get; private set; }
		public ButtonControl leftTriggerHold { get; private set; }
		public ButtonControl leftJoystickHold { get; private set; }

		public ButtonControl rightSystemHold { get; private set; }
		public ButtonControl rightMenuHold { get; private set; }
		public ButtonControl rightGripHold { get; private set; }
		public ButtonControl rightVolumeUpHold { get; private set; }
		public ButtonControl rightVolumeDownHold { get; private set; }
		public ButtonControl rightBumperHold { get; private set; }
		public ButtonControl rightAHold { get; private set; }
		public ButtonControl rightBHold { get; private set; }
		public ButtonControl rightXHold { get; private set; }
		public ButtonControl rightYHold { get; private set; }
		public ButtonControl rightBackHold { get; private set; }
		public ButtonControl rightEnterHold { get; private set; }
		public ButtonControl rightTouchpadHold { get; private set; }
		public ButtonControl rightTriggerHold { get; private set; }
		public ButtonControl rightJoystickHold { get; private set; }
		#endregion

		#region button touch
		public ButtonControl leftGripTouch { get; private set; }
		public ButtonControl leftBumperTouch { get; private set; }
		public ButtonControl leftATouch { get; private set; }
		public ButtonControl leftBTouch { get; private set; }
		public ButtonControl leftXTouch { get; private set; }
		public ButtonControl leftYTouch { get; private set; }
		public ButtonControl leftTouchpadTouch { get; private set; }
		public ButtonControl leftTriggerTouch { get; private set; }
		public ButtonControl leftJoystickTouch { get; private set; }
		public ButtonControl leftParkingTouch { get; private set; }

		public ButtonControl rightGripTouch { get; private set; }
		public ButtonControl rightBumperTouch { get; private set; }
		public ButtonControl rightATouch { get; private set; }
		public ButtonControl rightBTouch { get; private set; }
		public ButtonControl rightXTouch { get; private set; }
		public ButtonControl rightYTouch { get; private set; }
		public ButtonControl rightTouchpadTouch { get; private set; }
		public ButtonControl rightTriggerTouch { get; private set; }
		public ButtonControl rightJoystickTouch { get; private set; }
		public ButtonControl rightParkingTouch { get; private set; }
		#endregion

		#region button axis
		public AxisControl leftGripAxis { get; private set; }
		public AxisControl leftBumperAxis { get; private set; }
		public StickControl leftTouchpadAxis { get; private set; }
		public AxisControl leftTriggerAxis { get; private set; }
		public StickControl leftJoystickAxis { get; private set; }

		public AxisControl rightGripAxis { get; private set; }
		public AxisControl rightBumerAxis { get; private set; }
		public StickControl rightTouchpadAxis { get; private set; }
		public AxisControl rightTriggerAxis { get; private set; }
		public StickControl rightJoystickAxis { get; private set; }
		#endregion

		#region InputDevice overrides
		// FinishSetup is where our device setup is finalized. Here we can look up
		// the controls that have been created.
		protected override void FinishSetup()
		{
			base.FinishSetup();

			#region button press
			leftSystemHold = GetChildControl<ButtonControl>("leftSystemHold");
			leftMenuHold = GetChildControl<ButtonControl>("leftMenuHold");
			leftGripHold = GetChildControl<ButtonControl>("leftGripHold");
			leftVolumeUpHold = GetChildControl<ButtonControl>("leftVolumeUpHold");
			leftVolumeDownHold = GetChildControl<ButtonControl>("leftVolumeDownHold");
			leftBumperHold = GetChildControl<ButtonControl>("leftBumperHold");
			leftAHold = GetChildControl<ButtonControl>("leftAHold");
			leftBHold = GetChildControl<ButtonControl>("leftBHold");
			leftXHold = GetChildControl<ButtonControl>("leftXHold");
			leftYHold = GetChildControl<ButtonControl>("leftYHold");
			leftBackHold = GetChildControl<ButtonControl>("leftBackHold");
			leftEnterHold = GetChildControl<ButtonControl>("leftEnterHold");
			leftTouchpadHold = GetChildControl<ButtonControl>("leftTouchpadHold");
			leftTriggerHold = GetChildControl<ButtonControl>("leftTriggerHold");
			leftJoystickHold = GetChildControl<ButtonControl>("leftJoystickHold");

			rightSystemHold = GetChildControl<ButtonControl>("rightSystemHold");
			rightMenuHold = GetChildControl<ButtonControl>("rightMenuHold");
			rightGripHold = GetChildControl<ButtonControl>("rightGripHold");
			rightVolumeUpHold = GetChildControl<ButtonControl>("rightVolumeUpHold");
			rightVolumeDownHold = GetChildControl<ButtonControl>("rightVolumeDownHold");
			rightBumperHold = GetChildControl<ButtonControl>("rightBumperHold");
			rightAHold = GetChildControl<ButtonControl>("rightAHold");
			rightBHold = GetChildControl<ButtonControl>("rightBHold");
			rightXHold = GetChildControl<ButtonControl>("rightXHold");
			rightYHold = GetChildControl<ButtonControl>("rightYHold");
			rightBackHold = GetChildControl<ButtonControl>("rightBackHold");
			rightEnterHold = GetChildControl<ButtonControl>("rightEnterHold");
			rightTouchpadHold = GetChildControl<ButtonControl>("rightTouchpadHold");
			rightTriggerHold = GetChildControl<ButtonControl>("rightTriggerHold");
			rightJoystickHold = GetChildControl<ButtonControl>("rightJoystickHold");
			#endregion

			#region button touch
			leftGripTouch = GetChildControl<ButtonControl>("leftGripTouch");
			leftBumperTouch = GetChildControl<ButtonControl>("leftBumperTouch");
			leftATouch = GetChildControl<ButtonControl>("leftATouch");
			leftBTouch = GetChildControl<ButtonControl>("leftBTouch");
			leftXTouch = GetChildControl<ButtonControl>("leftXTouch");
			leftYTouch = GetChildControl<ButtonControl>("leftYTouch");
			leftTouchpadTouch = GetChildControl<ButtonControl>("leftTouchpadTouch");
			leftTriggerTouch = GetChildControl<ButtonControl>("leftTriggerTouch");
			leftJoystickTouch = GetChildControl<ButtonControl>("leftJoystickTouch");
			leftParkingTouch = GetChildControl<ButtonControl>("leftParkingTouch");

			rightGripTouch = GetChildControl<ButtonControl>("rightGripTouch");
			rightBumperTouch = GetChildControl<ButtonControl>("rightBumperTouch");
			rightATouch = GetChildControl<ButtonControl>("rightATouch");
			rightBTouch = GetChildControl<ButtonControl>("rightBTouch");
			rightXTouch = GetChildControl<ButtonControl>("rightXTouch");
			rightYTouch = GetChildControl<ButtonControl>("rightYTouch");
			rightTouchpadTouch = GetChildControl<ButtonControl>("rightTouchpadTouch");
			rightTriggerTouch = GetChildControl<ButtonControl>("rightTriggerTouch");
			rightJoystickTouch = GetChildControl<ButtonControl>("rightJoystickTouch");
			rightParkingTouch = GetChildControl<ButtonControl>("rightParkingTouch");
			#endregion

			#region button axis
			leftGripAxis = GetChildControl<AxisControl>("leftGripAxis");
			leftBumperAxis = GetChildControl<AxisControl>("leftBumperAxis");
			leftTouchpadAxis = GetChildControl<StickControl>("leftTouchpadAxis");
			leftTriggerAxis = GetChildControl<AxisControl>("leftTriggerAxis");
			leftJoystickAxis = GetChildControl<StickControl>("leftJoystickAxis");

			rightGripAxis = GetChildControl<AxisControl>("rightGripAxis");
			rightBumerAxis = GetChildControl<AxisControl>("rightBumerAxis");
			rightTouchpadAxis = GetChildControl<StickControl>("rightTouchpadAxis");
			rightTriggerAxis = GetChildControl<AxisControl>("rightTriggerAxis");
			rightJoystickAxis = GetChildControl<StickControl>("rightJoystickAxis");
			#endregion
		}

		public static WaveXRController current { get; private set; }
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
				current = null;
		}
		#endregion

		#region IInputUpdateCallbackReceiver overrides
		public void OnUpdate()
		{
			var state = new WaveXRControllerState();
			if (Application.isPlaying)
			{
				#region button press
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_System))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_System;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Menu))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Menu;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Grip))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Grip;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Volume_Up))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Volume_Up;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Volume_Down))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Volume_Down;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Bumper))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Bumper;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_A))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_A;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_B))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_B;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_X))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_X;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Y))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Y;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Back))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Back;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Enter))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Enter;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Touchpad))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Touchpad;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Trigger))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Trigger;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Thumbstick))
					state.pressLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Thumbstick;

				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_System))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_System;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Menu))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Menu;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Grip))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Grip;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Volume_Up))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Volume_Up;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Volume_Down))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Volume_Down;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Bumper))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Bumper;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_A))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_A;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_B))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_B;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_X))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_X;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Y))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Y;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Back))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Back;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Enter))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Enter;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Touchpad))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Touchpad;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Trigger))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Trigger;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Thumbstick))
					state.pressRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Thumbstick;
				#endregion

				#region button touch
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Grip))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Grip;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Bumper))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Bumper;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_A))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_A;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_B))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_B;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_X))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_X;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Y))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Y;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Touchpad))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Touchpad;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Trigger))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Trigger;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Thumbstick))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Thumbstick;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Parking))
					state.touchLeft |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Parking;

				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Grip))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Grip;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Bumper))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Bumper;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_A))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_A;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_B))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_B;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_X))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_X;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Y))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Y;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Touchpad))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Touchpad;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Trigger))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Trigger;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Thumbstick))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Thumbstick;
				if (WXRDevice.ButtonTouching(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Parking))
					state.touchRight |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Parking;
				#endregion

				#region button axis
				Vector2 axisLeft = Vector2.zero;
				axisLeft = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Grip);
				state.leftGripAxis = axisLeft.x;
				axisLeft = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Bumper);
				state.leftBumperAxis = axisLeft.x;
				axisLeft = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Trigger);
				state.leftTriggerAxis = axisLeft.x;

				state.leftTouchpadAxis = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Touchpad);
				state.leftJoystickAxis = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Left, WVR_InputId.WVR_InputId_Alias1_Thumbstick);

				Vector2 axisRight = Vector2.zero;
				axisRight = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Grip);
				state.rightGripAxis = axisRight.x;
				axisRight = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Bumper);
				state.rightBumerAxis = axisRight.x;
				axisRight = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Trigger);
				state.rightTriggerAxis = axisRight.x;

				state.rightTouchpadAxis = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Touchpad);
				state.rightJoystickAxis = WXRDevice.ButtonAxis(WVR_DeviceType.WVR_DeviceType_Controller_Right, WVR_InputId.WVR_InputId_Alias1_Thumbstick);
				#endregion
			}
			// Finally, queue the event.
			// NOTE: We are replacing the current device state wholesale here. An alternative
			//       would be to use QueueDeltaStateEvent to replace only select memory contents.
			InputSystem.QueueStateEvent(this, state);
		}
		#endregion

		public static void AddDevice()
		{
			var device = InputSystem.devices.FirstOrDefault(x => x is WaveXRController);
			if (device == null)
			{
				InputSystem.AddDevice(new InputDeviceDescription
				{
					interfaceName = kInterfaceName,
					product = "Wave Unity XR Controller"
				});
			}
		}
		public static void RemoveDevice()
		{
			var device = InputSystem.devices.FirstOrDefault(x => x is WaveXRController);
			if (device != null)
				InputSystem.RemoveDevice(device);
		}
	}
}
#endif
