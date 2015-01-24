using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {
	public float DebouceDelay = 0.25f;
	public float StickThreshold = 0.25f;
	public float DPadThreshold = 0.25f;

	private static string UP_NAME = "up";
	private static string DOWN_NAME = "down";

	// Use this for initialization
	void Start () {
		portraitImage = transform.FindChild ("Portrait").gameObject.GetComponent<Image> ();
		nameLabel = transform.FindChild ("Name").gameObject.GetComponent<Text> ();
		SetCharacter (0);
		lastTriggered[UP_NAME] = -1.0f;
		lastTriggered[DOWN_NAME] = -1.0f;

	}
	
	public void SetDevice(InputDevice device) {
		this.device = device;
	}

	public void Update() {
		if (device == null) {
			return;
		}
		string direction = null;
		int moveIndex = 0;
		if ((device.LeftStick.Y > StickThreshold) || (device.DPadY > DPadThreshold)) {
			direction = UP_NAME;
			moveIndex = -1;
		}
		if ((device.LeftStick.Y < -StickThreshold) || (device.DPadY < -DPadThreshold)) {
			direction = DOWN_NAME;
			moveIndex = 1;
		}
		if (direction == null) {
			lastTriggered[UP_NAME] = -1.0f;
			lastTriggered[DOWN_NAME] = -1.0f;
			return;
		}
		if (Time.time - lastTriggered [direction] > DebouceDelay) {
			int newIndex = currentCharacterIndex + moveIndex;
			if (newIndex < 0) {
				newIndex = characters.Length - 1;
			} else if (newIndex >= characters.Length) {
				newIndex = 0;
			}
			lastTriggered[direction] = Time.time;
			SetCharacter(newIndex);
		}
	}

	private void SetCharacter(int index) {
		if ((index < 0) || (index >= characters.Length)) {
			return;
		}
		nameLabel.text = characters[index];
		currentCharacterIndex = index;
	}

	private float GetLastTime(string direction) {
		float lastTime = -1;
		if (lastTriggered.ContainsKey (direction)) {
		}
		return lastTime;
	}

	private int currentCharacterIndex = 0;
	private string[] characters = new string[4]{"Character 1", "Character 2", "Character 3", "Character 4"};
	private InputDevice device;
	private Image portraitImage;
	private Text nameLabel;
	private System.Collections.Generic.Dictionary<string, float> lastTriggered = new System.Collections.Generic.Dictionary<string, float> ();
}
