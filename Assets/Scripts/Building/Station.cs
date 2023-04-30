using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : Building, IBuyable
{
    public int price => 88000;
    
    public Station()
    {
        level = 1;
        List<int> capacities = new List<int>();
        capacities.Add(15);
        capacities.Add(10);
        InventoryManager.instance.AddBuildingInventory(this, capacities);
    }
    //Inventaire
    
}
