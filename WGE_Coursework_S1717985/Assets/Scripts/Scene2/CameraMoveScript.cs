using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour {

    public GameObject player;
  
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        CameraFollowPlayerX();
	}

    public void CameraFollowPlayerX()
    {
        transform.SetPositionAndRotation(new Vector3(player.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

    public void LowerCamera()
    {
        transform.SetPositionAndRotation(transform.position + new Vector3(0, -6, 0), Quaternion.identity);
    }

    public void raiseCamera()
    {
        transform.SetPositionAndRotation(transform.position + new Vector3(0, 5, 0), Quaternion.identity);
    }
}
