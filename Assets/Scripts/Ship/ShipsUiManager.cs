using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsUiManager : MonoBehaviour
{
    private int _selectedShipIndex;
    private ShipStats _selectedShip;

    [SerializeField] private GameObject shipInfos;
    [SerializeField] private GameObject shipsUIPanel;
    [SerializeField] private GameObject shipUIPrefab;

    public static ShipsUiManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _selectedShipIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectShip(int index)
    {
        UnSelectShip();
        shipInfos.SetActive(true);
        _selectedShipIndex = index;
        _selectedShip = ShipsManager.Instance.GetShip(_selectedShipIndex);
    }

    public void UnSelectShip()
    {
        shipInfos.SetActive(false);
        _selectedShipIndex = -1;
        _selectedShip = null;
    }


    public void AddShip()
    {
        Instantiate(shipUIPrefab, shipsUIPanel.transform);
    }

    public void AssignQuest(JSON.Data quest, int shipIndex)
    {
        _selectedShip.AssignedQuest = quest;
        ShipsManager.Instance.AssignQuest(quest, shipIndex);
    }

    public void EndQuest(int shipIndex)
    {
        ShipsManager.Instance.EndQuest(shipIndex);
    }
}
