using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class MultiGameObjectRecorder : MonoBehaviour
{
	// Serialized field can't put in UNITY_EDITOR or error
	public GameObject target;
	public AnimationClip clip;
#if UNITY_EDITOR
	private GameObjectRecorder m_Recorder;
#endif

#if UNITY_EDITOR
	void Start()
	{
		// Create recorder and record the script GameObject.
		m_Recorder = new GameObjectRecorder(target);

		// Bind all the Transforms on the GameObject and all its children.
		m_Recorder.BindComponentsOfType<Transform>(target, true);
	}

	void LateUpdate()
	{
		if (clip == null)
			return;

		// Take a snapshot and record all the bindings values for this frame.
		m_Recorder.TakeSnapshot(Time.deltaTime);
	}

	void OnDisable()
	{
		if (clip == null)
			return;

		if (m_Recorder.isRecording)
		{
			// Save the recorded session to the clip.
			m_Recorder.SaveToClip(clip);
		}
	}
#endif
}


/*

	public GameObject target;
	public string clipName = "clip";
	public int count = 0;

	public class Record
	{
		public GameObject gameObject;
		public AnimationClip clip;
		public GameObjectRecorder recorder;
	}

	public List<Record> records;

	void FindChildren(List<GameObject> list, Transform parent)
	{
		if (parent.TryGetComponent(out MeshRenderer re))
		{
			if (re.enabled && !parent.gameObject.isStatic)
			{
				list.Add(parent.gameObject);
			}
		}
		int childCount = parent.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = parent.GetChild(i);
			FindChildren(list, child);
		}
	}

	void Start()
	{
		// Find MeshRenderers
		List<GameObject> targets = new List<GameObject>();
		FindChildren(targets, target.transform);

		records = new List<Record>();
		foreach (var obj in targets)
		{
			var record = new Record()
			{
				gameObject = obj,
				clip = new AnimationClip(),
				recorder = new GameObjectRecorder(obj)
			};
			record.clip.ClearCurves();
			record.clip.name = obj.name;
			record.recorder.BindComponentsOfType<Transform>(obj, true);
			records.Add(record);
		}

		count = records.Count;
		if (records.Count == 0)
		{
			records = null;
			enabled = false;
		}
	}

	void LateUpdate()
	{
		// Take a snapshot and record all the bindings values for this frame.
		foreach (var record in records)
		{
			record.recorder.TakeSnapshot(Time.deltaTime);
		}
	}

	private void OnEnable()
	{
	}

	void OnDisable()
	{
		if (records == null)
			return;

		int i = 0;
		foreach (var record in records)
		{
			record.recorder.SaveToClip(record.clip);
			AssetDatabase.CreateAsset(record.clip, "Assets/AnimationClips/" + clipName + i + ".asset");
			i++;
		}
		AssetDatabase.SaveAssets();
	}
*/
