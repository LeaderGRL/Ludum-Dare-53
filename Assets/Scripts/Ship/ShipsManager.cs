using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsManager : MonoBehaviour
{
    private List<Ship> shipList = new List<Ship>();
    public List<ShipStats> ships;
    public int MaxShip;

    private static ShipsManager _instance;
    public static ShipsManager Instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("Destroy instance");
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    void Start()
    {
        MaxShip = 1;
        ships = new List<ShipStats>();
        AddShip();
        Debug.Log(ships[0].ToString());
    }


    public ShipStats GetShip(int index)
    {
        return ships[index];
    }


    public void AddShip() { 
        if (ships.Count >= MaxShip)
        {
            return;
        }
        ShipStats newShip = new ShipStats();
        ships.Add(newShip);
        ShipsUiManager.Instance.AddShip();
    }

    public void UpgradeCapacity(int capacity)
    {
        MaxShip += capacity;
    }

    public void UseShip(int index)
    {
        if (index < 0 || index >= ships.Count)
        {
            return;
        }
        ships[index].Available = false;
    }

    public void UpgradeModuleLevel(int index, string moduleName)
    {
        ships[index].UpgradeModuleLevel(moduleName);
    }

    public void AssignQuest(JSON.Data quest, int index)
    {
        ships[index].AssignedQuest = quest;
    }

    public void EndQuest(int index)
    {
        ships[index].AssignedQuest = null;
        ships[index].Available = true;
    }


    //---JOJO---//
    public List<Ship> GetShipList()
    {
        return shipList;
    }

    public void AddShip(Ship ship)
    {
        GetShipList().Add(ship);
    }
    
    public void CalculateAllShipDurationToTarget()
    {
        foreach (var ship in GetShipList())
        {
            float speed = ship.Resource.shipStats.modulesDict["Reactor"].Stat;
            var targetPosition = ship.GetPlanet().transform.position;

            float distance = Vector3.Distance(ship.transform.position, targetPosition);
            float duration = distance / speed;
            //ship.SetDurationToTarget(duration);
        }
    }

    public float CalculateShipDurationToTarget(Ship ship)
    {
        
        float speed = ship.Resource.shipStats.modulesDict["Reactor"].Stat;
        var targetPosition = ship.GetPlanet().transform.position;

        float distance = Vector3.Distance(ship.transform.position, targetPosition);
        float duration = distance / speed;
        return duration;
         //ship.SetDurationToTarget(duration);
        
    }
}
