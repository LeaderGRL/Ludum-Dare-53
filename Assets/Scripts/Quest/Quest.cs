using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject textPrefab;
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
        foreach (var quest in page[nPage])
        {
            GameObject text = Instantiate(textPrefab, parent.transform);
            text.GetComponentInChildren<TMP_Text>().text = quest.name;
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,
                parent.GetComponent<RectTransform>().sizeDelta.y + parent.GetComponent<GridLayoutGroup>().cellSize.y);
            text.AddComponent<Button>().onClick.AddListener(() => { Debug.Log(quest.name); });
        }
    }

    private List<JSON.Data> filterQuest(JSON.Data[] quests, int level)
    {
        List<JSON.Data> filteredQuests = new List<JSON.Data>();
        foreach (var quest in quests)
        {
            if (level < quest.levelRequired) continue;
            filteredQuests.Add(quest);
        }

        return filteredQuests;
    }

    public void LevelUp()
    {
        Debug.Log(PlayerManager.instance.GetLevel());
        PlayerManager.instance.AddExperience(100);
        ShowQuest();
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