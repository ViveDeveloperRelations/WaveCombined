using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

/**
 * This is only a test for MSAA on/off switch to verify our own function.
 * We don't expect developer to do the switch it.  And we don't suggest
 * developer to turn MSAA off either.  Please keep at least 2x MSAA by
 * QualitySettings, and don't change QualityLevel for applying different
 * MSAA level in the runtime.  The MSAA level change will cause app
 * crashing.
**/
public class MSAAOnOffTest : MonoBehaviour
{
	Camera mainCamera;
	public Text status = null;
	bool doRandomOnOff = false;
	int msaa = 1;

	void Start() {
		doRandomOnOff = false;
		uint aa = 0;
		Wave.XR.Settings.SettingsHelper.GetInt("qsMSAA", ref aa);
		msaa = (int)aa;
		mainCamera = Camera.main;
		mainCamera.allowMSAA = msaa > 1;

		// No, follow the Settings's value.
		//aa = (uint)(mainCamera.allowMSAA ? QualitySettings.antiAliasing : 1);

		if (status == null)
			Debug.LogError("UI.Text named \"Status\" is missing.");
		else
			status.text = "MSAA " + aa + "x";
	}

	private static IntPtr GetFunctionPointerForDelegate(Delegate del)
	{
#if UNITY_ANDROID
		if (Application.isEditor)
			return IntPtr.Zero;
		else
			return Marshal.GetFunctionPointerForDelegate(del);
#else
		// In Windows, Marshal.GetFunctionPointerForDelegate() will cause application hang since Unity 2019
		return IntPtr.Zero;
#endif
	}

	public delegate void RenderEventDelegate(int e);
	private static readonly RenderEventDelegate handle = new RenderEventDelegate(RunInRenderThread);
	private static readonly IntPtr handlePtr = GetFunctionPointerForDelegate(handle);

	[MonoPInvokeCallback(typeof(RenderEventDelegate))]
	static void RunInRenderThread(int msaa)
	{
		// If you want to switch off MSAA from Camera.allowMSAA, you need tell Wave's XRSDK by this to take effect.
		// However we don't expect or suggest developer to change it.  For good UX. you should enable MSAA at least 2x.
		Wave.XR.Settings.SettingsHelper.SetInt("qsMSAA", (uint)msaa);
	}

	public void OnMSAAOn()
	{
		SetMSAA(QualitySettings.antiAliasing);
	}

	public void OnMSAAOff()
	{
		SetMSAA(1);
	}

	public void SetMSAA(int aa)
	{
		if (msaa == aa)
			return;

		if (aa <= 1)
			msaa = 1;
		else if (aa < 4)
			msaa = 2;
		else  // aa > 4
			msaa = 4;

		if (status)
			status.text = "MSAA " + aa + "x";

		mainCamera.allowMSAA = msaa > 1;
		mainCamera.enabled = false;
		if (handlePtr != IntPtr.Zero)
			GL.IssuePluginEvent(handlePtr, msaa);
		mainCamera.enabled = true;
	}

	public void OnRandom()
	{
		doRandomOnOff = !doRandomOnOff;
	}

	void Update()
	{
		if (!doRandomOnOff)
			return;

		int rand = UnityEngine.Random.Range(0, 40);
		switch (rand % 4)
		{
			case 0:
				SetMSAA(1);
				break;
			case 1:
				SetMSAA(QualitySettings.antiAliasing);
				break;
			case 2:
			case 3:
				// Do nothing
				break;
		}
	}
}
