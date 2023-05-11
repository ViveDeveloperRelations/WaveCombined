using UnityEngine;

public class Explode : MonoBehaviour
{
	public float explosionRadius = 2.5f;
	public float explosionForce = 50.0f;
	public float period = 5;  // seconds
	public float timeDelay = 0;
	float accTime = 0;
	ParticleSystem system;

	private void Start()
	{
		accTime = period - timeDelay;
	}

	public void Reset()
	{
		accTime = period - timeDelay;
	}

	public void OnEnable()
	{
		system = GetComponent<ParticleSystem>();
	}

	void Update()
    {
		accTime += Time.deltaTime;
		if (accTime < period)
			return;

		accTime = 0;

		Collider[] objects = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider h in objects)
		{
			Rigidbody r = h.GetComponent<Rigidbody>();
			if (r != null)
			{
				r.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
			}
		}

		if (system)
			system.Play();

		if (period == 0)
			enabled = false;
	}
}
