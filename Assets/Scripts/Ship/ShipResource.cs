using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipResource", menuName = "ScriptableObjects/ShipResource")]
public class ShipResource : Resource
{
    public ShipStats shipStats;
    public Building building;
    public Planet planet;


   
}
