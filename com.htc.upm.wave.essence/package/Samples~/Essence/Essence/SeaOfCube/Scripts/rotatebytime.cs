// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using UnityEngine;
using System.Collections;

public class rotatebytime : MonoBehaviour {
	// rotate 45 degree every second.
	public float AnglePerSecond = 45;
	// If set, not change every frame.
	public float ChangePerSecond = 0;
	private int count = 0;
	public bool AxisX = true;
	public bool AxisY = false;
	public bool AxisZ = true;

	// To trigger reset in Editor, click this in inspector
	public bool reset = false;

	public bool useFixedTime = true;

	private Quaternion defaultRot;

	public void Start()
	{
		defaultRot = transform.localRotation;
	}

	public void Reset()
	{
		transform.localRotation = defaultRot;
	}

	// Update is called once per frame
	void Update () {
		if (reset) {
			reset = false;
			Reset();
		}

		float deltaTime = useFixedTime ? Time.fixedDeltaTime : Time.deltaTime;

		float angle = 0;
		if (ChangePerSecond == 0)
		{
			angle = deltaTime * AnglePerSecond;
		}
		else
		{
			var pass = deltaTime * count++;
			if (pass >= ChangePerSecond)
			{
				count = 0;
				angle = pass * AnglePerSecond;
			}
		}

		transform.Rotate(AxisX ? angle : 0, AxisY ? angle : 0, AxisZ ? angle : 0);
	}
}
