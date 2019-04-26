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

    public void AscendingMergeSort()
    {
        inventoryList = MergeSort(inventoryList, false);
        DisplayListInOrder();
    }
    public void DescendingMergeSort()
    {
        inventoryList = MergeSort(inventoryList, true);
        DisplayListInOrder();
    }

    private static List<InventoryItemScript> MergeSort(List<InventoryItemScript> unsorted, bool descendingMerge)
    {
        if (unsorted.Count <= 1)                                    //makes sure the list has more than one element
        {
            return unsorted;
        }
            
        List<InventoryItemScript> left = new List<InventoryItemScript>();
        List<InventoryItemScript> right = new List<InventoryItemScript>();

        int middle = unsorted.Count / 2;
        for (int i = 0; i < middle; i++) 
        {
            left.Add(unsorted[i]);
        }                                                       //the first list is divided into two list of equal size
        for (int i = middle; i < unsorted.Count; i++)
        {
            right.Add(unsorted[i]);
        }

                               //the process is reapeated for the halved lists

        if (!descendingMerge)
        {
            left = MergeSort(left, false);
            right = MergeSort(right, false);
            return MergeAscending(left, right);
        }
        else
        {
            left = MergeSort(left, true);
            right = MergeSort(right, true);
            return MergeDescending(left, right);
        }
        
    }

    private static List<InventoryItemScript> MergeAscending(List<InventoryItemScript> left, List<InventoryItemScript> right)
    {
        List<InventoryItemScript> result = new List<InventoryItemScript>();  //creates a new list to store the arranged values 

        while (left.Count > 0 || right.Count > 0)                   //while there are element to arrange
        {
            if (left.Count > 0 && right.Count > 0)                   
            {
                if (left[0].itemAmount <= right[0].itemAmount) 
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);                              
                }
                else                                                   //if there are elements in both lists, check the first elements
                {                                                       //compare them and store the right one in the arranged list
                    result.Add(right[0]);
                    right.Remove(right[0]);
                }
            }
            else if (left.Count > 0)
            {
                result.Add(left[0]);
                left.Remove(left[0]);
            }                                                           //if only one of the two lists still hold elements,
            else if (right.Count > 0)                                    //simply store the first element in the arranged list
            {
                result.Add(right[0]);

                right.Remove(right[0]);
            }
        }
        return result;                                               //repeat until all elements are arranged in the desired order.
    }

    private static List<InventoryItemScript> MergeDescending(List<InventoryItemScript> left, List<InventoryItemScript> right)
    {
        List<InventoryItemScript> result = new List<InventoryItemScript>();  //creates a new list to store the arranged values 

        while (left.Count > 0 || right.Count > 0)                   //while there are element to arrange
        {
            if (left.Count > 0 && right.Count > 0)
            {
                if (left[0].itemAmount >= right[0].itemAmount)
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);
                }
                else                                                   //if there are elements in both lists, check the first elements
                {                                                       //compare them and store the right one in the arranged list
                    result.Add(right[0]);
                    right.Remove(right[0]);
                }
            }
            else if (left.Count > 0)
            {
                result.Add(left[0]);
                left.Remove(left[0]);
            }                                                           //if only one of the two lists still hold elements,
            else if (right.Count > 0)                                    //simply store the first element in the arranged list
            {
                result.Add(right[0]);

                right.Remove(right[0]);
            }
        }
        return result;                                               //repeat until all elements are arranged in the desired order.
    }
}
