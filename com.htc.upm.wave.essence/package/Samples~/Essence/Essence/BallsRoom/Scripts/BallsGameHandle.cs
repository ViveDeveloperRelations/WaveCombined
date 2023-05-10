using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Wave.Native;

public class BallsGameHandle : MonoBehaviour
{
	public Toggle LoadScenePhysical;
	public Toggle LoadSceneAnimation;

	public enum StartScene {
		BallRoomAnimation,
		BallRoomPhysical
	};

	public StartScene startScene = StartScene.BallRoomAnimation;

	string currentScene = "BallRoomAnimation";

	void Start()
	{
	}

	private void OnEnable()
	{
		currentScene = startScene.ToString();
		SceneManager.LoadScene("BallRoom", LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
	}

	public void OnButtonClick(Button btn)
	{
		if (btn.name == "AQ Off")
		{
			Interop.WVR_EnableAdaptiveQuality(false, 0);
		}
	}

	public void OnAMCToggleValueChange(Toggle toggle)
	{
		if (toggle.isOn)
		{
			if (toggle.gameObject.name == "AMC Off")
				Interop.WVR_SetAMCMode(WVR_AMCMode.Off);
			else if (toggle.gameObject.name == "AMC Force UMC")
				Interop.WVR_SetAMCMode(WVR_AMCMode.Force_UMC);
			else if (toggle.gameObject.name == "AMC Auto")
				Interop.WVR_SetAMCMode(WVR_AMCMode.Auto);
			else if (toggle.gameObject.name == "AMC Force PMC")
				Interop.WVR_SetAMCMode(WVR_AMCMode.Force_PMC);
		}
	}

	public void OnTimeSliderValueChange(Slider slider)
	{
		Time.timeScale = slider.value;
		Time.fixedDeltaTime = 1.0f / 75 * Time.timeScale;
	}
	
	public void OnSceneToggleValueChange(Toggle toggle)
	{
		if (toggle.isOn)
		{
			if (toggle == LoadScenePhysical)
				LoadScene("BallRoomPhysical");
			else if (toggle == LoadSceneAnimation)
				LoadScene("BallRoomAnimation");
		}
	}

	public void LoadScene(string sceneName)
	{
		LoadScenePhysical.interactable = false;
		LoadSceneAnimation.interactable = false;

		SceneManager.UnloadSceneAsync(currentScene);
		
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		currentScene = scene.name;
		LoadScenePhysical.interactable = true;
		LoadSceneAnimation.interactable = true;
	}

	public void OnResetBallsClicked()
	{
		SceneManager.UnloadSceneAsync(currentScene);
		SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);
	}

	private void OnDisable()
	{
		SceneManager.UnloadSceneAsync(currentScene);
		SceneManager.UnloadSceneAsync("BallRoom");
	}
}
