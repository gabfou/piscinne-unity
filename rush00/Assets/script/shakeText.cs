using UnityEngine;
using System.Collections;

public class shakeText : MonoBehaviour
{
	public float shake_decay;
	public float shake_intensity;
	public float timeBeforeShake;
	public float timeBetweenShake;

	private Transform _textTransform;
	private Vector3 originPosition;
	private Quaternion originRotation;
  	private float temp_shake_intensity = 0;

	// Use this for initialization
	void Start () {
		_textTransform = transform;
		InvokeRepeating("moveTitle", timeBeforeShake, timeBetweenShake);
		originPosition.y = _textTransform.position.y + 1.5f;
		originPosition.x = _textTransform.position.x + 0.6f;
		originPosition.z = _textTransform.position.z;
		originRotation = _textTransform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (temp_shake_intensity > 0) {
			_textTransform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			_textTransform.rotation = new Quaternion (
				originRotation.x + Random.Range (-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.y + Random.Range (-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.z + Random.Range (-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.w + Random.Range (-temp_shake_intensity, temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		} else {
			transform.position = originPosition;
		}
	}

	void moveTitle() {
		temp_shake_intensity = shake_intensity;
	}
}