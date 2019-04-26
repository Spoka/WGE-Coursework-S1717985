using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Animator animator;
    public CameraMoveScript cameraScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Platform")
        {
            animator.SetTrigger("CameraShake");
        }
        if (collider.tag == "Platform")
        {
            animator.SetTrigger("CameraBigShake");
            cameraScript.LowerCamera();
        }
        if (collider.tag == "Platform1")
        {
            cameraScript.raiseCamera();
        }
    }
}
