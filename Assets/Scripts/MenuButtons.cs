using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

	public AudioClip clickSound;
	public float volume;

	public GameObject credits;
	public Animator aliceAnim;
	public Rigidbody2D aliceBody;
	public float jumpPower = 10.0f;
	public Vector2 jumpDir;
	public float jumpDelay = 0.5f;
	public float jumpDuration = 1.0f;

	private bool isCredits_ = false;
	private bool isStarting_ = false;
	private AsyncOperation asyncLoad_;

	void Start() {
		asyncLoad_ = Application.LoadLevelAsync ("main");
		asyncLoad_.allowSceneActivation = false;
	}

	public void StartGame() {
		if (isStarting_) {
			return;
		}
		isStarting_ = true;
		AudioSource.PlayClipAtPoint (clickSound, transform.position, volume);

		aliceAnim.SetTrigger ("jump");
		Invoke ("jump", jumpDelay);
		//Application.LoadLevel ("main");
	}

	private void jump() {
		aliceBody.isKinematic = false;
		aliceBody.AddForce (jumpDir.normalized * jumpPower);
		Invoke ("nextLevel", jumpDuration);
	}

	private void nextLevel() {
		//Application.LoadLevel ("main");
		asyncLoad_.allowSceneActivation = true;
		isStarting_ = false;
	}

	public void QuitGame() {
		AudioSource.PlayClipAtPoint (clickSound, transform.position, volume);
		Application.Quit ();
	}

	public void Credits() {
		AudioSource.PlayClipAtPoint (clickSound, transform.position, volume);
		isCredits_ = !isCredits_;
		if (isCredits_) {
			credits.SetActive (true);
		} else {
			credits.SetActive (false);
		}
	}
}
