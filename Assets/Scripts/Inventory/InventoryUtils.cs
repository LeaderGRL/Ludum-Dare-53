using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUtils : MonoBehaviour
{

    private static InventoryUtils _instance;

    [Header("Ship Interface")]
    [SerializeField] private CanvasGroup shipInterface;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text planet;
    [SerializeField] private Slider fuelLevel;

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
            planet.text = spaceSheep.currentLocation;
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

    public static InventoryUtils GetInventoryUtils()
    {
        return _instance;
    }
}
