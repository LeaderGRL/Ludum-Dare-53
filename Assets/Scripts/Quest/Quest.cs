using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject textPrefab;

    private void Start()
    {
        JSON.Data[] quests = JSON.Reader<JSON.Data[]>("/Quest/quest.json");
        foreach (var quest in quests)
        {
            GameObject text = Instantiate(textPrefab, parent.transform);
            text.GetComponentInChildren<TMP_Text>().text = quest.name;
            parent.GetComponent<RectTransform>().rect.Set(parent.GetComponent<RectTransform>().rect.x,
                parent.GetComponent<RectTransform>().rect.y, parent.GetComponent<RectTransform>().rect.width,
                parent.GetComponent<RectTransform>().rect.height + 80);
            Debug.Log(quest.name);
        }

    }
}