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
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Dropdown))]
	sealed class DropdownHandler : MonoBehaviour
	{
		const string LOG_TAG = "Wave.Essence.Samples.NotifyDeviceActions.DropdownHandler";
		void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
		}

		[SerializeField]
		private NotifyDeviceActions m_NotifyActions = null;
		public NotifyDeviceActions NotifyActions { get { return m_NotifyActions; } set { m_NotifyActions = value; } }

		private Dropdown m_DropDown = null;
		private Text m_DropDownText = null;
		private string[] textStrings = new string[] {
			"HMD",
			"ControllerRight",
			"ControllerLeft",
			"Camera",
			"EyeTracking",
			"HandGestureRight",
			"HandGestureLeft",
			"NaturalHandRight",
			"NaturalHandLeft",
			"ElectronicHandRight",
			"ElectronicHandLeft",
			"Tracker",
		};
		private WVR_DeviceType[] textTypes = new WVR_DeviceType[]
		{
			WVR_DeviceType.WVR_DeviceType_HMD,
			WVR_DeviceType.WVR_DeviceType_Controller_Right,
			WVR_DeviceType.WVR_DeviceType_Controller_Left,
			WVR_DeviceType.WVR_DeviceType_Camera,
			WVR_DeviceType.WVR_DeviceType_EyeTracking,
			WVR_DeviceType.WVR_DeviceType_HandGesture_Right,
			WVR_DeviceType.WVR_DeviceType_HandGesture_Left,
			WVR_DeviceType.WVR_DeviceType_NaturalHand_Right,
			WVR_DeviceType.WVR_DeviceType_NaturalHand_Left,
			WVR_DeviceType.WVR_DeviceType_ElectronicHand_Right,
			WVR_DeviceType.WVR_DeviceType_ElectronicHand_Left,
			WVR_DeviceType.WVR_DeviceType_Tracker,
		};
		private Color m_Color = new Color(26, 7, 253, 255);

		void DropdownValueChanged(Dropdown change)
		{
			Log.d(LOG_TAG, "DropdownValueChanged(): " + change.value + ", " + textTypes[change.value], true);
			if (m_NotifyActions != null)
				m_NotifyActions.DeviceType = textTypes[change.value];
		}
		void Start()
		{
			m_DropDown = GetComponent<Dropdown>();
			m_DropDown.onValueChanged.AddListener(
				delegate { DropdownValueChanged(m_DropDown); }
				);
			m_DropDownText = GetComponentInChildren<Text>();

			// clear all option item
			m_DropDown.options.Clear();

			// fill the dropdown menu OptionData
			foreach (string c in textStrings)
			{
				m_DropDown.options.Add(new Dropdown.OptionData() { text = c });
			}

			m_DropDown.value = 0;

			if (m_NotifyActions != null)
				m_NotifyActions.DeviceType = textTypes[0];
		}
		void Update()
		{
			if (m_DropDownText == null)
				return;

			m_DropDownText.text = textStrings[m_DropDown.value];

			Canvas dropdown_canvas = m_DropDown.gameObject.GetComponentInChildren<Canvas>();
			Button[] buttons = m_DropDown.gameObject.GetComponentsInChildren<Button>();
			if (dropdown_canvas != null)
			{
				foreach (Button btn in buttons)
				{
					Log.d(LOG_TAG, "set button " + btn.name + " color.", true);
					ColorBlock cb = btn.colors;
					cb.normalColor = this.m_Color;
					btn.colors = cb;
				}
			}
		}
	}
}
