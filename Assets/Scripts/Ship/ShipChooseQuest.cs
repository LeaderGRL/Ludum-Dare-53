using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipChooseQuest : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    private void Start()
    {
        UpdateSelections();
    }

    public void UpdateSelections()
    {
        RemoveSelections();
        
        int playerLevel = PlayerManager.instance.GetLevel();
        List<JSON.Data> quests = Quest.instance.filterAcceptQuest(Quest.instance.GetQuests(), playerLevel);
        foreach (var item in quests)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(item.name));

        }
        dropdown.RefreshShownValue();
    }

    public void RemoveSelections()
    {
        if (dropdown == null)
        {
            Debug.Log("Not find dropdown component");
            return;
        }
        dropdown.options.Clear();
    }
}
