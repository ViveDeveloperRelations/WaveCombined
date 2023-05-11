using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandle : MonoBehaviour
{
	public List<Explode> explodes = new List<Explode>();

	public void OnResetEvent(float f)
	{
		foreach (var explode in explodes)
		{
			explode.Reset();
		}
	}
}
