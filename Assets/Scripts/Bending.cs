using UnityEngine;
using System.Collections;

public class Bending : MonoBehaviour {

	public float bendForce = 10.0f;
	public float minForce = 10.0f;
	public float maxForce = 50.0f;
	public float resetTime = 2.0f;

	private Rigidbody2D rb_;

	// Use this for initialization
	void Start () {
		rb_ = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("coll");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Vector3 dir = transform.position - other.transform.position;
			Vector2 forceDir = new Vector2(dir.x, dir.y).normalized;
			Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
			float power = Mathf.Min (playerRb.velocity.magnitude * bendForce, maxForce);
			power = Mathf.Max(power, minForce);
			Vector2 force = power * forceDir;
			rb_.AddForce(force, ForceMode2D.Impulse);
		}
	}
}
