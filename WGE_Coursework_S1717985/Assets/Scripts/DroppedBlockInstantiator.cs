using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedBlockInstantiator : MonoBehaviour {

    public GameObject grassBlock;
    public GameObject dirtBlock;
    public GameObject sandBlock;
    public GameObject stoneBlock;
    public PlayerScript playerScript;

    public void IntantiateDroppedBlock()
    {
        if (playerScript.bType == 1)
        {
            Instantiate(grassBlock, transform.position, Quaternion.identity);
        }
        if (playerScript.bType == 2)
        {
            Instantiate(dirtBlock, transform.position, Quaternion.identity);
        }
        if (playerScript.bType == 3)
        {
            Instantiate(sandBlock, transform.position, Quaternion.identity);
        }
        if (playerScript.bType == 4)
        {
            Instantiate(stoneBlock, transform.position, Quaternion.identity);
        }
    }
}
