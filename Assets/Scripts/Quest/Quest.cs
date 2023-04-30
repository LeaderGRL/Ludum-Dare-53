using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//to do : bouton accept et bouton quitter la quete
public class Quest : MonoBehaviour
{
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


    private int nPage = 0;
    private List<JSON.Data> filteredQuest;
    private List<List<JSON.Data>> page;
    private JSON.Data[] quests;

    private void Start()
    {
        quests = JSON.Reader<JSON.Data[]>("/Quest/quest.json");
        ShowQuest();
    }

    private void ShowQuest()
    {
        filteredQuest = filterQuest(quests, PlayerManager.instance.GetLevel());
        page = pagination(filteredQuest, 5);
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }

        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        for (int i = 0; i < page[nPage].Count; i++)
        {
            int index = i;
            JSON.Data quest = page[nPage][i];
            int id = quest.id;
            GameObject text = Instantiate(textPrefab, parent.transform);
            text.GetComponentInChildren<TMP_Text>().text = quest.name;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,
                parent.GetComponent<RectTransform>().sizeDelta.y + parent.GetComponent<GridLayoutGroup>().cellSize.y);
            text.AddComponent<Button>().onClick.AddListener(() =>
            {
                questDetails.SetActive(true);
                nameText.GetComponent<TMP_Text>().text = "Name : " + quest.name;
                descText.GetComponent<TMP_Text>().text = "Description : " + quest.description;
                timeTraverText.GetComponent<TMP_Text>().text = "Time travel : " + quest.time;
                RewardText.GetComponent<TMP_Text>().text =
                    "Rewards : " + quest.reward.xp + "xp " + quest.reward.gold + "Nebulite";
                DestinationText.GetComponent<TMP_Text>().text = "Destination : " + quest.destination["planet"];
                materialsText.GetComponent<TMP_Text>().text = "Required materials : ";
                acceptButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    quest.inProgress = true;
                    questDetails.SetActive(false);
                    quests.SetValue(quest, id);
                    ShowQuest();
                });
                foreach (var material in quest.materials)
                {
                    materialsText.GetComponent<TMP_Text>().text += material.Key + " " + material.Value + "\n";
                }
            });
        }
    }

    private List<JSON.Data> filterQuest(JSON.Data[] quests, int level)
    {
        List<JSON.Data> filteredQuests = new List<JSON.Data>();
        foreach (var quest in quests)
        {
            if (level < quest.levelRequired || quest.inProgress) continue;
            filteredQuests.Add(quest);
        }

        return filteredQuests;
    }

    public void LevelUp()
    {
        PlayerManager.instance.AddExperience(100);
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
}