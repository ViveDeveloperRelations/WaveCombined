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
using System;
using System.Collections.Generic;

namespace Wave.Essence.Raycast
{
	public class ControllerRaycastPointer : RaycastPointer
	{
		const string LOG_TAG = "Wave.Essence.Raycast.ControllerRaycastPointer";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, m_Controller + " " + msg, true);
		}

		[Serializable]
		public class ButtonOption
		{
			[SerializeField]
			private bool m_Primary2DAxisClick = false;
			public bool Primary2DAxisClick
			{
				get { return m_Primary2DAxisClick; }
				set
				{
					if (m_Primary2DAxisClick != value) { Update(); }
					m_Primary2DAxisClick = value;
				}
			}
			[SerializeField]
			private bool m_TriggerButton = true;
			public bool TriggerButton
			{
				get { return m_TriggerButton; }
				set
				{
					if (m_TriggerButton != value) { Update(); }
					m_TriggerButton = value;
				}
			}

			private List<InputFeatureUsage<bool>> m_OptionList = new List<InputFeatureUsage<bool>>();
			public List<InputFeatureUsage<bool>> OptionList { get { return m_OptionList; } }

			[HideInInspector]
			public List<bool> State = new List<bool>(), StateEx = new List<bool>();
			public void Update()
			{
				m_OptionList.Clear();
				State.Clear();
				StateEx.Clear();
				if (m_Primary2DAxisClick)
				{
					m_OptionList.Add(XR_BinaryButton.primary2DAxisClick);
					State.Add(false);
					StateEx.Add(false);
				}
				if (m_TriggerButton)
				{
					m_OptionList.Add(XR_BinaryButton.triggerButton);
					State.Add(false);
					StateEx.Add(false);
				}
			}
		}

		[SerializeField]
		private XR_Hand m_Controller = XR_Hand.Right;
		public XR_Hand Controller { get { return m_Controller; } set { m_Controller = value; } }

		[SerializeField]
		private ButtonOption m_ControlKey = new ButtonOption();
		public ButtonOption ControlKey { get { return m_ControlKey; } set { m_ControlKey = value; } }

		[SerializeField]
		private bool m_AlwaysEnable = false;
		public bool AlwaysEnable { get { return m_AlwaysEnable; } set { m_AlwaysEnable = value; } }

		#region MonoBehaviour overrides
		protected override void Awake()
		{
			base.Awake();

			m_ControlKey.Update();
			for (int i = 0; i < m_ControlKey.OptionList.Count; i++)
			{
				DEBUG("Awake() m_ControlKey[" + i + "] = " + m_ControlKey.OptionList[i].name);
			}
		}
		protected override void Update()
		{
			base.Update();

			if (!IsInteractable()) { return; }

			UpdateButtonStates();
		}
		#endregion

		private bool IsInteractable()
		{
			bool enabled = RaycastSwitch.Controller.Enabled;
			bool validPose = WXRDevice.IsTracked((XR_Device)m_Controller);
			bool hasFocus = ClientInterface.IsFocused;

			m_Interactable = m_AlwaysEnable || (enabled && validPose && hasFocus);

			if (Log.gpl.Print)
			{
				DEBUG("IsInteractable() enabled: " + enabled + ", validPose: " + validPose + ", hasFocus: " + hasFocus + ", m_AlwaysEnable: " + m_AlwaysEnable);
			}

			return m_Interactable;
		}

		private void UpdateButtonStates()
		{
			down = false;
			hold = false;

#if UNITY_EDITOR
			if (Application.isEditor)
			{
				for (int i = 0; i < m_ControlKey.OptionList.Count; i++)
				{
					down |= WXRDevice.ButtonPress(
						(WVR_DeviceType)m_Controller,
						m_ControlKey.OptionList[i].ViveFocus3Button(m_Controller == XR_Hand.Left)
						);
					hold |= WXRDevice.ButtonHold(
						(WVR_DeviceType)m_Controller,
						m_ControlKey.OptionList[i].ViveFocus3Button(m_Controller == XR_Hand.Left)
						);
				}
			} else
#endif
			{
				for (int i = 0; i < m_ControlKey.OptionList.Count; i++)
				{
					m_ControlKey.StateEx[i] = m_ControlKey.State[i];
					m_ControlKey.State[i] = WXRDevice.KeyDown((XR_Device)m_Controller, m_ControlKey.OptionList[i]);

					down |= (m_ControlKey.State[i] && !m_ControlKey.StateEx[i]);
					hold |= (m_ControlKey.State[i]);

					/*if (hold)
					{
						DEBUG("UpdateButtonStates() " +
							", " + m_ControlKey.OptionList[i].name +
							", down: " + down +
							", hold: " + hold);
					}*/
				}
			}
		}

		#region RaycastImpl Actions overrides
		internal bool down = false, hold = false;
		protected override bool OnDown()
		{
			return down;
		}
		protected override bool OnHold()
		{
			return hold;
		}
		#endregion
	}
}
