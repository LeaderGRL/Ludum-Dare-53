using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject spaceShipInventoryContainer;
    private List<InventorySlot> spaceShip = new List<InventorySlot>();

    private void Start()
    {
        for (var i = 0; i < spaceShipInventoryContainer.transform.childCount; i++)
        {
            spaceShip.Add(spaceShipInventoryContainer.transform.GetChild(i));
        }
    }
    public void AddItem(Resource item)
    {
        
    }

    public void SpawnNewItem(Resource item, InventorySlot slot)
    {

    }
}
       
