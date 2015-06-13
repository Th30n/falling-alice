using UnityEngine;
using System.Collections;

public class BendMotor : MonoBehaviour {

	public float angle = 30.0f;
	public float force = 10.0f;

	private HingeJoint2D hingeJoint_;
	private Rigidbody2D rigidBody_;

	// Use this for initialization
	void Start () {
		hingeJoint_ = GetComponent<HingeJoint2D> ();
		rigidBody_ = hingeJoint_.connectedBody;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hingeJoint_.jointAngle != angle) {
			Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			Vector2 forceDir = new Vector2(dir.x, dir.y).normalized;
			rigidBody_.AddForce(forceDir * force);
		}
	}
}
