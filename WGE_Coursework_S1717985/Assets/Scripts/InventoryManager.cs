using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public Transform parentPanel;

    public List<Sprite> itemSprites;
    public List<string> itemNames;
    public List<int> itemAmounts;

    public InventoryItemScript iis;

    public GameObject startItem;

    public List<InventoryItemScript> inventoryList;
    
	// Use this for initialization
	void Start ()
    { 
        inventoryList = new List<InventoryItemScript>();
        for (int i = 0; i < itemNames.Count; i++)
        {
            GameObject inventoryItem = (GameObject)Instantiate(startItem);
            inventoryItem.transform.SetParent(parentPanel);
            inventoryItem.SetActive(true);

            iis = inventoryItem.GetComponent<InventoryItemScript>();
            iis.itemSprite.sprite = itemSprites[i];
            iis.itemNameText.text = itemNames[i];
            iis.itemName = itemNames[i];
            iis.itemAmountText.text = itemAmounts[i].ToString();
            iis.itemAmount = itemAmounts[i];

            inventoryList.Add(iis);
        }
        DisplayListInOrder();
	}

    public void DisplayListInOrder()
    {
        float yOffset = 55f;
        Vector3 startPosition = startItem.transform.position;
        foreach(InventoryItemScript iis in inventoryList)
        {
            iis.transform.position = startPosition;
            startPosition.y -= yOffset;
        }
    }
	
    public void SelectionSortInventory()
    {
        for (int i = 0; i < inventoryList.Count - 1; i++)
        {
            int minIndex = i;
            for (int j = i; j < inventoryList.Count; j++)
            {
                if (inventoryList[j].itemAmount < inventoryList[minIndex].itemAmount)
                {
                    minIndex = j;
                }
            }
            if (minIndex!=i)
            {
                InventoryItemScript iis = inventoryList[i];
                inventoryList[i] = inventoryList[minIndex];
                inventoryList[minIndex] = iis;
            }
        }
        DisplayListInOrder();
    }

    List<InventoryItemScript> QuickSort(List<InventoryItemScript>listIn)
    {
        if (listIn.Count <= 1)
        {
            return listIn;
        }
        int pivotIndex = 0;

        List<InventoryItemScript> leftList = new List<InventoryItemScript>();
        List<InventoryItemScript> rightList = new List<InventoryItemScript>();
        for (int i = 1; i < listIn.Count; i++)
        {
            if (listIn[i].itemAmount > listIn[pivotIndex].itemAmount)
            {
                leftList.Add(listIn[i]);
            }
            else
            {
                rightList.Add(listIn[i]);
            }
        }

        leftList = QuickSort(leftList);
        rightList = QuickSort(rightList);

        leftList.Add(listIn[pivotIndex]);
        leftList.AddRange(rightList);

        return leftList;
    }

    public void StartQuickSort()
    {
        inventoryList = QuickSort(inventoryList);
        DisplayListInOrder();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
