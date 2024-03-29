﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NotificationController : MonoBehaviour {

	public GameObject NotificationPanel;
	public Text NotificationText;

	private bool showingNotification = false;

	// Use this for initialization
	void Start () {

		// Hide the notification panel.
		SetNotificationPanelAndTextAlpha (0);

		//ShowNotification ("Test");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNotification (string text) {
		// Change the text.
		NotificationText.text = text;

		if (showingNotification == false) {
			StartCoroutine ("FadeNotificationIn");
		}
	}

	public void CallFadeOut() {
	}

	private IEnumerator FadeNotificationOut () {

		// wait to fade out
		yield return new WaitForSeconds(1.3f);

		for (float f = 1f; f >= 0; f -= 0.02f) {
			SetNotificationPanelAndTextAlpha (f);
			yield return null;
		}
		showingNotification = false;
	}

	private IEnumerator FadeNotificationIn () {
		showingNotification = true;
		for (float f = 0f; f <= 1; f += 0.02f) {
			SetNotificationPanelAndTextAlpha (f);
			yield return null;
		}

		// call fade out function
		StartCoroutine("FadeNotificationOut");
	}

	private void SetNotificationPanelAndTextAlpha(float f) {
		// Fade the panel.
		NotificationPanel.GetComponent<CanvasRenderer>().SetAlpha(f);
		
		// Fade the text.
		NotificationText.GetComponent<CanvasRenderer>().SetAlpha(f);

	}
}