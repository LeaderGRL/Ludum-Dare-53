using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Resource item;
    [HideInInspector] public Transform parentAfterDrag;

    private InventoryUtils inventoryUtils = InventoryUtils.GetInventoryUtils();

    private void Start()
    {
        //InitialiseItem(item);
    }

    public void InitialiseItem(Resource newItem)
    {
        //Debug.Log(newItem.icon.name);
        item = newItem;
        image.sprite = newItem.icon;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }
        
        switch(item.type)
        {
            case "SpaceShip":
                //Check if the item is already shown
                if (!item.Equals(inventoryUtils.itemShowed))
                {
                    //Show the item
                    inventoryUtils.SpaceSheepInterface(item, true);
                    inventoryUtils.itemShowed = item;
                }
                else
                {
                    //Hide the item
                    inventoryUtils.SpaceSheepInterface(item, false);
                    inventoryUtils.itemShowed = null;
                }
                break;
            case "None":
                if (!item.Equals(inventoryUtils.itemShowed))
                {
                    //Show the Buy Menu
                    inventoryUtils.ShowSpaceSheepBuyMenu(item, true);
                    inventoryUtils.itemShowed = item;
                }
                else
                {
                    //Hide the Buy Menu
                    inventoryUtils.ShowSpaceSheepBuyMenu(item, false);
                    inventoryUtils.itemShowed = null;
                }
                break;
        }
    }
}