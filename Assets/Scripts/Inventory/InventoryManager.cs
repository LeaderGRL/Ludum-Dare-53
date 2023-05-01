using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField] private GameObject spaceShipInventoryContainer; 
    private List<InventorySlot> items = new List<InventorySlot>();

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