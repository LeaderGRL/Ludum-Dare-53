using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats
{
    public float MaxFuel;
    public float Fuel;

    public int ReactorsLevel;
    public int HoldLevel;
    public int WeaponsLevel;
    public int ShieldLevel;
    public int CommunicationSystemLevel;
    public int StyleLevel;

    public bool Available;

    // Ressources dans la soute
    // Place dans la soute

    public ShipStats()
    {
        MaxFuel = 250;
        Fuel = MaxFuel;

        ReactorsLevel = 1;
        HoldLevel = 1;
        WeaponsLevel = 1;
        ShieldLevel = 1;
        CommunicationSystemLevel = 1;
        StyleLevel = 1;

        Available = true;
    }

}
