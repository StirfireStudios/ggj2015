using UnityEngine;
using System;
using System.Collections;

public class Billboard : MonoBehaviour {

    Vector3 lookTarget = Vector3.zero;

    bool bFlip = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

        float f = (Convert.ToSingle(bFlip) - 0.5f) * 2.0f;
        Vector3 lookOffset = Vector3.back * f;

        // Allow this to work in the character test scene with no immediate parent
        if (gameObject.transform.parent) {

            lookTarget = gameObject.transform.parent.position + lookOffset;

            this.transform.LookAt(lookTarget, Vector3.up);

            const float debugDrawLength = 4.0f;
            Debug.DrawLine(
                transform.position,
                transform.position + debugDrawLength * transform.forward.normalized,
                Color.green
            );
        }
	}

    void FlipBillboard(bool flipped = true) {
        bFlip = flipped;
    }


}
