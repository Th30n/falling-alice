using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

	public SpringJoint2D breakPoint;
	public DistanceJoint2D breakDistJoint;
	public float breakScale = 1.0f;
	public AudioClip breakSound;
	public float breakVolume;

	private SpringJoint2D breakJoint_;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Collision on bridge");
		if (coll.gameObject.tag == "Player" && coll.gameObject.transform.localScale.x >= breakScale) {
			breakBridge();
		}
	}

	void breakBridge() {
		if (breakPoint.enabled) {
			breakPoint.enabled = false;
			breakDistJoint.enabled = false;
			AudioSource.PlayClipAtPoint (breakSound, breakPoint.transform.position, breakVolume);
		}
	}
}
