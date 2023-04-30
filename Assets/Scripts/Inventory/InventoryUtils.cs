using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUtils : MonoBehaviour
{

    private static InventoryUtils _instance;

    [SerializeField] private CanvasGroup mainGui;

    [SerializeField] private CanvasGroup shipInterface;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text shipPlanetName;
    [SerializeField] private Slider fuelLevel;

    [SerializeField] private CanvasGroup planetInterface;
    [SerializeField] private TMP_Text planetName;
    [SerializeField] private TMP_Text planetRotationSpeed;


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

    public static InventoryUtils GetInventoryUtils()
    {
        return _instance;
    }
}
