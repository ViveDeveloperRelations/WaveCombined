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
using Wave.XR.Settings;

namespace Wave.OpenXR
{
    public static class InputDeviceEye
    {
        const string LOG_TAG = "Wave.OpenXR.InputDeviceEye";
        public enum Expressions
        {
            LEFT_BLINK = 0,
            LEFT_WIDE = 1,
            RIGHT_BLINK = 2,
            RIGHT_WIDE = 3,
            LEFT_SQUEEZE = 4,
            RIGHT_SQUEEZE = 5,
            LEFT_DOWN = 6,
            RIGHT_DOWN = 7,
            LEFT_OUT = 8,
            RIGHT_IN = 9,
            LEFT_IN = 10,
            RIGHT_OUT = 11,
            LEFT_UP = 12,
            RIGHT_UP = 13,
            MAX,
        }
        public static readonly Expressions[] s_EyeExpressions = new Expressions[(int)Expressions.MAX]
        {
            Expressions.LEFT_BLINK, // 0
            Expressions.LEFT_WIDE,
            Expressions.RIGHT_BLINK,
            Expressions.RIGHT_WIDE,
            Expressions.LEFT_SQUEEZE,
            Expressions.RIGHT_SQUEEZE, // 5
            Expressions.LEFT_DOWN,
            Expressions.RIGHT_DOWN,
            Expressions.LEFT_OUT,
            Expressions.RIGHT_IN,
            Expressions.LEFT_IN, // 10
            Expressions.RIGHT_OUT,
            Expressions.LEFT_UP,
            Expressions.RIGHT_UP,
        };

        #region Wave XR Constants
        const string kIsEyeExpressionAvailable = "IsEyeExpressionAvailable";
        const string kHasEyeExpressionValue = "HasEyeExpressionValue";
        const string kLEFT_BLINK = "EYEEXPRESSION_LEFT_BLINK";    // 0
        const string kLEFT_WIDE = "EYEEXPRESSION_LEFT_WIDE";
        const string kRIGHT_BLINK = "EYEEXPRESSION_RIGHT_BLINK";
        const string kRIGHT_WIDE = "EYEEXPRESSION_RIGHT_WIDE";
        const string kLEFT_SQUEEZE = "EYEEXPRESSION_LEFT_SQUEEZE";
        const string kRIGHT_SQUEEZE = "EYEEXPRESSION_RIGHT_SQUEEZE"; // 5
        const string kLEFT_DOWN = "EYEEXPRESSION_LEFT_DOWN";
        const string kRIGHT_DOWN = "EYEEXPRESSION_RIGHT_DOWN";
        const string kLEFT_OUT = "EYEEXPRESSION_LEFT_OUT";
        const string kRIGHT_IN = "EYEEXPRESSION_RIGHT_IN";
        const string kLEFT_IN = "EYEEXPRESSION_LEFT_IN";       // 10
        const string kRIGHT_OUT = "EYEEXPRESSION_RIGHT_OUT";
        const string kLEFT_UP = "EYEEXPRESSION_LEFT_UP";
        const string kRIGHT_UP = "EYEEXPRESSION_RIGHT_UP";
        #endregion

        // Keys and Values size = 14
        static string[] s_ExpKeys = {
            kLEFT_BLINK, // 0
            kLEFT_WIDE,
            kRIGHT_BLINK,
            kRIGHT_WIDE,
            kLEFT_SQUEEZE,
            kRIGHT_SQUEEZE, // 5
            kLEFT_DOWN,
            kRIGHT_DOWN,
            kLEFT_OUT,
            kRIGHT_IN,
            kLEFT_IN, // 10
            kRIGHT_OUT,
            kLEFT_UP,
            kRIGHT_UP,
        };
        static float[] s_ExpValues = {
            0, // LEFT_BLINK = 0
            0, // LEFT_WIDE
            0, // RIGHT_BLINK
            0, // RIGHT_WIDE
            0, // LEFT_SQUEEZE
            0, // RIGHT_SQUEEZE = 5,
            0, // LEFT_DOWN
            0, // RIGHT_DOWN
            0, // LEFT_OUT
            0, // RIGHT_IN
            0, // LEFT_IN = 10,
            0, // RIGHT_OUT
            0, // LEFT_UP
            0, // RIGHT_UP
        };

        #region Wave XR Interface
        /// <summary>
        /// Enables or disables the Eye Expression feature.
        /// </summary>
        /// <param name="active">True for enable.</param>
        public static void ActivateEyeExpression(bool active)
        {
            WaveXRSettings settings = WaveXRSettings.GetInstance();
            if (settings != null)
            {
                // Check current Wave XR Eye Expression status before activation.
                settings.EnableEyeExpression = IsEyeExpressionAvailable();

                if (settings.EnableEyeExpression != active)
                {
                    settings.EnableEyeExpression = active;
                    Debug.Log(LOG_TAG + " ActivateEyeExpression() " + (settings.EnableEyeExpression ? "Activate." : "Deactivate."));
                    SettingsHelper.SetBool(WaveXRSettings.EnableEyeExpressionText, settings.EnableEyeExpression);
                }
                else
                {
                    Debug.Log(LOG_TAG + " ActivateLipExp() Eye Expression is already " + (settings.EnableEyeExpression ? "enabled." : "disabled."));
                }
            }
        }

        /// <summary>
        /// Checks if the Eye Expression feature is available currently.
        /// </summary>
        /// <returns>True for available.</returns>
        public static bool IsEyeExpressionAvailable()
        {
            bool enabled = false;
            SettingsHelper.GetBool(kIsEyeExpressionAvailable, ref enabled);
            return enabled;
        }

        static bool m_HasEyeExpressionValue = false;
        /// <summary>
        /// Checks if the Eye Expression value is provided in runtime.
        /// </summary>
        /// <returns>True for value provided.</returns>
        public static bool HasEyeExpressionValue()
        {
            UpdateExpressionValues();
            return m_HasEyeExpressionValue;
        }

        /// <summary>
        /// Retrieves the value of an <see cref="Expressions">eye expression</see>.
        /// </summary>
        /// <param name="exp">An <see cref="Expressions">eye expression</see>.</param>
        /// <returns>A float value.</returns>
        public static float GetEyeExpressionValue(Expressions exp)
        {
            UpdateExpressionValues();
            return s_ExpValues[(int)exp];
        }

        /// <summary>
        /// Retrieves all Eye Expression values in a float array. The values are sorted in the order of <see cref="Expressions">Eye Expressions</see>.
        /// </summary>
        /// <param name="values">All **Eye Expression** values in a float array</param>
        /// <returns>True for valid output value.</returns>
        public static bool GetEyeExpressionValues(out float[] values)
        {
            UpdateExpressionValues();
            values = s_ExpValues;
            return HasEyeExpressionValue();
        }

        static float m_EyeExpFrame = 0;
        static void UpdateExpressionValues()
        {
            if (m_EyeExpFrame == Time.frameCount) { return; }
            m_EyeExpFrame = Time.frameCount;

            SettingsHelper.GetBool(kHasEyeExpressionValue, ref m_HasEyeExpressionValue);

            for (int i = 0; i < s_EyeExpressions.Length; i++)
            {
                if (s_EyeExpressions[i] == Expressions.MAX) { continue; }

                if (m_HasEyeExpressionValue)
                    SettingsHelper.GetFloat(s_ExpKeys[i], ref s_ExpValues[i]);
                else
                    s_ExpValues[i] = 0;
            }
        }
        #endregion
    }
}