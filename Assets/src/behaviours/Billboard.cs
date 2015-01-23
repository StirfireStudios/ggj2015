using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    Vector3 lookTarget = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        lookTarget = transform.position + Vector3.back;

        this.transform.LookAt(lookTarget, -Vector3.up);
	}
}
