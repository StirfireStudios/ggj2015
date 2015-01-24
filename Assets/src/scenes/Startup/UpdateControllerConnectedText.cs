using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class UpdateControllerConnectedText : MonoBehaviour {

	private void OnNumControllersChanged(int connectedDevices) {
		if (connectedDevices == 0) {
			this.GetComponent<Text>().text = "No Controllers Found";
		} else if (connectedDevices == 1) {
			this.GetComponent<Text>().text = "1 Controller Found";
		} else if (connectedDevices > 1) {
			this.GetComponent<Text>().text = connectedDevices + " Controllers Found";
		}
	}
}
