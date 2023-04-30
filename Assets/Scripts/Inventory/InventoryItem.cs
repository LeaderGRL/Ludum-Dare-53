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

    private InventoryUtils _inventoryUtils = InventoryUtils.GetInventoryUtils();
    private Resource randomResource;
    private Resource actualShowedResource = null;
    private bool spaceSheetStatsShow;

    private void Start()
    {
        //InitialiseItem(item);
    }

    private void FixedUpdate()
    {
        // Debug.Log("Debug spécial: " + actualShowedResource.id);
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
                if (!item.Equals(actualShowedResource))
                {
                    actualShowedResource = item;
                    _inventoryUtils.SpaceSheepInterface(item, true);
                }
                else
                {
                    actualShowedResource = null;
                    _inventoryUtils.SpaceSheepInterface(item, false);
                }
                break;
        }
    }
}