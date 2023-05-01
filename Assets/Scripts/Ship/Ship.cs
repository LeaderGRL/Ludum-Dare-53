using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //t trop bo mec UwU
    private ShipResource shipRessource;
    private bool collide;

    private GameObject startPlanet;
    private GameObject targetPlanet;
    
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
        targetPlanet = GameObject.Find(target);
        GetComponent<Move>().enabled = true;
        GetComponent<Move>().target = targetPlanet.transform;
        GetComponent<Move>().SetSpeed(5);
        //GetComponent<Move>().SetSpeed(shipRessource.shipStats.modulesDict["Reactor"].Stat);
    }


    public GameObject GetPlanet()
    {
        return targetPlanet;
    }
    //salut ça va eliott ? moi ça va   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == planetName)
        {
            GetComponent<Move>().enabled = false;
            Station station = PlanetManager.instance.GetStation(other.gameObject);
            // Finir la quête si c'est le dernier

            if (station != null)
            {
                InventoryManager.instance.AddToBuilding(shipRessource, station, 0);
                if (Quest.instance.NeedZeroRessources(shipRessource.shipStats.AssignedQuest.Value.id))
                {
                    // Peut être a changer car si un vaisseau de la même quête arrive et que le dernier est parti elle s'exécute
                    Quest.instance.finishQuest(shipRessource.shipStats.AssignedQuest.Value.id);
                }
            }
            else
            {
                PlanetManager.instance.GetStation(startPlanet).RecallShip(gameObject, other.gameObject);
            }
            Destroy(gameObject);
            // shipStats.AssignedQuest = null;
        }
    }
}