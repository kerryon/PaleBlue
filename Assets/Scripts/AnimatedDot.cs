using UnityEngine;

public class AnimatedDot : MonoBehaviour
{

	private Vector3 originPosition;
	public float shake_intensity = .07f;

	void Update()
	{

		Shake();
		transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
	}

	void Shake()
	{
		originPosition = transform.position;
	}
}