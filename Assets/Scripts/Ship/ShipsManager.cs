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
        Debug.Log(ships[0].ToString());
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

    public void UseShip(int index)
    {
        if (index < 0 || index >= ships.Count)
        {
            return;
        }
        ships[index].Available = false;
    }

}
