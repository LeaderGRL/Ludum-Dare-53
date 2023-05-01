using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance;
    [SerializeField]
    List<GameObject> planetsList;
    [SerializeField]
    List<Planet> scriptableObjectsPlanetList;

    public Dictionary<GameObject, Planet> planets = new Dictionary<GameObject, Planet>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
    }
    void Start()
    {
        // Récupérer toutes les planètes
        foreach (var planet in planetsList)
        {
            foreach (var item in scriptableObjectsPlanetList)
            {
                item.Buildings = new List<Building>();
                if (item.Name == planet.name)
                {
                    planets.Add(planet, item);
                }
            }
            
        }
        //GameObject button = GameObject.Find("CreateStation");
        //button.GetComponent<Button>().onClick.AddListener(() => 
        //    AddBuilding(new Station(), GameObject.Find("Xenoth"))
        //    );
            
    }
    
    public void AddBuilding(Building building, GameObject planet)
    {
        Planet planetScript = planets[planet];
        planetScript.AddBuilding(building);
    }

    public GameObject PlanetGameObject(Building building)
    {
        foreach (var planet in planets)
        {
            List<Building> planetBuildings = planet.Value.Buildings;
            bool isPlanet = FetchBuilding(building, planetBuildings);
            if (isPlanet)
            {
                return planet.Key;
            }
        }
        return null;
    }

    public GameObject PlanetGameObject(Planet planet)
    {
        foreach (var item in planets)
        {
            if (item.Value == planet)
            {
                return item.Key;
            }
        }
        return null;
    }

    private bool FetchBuilding(Building building, List<Building> planetBuildings) 
    {
        
        foreach (var planetBuilding in planetBuildings)
        {
            if (planetBuilding == building)
            {
                return true;
            }
        }
        return false;
    }

    public Station GetStation(GameObject planet)
    {
        foreach (var building in planets[planet].Buildings)
        {
            if (typeof(Station).IsInstanceOfType(building))
            {
                return (Station)building;
            }
        }
        
        return null;
    }

}
