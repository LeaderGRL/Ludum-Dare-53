using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipsUiManager : MonoBehaviour
{
    private int _selectedShipIndex;
    private ShipStats _selectedShip;

    [SerializeField] private GameObject shipInfos;
    [SerializeField] private GameObject shipsUIPanel;
    [SerializeField] private GameObject shipUIPrefab;


    private List<GameObject> shipList;
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
        shipList = new List<GameObject>();
        _selectedShipIndex = -1;
    }

    // Update is called once per frame

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
        if (shipList.Count >= shipsUIPanel.transform.childCount)
        {
            return;
        }
        for (int i = 0; i < shipsUIPanel.transform.childCount; i++)
        {
            if (shipsUIPanel.transform.GetChild(i).childCount == 0)
            {
                CreateUIShip(shipsUIPanel.transform.GetChild(i));
                return;
            }
        }
       
    }

    private void CreateUIShip(Transform parent)
    {
        GameObject newShip = Instantiate(shipUIPrefab, parent);
        newShip.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Test");
        });

        shipList.Add(newShip);

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
