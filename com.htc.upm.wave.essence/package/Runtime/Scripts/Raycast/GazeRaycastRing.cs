// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Wave.Native;

namespace Wave.Essence.Raycast
{
	public class GazeRaycastRing : RaycastRing
	{
		const string LOG_TAG = "Wave.Essence.Raycast.GazeRaycastRing";
		private void DEBUG(string msg)
		{
			if (Log.EnableDebugLog)
				Log.d(LOG_TAG, msg, true);
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

		[Tooltip("Event triggered by gaze.")]
		[SerializeField]
		private GazeEvent m_InputEvent = GazeEvent.Down;
		public GazeEvent InputEvent { get { return m_InputEvent; } set { m_InputEvent = value; } }

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

		private bool m_KeyDown = false;
		protected override void Update()
		{
			base.Update();

			if (!IsInteractable()) { return; }

			m_KeyDown = ButtonPressed();
		}
		#endregion

		private bool IsInteractable()
		{
			bool enabled = RaycastSwitch.Gaze.Enabled;
			bool hasFocus = ClientInterface.IsFocused;

			m_Interactable = m_AlwaysEnable || (enabled && hasFocus);

			if (Log.gpl.Print)
			{
				DEBUG("IsInteractable() enabled: " + enabled + ", hasFocus: " + hasFocus + ", m_AlwaysEnable: " + m_AlwaysEnable);
			}

			return m_Interactable;
		}

		private bool ButtonPressed()
		{
			bool down = false;

#if UNITY_EDITOR
			if (Application.isEditor)
			{
				for (int i = 0; i < m_ControlKey.OptionList.Count; i++)
				{
					down |=
						WXRDevice.ButtonPress(WVR_DeviceType.WVR_DeviceType_Controller_Left,m_ControlKey.OptionList[i].ViveFocus3Button(true)) ||
						WXRDevice.ButtonPress(WVR_DeviceType.WVR_DeviceType_Controller_Right, m_ControlKey.OptionList[i].ViveFocus3Button(false));
				}
			}
			else
#endif
			{
				for (int i = 0; i < m_ControlKey.OptionList.Count; i++)
				{
					m_ControlKey.StateEx[i] = m_ControlKey.State[i];
					m_ControlKey.State[i] =
						WXRDevice.KeyDown(XR_Device.Left, m_ControlKey.OptionList[i]) ||
						WXRDevice.KeyDown(XR_Device.Right, m_ControlKey.OptionList[i]);

					down |= (m_ControlKey.State[i] && !m_ControlKey.StateEx[i]);
				}
			}

			return down;
		}

		#region RaycastImpl Actions overrides
		internal bool hold = false;
		protected override bool OnDown()
		{
			if (m_InputEvent != GazeEvent.Down) { return false; }

			bool down = false;
			if (m_RingPercent >= 100 || m_KeyDown)
			{
				m_RingPercent = 0;
				m_GazeOnTime = Time.unscaledTime;
				down = true;

				// Set hold to false after 0.1s to trigger PointerUp/Click event.
				hold = true;
				StartCoroutine(PointerUpCoroutine());
			}

			return down;
		}
		private IEnumerator PointerUpCoroutine()
		{
			yield return new WaitForSeconds(0.1f);
			hold = false;
		}
		protected override bool OnHold()
		{
			return hold;
		}
		protected override bool OnSubmit()
		{
			if (m_InputEvent != GazeEvent.Submit) { return false; }

			bool submit = false;
			if (m_RingPercent >= 100 || m_KeyDown)
			{
				m_RingPercent = 0;
				m_GazeOnTime = Time.unscaledTime;
				submit = true;
			}

			return submit;
		}
		#endregion
	}
}
