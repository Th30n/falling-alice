using UnityEngine;
using System.Collections;

public class GroundHit : MonoBehaviour {

	public AudioClip[] groundHit;
	public float volume = 0.6f;
	public float cooldown = 1.0f;

	private float hitTime_ = 0.0f;
	private Animator animator_;

	// Use this for initialization
	void Start () {
		animator_ = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Background" && Time.time > hitTime_ + cooldown) {
			int pickIx = pickSound();
			AudioSource.PlayClipAtPoint(groundHit[pickIx], transform.position, volume);
			animator_.SetTrigger("landing");
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Background") {
			hitTime_ = Time.time;
		}
	}

	private int pickSound() {
		int ix = Mathf.RoundToInt (Random.value * groundHit.Length) - 1;
		return Mathf.Max (0, ix);
	}
}
