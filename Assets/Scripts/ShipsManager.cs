using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsManager : MonoBehaviour
{
    public List<ShipStats> ships;

    public int MaxShip;


    void Start()
    {
        MaxShip = 1;
        ships = new List<ShipStats>();
        ships.Add(new ShipStats());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddShip() { 
        if (ships.Count >= MaxShip)
        {
            return;
        }
        ships.Add(new ShipStats());
    }

    public void UpgradeCapacity(int capacity)
    {
        MaxShip += capacity;
    }


}
