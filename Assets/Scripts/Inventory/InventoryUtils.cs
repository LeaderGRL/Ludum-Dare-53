using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUtils : MonoBehaviour
{

    private static InventoryUtils _instance;
    
    [HideInInspector] public Resource itemShowed;

    [SerializeField] private CanvasGroup mainGui;

    [SerializeField] private CanvasGroup shipInterface;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text shipPlanetName;
    [SerializeField] private Slider fuelLevel;
    
    [SerializeField] private CanvasGroup shipBuyMenu;

    [SerializeField] private CanvasGroup planetInterface;
    [SerializeField] private TMP_Text planetName;
    [SerializeField] private TMP_Text planetRotationSpeed;

    [SerializeField] private CanvasGroup buyBuildingInterface;
    [SerializeField] private Button hidePlanetInterfaceButton;

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
            shipInterface.alpha = 1;
            shipInterface.interactable = true;
            shipInterface.blocksRaycasts = true;

            level.text = "SpaceSheep";
            shipPlanetName.text = spaceSheep.currentLocation;
            fuelLevel.value = spaceSheep.fuelLevel;
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
                Debug.Log(planet.Buildings.Count);
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
            Debug.Log("Buy Station!");
            planet.AddBuilding(new Station());
            PlanetInterface(planet, true);
            UnDisplayBuyShipInterface();
        });

        buyDrillerButton.GetComponent<Button>().onClick.RemoveAllListeners();
        buyDrillerButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("Buy Driller!");
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
            InventoryManager.instance.AddToBuilding(new ShipResource(), station, 0);
        });

        UnDisplayBuyShipInterface();
    }

    public static InventoryUtils GetInventoryUtils()
    {
        return _instance;
    }
}
