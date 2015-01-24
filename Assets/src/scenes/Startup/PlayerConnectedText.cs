using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class PlayerConnectedText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		if (connectedDevices != InputManager.Devices.Count) {
			UpdateText();
		}
	}

	private void UpdateText() {
		connectedDevices = InputManager.Devices.Count;
		if (connectedDevices == 0) {
			this.GetComponent<Text>().text = "No Controllers Found";
		} else if (connectedDevices == 1) {
			this.GetComponent<Text>().text = "1 Controller Found";
		} else if (connectedDevices > 1) {
			this.GetComponent<Text>().text = connectedDevices + " Controllers Found";
		}
	}

	private int connectedDevices = 0;
}
