using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance;
    [SerializeField]
    List<GameObject> planetsList;

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
            planets.Add(planet, new Planet());
        }
        GameObject button = GameObject.Find("CreateStation");
        button.GetComponent<Button>().onClick.AddListener(() => 
            AddBuilding(new Station(), GameObject.Find("Xenoth"))
            );
            
    }
    
    public void AddBuilding(Building building, GameObject planet)
    {
        Planet planetScript = planets[planet];
        planetScript.AddBuilding(building);
    }
}
