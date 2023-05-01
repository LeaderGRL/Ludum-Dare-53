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
        GameObject newShip = GameObject.Instantiate(shipPrefab);
        
        newShip.transform.localScale = Vector3.one * 3;
        newShip.transform.position = PlanetManager.instance.PlanetGameObject(this).transform.position;
        newShip.GetComponent<Ship>().StartPlanet = PlanetManager.instance.PlanetGameObject(this);
        newShip.GetComponent<Ship>().Resource = ship;
        
    }

    public void RecallShip(GameObject ship, GameObject startPlanet, ShipResource shipResource) 
    {
        GameObject newShip = GameObject.Instantiate(ship);
        //newShip.transform.parent = null;
        newShip.transform.localScale = Vector3.one * 3;
        newShip.transform.position = startPlanet.transform.position;

        // A Changer par le bon shipstats
        newShip.GetComponent<Ship>().Resource = shipResource;
        newShip.GetComponent<Ship>().StartPlanet = startPlanet;
        newShip.GetComponent<Ship>().SetTarget(PlanetManager.instance.PlanetGameObject(this).name);

    }
}
