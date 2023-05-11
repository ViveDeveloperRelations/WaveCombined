// "Wave SDK 
// © 2020 HTC Corporation. All Rights Reserved.
//
// Unless otherwise required by copyright law and practice,
// upon the execution of HTC SDK license agreement,
// HTC grants you access to and use of the Wave SDK(s).
// You shall fully comply with all of HTC’s SDK license agreement terms and
// conditions signed by you and all SDK and API requirements,
// specifications, and documentation provided by HTC to You."

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using Wave.XR;

#if UNITY_EDITOR
namespace Wave.Essence.Editor
{
	internal class EssenceSettingsProvider : SettingsProvider
	{
		#region Essence.Controller.Model asset
		const string kControllerModelAsset = "Assets/Wave/Essence/ControllerModel.asset";
		public static void UpdateAssetControllerModel(bool importedControllerModelPackage)
		{
			PackageEssenceAsset asset = null;
			if (File.Exists(kControllerModelAsset))
			{
				asset = AssetDatabase.LoadAssetAtPath(kControllerModelAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedControllerModelPackage = importedControllerModelPackage;
			}
			else
			{
				asset = ScriptableObject.CreateInstance(typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedControllerModelPackage = importedControllerModelPackage;
				if (!Directory.Exists("Assets/Wave"))
					Directory.CreateDirectory("Assets/Wave");
				if (!Directory.Exists("Assets/Wave/Essence"))
					Directory.CreateDirectory("Assets/Wave/Essence");
				AssetDatabase.CreateAsset(asset, kControllerModelAsset);
			}
			AssetDatabase.SaveAssets();
			Debug.Log("UpdateAssetControllerModel() " + kControllerModelAsset + ", importedControllerModelPackage: " + asset.importedControllerModelPackage);
		}
		private static bool IsControllerModelPackageOnceImported()
		{
			if (!File.Exists(kControllerModelAsset))
				return false;

			PackageEssenceAsset asset = AssetDatabase.LoadAssetAtPath(kControllerModelAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
			return asset.importedControllerModelPackage;
		}
		#endregion

		#region Essence.InputModule asset
		const string kInputModuleAsset = "Assets/Wave/Essence/InputModule.asset";
		public static void UpdateAssetInputModule(bool importedInputModulePackage)
		{
			PackageEssenceAsset asset = null;
			if (File.Exists(kInputModuleAsset))
			{
				asset = AssetDatabase.LoadAssetAtPath(kInputModuleAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedInputModulePackage = importedInputModulePackage;
			}
			else
			{
				asset = ScriptableObject.CreateInstance(typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedInputModulePackage = importedInputModulePackage;
				AssetDatabase.CreateAsset(asset, kInputModuleAsset);
			}
			AssetDatabase.SaveAssets();
			Debug.Log("UpdateAssetInputModule() " + kInputModuleAsset + ", importedInputModulePackage: " + asset.importedInputModulePackage);
		}
		private static bool IsInputModulePackageOnceImported()
		{
			if (!File.Exists(kInputModuleAsset))
				return false;

			PackageEssenceAsset asset = AssetDatabase.LoadAssetAtPath(kInputModuleAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
			return asset.importedInputModulePackage;
		}
		#endregion

		#region Essence.Hand.Model asset
		const string kHandModelAsset = "Assets/Wave/Essence/HandModel.asset";
		public static void UpdateAssetHandModel(bool importedHandModelPackage)
		{
			PackageEssenceAsset asset = null;
			if (File.Exists(kHandModelAsset))
			{
				asset = AssetDatabase.LoadAssetAtPath(kHandModelAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedHandModelPackage = importedHandModelPackage;
			}
			else
			{
				asset = ScriptableObject.CreateInstance(typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedHandModelPackage = importedHandModelPackage;
				AssetDatabase.CreateAsset(asset, kHandModelAsset);
			}
			AssetDatabase.SaveAssets();
			Debug.Log("UpdateAssetHandModel() " + kHandModelAsset + ", importedHandModelPackage: " + asset.importedHandModelPackage);
		}
		private static bool IsHandModelPackageOnceImported()
		{
			if (!File.Exists(kHandModelAsset))
				return false;

			PackageEssenceAsset asset = AssetDatabase.LoadAssetAtPath(kHandModelAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
			return asset.importedHandModelPackage;
		}
		#endregion

		#region Essence.Interaction.Mode asset
		const string kInteractionModeAsset = "Assets/Wave/Essence/InteractionMode.asset";
		public static void UpdateAssetInteractionMode(bool importedInteractionModePackage)
		{
			PackageEssenceAsset asset = null;
			if (File.Exists(kInteractionModeAsset))
			{
				asset = AssetDatabase.LoadAssetAtPath(kInteractionModeAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedInteractionModePackage = importedInteractionModePackage;
			}
			else
			{
				asset = ScriptableObject.CreateInstance(typeof(PackageEssenceAsset)) as PackageEssenceAsset;
				asset.importedInteractionModePackage = importedInteractionModePackage;
				AssetDatabase.CreateAsset(asset, kInteractionModeAsset);
			}
			AssetDatabase.SaveAssets();
			Debug.Log("UpdateAssetInteractionMode() " + kInteractionModeAsset + ", importedInteractionModePackage: " + asset.importedInteractionModePackage);
		}
		private static bool IsInteractionModePackageOnceImported()
		{
			if (!File.Exists(kInteractionModeAsset))
				return false;

			PackageEssenceAsset asset = AssetDatabase.LoadAssetAtPath(kInteractionModeAsset, typeof(PackageEssenceAsset)) as PackageEssenceAsset;
			return asset.importedInteractionModePackage;
		}
		#endregion

		private static readonly string[] essenceKeywords = new string[]
		{
			"Wave",
			"Essence",
			"Controller",
			"InputModule",
			"RenderDoc",
			"Hand",
			"Interaction",
			"CompositorLayer",
			"XR",
		};

		internal static UnityEditor.PackageManager.PackageInfo pi = null;

		public EssenceSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
			: base(path, scope, essenceKeywords)
		{
			pi = SearchInPackageList(Constants.EssencePackageName);
		}

		internal static void Init()
		{
			pi = SearchInPackageList(Constants.EssencePackageName);
		}

		private const string FAKE_VERSION = "0.0.0";

		internal const string kControllerModelPath = "Assets/Wave/Essence/Controller/Model";
		internal const string kControllerModelPackage = "wave_essence_controller_model.unitypackage";
		internal const string kInputModulePath = "Assets/Wave/Essence/InputModule";
		internal const string kInputModulePackage = "wave_essence_inputmodule.unitypackage";
		internal const string kHandModelPath = "Assets/Wave/Essence/Hand/Model";
		internal const string kHandModelPackage = "wave_essence_hand_model.unitypackage";
		internal const string kInteractionModePath = "Assets/Wave/Essence/Interaction/Mode";
		internal const string kInteractionModePackage = "wave_essence_interaction_mode.unitypackage";
		internal const string kInteractionToolkitPath = "Assets/Wave/Essence/Interaction/Toolkit";
		internal const string kInteractionToolkitPackage = "wave_essence_interaction_toolkit.unitypackage";
		internal const string kCameraTexturePath = "Assets/Wave/Essence/CameraTexture/";
		internal const string kCameraTexturePackage = "wave_essence_cameratexture.unitypackage";
		internal const string kCompositorLayerPath = "Assets/Wave/Essence/CompositorLayer";
		internal const string kCompositorLayerPackage = "wave_essence_compositorlayer.unitypackage";
		internal const string kBundlePreviewPath = "Assets/Wave/Essence/BundlePreview";
		internal const string kBundlePreviewPackage = "wave_essence_bundlepreview.unitypackage";
		internal const string kRenderDocPath = "Assets/Wave/Essence/RenderDoc/";
		internal const string kRenderDocPackage = "wave_essence_renderdoc.unitypackage";

		internal static bool featureControllerModelImported = false;
		internal static bool featureInputModuleImported = false;
		internal static bool featureHandModelImported = false;
		internal static bool featureInteractionModeImported = false;
		internal static bool featureInteractionToolkitImported = false;
		internal static bool featureCameraTextureImported = false;
		internal static bool featureCompositorLayerImported = false;
		internal static bool featureBundlePreviewImported = false;
		internal static bool featureRenderDocImported = false;

		internal static bool featureControllerModelNeedUpdate = false;
		internal static bool featureInputModuleNeedUpdate = false;
		internal static bool featureHandModelNeedUpdate = false;
		internal static bool featureInteractionModeNeedUpdate = false;
		internal static bool featureInteractionToolkitNeedUpdate = false;
		internal static bool featureCameraTextureNeedUpdate = false;
		internal static bool featureCompositorLayerNeedUpdate = false;
		internal static bool featureBundlePreviewNeedUpdate = false;
		internal static bool featureRenderDocNeedUpdate = false;

		internal static bool hasFeatureNeedUpdate = false;

		internal static bool checkFeaturePackages()
		{
			featureControllerModelImported = Directory.Exists(kControllerModelPath);
			featureInputModuleImported = Directory.Exists(kInputModulePath);
			featureHandModelImported = Directory.Exists(kHandModelPath);
			featureInteractionModeImported = Directory.Exists(kInteractionModePath);
			featureInteractionToolkitImported = Directory.Exists(kInteractionToolkitPath);
			featureCameraTextureImported = Directory.Exists(kCameraTexturePath);
			featureCompositorLayerImported = Directory.Exists(kCompositorLayerPath);
			featureBundlePreviewImported = Directory.Exists(kBundlePreviewPath);
			featureRenderDocImported = Directory.Exists(kRenderDocPath);

			featureControllerModelNeedUpdate = featureControllerModelImported && !Directory.Exists(kControllerModelPath + "/" + pi.version) &&
				!Directory.Exists(kControllerModelPath + "/" + FAKE_VERSION);
			featureInputModuleNeedUpdate = featureInputModuleImported && !Directory.Exists(kInputModulePath + "/" + pi.version) &&
				!Directory.Exists(kInputModulePath + "/" + FAKE_VERSION);
			featureHandModelNeedUpdate = featureHandModelImported && !Directory.Exists(kHandModelPath + "/" + pi.version) &&
				!Directory.Exists(kHandModelPath + "/" + FAKE_VERSION);
			featureInteractionModeNeedUpdate = featureInteractionModeImported && !Directory.Exists(kInteractionModePath + "/" + pi.version) &&
				!Directory.Exists(kInteractionModePath + "/" + FAKE_VERSION);
			featureInteractionToolkitNeedUpdate = featureInteractionToolkitImported && !Directory.Exists(kInteractionToolkitPath + "/" + pi.version) &&
				!Directory.Exists(kInteractionToolkitPath + "/" + FAKE_VERSION);
			featureCameraTextureNeedUpdate = featureCameraTextureImported && !Directory.Exists(kCameraTexturePath + "/" + pi.version) &&
				!Directory.Exists(kCameraTexturePath + "/" + FAKE_VERSION);
			featureCompositorLayerNeedUpdate = featureCompositorLayerImported && !Directory.Exists(kCompositorLayerPath + "/" + pi.version) &&
				!Directory.Exists(kCompositorLayerPath + "/" + FAKE_VERSION);
			featureBundlePreviewNeedUpdate = featureBundlePreviewImported && !Directory.Exists(kBundlePreviewPath + "/" + pi.version) &&
				!Directory.Exists(kBundlePreviewPath + "/" + FAKE_VERSION);
			featureRenderDocNeedUpdate = featureRenderDocImported && !Directory.Exists(kRenderDocPath + "/" + pi.version) &&
				!Directory.Exists(kRenderDocPath + "/" + FAKE_VERSION);

			hasFeatureNeedUpdate = featureControllerModelNeedUpdate || featureInputModuleNeedUpdate || featureHandModelNeedUpdate || featureInteractionModeNeedUpdate || featureInteractionToolkitNeedUpdate ||
				featureCameraTextureNeedUpdate || featureCompositorLayerNeedUpdate || featureBundlePreviewNeedUpdate || featureRenderDocNeedUpdate;

			return hasFeatureNeedUpdate;
		}

		public static void UpdateAllModules()
		{
			checkFeaturePackages();
			if (featureControllerModelNeedUpdate)
				UpdateModule(kControllerModelPath, kControllerModelPackage);
			if (featureInputModuleNeedUpdate)
				UpdateModule(kInputModulePath, kInputModulePackage);
			if (featureHandModelNeedUpdate)
				UpdateModule(kHandModelPath, kHandModelPackage);
			if (featureInteractionModeNeedUpdate)
				UpdateModule(kInteractionModePath, kInteractionModePackage);
			if (featureCameraTextureNeedUpdate)
				UpdateModule(kCameraTexturePath, kCameraTexturePackage);
			if (featureCompositorLayerNeedUpdate)
				UpdateModule(kCompositorLayerPath, kCompositorLayerPackage);
			if (featureBundlePreviewNeedUpdate)
				UpdateModule(kBundlePreviewPath, kBundlePreviewPackage);
			if (featureRenderDocNeedUpdate)
				UpdateModule(kRenderDocPath, kRenderDocPackage);
			if (featureInteractionToolkitNeedUpdate)
				UpdateModule(kInteractionToolkitPath, kInteractionToolkitPackage);
		}

		public override void OnGUI(string searchContext)
		{
			bool hasKeyword = false;
			bool showControllerModel = searchContext.Contains("Controller");
			bool showInputModule = searchContext.Contains("InputModule");
			bool showHandModel = searchContext.Contains("Hand");
			bool showInteractionMode = searchContext.Contains("Interaction");
			bool showCameraTexture = searchContext.Contains("CameraTexture");
			bool showCompositorLayer = false;
			bool showBundlePreview = false;
			bool showRenderDoc = searchContext.Contains("RenderDoc");
			bool showInteractionToolkit = searchContext.Contains("Interaction");

			if (showControllerModel ||
				showInputModule ||
				showHandModel ||
				showInteractionMode ||
				showCameraTexture ||
				showCompositorLayer ||
				showBundlePreview ||
				showRenderDoc ||
				showInteractionToolkit)
			{
				hasKeyword = true;
			}

			/**
             * GUI layout of features.
             * 1. Controller Model
             * 2. Input Module
             * 3. Hand Model
			 * 4. Ineraction Mode
             * 5. Camera Texture
			 * 6. Compositor Layer
			 * 7. BundlePreview
             * 8. RenderDoc
             * 9. Interaction Toolkit
            **/

			checkFeaturePackages();

			GUILayout.BeginVertical(EditorStyles.helpBox);
			{
				GUILayout.Label("Check Packages", EditorStyles.boldLabel);
				GUILayout.Label("Checking if any packges need update.", EditorStyles.label);
				GUILayout.Space(5f);
				if (GUILayout.Button("Check packages", GUILayout.ExpandWidth(false)))
					EssenseSettingsConfigDialog.ShowDialog();
				GUILayout.Space(5f);
			}
			GUILayout.EndVertical();

			if (!PackageInfo.IsImporting &&
				!featureControllerModelImported && !IsControllerModelPackageOnceImported())
			{
				PackageInfo.IsImporting = true;
				UpdateAssetControllerModel(true);
				ImportModule(kControllerModelPackage);
			}
			if (showControllerModel || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Controller Model", EditorStyles.boldLabel);
					GUILayout.Label("This feature is imported by default.\n\n" +
						"This package provides features of render model, button effect and controller tips. \n" +
						"Please import XR interaction toolkit and refer Demo scene to check how to use it.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/Controller/Model.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !featureControllerModelImported || featureControllerModelNeedUpdate;
					if (featureControllerModelNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - Controller Model", GUILayout.ExpandWidth(false)))
							UpdateModule(kControllerModelPath, kControllerModelPackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - Controller Model", GUILayout.ExpandWidth(false)))
							ImportModule(kControllerModelPackage);
					}
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

			if (!PackageInfo.IsImporting &&
				!featureInputModuleImported && !IsInputModulePackageOnceImported())
			{
				PackageInfo.IsImporting = true;
				UpdateAssetInputModule(true);
				ImportModule(kInputModulePackage);
			}
			if (showInputModule || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Input Module", EditorStyles.boldLabel);
					GUILayout.Label("This feature is imported by default.\n\n" +
						"The Input Module feature provides a controller input module and a gaze input module. In the demo you will see how to interact with scene objects.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/InputModule.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !featureInputModuleImported || featureInputModuleNeedUpdate;
					if (featureInputModuleNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - Input Module", GUILayout.ExpandWidth(false)))
							UpdateModule(kInputModulePath, kInputModulePackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - Input Module", GUILayout.ExpandWidth(false)))
							ImportModule(kInputModulePackage);
					}
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

			if (!PackageInfo.IsImporting &&
				!featureHandModelImported && !IsHandModelPackageOnceImported())
			{
				PackageInfo.IsImporting = true;
				UpdateAssetHandModel(true);
				ImportModule(kHandModelPackage);
			}
			if (showHandModel || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Hand Model", EditorStyles.boldLabel);
					GUILayout.Label("This feature is imported by default.\n\n" +
						"The Hand Model feature provides the models of hand.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/Hand/Model.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !featureHandModelImported || featureHandModelNeedUpdate;
					if (featureHandModelNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - Hand Model", GUILayout.ExpandWidth(false)))
							UpdateModule(kHandModelPath, kHandModelPackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - Hand Model", GUILayout.ExpandWidth(false)))
							ImportModule(kHandModelPackage);
					}
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

			if (!PackageInfo.IsImporting &&
				featureControllerModelImported && featureInputModuleImported && featureHandModelImported &&
				!featureInteractionModeImported && !IsInteractionModePackageOnceImported())
			{
				PackageInfo.IsImporting = true;
				UpdateAssetInteractionMode(true);
				ImportModule(kInteractionModePackage);
			}
			if (showInteractionMode || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Interaction Mode", EditorStyles.boldLabel);
					GUILayout.Label("This feature is imported by default.\n" +
						"If you want to import this feature manually, you have to import \"Controller Model\", \"Input Module\" and \"Hand Model\" first.\n\n" +
						"There are three modes provided by Wave plugin: \n" +
						"- Gaze: A player will use gaze for interaction.\n" +
						"- Controller: A player will use controllers for interaction.\n" +
						"- Hand: A player will use his hands for interaction.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/Interaction/Mode.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = (!featureInteractionModeImported || featureInteractionModeNeedUpdate) && featureControllerModelImported && featureInputModuleImported && featureHandModelImported;
					if (featureInteractionModeNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - Interaction Mode", GUILayout.ExpandWidth(false)))
							UpdateModule(kInteractionModePath, kInteractionModePackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - Interaction Mode", GUILayout.ExpandWidth(false)))
							ImportModule(kInteractionModePackage);
					}
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

			if (showCameraTexture || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				GUILayout.Label("CameraTexture", EditorStyles.boldLabel);
				GUILayout.Label("This feature provides a way to access native camera and pose info.", new GUIStyle(EditorStyles.label) { wordWrap = true });
				GUILayout.Label("The feature will be imported at Assets/Wave/Essence/CameraTexture.", EditorStyles.label);
				GUILayout.Space(5f);
				if (featureCameraTextureNeedUpdate)
				{
					if (GUILayout.Button("Update Feature - CameraTexture", GUILayout.ExpandWidth(false)))
						UpdateModule(kCameraTexturePath, kCameraTexturePackage);
				}
				else
				{
					if (GUILayout.Button("Import Feature - CameraTexture", GUILayout.ExpandWidth(false)))
						ImportModule(kCameraTexturePackage);
				}
				GUILayout.EndVertical();
			}

			if (showCompositorLayer || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Compositor Layer", EditorStyles.boldLabel);
					GUILayout.Label("This feature leverages the Wave Multi-Layer Rendering Architecture to display textures on layers other than the eye buffer.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/CompositorLayer.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !featureCompositorLayerImported || featureCompositorLayerNeedUpdate;
					if (featureCompositorLayerNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - Compositor Layer", GUILayout.ExpandWidth(false)))
							UpdateModule(kCompositorLayerPath, kCompositorLayerPackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - Compositor Layer", GUILayout.ExpandWidth(false)))
							ImportModule(kCompositorLayerPackage);
					}
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

			if (showBundlePreview || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Bundle Preview", EditorStyles.boldLabel);
					GUILayout.Label("Bundle Preview allows you to quickly preview project changes by modularizing the project building process. \n" +
						"Select Wave/BundlePreview in the menu to start using this feature.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/BundlePreview.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !featureBundlePreviewImported || featureBundlePreviewNeedUpdate;
					if (featureBundlePreviewNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - BundlePreview", GUILayout.ExpandWidth(false)))
							UpdateModule(kBundlePreviewPath, kBundlePreviewPackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - BundlePreview", GUILayout.ExpandWidth(false)))
							ImportModule(kBundlePreviewPackage);
					}
					
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

			if (showRenderDoc || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				string renderDocLabel =
					"Developer can check out the graphic's detail problem with RenderDoc profiling tool.  " +
					"This tool is integrated within Wave's XR plugin.  " +
					"In this package, provide a basic class and sample.  " +
					"Because RenderDoc will cost performance, you can remove the imported content after your test.";
				GUILayout.Label("RenderDoc", EditorStyles.boldLabel);
				GUILayout.Label(renderDocLabel, new GUIStyle(EditorStyles.label) { wordWrap = true });
				GUILayout.Space(5f);
				GUILayout.Label("The feature will be imported at Assets/Wave/Essence/RenderDoc.", EditorStyles.label);
				if (featureRenderDocNeedUpdate)
				{
					if (GUILayout.Button("Update RenderDoc tool", GUILayout.ExpandWidth(false)))
						UpdateModule(kRenderDocPath, kRenderDocPackage);
				}
				else
				{
					if (GUILayout.Button("Import RenderDoc tool", GUILayout.ExpandWidth(false)))
						ImportModule(kRenderDocPackage);
				}
				GUILayout.EndVertical();
			}

			if (showInteractionToolkit || !hasKeyword)
			{
				GUILayout.BeginVertical(EditorStyles.helpBox);
				{
					GUILayout.Label("Interaction Toolkit", EditorStyles.boldLabel);
					GUILayout.Label("The Wave Extension of Unity XR Interaction Toolkit.", new GUIStyle(EditorStyles.label) { wordWrap = true });
					GUILayout.Label("The feature will be imported at Assets/Wave/Essence/Interaction/Toolkit.", EditorStyles.label);
					GUILayout.Space(5f);
					GUI.enabled = !featureInteractionToolkitImported | featureInteractionToolkitNeedUpdate;
					if (featureInteractionToolkitNeedUpdate)
					{
						if (GUILayout.Button("Update Feature - Interaction Toolkit", GUILayout.ExpandWidth(false)))
							UpdateModule(kInteractionToolkitPath, kInteractionToolkitPackage);
					}
					else
					{
						if (GUILayout.Button("Import Feature - Interaction Toolkit", GUILayout.ExpandWidth(false)))
							ImportModule(kInteractionToolkitPackage);
					}
					GUILayout.Space(5f);
					GUI.enabled = true;
				}
				GUILayout.EndVertical();
			}

		}

		public static void DeleteFolder(string path)
		{
			if (Directory.Exists(path))
			{
				var files = Directory.GetFiles(path);
				var dirs = Directory.GetDirectories(path);
				foreach (var file in files)
				{
					File.Delete(file);
				}
				foreach (var dir in dirs)
				{
					Directory.Delete(dir, true);
				}
			}
		}

		internal static void UpdateModule(string ModelPath, string packagePath)
		{
			DeleteFolder(ModelPath);
			AssetDatabase.Refresh();
			string target = Path.Combine("Packages/" + Constants.EssencePackageName + "/UnityPackages~", packagePath);
			Debug.Log("Import: " + target);
			AssetDatabase.ImportPackage(target, false);
		}

		internal static void ImportModule(string packagePath)
		{
			string target = Path.Combine("Packages/" + Constants.EssencePackageName + "/UnityPackages~", packagePath);
			Debug.Log("Import: " + target);
			AssetDatabase.ImportPackage(target, false);
		}

		[SettingsProvider]
		static SettingsProvider Create()
		{
			Debug.Log("Create EssenceSettingsProvider");
			return new EssenceSettingsProvider("Project/Wave XR/Essence");
		}

		private static UnityEditor.PackageManager.PackageInfo SearchInPackageList(string packageName)
		{
			var listRequest = Client.List(true);
			do
			{
				if (listRequest.IsCompleted)
				{
					if (listRequest.Result == null)
					{
						Debug.Log("List result: is empty");
						return null;
					}

					foreach (var pi in listRequest.Result)
					{
						//Debug.Log("List has: " + pi.name + " == " + packageName);
						if (pi.name == packageName)
						{
							Debug.Log("Found " + packageName);

							return pi;
						}
					}
					break;
				}
				Thread.Sleep(100);
			} while (true);
			return null;
		}
	} // class EssenceSettingProvider

	[InitializeOnLoad]
	public class PackageInfo : AssetPostprocessor
	{
		public static bool IsImporting = false;

		static PackageInfo()
		{
			Debug.Log("PackageInfo()");
			AssetDatabase.importPackageStarted += OnImportPackageStarted;
			AssetDatabase.importPackageCompleted += OnImportPackageCompleted;
		}

		private static void OnImportPackageStarted(string packagename)
		{
			Debug.Log("OnImportPackageStarted() " + packagename);
		}

		private static void OnImportPackageCompleted(string packagename)
		{
			Debug.Log("OnImportPackageCompleted() " + packagename);
			IsImporting = false;
		}

		public static void ResetToDefaultPackages()
		{
			EssenceSettingsProvider.UpdateAssetControllerModel(false);
			EssenceSettingsProvider.UpdateAssetInputModule(false);
			EssenceSettingsProvider.UpdateAssetHandModel(false);
			EssenceSettingsProvider.UpdateAssetInteractionMode(false);
			EssenceSettingsProvider.DeleteFolder("Assets/Wave/Essence");
			AssetDatabase.Refresh();			
		}
	}

	[InitializeOnLoad]
	public class EssenseSettingsConfigDialog : EditorWindow
	{
		List<Item> items;

		public class Item
		{
			const string currentValue = " (Need update = {0})";

			public delegate bool DelegateIsShow();
			public delegate bool DelegateIsReady();
			public delegate string DelegateGetCurrent();

			public DelegateIsShow IsShow;
			public DelegateIsReady IsReady;
			public DelegateGetCurrent GetCurrent;

			public string title { get; private set; }

			public Item(string title)
			{
				this.title = title;
			}

			// Return true when setting is not ready.
			public bool Show()
			{
				if (IsShow())
					GUILayout.Label(title + string.Format(currentValue, GetCurrent()));
				if (IsReady())
					return false;
				return true;
			}
		}

		static List<Item> GetItems()
		{
			var ControllerModel = new Item("Controller Model")
			{
				IsShow = () => { return EssenceSettingsProvider.featureControllerModelImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureControllerModelNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureControllerModelNeedUpdate.ToString(); },
			};

			var InputModule = new Item("Input Module")
			{
				IsShow = () => { return EssenceSettingsProvider.featureInputModuleImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureInputModuleNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureInputModuleNeedUpdate.ToString(); },
			};

			var HandModel = new Item("Hand Model")
			{
				IsShow = () => { return EssenceSettingsProvider.featureHandModelImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureHandModelNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureHandModelNeedUpdate.ToString(); },
			};

			var InteractionMode = new Item("Interaction Mode")
			{
				IsShow = () => { return EssenceSettingsProvider.featureInteractionModeImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureInteractionModeNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureInteractionModeNeedUpdate.ToString(); },
			};

			var InteractionToolkit = new Item("Interaction Toolkit")
			{
				IsShow = () => { return EssenceSettingsProvider.featureInteractionToolkitImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureInteractionToolkitNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureInteractionToolkitNeedUpdate.ToString(); },
			};

			var CameraTexture = new Item("Camera Texture")
			{
				IsShow = () => { return EssenceSettingsProvider.featureCameraTextureImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureCameraTextureNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureCameraTextureNeedUpdate.ToString(); },
			};

			var CompositorLayer = new Item("Compositor Layer")
			{
				IsShow = () => { return EssenceSettingsProvider.featureCompositorLayerImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureCompositorLayerNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureCompositorLayerNeedUpdate.ToString(); },
			};

			var BundlePreview = new Item("Bundle Preview")
			{
				IsShow = () => { return EssenceSettingsProvider.featureBundlePreviewImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureBundlePreviewNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureBundlePreviewNeedUpdate.ToString(); },
			};

			var RenderDoc = new Item("Render Doc")
			{
				IsShow = () => { return EssenceSettingsProvider.featureRenderDocImported; },
				IsReady = () => { return !EssenceSettingsProvider.featureRenderDocNeedUpdate; },
				GetCurrent = () => { return EssenceSettingsProvider.featureRenderDocNeedUpdate.ToString(); },
			};

			return new List<Item>()
			{
				ControllerModel,
				InputModule,
				HandModel,
				InteractionMode,
				CameraTexture,
				CompositorLayer,
				BundlePreview,
				RenderDoc,
				InteractionToolkit
			};
		}

		static EssenseSettingsConfigDialog window;

		static EssenseSettingsConfigDialog()
		{
			EditorApplication.update += Update;
		}

		public static void ShowDialog()
		{
			EssenceSettingsProvider.Init();
			EssenceSettingsProvider.checkFeaturePackages();
			var items = GetItems();
			UpdateInner(items, true);
		}

		static void Update()
		{
			Debug.Log("Check for Essense Settings Update.");
			EssenceSettingsProvider.Init();
			EssenceSettingsProvider.checkFeaturePackages();
			var items = GetItems();
			UpdateInner(items, false);

			EditorApplication.update -= Update;
		}

		public static void UpdateInner(List<Item> items, bool forceShow)
		{
			bool show = forceShow;
			if (!forceShow)
			{
				foreach (var item in items)
				{
					show |= !item.IsReady();
				}
			}

			if (show)
			{
				window = GetWindow<EssenseSettingsConfigDialog>(true);
				window.minSize = new Vector2(480, 240);
				window.items = items;
			}
		}

		Vector2 scrollPosition;

		public void OnGUI()
		{
			if (items == null)
				return;

			scrollPosition = GUILayout.BeginScrollView(scrollPosition);

			int notReadyItems = 0;
			GUILayout.Label("List imported packages :", EditorStyles.boldLabel);
			foreach (var item in items)
			{
				if(item.Show())
					notReadyItems++;
			}

			GUILayout.EndScrollView();

			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Space(5f);
				GUILayout.Label("Reset to Default Packages", EditorStyles.boldLabel);
				if (GUILayout.Button("Reset to default packages", GUILayout.ExpandWidth(false)))
				{
					if (EditorUtility.DisplayDialog("Reset to Default Packages", "Are you sure?", "Yes, Reset to Default Packages", "Cancel"))
					{
						PackageInfo.ResetToDefaultPackages();
						Close();
					}
				}
				GUILayout.Space(5f);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (notReadyItems > 0)
			{
				if (GUILayout.Button("Update All"))
				{
					EssenceSettingsProvider.UpdateAllModules();
					EditorUtility.DisplayDialog("Update All", "Update all packages!", "Ok");
					Close();
				}
			}
			else
			{
				if (GUILayout.Button("Close"))
					Close();
			}
			GUILayout.EndHorizontal();
		}
	}
}
#endif
