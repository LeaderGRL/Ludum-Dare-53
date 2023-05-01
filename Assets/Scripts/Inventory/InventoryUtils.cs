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

    [Header ("Planet shop")]
    [SerializeField] private CanvasGroup planetShopInterface;
    [SerializeField] private GameObject buyStationButton;
    [SerializeField] private GameObject buyExtractorButton;

    [Header("Planet Structures")]
    [SerializeField] private GameObject structuresGroup;
    [SerializeField] private GameObject structureDisplayPrefab;

    [SerializeField] private Sprite iconeStation;
    [SerializeField] private Sprite iconeExtractor;
    [SerializeField] private Sprite iconeDefaultStruct;
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
                        List<GameObject> inventories = InventoryManager.instance.buildingsInventories[planet.Buildings[i]];
                        slot.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                            foreach (var item in inventories)
                            {
                                item.SetActive(true);
                            }
                        });


                        continue;
                    }
                    
                }
                // Mettre icone acheter building
                slot.GetChild(0).gameObject.GetComponent<Image>().sprite = iconeDefaultStruct;
                slot.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
                {
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
        
    }

    private void UnDisplayBuyShipInterface()
    {
        planetShopInterface.alpha = 0;
        planetShopInterface.interactable = false;
        planetShopInterface.blocksRaycasts = false;
    }



    public static InventoryUtils GetInventoryUtils()
    {
        return _instance;
    }
}
