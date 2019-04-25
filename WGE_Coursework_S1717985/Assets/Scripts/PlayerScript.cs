using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class PlayerScript : MonoBehaviour {

    public VoxelChunk voxelChunk;
    public DroppedBlockInstantiator instantiatePrefab;
    bool controlsEnabled;
    public GameObject controlsDisabledText;
    public FirstPersonController fpsController;
    public Vector3 blockTypeToTransfer;
    public int bType;

    // Use this for initialization
    void Start ()
    {
        controlsEnabled = true;
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if (Input.GetButtonDown("Fire1") && controlsEnabled)
        {
            Vector3 v;
            if (PickThisBlock(out v, 5))
            {
                blockTypeToTransfer = v;
                bType = voxelChunk.GetDestroyedBlockType();
                voxelChunk.SetBlock(v, 0);
                instantiatePrefab.IntantiateDroppedBlock();
            }
        }
        if(Input.GetButtonDown("Fire2") && controlsEnabled)
        {
            Vector3 v;
            if (PickEmptyBlock(out v, 5))
            {
                voxelChunk.SetBlock(v, voxelChunk.blockToPlace);
            }
        }
	}

    public void EnableControls()
    {
        fpsController.m_MouseLook.SetCursorLock(true);
        fpsController.m_MouseLook.UpdateCursorLock();
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
        controlsDisabledText.SetActive(false);
        controlsEnabled = true;
    }

    public void DisableControls()
    {
        fpsController.m_MouseLook.SetCursorLock(false);
        fpsController.m_MouseLook.UpdateCursorLock();
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
        controlsDisabledText.SetActive(true);
        controlsEnabled = false;
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
