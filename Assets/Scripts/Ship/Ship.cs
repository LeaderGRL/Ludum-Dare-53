using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private ShipStats shipStats;
    private bool collide;

    private string planetName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        planetName = "Vespera";
        if (planetName != null && !collide)
        {
            GetComponent<Move>().enabled = true;
            GetComponent<Move>().target =
                GameObject.Find(planetName).transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == planetName)
        {
            GetComponent<Move>().enabled = false;
            collide = true;
            // shipStats.AssignedQuest = null;
        }
    }
}