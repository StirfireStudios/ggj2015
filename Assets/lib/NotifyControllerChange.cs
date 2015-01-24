using UnityEngine;
using System.Collections;
using InControl;

public class NotifyControllerChange : MonoBehaviour {

	public void Start () {
		InputManager.OnDeviceAttached += OnDeviceAttached;
		InputManager.OnDeviceDetached += OnDeviceDetached;
	}

	public void OnDeviceAttached(InputDevice device) {
		this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
	}

	public void OnDeviceDetached(InputDevice device) {
		this.SendMessage("OnNumControllersChanged", InputManager.Devices.Count, SendMessageOptions.DontRequireReceiver);
	}
}
