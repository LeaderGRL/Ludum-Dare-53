using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller : Building, IBuyable
{
    public int price => 3000;
    private int collectTime = 0;

    public Driller() 
    {
        level = 1;
        List<int> capacities = new List<int>
        {
            3
        };
        InventoryManager.instance.AddBuildingInventory(this, capacities);
        SetCollectTime(1);
    }

    public IEnumerator CollectResource(Planet planet)
    {
        while (true)
        {
            yield return new WaitForSeconds(GetCollectTime());
            Debug.Log("Collect!");
            foreach (var resource in planet.Resources)
            {
                InventoryManager.instance.AddToBuilding(resource, this, 0);
            }
        }
        
    }
    
    public void SetCollectTime(int time)
    {
        collectTime = time;
    }
    public int GetCollectTime()
    {
        return collectTime;
    }
}
