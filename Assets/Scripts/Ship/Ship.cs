using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private ShipResource shipRessource;
    private bool collide;

    private GameObject startPlanet;
    private string planetName;

    public GameObject StartPlanet
    {
        get { return startPlanet; }
        set { startPlanet = value; }
    }

    public ShipResource Resource { 
        get { return shipRessource; }
        set { 
            shipRessource = value;
            if (shipRessource.shipStats.AssignedQuest != null)
            {
                planetName = shipRessource.shipStats.AssignedQuest.Value.destination["planet"];
                SetTarget(planetName);
            }
        }
    }



    public void SetTarget(string target)
    {
        GetComponent<Move>().enabled = true;
        GetComponent<Move>().target = GameObject.Find(target).transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == planetName)
        {
            GetComponent<Move>().enabled = false;
            Station station = PlanetManager.instance.GetStation(other.gameObject);
            // Finir la qu�te si c'est le dernier

            if (station != null)
            {
                InventoryManager.instance.AddToBuilding(shipRessource, station, 0);
            }
            else
            {
                
            }
            Destroy(gameObject);
            // shipStats.AssignedQuest = null;
        }
    }
}