// "WaveVR SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using UnityEngine.Scripting;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;

namespace Wave.XR
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    [Preserve, InputControlLayout(displayName = "VIVE Focus 3 HMD", canRunInBackground = true)]
    public class VIVEFocus3HMD : XRHMD
    {
        const string LOG_TAG = "Wave.XR.VIVEFocus3HMD";
        static void DEBUG(string msg) { Debug.Log(LOG_TAG + " " + msg); }

        const string kInterfaceName = XRUtilities.InterfaceMatchAnyVersion;
        const string kManufacturer = "NA";
        const string kProductFocusHeadset = "Vive Focus";
        const string kProductFocusPlus = "Vive Focus Plus";
        const string kProductFocus3 = "WVR HMD";
        const string kProducts = "^(" + kProductFocusHeadset + ")|^(" + kProductFocusPlus + ")|^(" + kProductFocus3 + ")";

        /// <summary>
        /// Registers the <see cref="VIVEFocus3HMD"/> layout with the Input System.
        /// </summary>
        static VIVEFocus3HMD()
        {
#if !USE_VIVE_WAVE_XR_5_0_4
			Debug.LogWarning(LOG_TAG + " VIVEFocus3HMD() Wave.XR.Loader.WaveXRLoader is NOT assigned.");
			return;
#else
            DEBUG("VIVEFocus3HMD()");
            InputSystem.RegisterLayout(
                typeof(VIVEFocus3HMD),
                matches: new InputDeviceMatcher()
                        .WithInterface(kInterfaceName)
                        .WithManufacturer(kManufacturer)
                        .WithProduct(@kProducts));
#endif
        }

        [RuntimeInitializeOnLoadMethod]
        private static void InitializeInPlayer()
        {
        }

        [Preserve, InputControl(alias = "HMDTrackingState")]
        public new IntegerControl trackingState { get; private set; }

        [Preserve, InputControl(alias = "HMDIsTracked")]
        public new ButtonControl isTracked { get; private set; }

        [Preserve, InputControl(alias = "HMDPosition")]
        public new Vector3Control devicePosition { get; private set; }

        [Preserve, InputControl(alias = "HMDRotation")]
        public new QuaternionControl deviceRotation { get; private set; }

        [Preserve, InputControl(alias = "HMDCenterEyePosition")]
        public new Vector3Control centerEyePosition { get; private set; }

        [Preserve, InputControl(alias = "HMDCenterEyeRotation")]
        public new QuaternionControl centerEyeRotation { get; private set; }

        /// <summary>
        /// Internal call used to assign controls to the the correct element.
        /// </summary>
        protected override void FinishSetup()
        {
            DEBUG("FinishSetup() description "
                + "deviceClass: " + description.deviceClass
                + ", interfaceName: " + description.interfaceName
                + ", manufacturer: " + description.manufacturer
                + ", product: " + description.product
                + ", serial: " + description.serial
                + ", version: " + description.version);

            base.FinishSetup();

            trackingState = GetChildControl<IntegerControl>("trackingState");
            isTracked = GetChildControl<ButtonControl>("isTracked");
            devicePosition = GetChildControl<Vector3Control>("devicePosition");
            deviceRotation = GetChildControl<QuaternionControl>("deviceRotation");
            centerEyePosition = GetChildControl<Vector3Control>("centerEyePosition");
            centerEyeRotation = GetChildControl<QuaternionControl>("centerEyeRotation");
        }
    }
}
#endif