using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Transform player;
	public float followSpeed = 2.0f;
	public float startFollowingDistance = 1.0f;
	public float farDistance = 2.0f;

	public Rect dimensions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector2 playerPos = new Vector2 (player.position.x, player.position.y);
		Vector2 cameraPos = new Vector2 (transform.position.x, transform.position.y);
		float distance = Vector2.Distance (playerPos, cameraPos);
		if (distance >= startFollowingDistance) {
			float speed = getSpeed (distance);
			float x = Mathf.Lerp (transform.position.x, player.position.x, speed);
			float y = Mathf.Lerp (transform.position.y, player.position.y, speed);
			Vector3 pos = new Vector3 (x, y, transform.position.z);
			pos.x = Mathf.Min(dimensions.xMax, Mathf.Max(dimensions.xMin, pos.x));
			pos.y = Mathf.Min (dimensions.yMax, Mathf.Max (dimensions.yMin, pos.y));
			gameObject.transform.position = pos;
		}
	}

	float getSpeed (float distance)
	{
		if (distance < farDistance) {
			float speedCoef = distance - startFollowingDistance / farDistance - startFollowingDistance;
			return Time.deltaTime * followSpeed * speedCoef;
		} else {
			return Time.deltaTime * followSpeed;
		}
	}
}
