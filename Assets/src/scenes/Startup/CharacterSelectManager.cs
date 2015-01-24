using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {
	public float DebouceDelay = 0.25f;
	public float StickThreshold = 0.25f;
	public float DPadThreshold = 0.25f;

	// Use this for initialization
	void Start () {
		portraitImage = transform.FindChild ("Portrait").gameObject.GetComponent<Image> ();
		nameLabel = transform.FindChild ("Name").gameObject.GetComponent<Text> ();
		nameLabel.text = characters[0];
	}
	
	public void SetDevice(InputDevice device) {
		this.device = device;
	}

	public void Update() {
		if (device == null) {
			return;
		}
		if ((device.LeftStick.Y > StickThreshold) || (device.DPadY > DPadThreshold)) {
			Debug.Log ("UP");
		}
		if ((device.LeftStick.Y < -StickThreshold) || (device.DPadY < -DPadThreshold)) {
			Debug.Log ("DOWN");
		}
		// check left stick.
		// check dpad
	}

	private string[] characters = new string[4]{"Character 1", "Character 2", "Character 3", "Character 4"};
	private InputDevice device;
	private Image portraitImage;
	private Text nameLabel;
	private System.Collections.Generic.Dictionary<string, float> lastTriggered = new System.Collections.Generic.Dictionary<string, float> ();
}
