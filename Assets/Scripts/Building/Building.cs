using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected Planet currentPlanet;
    protected int level;

    public Building()
    {
        level = 1;
    }

    public int GetLevel()
    {
        return level;
    }

    public void Upgrade()
    {
        level++;
    }

    public Planet GetCurrentPlanet()
    {
        return currentPlanet;
    }
    
    
}
