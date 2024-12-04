using UnityEngine;

public class QuestCollision : MonoBehaviour
{
    [SerializeField] private QuestManager.Quests quest;
    [SerializeField] private bool completing = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            if(completing)
                QuestManager.Instance.FinishQuest(quest);
            else
                QuestManager.Instance.AddQuest(quest);
    }
}
