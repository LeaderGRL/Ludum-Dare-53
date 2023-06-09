using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUtils : MonoBehaviour
{

    private static InventoryUtils _instance;
    
    [HideInInspector] public Resource itemShowed;

    [SerializeField] private CanvasGroup mainGui;

    [Header("Ship Interface")]
    [SerializeField] private CanvasGroup shipInterface;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text shipPlanetName;
    [SerializeField] private Slider fuelLevel;
    [SerializeField] private Button sendButton;

    [SerializeField] private CanvasGroup shipBuyMenu;

    [SerializeField] private CanvasGroup planetInterface;
    [SerializeField] private TMP_Text planetName;
    [SerializeField] private TMP_Text planetRotationSpeed;

    [SerializeField] private CanvasGroup buyBuildingInterface;
    [SerializeField] private Button hidePlanetInterfaceButton;

    [Header("Global Ships")]
    [SerializeField] private GameObject globalShipsInventory;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject shipPrefab;

    [Header ("Planet shop")]
    [SerializeField] private CanvasGroup planetShopInterface;
    [SerializeField] private GameObject buyStationButton;
    [SerializeField] private GameObject buyDrillerButton;

    [Header("Planet Structures")]
    [SerializeField] private GameObject structuresGroup;
    [SerializeField] private GameObject structureDisplayPrefab;

    [SerializeField] private Sprite iconeStation;
    [SerializeField] private Sprite iconeExtractor;
    [SerializeField] private Sprite iconeDefaultStruct;

    [Header("Station Inventory")]
    [SerializeField] private GameObject stationInterface;
    [SerializeField] private Button buyShipButton;
    [SerializeField] private ShipResource shipResource;

    [SerializeField] private Animator hangar;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            return;
        }
    }
    
    /*
     * Ship Interface
     */
    public void SpaceSheepInterface(Resource spaceSheep, bool show)
    {
        if (show)
        {
            hangar.Play("hangar.hangar", -1, 0f);
            shipInterface.alpha = 1;
            shipInterface.interactable = true;
            shipInterface.blocksRaycasts = true;

            level.text = "SpaceShip";
            ShipResource shipResource = (ShipResource)spaceSheep;
            shipPlanetName.text = shipResource.planet.name;
            fuelLevel.value = spaceSheep.fuelLevel;

            string questName = "";
            
            sendButton.onClick.RemoveAllListeners();
            sendButton.onClick.AddListener(() =>
            {
                if (dropdown.options.Count > 0)
                {
                    questName = dropdown.options[dropdown.value].text;
                }
                if (questName == null || questName == "")
                {
                    return;
                }
                JSON.Data? quest = Quest.instance.GetQuestByName(questName);
                if (quest == null)
                {
                    return;    
                }
                

                Station station = (Station)shipResource.building;
                shipResource.shipStats.SetAssignedQuest(quest);
                station.SendShip(shipResource, shipPrefab);
                SpaceSheepInterface(spaceSheep, false);
            });
        }
        else
        {
            shipInterface.alpha = 0;
            shipInterface.interactable = false;
            shipInterface.blocksRaycasts = false;
        }
    }
    
    public void ShowSpaceSheepBuyMenu(Resource item, bool show)
    {
        if (show)
        {
            shipBuyMenu.interactable = true;
            shipBuyMenu.blocksRaycasts = true;
            shipBuyMenu.alpha = 1;
        }
        else
        {
            shipBuyMenu.interactable = false;
            shipBuyMenu.blocksRaycasts = false;
            shipBuyMenu.alpha = 0;
        }
    }
    
    public void BuySpaceSheep()
    {
        InventoryManager.instance.ChangeToSpaceShip();
        shipBuyMenu.interactable = false;
        shipBuyMenu.blocksRaycasts = false;
        shipBuyMenu.alpha = 0;
        itemShowed = null;
    }
    
    public void CancelBuySpaceSheep()
    {
        shipBuyMenu.interactable = false;
        shipBuyMenu.blocksRaycasts = false;
        shipBuyMenu.alpha = 0;
        itemShowed = null;
    }
    
    public void AssignMission(int value)
    {
        
    }
    
    /*
     * Planet Interface
     */
    public void PlanetInterface(Planet planet, bool show)
    {
        if (show)
        {
            mainGui.alpha = 0;
            mainGui.interactable = false;
            mainGui.blocksRaycasts = false;

            planetName.text = planet.name;
            planetRotationSpeed.text = planet.RotationSpeed.ToString();
            
            planetInterface.alpha = 1;
            planetInterface.interactable = true;
            planetInterface.blocksRaycasts = true;

            hidePlanetInterfaceButton.onClick.RemoveAllListeners();
            hidePlanetInterfaceButton.onClick.AddListener(() =>
            {
                PlanetInterface(planet, !show);
            });

            for (int i = 0; i < structuresGroup.transform.childCount; i++)
            {
                Transform slot = structuresGroup.transform.GetChild(i);
                slot.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                if (i < planet.Buildings.Count)
                {
                    // Mettre l'icone d'ui building
                    if (typeof(Station).IsInstanceOfType(planet.Buildings[i]))
                    {
                        slot.GetChild(0).gameObject.GetComponent<Image>().sprite = iconeStation;
                        Station station = (Station)planet.Buildings[i];
                        slot.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                            DisplayStationInterface(station);
                        });


                        continue;
                    }
                    if (typeof(Driller).IsInstanceOfType(planet.Buildings[i]))
                    {
                        slot.GetChild(0).gameObject.GetComponent<Image>().sprite = iconeExtractor;
                        Driller driller = (Driller)planet.Buildings[i];
                        slot.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
                        {
                            foreach (var item in InventoryManager.instance.GetAllInventories())
                            {
                                item.SetActive(false);
                            }
                            foreach (var inventory in InventoryManager.instance.buildingsInventories[driller])
                            {
                                inventory.SetActive(true);
                            }
                            UnDisplayBuyShipInterface();
                        });
                        
                        
                        continue;
                    }


                }
                // Mettre icone acheter building
                slot.GetChild(0).gameObject.GetComponent<Image>().sprite = iconeDefaultStruct;
                slot.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
                {
                    foreach (var item in InventoryManager.instance.GetAllInventories())
                    {
                        item.SetActive(false);
                    }
                    DisplayBuyShipInterface(planet);
                });
               

            }
        }
        else
        {
            planetInterface.alpha = 0;
            planetInterface.interactable = false;
            planetInterface.blocksRaycasts = false;
            
            mainGui.alpha = 1;
            mainGui.interactable = true;
            mainGui.blocksRaycasts = true;

            foreach (var item in InventoryManager.instance.GetAllInventories())
            {
                item.SetActive(false);
            }

            stationInterface.SetActive(false);
            DisplayGlobalsShips();

        }
    }

    private void DisplayBuyShipInterface(Planet planet)
    {
        planetShopInterface.alpha = 1;
        planetShopInterface.interactable = true;
        planetShopInterface.blocksRaycasts = true;
        buyStationButton.GetComponent<Button>().onClick.RemoveAllListeners();
        buyStationButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            planet.AddBuilding(new Station());
            PlanetInterface(planet, true);
            UnDisplayBuyShipInterface();
        });

        buyDrillerButton.GetComponent<Button>().onClick.RemoveAllListeners();
        buyDrillerButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Driller driller = new Driller();
            planet.AddBuilding(driller);
            PlanetInterface(planet, true);
            UnDisplayBuyShipInterface();
            StartCoroutine(driller.CollectResource(planet));
        });

    }

    private void UnDisplayBuyShipInterface()
    {
        planetShopInterface.alpha = 0;
        planetShopInterface.interactable = false;
        planetShopInterface.blocksRaycasts = false;
    }

    private void DisplayStationInterface(Station station)
    {
        foreach (var item in InventoryManager.instance.GetAllInventories())
        {
            item.SetActive(false);
        }

        foreach (var item in InventoryManager.instance.buildingsInventories[station])
        {
            item.SetActive(true);
        }

        stationInterface.SetActive(true);

        buyShipButton.onClick.RemoveAllListeners();
        buyShipButton.onClick.AddListener(() =>
        {
            ShipResource newShip = ScriptableObject.CreateInstance<ShipResource>();
            newShip.type = shipResource.type;
            newShip.name = shipResource.name;
            newShip.shipStats = new ShipStats();
            newShip.planet = shipResource.planet;
            newShip.value = shipResource.value;
            newShip.icon = shipResource.icon;
            newShip.fuelLevel = shipResource.fuelLevel;
            newShip.currentLocation = shipResource.currentLocation;
            InventoryManager.instance.AddToBuilding(newShip, station, 0);
        });

        UnDisplayBuyShipInterface();
    }

    private void DisplayGlobalsShips()
    {
        RemoveGlobalShips();
        List<ShipResource> ships = InventoryManager.instance.GetAllShips();
        
        for (int i = 0; i < ships.Count; i++)
        {
            InventorySlot slot = globalShipsInventory.transform.GetChild(i).GetComponent<InventorySlot>();
            GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
            newItem.GetComponent<InventoryItem>().InitialiseItem(ships[i]);
        }
    }

    private void RemoveGlobalShips()
    {
        for (int i = 0; i < globalShipsInventory.transform.childCount; i++)
        {
            Transform slot = globalShipsInventory.transform.GetChild(i);
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }

    public static InventoryUtils GetInventoryUtils()
    {
        return _instance;
    }


}
