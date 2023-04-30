using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//to do : bouton accept et bouton quitter la quete
public class Quest : MonoBehaviour
{
    public static Quest instance;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private GameObject questDetails;
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject descText;
    [SerializeField] private GameObject timeTraverText;
    [SerializeField] private GameObject RewardText;
    [SerializeField] private GameObject DestinationText;
    [SerializeField] private GameObject materialsText;
    [SerializeField] private GameObject acceptButton;
    [SerializeField] private GameObject parentInProgress;


    private int nPage = 0;
    private List<JSON.Data> filteredQuest;
    private List<List<JSON.Data>> page;
    private JSON.Data[] quests;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            quests = JSON.Reader<JSON.Data[]>("/Quest/quest.json");
            return;
        }

        Destroy(gameObject);
        
    }

    private void Start()
    {
        
        ShowQuest();
    }

    private GameObject InstantiateQuest(JSON.Data quest, GameObject parentOfChild)
    {
        GameObject text = Instantiate(textPrefab, parentOfChild.transform);
        text.GetComponentInChildren<TMP_Text>().text = quest.name;
        parentOfChild.GetComponent<RectTransform>().sizeDelta = new Vector2(0,
            parent.GetComponent<RectTransform>().sizeDelta.y +
            parent.GetComponent<GridLayoutGroup>().cellSize.y);
        return text;
    }

    private void ShowQuestInProgress()
    {
        filteredQuest = filterAcceptQuest(quests, PlayerManager.instance.GetLevel());
        foreach (Transform child in parentInProgress.transform)
        {
            Destroy(child.gameObject);
        }

        // set the parent transform height to 0
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        for (int i = 0; i < filteredQuest.Count; i++)
        {
            JSON.Data quest = filteredQuest[i];
            InstantiateQuest(quest, parentInProgress);
        }
    }

    private void ShowQuest()
    {
        filteredQuest = filterQuest(quests, PlayerManager.instance.GetLevel());
        page = pagination(filteredQuest, 5);
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }

        // set the parent transform height to 0
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        if (page != null && page.Count > nPage)
        {
            for (int i = 0; i < page[nPage].Count; i++)
            {
                var quest = new JSON.Data();
                if (i >= 0 && i < page[nPage].Count)
                {
                    quest = page[nPage][i];
                }

                int id = quest.id;
                GameObject text = InstantiateQuest(quest, parent);
                text.AddComponent<Button>().onClick.AddListener(() =>
                {
                    JSON.Data selectedQuest = quest;
                    questDetails.SetActive(true);
                    nameText.GetComponent<TMP_Text>().text = "Name : " + selectedQuest.name;
                    descText.GetComponent<TMP_Text>().text = "Description : " + selectedQuest.description;
                    timeTraverText.GetComponent<TMP_Text>().text = "Time travel : " + selectedQuest.time;
                    RewardText.GetComponent<TMP_Text>().text =
                        "Rewards : " + selectedQuest.reward.xp + "xp " + selectedQuest.reward.gold + "Nebulite";
                    DestinationText.GetComponent<TMP_Text>().text =
                        "Destination : " + selectedQuest.destination["planet"];
                    materialsText.GetComponent<TMP_Text>().text = "Required materials : ";
                    foreach (var material in selectedQuest.materials)
                    {
                        materialsText.GetComponent<TMP_Text>().text += material.Key + " : " + material.Value + "\n";
                    }

                    acceptButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        questDetails.SetActive(false);
                        for (int j = 0; j < quests.Length; j++)
                        {
                            if (quests[j].id != id) continue;
                            selectedQuest.inProgress = true;
                            quests[j] = selectedQuest;
                            break;
                        }

                        ShowQuestInProgress();
                        ShowQuest();
                        GameObject.Find("DropdownChooseQuest").GetComponent<ShipChooseQuest>().UpdateSelections();
                    });
                });
            }
        }
        else
        {
            Debug.Log("La liste 'page' est nulle ou ne contient pas assez d'éléments.");
        }
    }

    public List<JSON.Data> filterAcceptQuest(JSON.Data[] quests, int level)
    {
        List<JSON.Data> filteredQuests = new List<JSON.Data>();
        foreach (var quest in quests)
        {
            if (level < quest.levelRequired || !quest.inProgress) continue;
            filteredQuests.Add(quest);
        }

        return filteredQuests;
    }

    public List<JSON.Data> filterQuest(JSON.Data[] quests, int level)
    {
        List<JSON.Data> filteredQuests = new List<JSON.Data>();
        foreach (var quest in quests)
        {
            if (quest.once && quest.done) continue;
            if (level < quest.levelRequired || quest.inProgress) continue;
            filteredQuests.Add(quest);
        }

        return filteredQuests;
    }

    public void finishQuest(int questID)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            JSON.Data quest = quests[i];
            if (quest.id != questID) continue;
            quest.done = true;
            quest.inProgress = false;
            quests[i] = quest;
        }
    }

    public void RemoveItem(int questID, Dictionary<string, int> materials)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            JSON.Data quest = quests[i];
            if (quest.id != questID) continue;
            foreach (var material in materials)
            {
                quest.materials[material.Key] -= material.Value;
            }
            quests[i] = quest;
        }
    }
    
        
    
    public void LevelUp()
    {
        PlayerManager.instance.AddExperience(100);
        Debug.Log(PlayerManager.instance.GetLevel());
        ShowQuest();
    }

    public void CloseButton()
    {
        questDetails.SetActive(false);
    }

    private List<List<JSON.Data>> pagination(List<JSON.Data> filteredQuest, int itemsPerPage)
    {
        List<List<JSON.Data>> res = new();
        List<JSON.Data> page = new List<JSON.Data>();
        for (int i = 0; i < filteredQuest.Count; i++)
        {
            if ((i % itemsPerPage == 0 && i != 0) || i == filteredQuest.Count - 1)
            {
                if (i == filteredQuest.Count - 1)
                {
                    page.Add(filteredQuest[i]);
                }

                res.Add(page);
                page = new List<JSON.Data>();
            }

            page.Add(filteredQuest[i]);
        }

        return res;
    }

    public void Next()
    {
        if (nPage < page.Count - 1)
        {
            nPage++;
            ShowQuest();
        }
    }

    public void Previous()
    {
        if (nPage > 0)
        {
            nPage--;
            ShowQuest();
        }
    }

    public JSON.Data[] GetQuests()
    {
        return quests;
    }
}