using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour {

	public float maxLandingSpeed = 3.0f;

	public Rigidbody2D player;
	private float playerSpeed_;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
		playerSpeed_ = player.velocity.sqrMagnitude;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log ("Landed");
		Debug.Log (playerSpeed_);
		if (playerSpeed_ > maxLandingSpeed * maxLandingSpeed) {
			Debug.Log ("Dead");
		}
	}
}
