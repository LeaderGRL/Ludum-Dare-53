using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ShipsManager : MonoBehaviour
{
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
        ships.Add(new ShipStats());
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

}
