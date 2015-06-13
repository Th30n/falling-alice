using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour {

	public float maxScale = 1.0f;
	public float minScale = 1.0f;
	public float scale = 0.5f;
	public float rotationAngle = 30.0f;
	public float rotationSpeed = 2.0f;
	public GameObject hidden;

	private AudioSource audio_;
	private SpriteRenderer renderer_;
	private bool isDestroyed_ = false;
	private float leftAngle_;
	private float rightAngle_;
	private float rotSpeed_;

	// Use this for initialization
	void Start () {
		audio_ = GetComponent<AudioSource> ();
		renderer_ = GetComponent<SpriteRenderer> ();
		float initialRotationAngle = transform.localEulerAngles.z;
		leftAngle_ = initialRotationAngle - rotationAngle;
		rightAngle_ = initialRotationAngle + rotationAngle;
		rotSpeed_ = rotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0.0f, 0.0f, rotSpeed_ * Time.deltaTime));
		float angle = transform.localEulerAngles.z;
		if (angle > rightAngle_ || angle < leftAngle_) {
			rotSpeed_ = -rotSpeed_;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !isDestroyed_) {
			renderer_.enabled = false;
			isDestroyed_ = true;
			Vector3 playerScale = other.gameObject.transform.localScale;
			playerScale -= new Vector3(scale, scale, 0.0f);
			if (playerScale.x > minScale && playerScale.x < maxScale) {
				other.gameObject.transform.localScale = playerScale;
			}
			audio_.Play ();
			Destroy(gameObject, audio_.clip.length);
			if (hidden != null) {
				Destroy(hidden);
				hidden = null;
			}
		}
	}
}
