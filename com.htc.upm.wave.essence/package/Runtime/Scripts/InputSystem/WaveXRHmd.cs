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
	public struct WaveXRHmdState : IInputStateTypeInfo
	{
		public FourCC format => new FourCC('W', 'A', 'V', 'E');

		#region button press
		[InputControl(name = "hmdSystemHold", layout = "Button", bit = 0, displayName = "Press(HMD) System/Home")]
		[InputControl(name = "hmdBackHold", layout = "Button", bit = 14, displayName = "Press(HMD) Back")]
		[InputControl(name = "hmdEnterHold", layout = "Button", bit = 15, displayName = "Press(HMD) Enter")]
		public uint pressHmd;
		#endregion
	}

#if UNITY_EDITOR
	[InitializeOnLoad] // Call static class constructor in editor.
#endif
	[InputControlLayout(stateType = typeof(WaveXRHmdState))]
	public class WaveXRHmd : InputDevice, IInputUpdateCallbackReceiver
	{
		// [InitializeOnLoad] will ensure this gets called on every domain (re)load
		// in the editor.
#if UNITY_EDITOR
		static WaveXRHmd()
		{
			// Trigger our RegisterLayout code in the editor.
			Initialize();
		}
#endif

		const string kInterfaceName = "WaveHmd";
		// In the player, [RuntimeInitializeOnLoadMethod] will make sure our
		// initialization code gets called during startup.
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			InputSystem.RegisterLayout<WaveXRHmd>(
				matches: new InputDeviceMatcher()
					.WithInterface(kInterfaceName));
		}

		#region button press
		public ButtonControl hmdSystemHold { get; private set; }
		public ButtonControl hmdBackHold { get; private set; }
		public ButtonControl hmdEnterHold { get; private set; }
		#endregion

		#region InputDevice overrides
		// FinishSetup is where our device setup is finalized. Here we can look up
		// the controls that have been created.
		protected override void FinishSetup()
		{
			base.FinishSetup();

			#region button press
			hmdSystemHold = GetChildControl<ButtonControl>("hmdSystemHold");
			hmdBackHold = GetChildControl<ButtonControl>("hmdBackHold");
			hmdEnterHold = GetChildControl<ButtonControl>("hmdEnterHold");
			#endregion
		}

		public static WaveXRHmd current { get; private set; }
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
			var state = new WaveXRHmdState();
			if (Application.isPlaying)
			{
				#region button press
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_HMD, WVR_InputId.WVR_InputId_Alias1_System))
					state.pressHmd |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_System;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_HMD, WVR_InputId.WVR_InputId_Alias1_Back))
					state.pressHmd |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Back;
				if (WXRDevice.ButtonHold(WVR_DeviceType.WVR_DeviceType_HMD, WVR_InputId.WVR_InputId_Alias1_Enter))
					state.pressHmd |= 1 << (int)WVR_InputId.WVR_InputId_Alias1_Enter;
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
			var device = InputSystem.devices.FirstOrDefault(x => x is WaveXRHmd);
			if (device == null)
			{
				InputSystem.AddDevice(new InputDeviceDescription
				{
					interfaceName = kInterfaceName,
					product = "Wave Unity XR Hmd"
				});
			}
		}
		public static void RemoveDevice()
		{
			var device = InputSystem.devices.FirstOrDefault(x => x is WaveXRHmd);
			if (device != null)
				InputSystem.RemoveDevice(device);
		}
	}
}
#endif