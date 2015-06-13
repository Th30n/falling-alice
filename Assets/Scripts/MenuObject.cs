using UnityEngine;
using System.Collections;

public class MenuObject : MonoBehaviour {

	public GameObject menu;
	public GameObject endText;
	public float endDelay = 1.0f;

	private bool displayMenu_ = false;
	private bool end_ = false;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			end_ = true;
			Invoke("end", endDelay);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !end_) {
			displayMenu_ = !displayMenu_;
			if (displayMenu_) {
				Time.timeScale = 0.0f;
				menu.SetActive(true);
			} else {
				Time.timeScale = 1.0f;
				menu.SetActive(false);
			}
		}
	}

	public void Restart() {
		Time.timeScale = 1.0f;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void BackToMenu() {
		Time.timeScale = 1.0f;
		Application.LoadLevel ("main-menu");
	}

	private void end() {
		Time.timeScale = 0.0f;
		menu.SetActive(true);
		endText.SetActive(true);
	}
}
