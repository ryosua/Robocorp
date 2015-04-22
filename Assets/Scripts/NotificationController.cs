using UnityEngine;
using System.Collections;

public class NotificationController : MonoBehaviour {

	public GameObject NotificationPanel;
	public GameObject NotificationText;

	private bool showingNotification = false;

	// Use this for initialization
	void Start () {
		// Hide the notification panel.
		SetNotificationPanelAndTextAlpha (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowNotification () {
		if (showingNotification == false) {
			StartCoroutine ("FadeNotificationIn");
		}
	}

	private IEnumerator FadeNotificationOut () {
		for (float f = 1f; f >= 0; f -= 0.01f) {
			SetNotificationPanelAndTextAlpha (f);
			yield return null;
		}
		showingNotification = false;
	}

	private IEnumerator FadeNotificationIn () {
		showingNotification = true;
		for (float f = 0f; f <= 1; f += 0.01f) {
			SetNotificationPanelAndTextAlpha (f);
			yield return null;
		}
		StartCoroutine ("FadeNotificationOut");
	}

	private void SetNotificationPanelAndTextAlpha(float f) {
		// Fade the panel.
		NotificationPanel.GetComponent<CanvasRenderer>().SetAlpha(f);
		
		// Fade the text.
		NotificationText.GetComponent<CanvasRenderer>().SetAlpha(f);
	}
}