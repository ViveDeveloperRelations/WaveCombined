// "WaveVR SDK 
// © 2017 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the WaveVR SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

#if UNITY_EDITOR
using System.IO;
using UnityEditor.Build;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.XR.Management;

using Wave.XR.Loader;
using UnityEngine;
using UnityEditor.XR.Management.Metadata;
using UnityEngine.XR.Management;
using System.Xml;
using Wave.XR.Settings;

namespace Wave.XR.BuildCheck
{
	static class CustomBuildProcessor
	{
		private static string WaveXRPath = "Assets/Wave/XR";

		const string CustomAndroidManifestPathSrc = "/Platform/Android/AndroidManifest.xml";
		const string AndroidManifestPathSrc = "Packages/" + Constants.SDKPackageName + "/Runtime/Android/AndroidManifest.xml";
		const string AndroidManifestPathDest = "Assets/Plugins/Android/AndroidManifest.xml";
		const string ForceBuildWVR = "ForceBuildWVR.txt";

		static bool isAndroidManifestPathDestExisted = false;

		internal static void AddHandtrackingAndroidManifest()
		{
			WaveXRSettings settings;
			EditorBuildSettings.TryGetConfigObject(Constants.k_SettingsKey, out settings);
			if (settings != null)
				WaveXRPath = settings.waveXRFolder;

			if (File.Exists(AndroidManifestPathDest))
			{
				if (!checkHandtrackingFeature(AndroidManifestPathDest))
				{
					appendFile(
						filename: AndroidManifestPathDest,
						handtracking: true);
				}
			}
			if (File.Exists(WaveXRPath + CustomAndroidManifestPathSrc))
			{
				if (!checkHandtrackingFeature(WaveXRPath + CustomAndroidManifestPathSrc))
				{
					appendFile(
						filename: WaveXRPath + CustomAndroidManifestPathSrc,
						handtracking: true);
				}
			}
		}

		internal static void AddTrackerAndroidManifest()
		{
			WaveXRSettings settings;
			EditorBuildSettings.TryGetConfigObject(Constants.k_SettingsKey, out settings);
			if (settings != null)
				WaveXRPath = settings.waveXRFolder;

			if (File.Exists(AndroidManifestPathDest))
			{
				if (!checkTrackerFeature(AndroidManifestPathDest))
				{
					appendFile(
						filename: AndroidManifestPathDest,
						tracker: true);
				}
			}
			if (File.Exists(WaveXRPath + CustomAndroidManifestPathSrc))
			{
				if (!checkTrackerFeature(WaveXRPath + CustomAndroidManifestPathSrc))
				{
					appendFile(
						filename: WaveXRPath + CustomAndroidManifestPathSrc,
						tracker: true);
				}
			}
		}

		internal static void AddSimultaneousInteractionAndroidManifest()
		{
			WaveXRSettings settings;
			EditorBuildSettings.TryGetConfigObject(Constants.k_SettingsKey, out settings);
			if (settings != null)
				WaveXRPath = settings.waveXRFolder;

			if (File.Exists(AndroidManifestPathDest))
			{
				if (!checkSimultaneousInteractionFeature(AndroidManifestPathDest))
				{
					appendFile(
						filename: AndroidManifestPathDest,
						simultaneous_interaction: true);
				}
			}
			if (File.Exists(WaveXRPath + CustomAndroidManifestPathSrc))
			{
				if (!checkSimultaneousInteractionFeature(WaveXRPath + CustomAndroidManifestPathSrc))
				{
					appendFile(
						filename: WaveXRPath + CustomAndroidManifestPathSrc,
						simultaneous_interaction: true);
				}
			}
		}

		static void CopyAndroidManifest()
		{
			const string PluginAndroidPath = "Assets/Plugins/Android";
			WaveXRSettings settings;
			EditorBuildSettings.TryGetConfigObject(Constants.k_SettingsKey, out settings);
			if (settings != null)
				WaveXRPath = settings.waveXRFolder;

			if (!Directory.Exists(PluginAndroidPath))
				Directory.CreateDirectory(PluginAndroidPath);
			isAndroidManifestPathDestExisted = File.Exists(AndroidManifestPathDest);
			if (isAndroidManifestPathDestExisted)
			{
				Debug.Log("Using the Android Manifest at Assets/Plugins/Android");
				if (settings != null && settings.supportedFPS != WaveXRSettings.SupportedFPS.HMD_Default && !checkDefSupportedFPS(AndroidManifestPathDest))
				{
					appendFile(
						filename: AndroidManifestPathDest,
						supportedFPS: settings.supportedFPS);
				}
				return; // not to overwrite existed AndroidManifest.xml
			}

			if (File.Exists(WaveXRPath + CustomAndroidManifestPathSrc))
			{
				Debug.Log("Using the Android Manifest at " + WaveXRPath + "/Platform/Android");
				File.Copy(WaveXRPath + CustomAndroidManifestPathSrc, AndroidManifestPathDest, false);
			}
			else if (File.Exists(AndroidManifestPathSrc))
			{
				Debug.Log("Using the Android Manifest at Packages/com.htc.upm.wave.xrsdk/Runtime/Android");
				File.Copy(AndroidManifestPathSrc, AndroidManifestPathDest, false);
			}

			bool addHandTracking = (EditorPrefs.GetBool(CheckIfHandTrackingEnabled.MENU_NAME, false) && !checkHandtrackingFeature(AndroidManifestPathDest));
			bool addTracker	     = (EditorPrefs.GetBool(CheckIfTrackerEnabled.MENU_NAME, false)      && !checkTrackerFeature(AndroidManifestPathDest));
			bool addSimultaneousInteraction      = (EditorPrefs.GetBool(CheckIfSimultaneousInteractionEnabled.MENU_NAME, false)      && !checkSimultaneousInteractionFeature(AndroidManifestPathDest));

			appendFile(
				filename: AndroidManifestPathDest,
				handtracking: addHandTracking,
				settings.supportedFPS,
				tracker: addTracker,
				simultaneous_interaction: addSimultaneousInteraction);
		}

		static void appendFile(string filename
			, bool handtracking = false
			, WaveXRSettings.SupportedFPS supportedFPS = WaveXRSettings.SupportedFPS.HMD_Default
			, bool tracker = false
			, bool simultaneous_interaction = false)
		{
			string line;

			// Read the file and display it line by line.  
			StreamReader file1 = new StreamReader(filename);
			StreamWriter file2 = new StreamWriter(filename + ".tmp");
			bool appendFPS120 = supportedFPS == WaveXRSettings.SupportedFPS._120;
			while ((line = file1.ReadLine()) != null)
			{
				if (line.Contains("</application>") && appendFPS120)
				{
					file2.WriteLine("		<meta-data android:name=\"com.htc.vr.content.SupportedFPS\" android:value=\"120\" />");
				}
				if (line.Contains("</manifest>") && handtracking)
				{
					file2.WriteLine("	<uses-feature android:name=\"wave.feature.handtracking\" android:required=\"true\" />");
				}
				if (line.Contains("</manifest>") && tracker)
				{
					file2.WriteLine("	<uses-feature android:name=\"wave.feature.tracker\" android:required=\"true\" />");
				}
				if (line.Contains("</manifest>") && simultaneous_interaction)
				{
					file2.WriteLine("	<uses-feature android:name=\"wave.feature.simultaneous_interaction\" android:required=\"true\" />");
				}
				file2.WriteLine(line);
			}

			file1.Close();
			file2.Close();
			File.Delete(filename);
			File.Move(filename + ".tmp", filename);
		}

		static bool checkHandtrackingFeature(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			XmlNodeList metadataNodeList = doc.SelectNodes("/manifest/uses-feature");

			if (metadataNodeList != null)
			{
				foreach (XmlNode metadataNode in metadataNodeList)
				{
					string name = metadataNode.Attributes["android:name"].Value;
					string required = metadataNode.Attributes["android:required"].Value;

					if (name.Equals("wave.feature.handtracking"))
						return true;
				}
			}
			return false;
		}

		static bool checkSimultaneousInteractionFeature(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			XmlNodeList metadataNodeList = doc.SelectNodes("/manifest/uses-feature");

			if (metadataNodeList != null)
			{
				foreach (XmlNode metadataNode in metadataNodeList)
				{
					string name = metadataNode.Attributes["android:name"].Value;
					string required = metadataNode.Attributes["android:required"].Value;

					if (name.Equals("wave.feature.simultaneous_interaction"))
						return true;
				}
			}
			return false;
		}

		static bool checkTrackerFeature(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			XmlNodeList metadataNodeList = doc.SelectNodes("/manifest/uses-feature");

			if (metadataNodeList != null)
			{
				foreach (XmlNode metadataNode in metadataNodeList)
				{
					string name = metadataNode.Attributes["android:name"].Value;
					string required = metadataNode.Attributes["android:required"].Value;

					if (name.Equals("wave.feature.tracker"))
						return true;
				}
			}
			return false;
		}

		static bool checkDefSupportedFPS(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			XmlNodeList metadataNodeList = doc.SelectNodes("/manifest/application/meta-data");

			if (metadataNodeList != null)
			{
				foreach (XmlNode metadataNode in metadataNodeList)
				{
					string name = metadataNode.Attributes["android:name"].Value;

					if (name.Equals("com.htc.vr.content.SupportedFPS"))
						return true;
				}
			}
			return false;
		}

		static void DelAndroidManifest()
		{
			if (File.Exists(AndroidManifestPathDest))
				File.Delete(AndroidManifestPathDest);

			string AndroidManifestMetaPathDest = AndroidManifestPathDest + ".meta";
			if (File.Exists(AndroidManifestMetaPathDest))
				File.Delete(AndroidManifestMetaPathDest);
		}

		static bool SetBuildingWave()
		{
			var androidGenericSettings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Android);
			var androidXRSettings = androidGenericSettings.AssignedSettings;
			
			if (androidXRSettings == null)
			{
				androidXRSettings = ScriptableObject.CreateInstance<XRManagerSettings>() as XRManagerSettings;
			}
			var didAssign = XRPackageMetadataStore.AssignLoader(androidXRSettings, "Wave.XR.Loader.WaveXRLoader", BuildTargetGroup.Android);
			if (!didAssign)
			{
				Debug.LogError("Fail to add android WaveXRLoader.");
			}
			return didAssign;
		}

		static bool CheckIsBuildingWave()
		{
			var androidGenericSettings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Android);
			if (androidGenericSettings == null)
				return false;

			var androidXRMSettings = androidGenericSettings.AssignedSettings;
			if (androidXRMSettings == null)
				return false;

			var loaders = androidXRMSettings.loaders;
			foreach (var loader in loaders)
			{
				if (loader.GetType() == typeof(WaveXRLoader))
				{
					return true;
				}
			}
			return false;
		}

		private class CustomPreprocessor : IPreprocessBuildWithReport
        {
            public int callbackOrder { get { return 0; } }

            public void OnPreprocessBuild(BuildReport report)
            {
				if (File.Exists(ForceBuildWVR))
				{
					//SetBuildingWave();
					AddHandtrackingAndroidManifest();
					AddTrackerAndroidManifest();
					CopyAndroidManifest();
				}
				else if (report.summary.platform == BuildTarget.Android && CheckIsBuildingWave())
                {
                    CopyAndroidManifest();
                }
            }
        }

        private class CustomPostprocessor : IPostprocessBuildWithReport
        {
            public int callbackOrder { get { return 0; } }

            public void OnPostprocessBuild(BuildReport report)
            {
				if (File.Exists(ForceBuildWVR))
				{
					if (!isAndroidManifestPathDestExisted) // not to delete existed AndroidManifest.xml
						DelAndroidManifest();
					File.Delete(ForceBuildWVR);
				}
				else if (report.summary.platform == BuildTarget.Android && CheckIsBuildingWave())
                {
					if (!isAndroidManifestPathDestExisted) // not to delete existed AndroidManifest.xml
						DelAndroidManifest();
                }
            }
        }
    }

	[InitializeOnLoad]
	public static class CheckIfHandTrackingEnabled
	{
		internal const string MENU_NAME = "Wave/Hand Tracking/Enable Hand Tracking";

		private static bool enabled_;
		static CheckIfHandTrackingEnabled()
		{
			CheckIfHandTrackingEnabled.enabled_ = EditorPrefs.GetBool(CheckIfHandTrackingEnabled.MENU_NAME, false);

			/// Delaying until first editor tick so that the menu
			/// will be populated before setting check state, and
			/// re-apply correct action
			EditorApplication.delayCall += () =>
			{
				PerformAction(CheckIfHandTrackingEnabled.enabled_);
			};
		}

		[MenuItem(CheckIfHandTrackingEnabled.MENU_NAME, priority = 601)]
		private static void ToggleAction()
		{
			/// Toggling action
			PerformAction(!CheckIfHandTrackingEnabled.enabled_);
		}

		public static void PerformAction(bool enabled)
		{
			/// Set checkmark on menu item
			Menu.SetChecked(CheckIfHandTrackingEnabled.MENU_NAME, enabled);
			if (enabled)
				CustomBuildProcessor.AddHandtrackingAndroidManifest();
			/// Saving editor state
			EditorPrefs.SetBool(CheckIfHandTrackingEnabled.MENU_NAME, enabled);

			CheckIfHandTrackingEnabled.enabled_ = enabled;
		}

		[MenuItem(CheckIfHandTrackingEnabled.MENU_NAME, validate = true, priority = 601)]
		public static bool ValidateEnabled()
		{
			Menu.SetChecked(CheckIfHandTrackingEnabled.MENU_NAME, enabled_);
			return true;
		}
	}

	[InitializeOnLoad]
	public static class CheckIfTrackerEnabled
	{
		internal const string MENU_NAME = "Wave/Tracker/Enable Tracker";

		private static bool enabled_;
		static CheckIfTrackerEnabled()
		{
			CheckIfTrackerEnabled.enabled_ = EditorPrefs.GetBool(CheckIfTrackerEnabled.MENU_NAME, false);

			/// Delaying until first editor tick so that the menu
			/// will be populated before setting check state, and
			/// re-apply correct action
			EditorApplication.delayCall += () =>
			{
				PerformAction(CheckIfTrackerEnabled.enabled_);
			};
		}

		[MenuItem(CheckIfTrackerEnabled.MENU_NAME, priority = 602)]
		private static void ToggleAction()
		{
			/// Toggling action
			PerformAction(!CheckIfTrackerEnabled.enabled_);
		}

		public static void PerformAction(bool enabled)
		{
			/// Set checkmark on menu item
			Menu.SetChecked(CheckIfTrackerEnabled.MENU_NAME, enabled);
			if (enabled)
				CustomBuildProcessor.AddTrackerAndroidManifest();
			/// Saving editor state
			EditorPrefs.SetBool(CheckIfTrackerEnabled.MENU_NAME, enabled);

			CheckIfTrackerEnabled.enabled_ = enabled;
		}

		[MenuItem(CheckIfTrackerEnabled.MENU_NAME, validate = true, priority = 602)]
		public static bool ValidateEnabled()
		{
			Menu.SetChecked(CheckIfTrackerEnabled.MENU_NAME, enabled_);
			return true;
		}
	}

	[InitializeOnLoad]
	public static class CheckIfSimultaneousInteractionEnabled
	{
		internal const string MENU_NAME = "Wave/Interaction Mode/Enable Simultaneous Interaction";

		private static bool enabled_;
		static CheckIfSimultaneousInteractionEnabled()
		{
			CheckIfSimultaneousInteractionEnabled.enabled_ = EditorPrefs.GetBool(CheckIfSimultaneousInteractionEnabled.MENU_NAME, false);

			/// Delaying until first editor tick so that the menu
			/// will be populated before setting check state, and
			/// re-apply correct action
			EditorApplication.delayCall += () =>
			{
				PerformAction(CheckIfSimultaneousInteractionEnabled.enabled_);
			};
		}

		[MenuItem(CheckIfSimultaneousInteractionEnabled.MENU_NAME, priority = 603)]
		private static void ToggleAction()
		{
			/// Toggling action
			PerformAction(!CheckIfSimultaneousInteractionEnabled.enabled_);
		}

		public static void PerformAction(bool enabled)
		{
			/// Set checkmark on menu item
			Menu.SetChecked(CheckIfSimultaneousInteractionEnabled.MENU_NAME, enabled);
			if (enabled)
				CustomBuildProcessor.AddSimultaneousInteractionAndroidManifest();
			/// Saving editor state
			EditorPrefs.SetBool(CheckIfSimultaneousInteractionEnabled.MENU_NAME, enabled);

			CheckIfSimultaneousInteractionEnabled.enabled_ = enabled;
		}

		[MenuItem(CheckIfSimultaneousInteractionEnabled.MENU_NAME, validate = true, priority = 603)]
		public static bool ValidateEnabled()
		{
			Menu.SetChecked(CheckIfSimultaneousInteractionEnabled.MENU_NAME, enabled_);
			return true;
		}
	}
}
#endif
