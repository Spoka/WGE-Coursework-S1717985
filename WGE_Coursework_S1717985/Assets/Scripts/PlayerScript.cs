using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class PlayerScript : MonoBehaviour {

    public VoxelChunk voxelChunk;
    public DroppedBlockInstantiator instantiatePrefab;
    public InventoryManager inventory;
    public InventoryItemScript iItemScript;
    public GameObject inventoryPanel;
    public FirstPersonController fpsController;
    public GameObject controlsDisabledText;
    public bool controlsEnabled;
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
                if(voxelChunk.blockToPlace == 1 && inventory.itemAmounts[0] > 0)
                {
                    voxelChunk.SetBlock(v, voxelChunk.blockToPlace);
                    inventory.itemAmounts[0]--;
                    inventory.inventoryList[0].itemAmount = inventory.itemAmounts[0];
                    inventory.inventoryList[0].itemAmountText.text = inventory.itemAmounts[0].ToString();
                }
                if (voxelChunk.blockToPlace == 2 && inventory.itemAmounts[1] > 0)
                {
                    voxelChunk.SetBlock(v, voxelChunk.blockToPlace);
                    inventory.itemAmounts[1]--;
                    inventory.inventoryList[1].itemAmount = inventory.itemAmounts[1];
                    inventory.inventoryList[1].itemAmountText.text = inventory.itemAmounts[1].ToString();
                }
                if (voxelChunk.blockToPlace == 3 && inventory.itemAmounts[2] > 0)
                {
                    voxelChunk.SetBlock(v, voxelChunk.blockToPlace);
                    inventory.itemAmounts[2]--;
                    inventory.inventoryList[2].itemAmount = inventory.itemAmounts[2];
                    inventory.inventoryList[2].itemAmountText.text = inventory.itemAmounts[2].ToString();
                }
                if (voxelChunk.blockToPlace == 4 && inventory.itemAmounts[3] > 0)
                {
                    voxelChunk.SetBlock(v, voxelChunk.blockToPlace);
                    inventory.itemAmounts[3]--;
                    inventory.inventoryList[3].itemAmount = inventory.itemAmounts[3];
                    inventory.inventoryList[3].itemAmountText.text = inventory.itemAmounts[3].ToString();
                }
                for (int i = 0; i < inventory.itemAmounts.Count; i++)
                {
                    iItemScript.itemAmountText.text = inventory.itemAmounts[i].ToString();
                }
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            foreach (Collider collider in Physics.OverlapSphere(transform.position, 2.5f))
            {
                if (collider.tag == "DroppedBlock")
                {
                    Vector3 forceDirection = transform.position - collider.transform.position;
                    Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                    rb.AddForce(forceDirection.normalized * Time.deltaTime * 100);
                }
            }
            foreach (Collider collider in Physics.OverlapSphere(transform.position, .5f))
            {
                if (collider.tag == "DroppedBlock")
                {
                    if (collider.name == "GrassDrop(Clone)")
                    {
                        inventory.itemAmounts[0]++;
                        inventory.inventoryList[0].itemAmount = inventory.itemAmounts[0];
                        inventory.inventoryList[0].itemAmountText.text = inventory.itemAmounts[0].ToString();
                    }
                    if (collider.name == "DirtDrop(Clone)")
                    {
                        inventory.itemAmounts[1]++;
                        inventory.inventoryList[1].itemAmount = inventory.itemAmounts[1];
                        inventory.inventoryList[1].itemAmountText.text = inventory.itemAmounts[1].ToString();
                    }
                    if (collider.name == "SandDrop(Clone)")
                    {
                        inventory.itemAmounts[2]++;
                        inventory.inventoryList[2].itemAmount = inventory.itemAmounts[2];
                        inventory.inventoryList[2].itemAmountText.text = inventory.itemAmounts[2].ToString();
                    }
                    if (collider.name == "StoneDrop(Clone)")
                    {
                        inventory.itemAmounts[3]++;
                        inventory.inventoryList[3].itemAmount = inventory.itemAmounts[3];
                        inventory.inventoryList[3].itemAmountText.text = inventory.itemAmounts[3].ToString();
                    }
                    Destroy(collider.gameObject);
                }
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
        inventoryPanel.SetActive(false);
    }

    public void DisableControls()
    {
        fpsController.m_MouseLook.SetCursorLock(false);
        fpsController.m_MouseLook.UpdateCursorLock();
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
        controlsDisabledText.SetActive(true);
        controlsEnabled = false;
        inventoryPanel.SetActive(true);
        inventory.startItem.SetActive(false);
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
