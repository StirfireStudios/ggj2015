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

    GGJ.GameConfig gc = GGJ.GameConfig.Instance;

    public void Update()
    {
        if (startTime < 0.0f)
        {
            return;
        }

        if (startTime < Time.time)
        {
            Debug.Log("START GAME");

            Application.LoadLevel("Testbench");
             
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
		GGJ.Data.CharacterInfo.Type character = (GGJ.Data.CharacterInfo.Type)data["character"];

		gc.SetCharacterForDevice (device, character);
        CheckReadyState();
    }

    public void RemoveCharacterForDevice(InputDevice device)
    {
		gc.RemoveCharacterForDevice(device);
        CheckReadyState();
    }

    public void OnKeyboardEnabled()
    {
        keyboardEnabled = true;
        SetControllerText(InputManager.Devices.Count);
    }

    public void OnKeyboardDisabled()
    {
        keyboardEnabled = false;
        SetControllerText(InputManager.Devices.Count);
    }

    private void CheckReadyState()
    {
		if (
              ((gc.NumberOfPlayers == InputManager.Devices.Count) && (keyboardEnabled)) ||
              ((gc.NumberOfPlayers == InputManager.Devices.Count - 1) && (!keyboardEnabled)) &&
              (gc.NumberOfPlayers > 0)
           )
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
        string text = "";

        if (connectedDevices == 1)
        {
            text = "No Controllers Found";
        }
        else if (connectedDevices == 2)
        {
            text = "1 Controller Found";
        }
        else if (connectedDevices > 2)
        {
            text = connectedDevices - 1 + " Controllers Found";
        }
        if (keyboardEnabled)
        {
            text += "\nTo disable keyboard press the space key";
        }
        else
        {
            text += "\nTo enable keyboard press 'f' or the enter key";
        }

        this.GetComponent<Text>().text = text;
    }

    private float startTime = -1.0f;
    private float lastTextUpdate = -1.0f;
    private bool keyboardEnabled = false;
}
