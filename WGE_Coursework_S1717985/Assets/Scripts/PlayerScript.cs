using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public VoxelChunk voxelChunk;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 v;
            if (PickThisBlock(out v, 5))
            {
                voxelChunk.SetBlock(v, 0);
            }
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Vector3 v;
            if (PickEmptyBlock(out v, 5))
            {
                Debug.Log(v);
                voxelChunk.SetBlock(v, 1);
            }
        }
	}

    bool PickThisBlock(out Vector3 v, float dist)
    {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        
        if(Physics.Raycast (ray, out hit, dist))
        {
            v = hit.point - hit.normal / 2;
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);
            return true;
        }
        return false;
    }

    bool PickEmptyBlock(out Vector3 v, float dist)
    {
        v = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            v = hit.point + hit.normal / 2;
            v.x = Mathf.Floor(v.x);
            v.y = Mathf.Floor(v.y);
            v.z = Mathf.Floor(v.z);
            return true;
        }
        return false;
    }
}
