using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Wave.XR
{
    internal class XRSDKSettingsProvider : SettingsProvider
    {
		private static readonly string[] xrsdkKeywords = new string[]
		{
			"Wave",
			"XR",
			"AndroidManifest",
		};

		public XRSDKSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope, xrsdkKeywords)
        {
        }

        public override void OnGUI(string searchContext)
        {
			bool m_FeatureAndroidManifestImported = Directory.Exists("Assets/Wave/XR/Platform/Android");

			bool hasKeyword = false;
			bool showAndroidManifest = false;
			showAndroidManifest = searchContext.Contains("AndroidManifest");
			if(showAndroidManifest)
				hasKeyword = true;

			/**
             * GUI layout of features.
             * 1. AndroidManifest
            **/
			if (showAndroidManifest || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Custom AndroidManifest", EditorStyles.boldLabel);
					GUILayout.Label("This package provides features of custom android manifest.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/XR/Platform/Android.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !m_FeatureAndroidManifestImported;
					if (GUILayout.Button("Import Feature - Custom Android Manifest", GUILayout.ExpandWidth(false)))
						ImportModule("wave_xrsdk_androidmanifest.unitypackage");
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}
		}

		private void ImportModule(string packagePath)
		{
			string target = Path.Combine("Packages/com.htc.upm.wave.xrsdk/UnityPackages~", packagePath);
			Debug.Log("Import: " + target);
			AssetDatabase.ImportPackage(target, true);
		}

		[SettingsProvider]
        static SettingsProvider Create()
        {
            return new XRSDKSettingsProvider("Project/Wave XR/XRSDK");
        }
    }
}
