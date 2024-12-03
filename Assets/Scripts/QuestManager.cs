using TMPro;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public enum Quests
    {
        Default,
        VillagePerson1,
        VillagePerson2,
    }

    private Dictionary<Quests, string> questTexts = new Dictionary<Quests, string>();

    [SerializeField] private GameObject questFab;
    private List<TextMeshProUGUI> questList = new List<TextMeshProUGUI>();
    private Dictionary<Quests, int> questLoc = new Dictionary<Quests, int>();

    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if(instance == null)
                instance = new QuestManager();

            return instance;
        }
    }

    public void AddQuest(Quests quest)
    {
        questList.Add(Instantiate(questFab, transform).GetComponent<TextMeshProUGUI>());
        questLoc.Add(quest, questList.Count);
        questList.Last().transform.position += new Vector3(0, -questList.Last().rectTransform.rect.height * (questList.Count - 1));
        questList.Last().text = questTexts[quest];
    }

    public void FinishQuest(Quests quest)
    {
        questList[questLoc[quest]].text = "<s>" + questList[questLoc[quest]].text + "<s>";
    }

    public void Flush()
    {
        questList.ForEach(each => Destroy(each.gameObject));
        questList.Clear();
    }

    private void Awake()
    {
        AddQuestTexts();
        AddQuest(Quests.VillagePerson1);
        AddQuest(Quests.VillagePerson2);
        FinishQuest(Quests.VillagePerson1);
    }

    private void AddQuestTexts()
    {
        questTexts.Add(Quests.VillagePerson1, "Talk with Guy");
        questTexts.Add(Quests.VillagePerson2, "Talk with Duda");
    }
}
