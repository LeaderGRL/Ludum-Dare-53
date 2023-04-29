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


    public int Capacity;

    // A remplacer par un objet ressource;
    
    private int _ressources;
    public int Ressources
    {
        get { return _ressources; }
        set {
            if (value > Capacity)
            {
                return;
            }
            _ressources = value; 
        }
    }

    public ShipStats()
    {
        // Rempli de carburant
        MaxFuel = 250;
        Fuel = MaxFuel;

        // Soute Vide
        Capacity = 10;
        Ressources = 0;

        // Tout au niveau 1
        ReactorsLevel = 1;
        HoldLevel = 1;
        WeaponsLevel = 1;
        ShieldLevel = 1;
        CommunicationSystemLevel = 1;
        StyleLevel = 1;

        Available = true;

        
    }

}
