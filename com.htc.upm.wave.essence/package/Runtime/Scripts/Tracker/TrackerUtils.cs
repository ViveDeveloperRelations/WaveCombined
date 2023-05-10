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
		public static int Num(this WVR_TrackerId trackerId)
		{
			if (trackerId == WVR_TrackerId.WVR_TrackerId_0) { return 0; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_1) { return 1; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_2) { return 2; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_3) { return 3; }

			return 0;
		}
		public static TrackerId Id(this WVR_TrackerId trackerId)
		{
			if (trackerId == WVR_TrackerId.WVR_TrackerId_0) { return TrackerId.Tracker0; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_1) { return TrackerId.Tracker1; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_2) { return TrackerId.Tracker2; }
			if (trackerId == WVR_TrackerId.WVR_TrackerId_3) { return TrackerId.Tracker3; }

			return TrackerId.Tracker0;
		}

		public static int Num(this TrackerId trackerId)
		{
			return ((WVR_TrackerId)trackerId).Num();
		}
		public static WVR_TrackerId Id(this TrackerId trackerId)
		{
			return (WVR_TrackerId)trackerId;
		}

		#region Unity XR Tracker definitions
		const string kTracker0Name = "Wave Tracker0";
		const string kTracker1Name = "Wave Tracker1";
		const string kTracker2Name = "Wave Tracker2";
		const string kTracker3Name = "Wave Tracker3";

		const string kTracker0SN = "HTC-211012-Tracker0";
		const string kTracker1SN = "HTC-211012-Tracker1";
		const string kTracker2SN = "HTC-211012-Tracker2";
		const string kTracker3SN = "HTC-211012-Tracker3";

		public static string Name(this TrackerId trackerId)
		{
			if (trackerId == TrackerId.Tracker0) { return kTracker0Name; }
			if (trackerId == TrackerId.Tracker1) { return kTracker1Name; }
			if (trackerId == TrackerId.Tracker2) { return kTracker2Name; }
			if (trackerId == TrackerId.Tracker3) { return kTracker3Name; }
			return kTracker0Name;
		}
		public static string SerialNumber(this TrackerId trackerId)
		{
			if (trackerId == TrackerId.Tracker0) { return kTracker0SN; }
			if (trackerId == TrackerId.Tracker1) { return kTracker1SN; }
			if (trackerId == TrackerId.Tracker2) { return kTracker2SN; }
			if (trackerId == TrackerId.Tracker3) { return kTracker3SN; }
			return kTracker0SN;
		}
		#endregion

		public static int Num(this WVR_InputId id)
		{
			if (id == WVR_InputId.WVR_InputId_Max) { return 0; }
			return (int)id;
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
	}
}