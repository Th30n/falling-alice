using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float windPower = 5.0f;
	public float maxSpeed = 10.0f;

	public Transform clouds;
	public float cloudSpeed = 2.0f;
	public float fadeSpeed = 0.1f;
	public float maxVolume = 0.8f;

	public bool dragControls = false;
	public AudioClip windSound;
	private AudioSource audioSource_;
	private float audioVolume_ = 0.0f;
	private Vector3 dragStartPos_ = Vector3.zero;
	private bool addForce_ = false;
	private Vector2 windForce_ = Vector2.zero;

	private Rigidbody2D rb_;
	private Animator animator_;

	// Use this for initialization
	void Start () {
		rb_ = gameObject.GetComponent<Rigidbody2D>();
		audioSource_ = GetComponent<AudioSource> ();
		animator_ = GetComponent<Animator> ();
	}

	void Update() {
		if (dragControls) {
			dragControl();
		}
	}
	
	void FixedUpdate () {
		// Debug.Log (rb_.velocity.sqrMagnitude);
		if (!dragControls) {
			clickControl ();
		} else if (addForce_) {
			rb_.AddForce(windForce_);
			addForce_ = false;
		}
		if (rb_.velocity.magnitude >= maxSpeed) {
			rb_.velocity = Vector2.ClampMagnitude(rb_.velocity, maxSpeed);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Powerup") {
			//GameObject.Destroy(other.gameObject);
		}
	}

	void clickControl() {
		if (Input.GetButton ("Fire1")) {
			Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 windDir = gameObject.transform.position - mp;
			Vector2 forceDir = new Vector2 (windDir.x, windDir.y).normalized;
			rb_.AddForce (forceDir * windPower);
			clouds.Translate(windDir.normalized * Time.deltaTime * cloudSpeed);
			fadeIn();
			animate(forceDir);
		} else {
			fadeOut();
			stopWindAnimating();
		}
	}

	void dragControl() {
		if (Input.GetButtonDown ("Fire1")) {
			dragStartPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if (Input.GetButtonUp ("Fire1")) {
			Vector3 mp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 windDir = mp - dragStartPos_;
			float power = windDir.magnitude;
			Vector2 forceDir = new Vector2(windDir.x, windDir.y).normalized;
			addForce_ = true;
			windForce_ = forceDir * windPower * power;
			rb_.AddForce(forceDir * windPower * power);
			AudioSource.PlayClipAtPoint(windSound, dragStartPos_);
		}
	}

	private void fadeIn() {
		if (!audioSource_.isPlaying) {
			audioSource_.clip = windSound;
			audioSource_.loop = true;
			audioSource_.Play();
		}
		audioVolume_ = Mathf.Min (maxVolume, audioVolume_ + fadeSpeed * Time.deltaTime);
		audioSource_.volume = audioVolume_;
	}

	private void fadeOut() {
		audioVolume_ = Mathf.Max (0.0f, audioVolume_ - fadeSpeed * Time.deltaTime);
		audioSource_.volume = audioVolume_;
		if (audioVolume_ == 0.0f) {
			audioSource_.Stop();
		}
	}

	private void animate(Vector2 windDir) {
		stopWindAnimating ();
		if (Mathf.Abs(windDir.x) > Mathf.Abs(windDir.y)) {
			if (windDir.x > 0) {
				animator_.SetBool ("wind_right", true);
			} else {
				animator_.SetBool("wind_left", true);
			}
		} else {
			if (windDir.y > 0) {
				animator_.SetBool("wind_up", true);
			} else {
				animator_.SetBool ("wind_down", true);
			}
		}
	}
	private void stopWindAnimating() {
		animator_.SetBool ("idle_anim", true);
		animator_.SetBool ("wind_right", false);
		animator_.SetBool("wind_left", false);
		animator_.SetBool("wind_up", false);
		animator_.SetBool ("wind_down", false);
	}
}
