using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "ScriptableObjects/Planet", order = 1)]
public class Planet : ScriptableObject
{
    private int rotationSpeed;
    private List<Building> buildings = new List<Building>();
        
}
