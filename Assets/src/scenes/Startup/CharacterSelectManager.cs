using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
	public float DebouceDelay = 0.25f;
	public float StickThreshold = 0.25f;
	public float DPadThreshold = 0.25f;

	private static string UP_ACTION_NAME = "up";
	private static string DOWN_ACTION_NAME = "down";
	private static string READY_ACTION_NAME = "ready";

	// Use this for initialization
	void Start ()
	{
		portraitImage = transform.FindChild ("Portrait").gameObject.GetComponent<Image> ();
		nameLabel = transform.FindChild ("Name").gameObject.GetComponent<Text> ();
		readyLabel = transform.FindChild ("Ready").gameObject;
		readyLabel.SetActive (false);
		SetCharacter (0);
		ResetTriggerTimes();
	}
	
	public void SetDevice(InputDevice device)
	{
		this.device = device;
	}

	public void Update()
	{
		if (device == null)
		{
			return;
		}
		string action = null;
		int moveIndex = 0;
		if ((device.LeftStick.Y > StickThreshold) || (device.DPadY > DPadThreshold))
		{
			action = UP_ACTION_NAME;
			moveIndex = -1;
		}
		if ((device.LeftStick.Y < -StickThreshold) || (device.DPadY < -DPadThreshold))
		{
			action = DOWN_ACTION_NAME;
			moveIndex = 1;
		}
		if (device.Action1)
		{
			action = READY_ACTION_NAME;
		}

		if (action == null)
		{
			ResetTriggerTimes();
			return;
		}
		if (Time.time - lastTriggered [action] > DebouceDelay)
		{
			int newIndex = currentCharacterIndex + moveIndex;
			if (newIndex < 0)
			{
				newIndex = characters.Length - 1;
			} else if (newIndex >= characters.Length)
			{
				newIndex = 0;
			}
			lastTriggered[action] = Time.time;
			if ((action == UP_ACTION_NAME) || (action == DOWN_ACTION_NAME))
			{
				SetCharacter(newIndex);
			}
			if (action == READY_ACTION_NAME)
			{
				ToggleReady();
			}
		}
	}

	private void SetCharacter(int index) {
		if ((index < 0) || (index >= characters.Length))
		{
			return;
		}
		nameLabel.text = characters[index];
		currentCharacterIndex = index;
	}

	private void ResetTriggerTimes() {
		lastTriggered[UP_ACTION_NAME] = -1.0f;
		lastTriggered[DOWN_ACTION_NAME] = -1.0f;
		lastTriggered[READY_ACTION_NAME] = -1.0f;
	}

	private void ToggleReady()
	{
		characterReady = !characterReady;
		readyLabel.SetActive(characterReady);
		if (characterReady)
		{
			System.Collections.Generic.Dictionary<string, object> data = new System.Collections.Generic.Dictionary<string, object> ();
			data ["controller"] = device;
			data ["character"] = characters [currentCharacterIndex];
			SendMessageUpwards ("SetCharacterPairing", data);
		} else
		{
			SendMessageUpwards ("RemoveCharacterForDevice", device);
		}
	}

	private int currentCharacterIndex = 0;
	private bool characterReady;
	private string[] characters = new string[4]{"Character 1", "Character 2", "Character 3", "Character 4"};
	private InputDevice device;
	private Image portraitImage;
	private Text nameLabel;
	private GameObject readyLabel;
	private System.Collections.Generic.Dictionary<string, float> lastTriggered = new System.Collections.Generic.Dictionary<string, float> ();
}
