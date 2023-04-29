using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsManager : MonoBehaviour
{
    public List<ShipStats> ships;




    void Start()
    {
        ships = new List<ShipStats>();
        ships.Add(new ShipStats());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
