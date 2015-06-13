using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public Vector3 spawnPoint;
	private GameObject player_;

	// Use this for initialization
	void Start () {
		player_ = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("Triggered");
		//resetPlayer ();
	}

	private void resetPlayer() {
		player_.transform.position = spawnPoint;
		Rigidbody2D rb = player_.GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (0.0f, 0.0f);
	}
}
