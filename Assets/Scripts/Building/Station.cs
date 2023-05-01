using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : Building, IBuyable
{
    public int price => 88000;
    
    public Station()
    {
        level = 1;
        List<int> capacities = new List<int>
        {
            15,
            10
        };
        InventoryManager.instance.AddBuildingInventory(this, capacities);
    }
    //Inventaire

    public void SendShip(ShipResource ship, GameObject shipPrefab)
    {
        // Retirer le vaisseau de l'inventaire
        InventoryManager.instance.RemoveItemFromBuilding(ship, this);
        GameObject newShip = GameObject.Instantiate(shipPrefab, PlanetManager.instance.PlanetGameObject(this).transform);
        newShip.GetComponent<Ship>().StartPlanet = PlanetManager.instance.PlanetGameObject(this);
        newShip.GetComponent<Ship>().Resource = ship;
        
    }

    public void RecallShip(GameObject shipPrefab, GameObject startPlanet) 
    {
        GameObject newShip = GameObject.Instantiate(shipPrefab, startPlanet.transform);
        // A Changer par le bon shipstats
        newShip.GetComponent<Ship>().StartPlanet = startPlanet;
    }
}
