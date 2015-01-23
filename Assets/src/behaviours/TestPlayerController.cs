using UnityEngine;
using System.Collections;
using InControl;


public class TestPlayerController : MonoBehaviour {

    InputDevice inputDevice = InputDevice.Null;

    public int playerIndex = 0;

    public float RotationSpeed = 3.0f;
    public float Speed = 4.0f;
    #region movement
    Vector3 desiredHeading = Vector3.zero;
    Vector3 _mvec_ls = Vector3.zero;
    Vector3 _mvec_dp = Vector3.zero;
    const float _invPI = 1.0f / Mathf.PI;

    float desiredSpeedFactor = 0.0f;
    #endregion

    GameObject SpriterObject = null;


    void Start () {
        inputDevice = InputManager.ActiveDevice;//Devices[playerIndex];

        foreach (Transform child in transform)
        {
            if (child.CompareTag("SpriterObject"))
                SpriterObject = child.gameObject;
        }
	}
	
	void Update () {

        inputDevice = InputManager.ActiveDevice;
        if (inputDevice != InputDevice.Null) {

            // Move target object with left stick or dpad.
            _mvec_ls.Set(
                inputDevice.LeftStickX,
                0.0f,
                inputDevice.LeftStickY
            );
            _mvec_dp.Set(
                inputDevice.DPad.X,
                0.0f,
                inputDevice.DPad.Y
            );
            desiredHeading = (_mvec_ls + _mvec_dp);
            desiredSpeedFactor = Mathf.Clamp(desiredHeading.magnitude, 0.0f, 1.0f);
            desiredHeading.Normalize();

            // Cache results of cross product and dot product for later use.
            float DotProd = Vector3.Dot(transform.forward, desiredHeading);
            Vector3 CrossProd = Vector3.Cross(transform.forward, desiredHeading);

            float RotationSpeedScale = Mathf.Abs(((DotProd * _invPI) - 0.5f) * 2.0f) * RotationSpeed;

            const float debugDrawLength = 4.0f;
            Debug.DrawLine(
                transform.position,
                transform.position + debugDrawLength * transform.forward.normalized,
                Color.red
            );
            Debug.DrawLine(
                transform.position,
                transform.position + debugDrawLength * desiredHeading,
                Color.yellow
            );
            //Debug.Log(desiredHeading.ToString());

            if (desiredSpeedFactor > 0.05f)
            {
                // Send a flip message to the billboard, but only if we're
                // moving substantially in the X axis.
                bool bFlip = Vector3.Dot(Vector3.right, desiredHeading) < 0.0f || Vector3.Dot(Vector3.right, transform.forward) < 0.0f;
                if (Mathf.Abs(desiredHeading.x) > 0.10f)
                    SpriterObject.SendMessage("FlipBillboard", bFlip);

                // Figure out how to rotate the character.
                if (CrossProd.y > 0.0f)
                {
                    transform.Rotate(Vector3.up * (Time.deltaTime + 4) * RotationSpeedScale, Space.World);
                }
                else
                {
                    transform.Rotate(Vector3.up * (Time.deltaTime + -4) * RotationSpeedScale, Space.World);
                }

                // Move the character.
                transform.Translate(Vector3.forward * Speed * desiredSpeedFactor * Time.deltaTime, Space.Self);
            }

        }

	}
}
