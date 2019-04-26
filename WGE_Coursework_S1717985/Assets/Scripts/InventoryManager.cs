﻿using System.Collections;
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
    public List<InventoryItemScript> sortedInventoryList;
    
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

    void DisplayListInOrder()
    {
        float yOffset = 55f;
        Vector3 startPosition = startItem.transform.position;
        foreach(InventoryItemScript iis in inventoryList)
        {
            iis.transform.position = startPosition;
            startPosition.y -= yOffset;
        }
    }

    public void ApplyMergeSort()
    {
        inventoryList = MergeSort(inventoryList);
        DisplayListInOrder();
    }

    private static List<InventoryItemScript> MergeSort(List<InventoryItemScript> unsorted)
    {
        if (unsorted.Count <= 1)
            return unsorted;

        List<InventoryItemScript> left = new List<InventoryItemScript>();
        List<InventoryItemScript> right = new List<InventoryItemScript>();

        int middle = unsorted.Count / 2;
        for (int i = 0; i < middle; i++)  //Dividing the unsorted list
        {
            left.Add(unsorted[i]);
        }
        for (int i = middle; i < unsorted.Count; i++)
        {
            right.Add(unsorted[i]);
        }

        left = MergeSort(left);
        right = MergeSort(right);
        return Merge(left, right);
    }

    private static List<InventoryItemScript> Merge(List<InventoryItemScript> left, List<InventoryItemScript> right)
    {
        List<InventoryItemScript> result = new List<InventoryItemScript>();

        while (left.Count > 0 || right.Count > 0)
        {
            if (left.Count > 0 && right.Count > 0)
            {
                if (left[0].itemAmount <= right[0].itemAmount)  //Comparing First two elements to see which is smaller
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);      //Rest of the list minus the first element
                }
                else
                {
                    result.Add(right[0]);
                    right.Remove(right[0]);
                }
            }
            else if (left.Count > 0)
            {
                result.Add(left[0]);
                left.Remove(left[0]);
            }
            else if (right.Count > 0)
            {
                result.Add(right[0]);

                right.Remove(right[0]);
            }
        }
        return result;
    }

    /*public void SelectionSortInventory()
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
    }*/

    // Update is called once per frame
    void Update () {
		
	}
}
