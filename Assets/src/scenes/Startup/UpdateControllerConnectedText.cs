using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class UpdateControllerConnectedText : MonoBehaviour
{
    public float updateClockInterval = 0.01f;

    public void OnNumControllersChanged(int connectedDevices)
    {
        SetControllerText(connectedDevices);
        CheckReadyState();
    }

    public void Update()
    {
        if (startTime < 0.0f)
        {
            return;
        }

        if (startTime < Time.time)
        {
            Debug.Log("START GAME");
            startTime = -1.0f;
        }

        if (Time.time - lastTextUpdate > updateClockInterval)
        {
            this.GetComponent<Text>().text = (startTime - Time.time) + " Seconds left";
            lastTextUpdate = Time.time;
        }
    }

    public void SetCharacterPairing(System.Collections.Generic.Dictionary<string, object> data)
    {
        InputDevice device = (InputDevice)data["controller"];
        string name = (string)data["character"];
        characterMapping[device] = name;
        CheckReadyState();
    }

    public void RemoveCharacterForDevice(InputDevice device)
    {
        if (characterMapping.ContainsKey(device))
        {
            characterMapping.Remove(device);
        }
        CheckReadyState();
    }

    private void CheckReadyState()
    {
        if (InputManager.Devices.Count > 0)
            if (characterMapping.Count == InputManager.Devices.Count)
            {
                startTime = Time.time + 5.0f;
            }
            else
            {
                startTime = -1.0f;
                SetControllerText(InputManager.Devices.Count);
            }
    }

    private void SetControllerText(int connectedDevices)
    {
        if (connectedDevices == 0)
        {
            this.GetComponent<Text>().text = "No Controllers Found";
        }
        else if (connectedDevices == 1)
        {
            this.GetComponent<Text>().text = "1 Controller Found";
        }
        else if (connectedDevices > 1)
        {
            this.GetComponent<Text>().text = connectedDevices + " Controllers Found";
        }
    }

    private float startTime = -1.0f;
    private float lastTextUpdate = -1.0f;
    private System.Collections.Generic.Dictionary<InputDevice, string> characterMapping = new System.Collections.Generic.Dictionary<InputDevice, string>();
}
