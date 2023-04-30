using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField] private GameObject spaceShipInventoryContainer;
    [SerializeField] private GameObject inventoryContainerPrefab;
    [SerializeField] private GameObject slotPrefab;
    private List<InventorySlot> items = new List<InventorySlot>();
    private Dictionary<Building, List<GameObject>> buildingsInventories;

    public GameObject inventoryItemPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        for (var i = 0; i < spaceShipInventoryContainer.transform.childCount; i++)
        {
            items.Add(spaceShipInventoryContainer.transform.GetChild(i).GetComponent<InventorySlot>());
        }
        buildingsInventories = new Dictionary<Building, List<GameObject>>();
        
    }

    public void Add(Resource item)
    {
        for (int i = 0; i <= items.Count; i++)
        {
            InventorySlot slot = items[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
            
        }
    }
    
    public void SpawnNewItem(Resource item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void AddBuildingInventory(Building building, List<int> inventoriesCapcities)
    {
        List<GameObject> inventories = new List<GameObject>();
        foreach (var capcity in inventoriesCapcities)
        {
            GameObject newInventory = InstantiateInventory(capcity);
            inventories.Add(newInventory);
        }
        buildingsInventories.Add(building, inventories);
        
    }

    public GameObject InstantiateInventory(int capacity)
    {
        GameObject newInventory = Instantiate(inventoryContainerPrefab, GameObject.Find("Canvas").transform);
        for (int i = 0; i < capacity; i++)
        {
            Debug.Log("Test");
            Instantiate(slotPrefab, newInventory.transform);
        }
        
        return newInventory;
    }
    //public void Remove(Resource item)
    //{
    //    for (var i = 0; i < spaceShipInventoryContainer.transform.childCount; i++)
    //    {
    //        var slot = spaceShipInventoryContainer.transform.GetChild(i).GetComponent<InventorySlot>();
    //        if (slot.item == item)
    //        {
    //            slot.RemoveItem();
    //            return;
    //        }
    //    }
    //}
}
