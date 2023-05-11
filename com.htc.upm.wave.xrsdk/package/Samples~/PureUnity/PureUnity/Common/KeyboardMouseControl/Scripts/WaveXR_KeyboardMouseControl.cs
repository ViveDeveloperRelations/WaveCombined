using UnityEngine;
using UnityEngine.SpatialTracking;
#if XR_INTERACTION_TOOLKIT
using UnityEngine.XR.Interaction.Toolkit;
#endif

namespace Wave.XR.Sample.KMC
{
	// Control Camera.main and controllers by keyboard and mouse.
	// Use WASDQE key to move, and use mouse right button to look around.
	// Use ZXC key to switch left controller, Camera.main, right controller.
	// Some code wre copied from Unity's Standard Assets package
	public class WaveXR_KeyboardMouseControl : MonoBehaviour
	{
		private WaveXR_PoseProviderForKMC poseProviderHMD = null;
		private WaveXR_PoseProviderForKMC poseProviderCtrlL = null;
		private WaveXR_PoseProviderForKMC poseProviderCtrlR = null;

		public class SimulatedPose
		{
			public SimulatedPose()
			{
				m_AccPos = Vector3.zero;
				m_AccRot = m_RotHorizontal = m_RotVertical = Quaternion.identity;
			}

			public Quaternion m_RotHorizontal;
			public Quaternion m_RotVertical;
			public Quaternion m_AccRot;
			public Vector3 m_AccPos;
		}

		SimulatedPose poseCurrent = null;

		public SimulatedPose PoseHMD { get; } = new SimulatedPose();
		public SimulatedPose PoseCtrlL { get; } = new SimulatedPose();
		public SimulatedPose PoseCtrlR { get; } = new SimulatedPose();

		public int CurrentTarget { get; private set; } // 0 HMD, 1 Left Ctontroller, 2 Right Ctontroller

		public float XRotSensitivity = 2f;
		public float YRotSensitivity = 2f;
		public float MoveSensitivity = 5f;

		public float MinimumX = -90F;
		public float MaximumX = 90F;

		public Vector3 MovePosition(SimulatedPose pose)
		{
			bool shift = WXRInput.GetKey(KeyCode.LeftShift);
			float xPos = -(WXRInput.GetKey("a") ? 1 : 0) + (WXRInput.GetKey("d") ? 1 : 0);
			float yPos = -(WXRInput.GetKey("q") ? 1 : 0) + (WXRInput.GetKey("e") ? 1 : 0);
			float zPos = (WXRInput.GetKey("w") ? 1 : 0) - (WXRInput.GetKey("s") ? 1 : 0);

			pose.m_AccPos += pose.m_RotHorizontal * pose.m_RotVertical * new Vector3(xPos, yPos, zPos) * MoveSensitivity * Time.deltaTime * (shift ? 10 : 1);
			return pose.m_AccPos;
		}

		public Quaternion LookRotation(SimulatedPose pose)
		{
			float yRot = xAxis * XRotSensitivity;
			float xRot = yAxis * YRotSensitivity;

			pose.m_RotHorizontal *= Quaternion.Euler(0f, yRot, 0f);
			pose.m_RotVertical *= Quaternion.Euler(-xRot, 0f, 0f);

			pose.m_RotVertical = ClampRotationAroundXAxis(pose.m_RotVertical);
			pose.m_AccRot = pose.m_RotHorizontal * pose.m_RotVertical;
			return pose.m_AccRot;
		}

		Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
			angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}

		public GameObject kmcObjHMD = null;
		public GameObject kmcObjCtrlL = null;
		public GameObject kmcObjCtrlL_HMDMount = null;
		public GameObject kmcObjCtrlR = null;
		public GameObject kmcObjCtrlR_HMDMount = null;
		public GameObject kmcObjCurrent = null;

		private void ApplyToAllTrackedPoseDriver()
		{
			var drivers = FindObjectsOfType<TrackedPoseDriver>();

			foreach (var driver in drivers)
			{
				// For HMD
				if (driver.deviceType == TrackedPoseDriver.DeviceType.GenericXRDevice &&
					driver.poseSource == TrackedPoseDriver.TrackedPose.Center)
				{
					var providerHMD = kmcObjHMD.GetComponent<WaveXR_PoseProviderForKMC>();
					if (providerHMD == null)
					{
						poseProviderHMD = kmcObjHMD.AddComponent<WaveXR_PoseProviderForKMC>();
						poseProviderHMD.poseSource = TrackedPoseDriver.TrackedPose.Center;
						poseProviderHMD.kmc = this;
					}
					driver.poseProviderComponent = poseProviderHMD;

					continue;
				}

				// For controllers
				if (driver.deviceType != TrackedPoseDriver.DeviceType.GenericXRController)
					continue;

				if (driver.poseSource == TrackedPoseDriver.TrackedPose.LeftPose)
				{
					if (kmcObjCtrlL == null)
					{
						kmcObjCtrlL = new GameObject("KMControlPosition_CtrlL");
						poseProviderCtrlL = kmcObjCtrlL.AddComponent<WaveXR_PoseProviderForKMC>();
						poseProviderCtrlL.poseSource = driver.poseSource;
						poseProviderCtrlL.kmc = this;
						kmcObjCtrlL.transform.SetParent(driver.transform.parent, false);

						kmcObjCtrlL_HMDMount = new GameObject("KMC_HMD_MountPoint");
						kmcObjCtrlL_HMDMount.transform.SetParent(kmcObjCtrlL.transform, false);
						kmcObjCtrlL_HMDMount.transform.localPosition = new Vector3(0, 0.1f, -0.3f); // a little behind controller
					}
					driver.poseProviderComponent = poseProviderCtrlL;
				}
				if (driver.poseSource == TrackedPoseDriver.TrackedPose.RightPose)
				{
					if (kmcObjCtrlR == null)
					{
						kmcObjCtrlR = new GameObject("KMControlPosition_CtrlR");
						poseProviderCtrlR = kmcObjCtrlR.AddComponent<WaveXR_PoseProviderForKMC>();
						poseProviderCtrlR.poseSource = driver.poseSource;
						poseProviderCtrlR.kmc = this;
						kmcObjCtrlR.transform.SetParent(driver.transform.parent, false);

						kmcObjCtrlR_HMDMount = new GameObject("KMC_HMD_MountPoint");
						kmcObjCtrlR_HMDMount.transform.SetParent(kmcObjCtrlR.transform, false);
						kmcObjCtrlR_HMDMount.transform.localPosition = new Vector3(0, 0.1f, -0.3f); // a little behind controller
					}
					driver.poseProviderComponent = poseProviderCtrlR;
				}
			}

#if XR_INTERACTION_TOOLKIT
			// Fov XR.Interaction.Toolkit
			var controllers = FindObjectsOfType<XRController>();
			foreach (var controller in controllers)
			{

				if (controller.controllerNode == UnityEngine.XR.XRNode.LeftHand)
				{
					if (kmcObjCtrlL == null)
					{
						kmcObjCtrlL = new GameObject("KMControlPosition_CtrlL");
						poseProviderCtrlL = kmcObjCtrlL.AddComponent<WaveXR_PoseProviderForKMC>();
						poseProviderCtrlL.poseSource = TrackedPoseDriver.TrackedPose.LeftPose;
						poseProviderCtrlL.kmc = this;
						kmcObjCtrlL.transform.SetParent(controller.transform.parent, false);

						kmcObjCtrlL_HMDMount = new GameObject("KMC_HMD_MountPoint");
						kmcObjCtrlL_HMDMount.transform.SetParent(kmcObjCtrlL.transform, false);
						kmcObjCtrlL_HMDMount.transform.localPosition = new Vector3(0, 0.1f, -0.3f); // a little behind controller
					}
					controller.poseProvider = poseProviderCtrlL;
				}
				if (controller.controllerNode == UnityEngine.XR.XRNode.RightHand)
				{
					if (kmcObjCtrlR == null)
					{
						kmcObjCtrlR = new GameObject("KMControlPosition_CtrlR");
						poseProviderCtrlR = kmcObjCtrlR.AddComponent<WaveXR_PoseProviderForKMC>();
						poseProviderCtrlR.poseSource = TrackedPoseDriver.TrackedPose.RightPose;
						poseProviderCtrlR.kmc = this;
						kmcObjCtrlR.transform.SetParent(controller.transform.parent, false);

						kmcObjCtrlR_HMDMount = new GameObject("KMC_HMD_MountPoint");
						kmcObjCtrlR_HMDMount.transform.SetParent(kmcObjCtrlR.transform, false);
						kmcObjCtrlR_HMDMount.transform.localPosition = new Vector3(0, 0.1f, -0.3f); // a little behind controller
					}
					controller.poseProvider = poseProviderCtrlR;
				}
			}
#endif  // XR_INTERACTION_TOOLKIT
		}

		private void AttachHMDTo(int target)
		{
			switch(target)
			{
				case 0:
					Camera.main.transform.SetParent(kmcObjHMD.transform.parent, false);
					kmcObjCurrent = kmcObjHMD;
					poseCurrent = PoseHMD;
					break;
				case 1:
					if (kmcObjCtrlL_HMDMount != null)
						Camera.main.transform.SetParent(kmcObjCtrlL_HMDMount.transform, false);
					kmcObjCurrent = kmcObjCtrlL;
					poseCurrent = PoseCtrlL;
					break;
				case 2:
					if (kmcObjCtrlR_HMDMount != null)
						Camera.main.transform.SetParent(kmcObjCtrlR_HMDMount.transform, false);
					kmcObjCurrent = kmcObjCtrlR;
					poseCurrent = PoseCtrlR;
					break;
				default:
					return;
			}
		}

		#region com.unity.inputsystem
#if ENABLE_INPUT_SYSTEM
		WaveXR_KMCInput kmcInput = null;
		private void CreateKMCInput()
		{
			if (kmcInput == null)
			{
				kmcInput = new WaveXR_KMCInput();
			}
		}
#endif
		#endregion

		private void Awake()
		{
#if ENABLE_INPUT_SYSTEM
			CreateKMCInput();
#endif
		}

		private void OnEnable()
		{
			if (!Application.isEditor)
				enabled = false;

			// Set as Camera.main's parent
			kmcObjHMD = new GameObject("KMControlPosition");

#if ENABLE_INPUT_SYSTEM
			kmcInput.Enable();
#endif
		}

		private void Start()
		{
			poseCurrent = PoseHMD;
			CurrentTarget = 0;
			kmcObjCurrent = kmcObjHMD;

			// Wait all object is ready
			kmcObjHMD.transform.SetParent(Camera.main.transform.parent, false);
			AttachHMDTo(0);
			ApplyToAllTrackedPoseDriver();
		}

#if ENABLE_INPUT_SYSTEM
		private Vector2 mouseAxis = Vector2.zero, mouseAxisEx = Vector2.zero;
		private float axisProportion = .1f;
#endif
		private float xAxis = 0, yAxis = 0;
		private void Update()
		{
#if ENABLE_LEGACY_INPUT_MANAGER
			xAxis = Input.GetAxis("Mouse X");
			yAxis = Input.GetAxis("Mouse Y");
#elif ENABLE_INPUT_SYSTEM
			mouseAxis = kmcInput.mouse.axis.ReadValue<Vector2>() * axisProportion;
			xAxis = -(mouseAxisEx.x - mouseAxis.x);
			yAxis = -(mouseAxisEx.y - mouseAxis.y);
			mouseAxisEx = mouseAxis;
#endif
			if (WXRInput.GetMouseButton(1))
			{
				LookRotation(poseCurrent);
				MovePosition(poseCurrent);

				if (kmcObjCurrent != null)
				{
					kmcObjCurrent.transform.localPosition = poseCurrent.m_AccPos;
					kmcObjCurrent.transform.localRotation = poseCurrent.m_AccRot;
				}
			}
			else
			{
				// Change target
				var target = CurrentTarget;
				if (WXRInput.GetKeyUp("z"))  // left controller
					target = 1;
				if (WXRInput.GetKeyUp("c"))  // right controller
					target = 2;
				if (WXRInput.GetKeyUp("x"))  // hmd
					target = 0;

				if (target != CurrentTarget)
				{
					CurrentTarget = target;
					AttachHMDTo(target);
				}
			}
		}

		private void OnDisable()
		{
#if ENABLE_INPUT_SYSTEM
			kmcInput.Disable();
#endif
		}
	}
}
