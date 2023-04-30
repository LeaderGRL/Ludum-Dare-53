using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Resource item;
    [HideInInspector] public Transform parentAfterDrag;

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
        //Print the name of the GameObject clicked
        if (item == null)
        {
            return;
        }
        
        switch(item.name)
        {
            case "SpaceShip":
                Debug.Log("test"); //DISPLAY UI
                break;
        }
        Debug.Log(gameObject.name);
    }
}