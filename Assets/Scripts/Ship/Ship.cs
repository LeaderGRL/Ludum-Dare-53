using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
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
        planetName=target;
        targetPlanet = GameObject.Find(target);
        GetComponent<Move>().enabled = true;
        GetComponent<Move>().target = targetPlanet.transform;
        //GetComponent<Move>().SetSpeed(shipRessource.shipStats.modulesDict["Reactor"].Stat);
        GetComponent<Move>().SetSpeed(10f);
    }


    public GameObject GetPlanet()
    {
        return targetPlanet;
    }
    //salut �a va eliott ? moi �a va   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == planetName)
        {
            GetComponent<Move>().enabled = false;
            Station station = PlanetManager.instance.GetStation(other.gameObject);

            // Finir la qu�te si c'est le dernier
            if (shipRessource.shipStats.AssignedQuest != null)
            {
                if (Quest.instance.NeedZeroRessources(shipRessource.shipStats.AssignedQuest.Value.id))
                {
                    // Peut �tre a changer car si un vaisseau de la m�me qu�te arrive et que le dernier est parti elle s'ex�cute

                    Quest.instance.finishQuest(shipRessource.shipStats.AssignedQuest.Value.id);

                }
                shipRessource.shipStats.AssignedQuest = null;
            }
            
            if (station != null)
            {
                InventoryManager.instance.AddToBuilding(shipRessource, station, 0);
                
            }
            else
            {
                PlanetManager.instance.GetStation(startPlanet).RecallShip(gameObject, other.gameObject, shipRessource);
            }
            Destroy(gameObject);
            // shipStats.AssignedQuest = null;
        }
    }
}