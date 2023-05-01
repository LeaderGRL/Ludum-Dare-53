using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driller : Building, IBuyable
{
    public int price => 3000;
    private int collectTime = 0;

    private void Awake()
    {
        
    }
    private void Start()
    {
        level = 1;
        List<int> capacities = new List<int>
        {
            3
        };
        InventoryManager.instance.AddBuildingInventory(this, capacities);
        SetCollectTime(10);
    }

    private IEnumerator CollectResource(Planet planet)
    {
        yield return new WaitForSeconds(GetCollectTime());
        foreach (var resource in planet.Resources)
        {
            InventoryManager.instance.AddToBuilding(resource, this, 0);
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
