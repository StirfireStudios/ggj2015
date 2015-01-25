using UnityEngine;
using System.Collections;
using InControl;
using N;

/**
 * A player.
 */
public class GGJ15Player : GGJ15Character {

    /**
     * The InputDevice controlling this player.
     */
    [HideInInspector]
    public InputDevice ControllingDevice = InputDevice.Null;

    Vector3 _mvec_ls = Vector3.zero;
    Vector3 _mvec_dp = Vector3.zero;

    /**
     * On instantiation.
     */
    new void Start() {

        // @todo don't just use the last-updated device!
        // ControllingDevice = InputManager.ActiveDevice;

        base.Start();
    }

    /**
     * Set the controlling device.
     */
    public void SetControllingDevice(InputDevice device) {
        ControllingDevice = device;
    }

    /**
     * Every tick.
     */
    new void Update() {
        InputDevice device = ControllingDevice;
        if (device == InputDevice.Null)
            device = InputManager.ActiveDevice; 

        
        // Support moving with left stick or dpad.
        _mvec_ls.Set(
            device.LeftStickX,
            0.0f,
            device.LeftStickY
        );
        _mvec_dp.Set(
            device.DPad.X,
            0.0f,
            device.DPad.Y
        );
        base.DesiredHeading = (_mvec_ls + _mvec_dp);
        base.DesiredSpeedFactor = Mathf.Clamp(base.DesiredHeading.magnitude, 0.0f, 1.0f);
        base.DesiredHeading.Normalize();

        base.Update();

        if (device.Action1)
            gameObject.GetComponent<GGJ.Actions.Shoot>().apply();
        
        if (device.Action2)
            gameObject.GetComponent<GGJ.Actions.Jump>().apply();
    }
}
