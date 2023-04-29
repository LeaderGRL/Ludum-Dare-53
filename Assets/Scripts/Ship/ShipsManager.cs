using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ShipsManager : MonoBehaviour
{
    public List<ShipStats> ships;

    public int MaxShip;

    private static GameObject _instance;

    public static GameObject Instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.Log("Destroy instance");
            Destroy(gameObject);
            return;
        }
        _instance = gameObject;
    }

    void Start()
    {
        MaxShip = 1;
        ships = new List<ShipStats>
        {
            new ShipStats()
        };
        Debug.Log(ships[0].ToString());
    }

    public void AddShip() { 
        if (ships.Count >= MaxShip)
        {
            return;
        }
        ships.Add(new ShipStats());
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

}
