// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC\u2019s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using Wave.Native;
using Wave.OpenXR;

namespace Wave.Essence.Tracker
{
	public enum TrackerId
	{
		Tracker0 = WVR_TrackerId.WVR_TrackerId_0,
		Tracker1 = WVR_TrackerId.WVR_TrackerId_1,
		Tracker2 = WVR_TrackerId.WVR_TrackerId_2,
		Tracker3 = WVR_TrackerId.WVR_TrackerId_3,
	}

	public enum TrackerRole
	{
		Undefined = WVR_TrackerRole.WVR_TrackerRole_Undefined,
		Standalone = WVR_TrackerRole.WVR_TrackerRole_Standalone,
		Pair1_Right = WVR_TrackerRole.WVR_TrackerRole_Pair1_Right,
		Pair1_Left = WVR_TrackerRole.WVR_TrackerRole_Pair1_Left,
	}

	public enum TrackerButton
	{
		System = WVR_InputId.WVR_InputId_0,
		Menu = WVR_InputId.WVR_InputId_Alias1_Menu,
		A = WVR_InputId.WVR_InputId_Alias1_A,
		B = WVR_InputId.WVR_InputId_Alias1_B,
		X = WVR_InputId.WVR_InputId_Alias1_X,
		Y = WVR_InputId.WVR_InputId_Alias1_Y,
		Trigger = WVR_InputId.WVR_InputId_Alias1_Trigger,
	}

	public enum AxisType
	{
		None = WVR_AnalogType.WVR_AnalogType_None,
		XY = WVR_AnalogType.WVR_AnalogType_2D,
		XOnly = WVR_AnalogType.WVR_AnalogType_1D,
	}

	public static class TrackerUtils
	{
		public static TrackerId Id(this WVR_TrackerId trackerId)
		{
			if (trackerId == WVR_TrackerId.WVR_TrackerId_0) { return TrackerId.Tracker0; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_1) { return TrackerId.Tracker1; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_2) { return TrackerId.Tracker2; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_3) { return TrackerId.Tracker3; }

			return TrackerId.Tracker0;
		}
		public static WVR_TrackerId Id(this TrackerId trackerId)
		{
			return (WVR_TrackerId)trackerId;
		}

		public static int Num(this TrackerId trackerId)
		{
			if (trackerId == TrackerId.Tracker0) { return 0; }
			if (trackerId == TrackerId.Tracker1) { return 1; }
			if (trackerId == TrackerId.Tracker2) { return 2; }
			if (trackerId == TrackerId.Tracker3) { return 3; }

			return 0;
		}
		public static InputDeviceTracker.TrackerId InputDevice(this TrackerId id)
		{
			if (id == TrackerId.Tracker0) { return InputDeviceTracker.TrackerId.Tracker0; }
			if (id == TrackerId.Tracker1) { return InputDeviceTracker.TrackerId.Tracker1; }
			if (id == TrackerId.Tracker2) { return InputDeviceTracker.TrackerId.Tracker2; }
			if (id == TrackerId.Tracker3) { return InputDeviceTracker.TrackerId.Tracker3; }

			return InputDeviceTracker.TrackerId.Tracker0;
		}

		public static InputDeviceTracker.TrackerRole InputDevice(this TrackerRole role)
		{
			if (role == TrackerRole.Undefined) { return InputDeviceTracker.TrackerRole.Undefined; }
			if (role == TrackerRole.Standalone) { return InputDeviceTracker.TrackerRole.Standalone; }
			if (role == TrackerRole.Pair1_Left) { return InputDeviceTracker.TrackerRole.Pair1_Left; }
			if (role == TrackerRole.Pair1_Right) { return InputDeviceTracker.TrackerRole.Pair1_Right; }

			return InputDeviceTracker.TrackerRole.Undefined;
		}
		public static TrackerRole Role(this InputDeviceTracker.TrackerRole role)
		{
			if (role == InputDeviceTracker.TrackerRole.Undefined) { return TrackerRole.Undefined; }
			if (role == InputDeviceTracker.TrackerRole.Standalone) { return TrackerRole.Standalone; }
			if (role == InputDeviceTracker.TrackerRole.Pair1_Left) { return TrackerRole.Pair1_Left; }
			if (role == InputDeviceTracker.TrackerRole.Pair1_Right) { return TrackerRole.Pair1_Right; }

			return TrackerRole.Undefined;
		}

		public static int Num(this TrackerButton button)
		{
			if (button == TrackerButton.System) { return 0; }
			if (button == TrackerButton.Menu) { return 1; }
			if (button == TrackerButton.A) { return 10; }
			if (button == TrackerButton.B) { return 11; }
			if (button == TrackerButton.X) { return 12; }
			if (button == TrackerButton.Y) { return 13; }
			if (button == TrackerButton.Trigger) { return 17; }

			return 31;
		}
		public static WVR_InputId Id(this TrackerButton button)
		{
			if (button == TrackerButton.System) { return WVR_InputId.WVR_InputId_Alias1_System; }
			if (button == TrackerButton.Menu) { return WVR_InputId.WVR_InputId_Alias1_Menu; }
			if (button == TrackerButton.A) { return WVR_InputId.WVR_InputId_Alias1_A; }
			if (button == TrackerButton.B) { return WVR_InputId.WVR_InputId_Alias1_B; }
			if (button == TrackerButton.X) { return WVR_InputId.WVR_InputId_Alias1_X; }
			if (button == TrackerButton.Y) { return WVR_InputId.WVR_InputId_Alias1_Y; }
			if (button == TrackerButton.Trigger) { return WVR_InputId.WVR_InputId_Alias1_Trigger; }

			return WVR_InputId.WVR_InputId_Alias1_System;
		}

		#region Native
		public static bool ValidWVRInputId(this uint id)
		{
			if (id >= (uint)WVR_InputId.WVR_InputId_0 && id <= (uint)WVR_InputId.WVR_InputId_19)
				return true;

			return false;
		}
		public static int ArrayIndex(this WVR_InputId id)
		{
			if (id == WVR_InputId.WVR_InputId_Max) { return 0; } // prevent overflow
			return (int)id;
		}

		public static AxisType Id(this WVR_AnalogType analog)
		{
			if (analog == WVR_AnalogType.WVR_AnalogType_None) { return AxisType.None; }
			if (analog == WVR_AnalogType.WVR_AnalogType_2D) { return AxisType.XY; }
			if (analog == WVR_AnalogType.WVR_AnalogType_1D) { return AxisType.XOnly; }

			return AxisType.None;
		}

		public static TrackerRole Id(this WVR_TrackerRole role)
		{
			if (role == WVR_TrackerRole.WVR_TrackerRole_Undefined) { return TrackerRole.Undefined; }
			if (role == WVR_TrackerRole.WVR_TrackerRole_Standalone) { return TrackerRole.Standalone; }
			if (role == WVR_TrackerRole.WVR_TrackerRole_Pair1_Right) { return TrackerRole.Pair1_Right; }
			if (role == WVR_TrackerRole.WVR_TrackerRole_Pair1_Left) { return TrackerRole.Pair1_Left; }

			return TrackerRole.Undefined;
		}
		#endregion
	}
}