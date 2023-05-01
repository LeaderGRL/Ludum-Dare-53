using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private int maxStack = 64;

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

    public bool Add(Resource item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            InventorySlot slot = items[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                Debug.Log("NOT FULL");
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }

        }

        for (var i = 0; i < items.Count; i++)
        {
            var slot = items[i];
            var itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                item.id = i;
                SpawnNewItem(item, slot);
                return true;
            }

        }
        return false;
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

    public bool AddToBuilding(Resource item, GameObject inventory)
    {
        if (typeof(ShipResource).IsInstanceOfType(item))
        {
            Building building = GetBuildingByInventory(inventory);
            GameObject planet = PlanetManager.instance.PlanetGameObject(building);
            ShipResource shipItem = (ShipResource)item;
            shipItem.planet = PlanetManager.instance.planets[planet];
            shipItem.building = building;
            item = shipItem;
        }

        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            InventorySlot slot = inventory.transform.GetChild(i).GetComponent<InventorySlot>();
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
            {
                Debug.Log("NOT FULL");
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }

        }
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            InventorySlot slot = inventory.transform.GetChild(i).GetComponent<InventorySlot>();
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public void AddToBuilding(Resource item, Building building, int inventoryIndex, int count)
    {
        GameObject inventory = buildingsInventories[building][inventoryIndex];
        AddToBuilding(item, inventory);
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

    public void PlaceBuildingInventory(Building building, GameObject inventory, int inventoryNumber)
    {
        if (typeof(Station).IsInstanceOfType(building))
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
        else
        {

        }
    }

    public GameObject InstantiateInventory(int capacity)
    {
        GameObject newInventory = Instantiate(inventoryContainerPrefab, GameObject.Find("CanvasPlanet").transform);
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

    public List<ShipResource> GetAllShips()
    {
        List<ShipResource> shipRessources = new List<ShipResource>();
        List<GameObject> inventories = GetAllInventories();
        foreach (var inventory in inventories)
        {
            for (int i = 0; i < inventory.transform.childCount; i++)
            {
                Transform slot = inventory.transform.GetChild(i);
                if (slot.childCount > 0 && typeof(ShipResource).IsInstanceOfType(slot.GetChild(0).GetComponent<InventoryItem>().item))
                {
                    shipRessources.Add((ShipResource)slot.GetChild(0).GetComponent<InventoryItem>().item);
                }
            }
        }
        return shipRessources;
    }

    public Building GetBuildingByInventory(GameObject inventory)
    {
        foreach (var item in buildingsInventories)
        {
            if (item.Value.Contains(inventory))
            {
                return item.Key;
            }
        }
        return null;
    }

    public void TransferItem(Resource item, Building from, Building to, int inventoryIndex)
    {
        GameObject fromInventory = buildingsInventories[from][inventoryIndex];
        RemoveItemFromBuilding(item, from);
        AddToBuilding(item, to, 0, fromInventory.GetComponent<InventoryItem>().count);
    }

    //public void TransferItem(Resource item, Building from, Building to, int inventoryIndex)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, to, inventoryIndex);
    //}

    //public void TransferItem(Resource item, Building from, Building to, GameObject inventory)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventory);
    //}

    //public void TransferItem(Resource item, Building from, Building to, GameObject inventory, int inventoryIndex)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventory);
    //}

    //public void TransferItem(Resource item, Building from, Building to, int inventoryIndexFrom, int inventoryIndexTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, to, inventoryIndexTo);
    //}

    //public void TransferItem(Resource item, Building from, Building to, int inventoryIndexFrom, GameObject inventoryTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventoryTo);
    //}

    //public void TransferItem(Resource item, Building from, Building to, GameObject inventoryFrom, int inventoryIndexTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, to, inventoryIndexTo);
    //}

    //public void TransferItem(Resource item, Building from, Building to, GameObject inventoryFrom, GameObject inventoryTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventoryTo);
    //}

    //public void TransferItem(Resource item, Building from, Building to, GameObject inventoryFrom, GameObject inventoryTo, int inventoryIndexTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventoryTo, inventoryIndexTo);
    //}

    //public void TransferItem(Resource item, Building from, Building to, int inventoryIndexFrom, GameObject inventoryTo, int inventoryIndexTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventoryTo, inventoryIndexTo);
    //}

    //public void TransferItem(Resource item, Building from, Building to, int inventoryIndexFrom, int inventoryIndexTo, GameObject inventoryTo)
    //{
    //    RemoveItemFromBuilding(item, from);
    //    AddToBuilding(item, inventoryTo, inventoryIndexTo);
    //}
}