using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField] private GameObject spaceShipInventoryContainer;
    [SerializeField] private GameObject inventoryContainerPrefab;
    [SerializeField] private GameObject slotPrefab;
    private List<InventorySlot> items = new List<InventorySlot>();
    public Dictionary<Building, List<GameObject>> buildingsInventories;

    public GameObject inventoryItemPrefab;
    public Image image;
    public Sprite spaceShipIcon;

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
        for (var i = 0; i <= items.Count; i++)
        {
            var slot = items[i];
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                item.id = i;
                SpawnNewItem(item, slot);
                return;
            }
            
        }
    }

    public void ChangeToSpaceShip()
    {
        foreach (var slot in items)
        {
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot.item.type != "SpaceShip")
            {
                itemInSlot.item.type = "SpaceShip";
                itemInSlot.item.icon = spaceShipIcon;
                itemInSlot.image.sprite = spaceShipIcon;
                break;
            }
        }
    }

    public void SpawnNewItem(Resource item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void AddToBuilding(Resource item, Building building, int inventoryIndex)
    {
        GameObject inventory = buildingsInventories[building][inventoryIndex];
        AddToBuilding(item, inventory);
    }

    public void AddToBuilding(Resource item, GameObject inventory)
    {
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            InventorySlot slot = inventory.transform.GetChild(i).GetComponent<InventorySlot>();
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }

    public void AddBuildingInventory(Building building, List<int> inventoriesCapcities)
    {
        List<GameObject> inventories = new List<GameObject>();

        foreach (var capcity in inventoriesCapcities)
        {
            
            GameObject newInventory = InstantiateInventory(capcity);
            inventories.Add(newInventory);
            PlaceBuildingInventory(building, newInventory, inventories.Count);
        }
        buildingsInventories.Add(building, inventories);
        
    }

    public void PlaceBuildingInventory(Building building, GameObject inventory,  int inventoryNumber)
    {
        if (typeof(Station).IsInstanceOfType(building) )
        {
            switch (inventoryNumber)
            {
                case 1:
                    inventory.GetComponent<RectTransform>().position = new Vector3(300, 850, 0);
                    break;
                case 2:
                    inventory.GetComponent<RectTransform>().position = new Vector3(300, 600, 0);
                    break;
            }
            return;
        }
    }

    public GameObject InstantiateInventory(int capacity)
    {
        GameObject newInventory = Instantiate(inventoryContainerPrefab, GameObject.Find("Canvas").transform);
        for (int i = 0; i < capacity; i++)
        {
            Instantiate(slotPrefab, newInventory.transform);
        }
        newInventory.SetActive(false);
        return newInventory;
    }

    public List<GameObject> GetAllInventories()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (KeyValuePair<Building, List<GameObject>> item in buildingsInventories)
        {
            foreach (GameObject inventory in item.Value)
            {
                list.Add(inventory);
            }
        }


        return list;
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

    public void RemoveItemFromBuilding(Resource item, Building building)
    {
        foreach (var inventory in buildingsInventories[building])
        {
            if (RemoveItemFromInventory(item, inventory))
            {
                return;
            }
        }
    }

    private bool RemoveItemFromInventory(Resource item, GameObject inventory)
    {
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            GameObject slot = inventory.transform.GetChild(i).gameObject;
            if (slot.transform.childCount <= 0)
            {
                continue;
            }
            InventoryItem itemSlot = slot.transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
            if (itemSlot.item == item)
            {
                Destroy(itemSlot.gameObject);
                return true;
            }

        }
        return false;
    }
}
