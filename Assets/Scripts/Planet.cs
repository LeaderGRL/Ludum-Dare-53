using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "ScriptableObjects/Planet", order = 1)]
public class Planet : ScriptableObject
{
    [SerializeField] private int rotationSpeed;
    [SerializeField] private List<Building> buildings = new List<Building>();
    [SerializeField] private List<Resource> resources = new List<Resource>();

    public int RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public List<Building> Buildings { get => buildings; set => buildings = value; }
    public List<Resource> Resources { get => resources; set => resources = value; }
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    public void RemoveBuilding(Building building)
    {
        buildings.Remove(building);
    }

    public void AddResource(Resource resource)
    {
        resources.Add(resource);
    }

    public void RemoveResource(Resource resource)
    {
        resources.Remove(resource);
    }

}
