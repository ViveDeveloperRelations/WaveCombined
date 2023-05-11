// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wave.Native;

public class FadeOutHandle : MonoBehaviour {
	private static string LOG_TAG = "FadeOutHandle";
	private const string FADEOUT_CLASSNAME = "com.htc.vr.fadeout.FadeOutClass";
	private AndroidJavaObject fadeout_class = null;
	// Use this for initialization
	void Start () {

#if UNITY_EDITOR
		if (Application.isPlaying)
		{
			return;
		}
		else
#endif
		{
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                Log.d(LOG_TAG, " init AndroidJavaClass");
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    AndroidJavaClass ajc_setting = new AndroidJavaClass(FADEOUT_CLASSNAME);
                    if (ajc_setting == null)
                    {
                        Log.e(LOG_TAG, "Start() " + FADEOUT_CLASSNAME + " is null");
                        return;
                    }
                    fadeout_class = ajc_setting.CallStatic<AndroidJavaObject>("getInstance", jo);
                    if (fadeout_class == null)
                    {
                        Log.e(LOG_TAG, "Start() could NOT get instance of " + FADEOUT_CLASSNAME);
                    }
                    Log.d(LOG_TAG, "Start() : " + FADEOUT_CLASSNAME);
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {

	}

	public void fadeout()
	{
#if UNITY_EDITOR
		if (Application.isEditor)
		{
			return;
		}
		else
#endif
		{
			Log.d(LOG_TAG, "fadeout() : " + FADEOUT_CLASSNAME);
			fadeout_class.Call("FadeOut");
		}
	}
}
