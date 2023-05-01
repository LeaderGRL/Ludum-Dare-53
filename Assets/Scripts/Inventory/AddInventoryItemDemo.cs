using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventoryItemDemo : MonoBehaviour
{
    // public Resource item;

    // public Resource item2;

    public Sprite imageSpaceShip;
    public Sprite imageNone;
    
    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < 15; i++)
        {
            var item = ScriptableObject.CreateInstance<Resource>();
            item.id = i;
            item.type = "None";
            item.icon = imageNone;

            InventoryManager.instance.Add(item);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    //     if (!Input.GetKeyDown(KeyCode.K)) return;
    //     item.type = "SpaceShip";
    //     item.currentLocation = "Testy";
    //     item.fuelLevel = 0.5f;
    //     item.icon = imageSpaceShip;
    //     InventoryManager.instance.Add(item);
    //         
    //     item2.type = "SpaceShip";
    //     item2.currentLocation = "Lyon";
    //     item2.fuelLevel = 0;
    //     item2.icon = imageSpaceShip;
    //     InventoryManager.instance.Add(item2);
    }
}
