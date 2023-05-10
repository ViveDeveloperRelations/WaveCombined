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

namespace Wave.XR.BuildCheck
{
	static class CustomBuildProcessor
	{
		const string CustomAndroidManifestPathSrc = "Assets/Wave/XR/Platform/Android/AndroidManifest.xml";
		const string AndroidManifestPathSrc = "Packages/" + Constants.SDKPackageName + "/Runtime/Android/AndroidManifest.xml";
		const string AndroidManifestPathDest = "Assets/Plugins/Android/AndroidManifest.xml";
		const string ForceBuildWVR = "ForceBuildWVR.txt";

		static bool isAndroidManifestPathDestExisted = false;

		static void CopyAndroidManifest()
		{
			const string PluginAndroidPath = "Assets/Plugins/Android";
			if (!Directory.Exists(PluginAndroidPath))
				Directory.CreateDirectory(PluginAndroidPath);
			isAndroidManifestPathDestExisted = File.Exists(AndroidManifestPathDest);
			if (isAndroidManifestPathDestExisted)
			{
				Debug.Log("Using the Android Manifest at Assets/Plugins/Android");
				return; // not to overwrite existed AndroidManifest.xml
			}
			if (File.Exists(CustomAndroidManifestPathSrc))
			{
				Debug.Log("Using the Android Manifest at Assets/Wave/XR/Platform/Android");
				File.Copy(CustomAndroidManifestPathSrc, AndroidManifestPathDest, false);
			}
			else if (File.Exists(AndroidManifestPathSrc))
			{
				Debug.Log("Using the Android Manifest at Packages/com.htc.upm.wave.xrsdk/Runtime/Android");
				File.Copy(AndroidManifestPathSrc, AndroidManifestPathDest, false);
			}
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
					SetBuildingWave();
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
}
#endif
