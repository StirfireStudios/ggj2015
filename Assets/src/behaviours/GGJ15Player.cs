using UnityEngine;
using System.Collections;
using InControl;

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
        ControllingDevice = InputManager.ActiveDevice;

        base.Start();
    }

    /**
     * Every tick.
     */
    new void Update() {

        // @todo don't just use the last-updated device!
        ControllingDevice = InputManager.ActiveDevice;
        if (ControllingDevice != InputDevice.Null)
        {

            // Support moving with left stick or dpad.
            _mvec_ls.Set(
                ControllingDevice.LeftStickX,
                0.0f,
                ControllingDevice.LeftStickY
            );
            _mvec_dp.Set(
                ControllingDevice.DPad.X,
                0.0f,
                ControllingDevice.DPad.Y
            );
            base.DesiredHeading = (_mvec_ls + _mvec_dp);
            base.DesiredSpeedFactor = Mathf.Clamp(base.DesiredHeading.magnitude, 0.0f, 1.0f);
            base.DesiredHeading.Normalize();

        }

        base.Update();
    }

}