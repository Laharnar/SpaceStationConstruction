
using System.Collections;
using UnityEngine;
public class QuestSet {
    QuestInfoScriptableObj[] quests;

}
public class QuestManager:MonoBehaviour {

    [HideInInspector]public SpawnQuest quest;
    public bool noQuests { get; private set; }

    // currently selected quest
    public QuestInfoScriptableObj[] selectedQuests;

    private void Start() {
        StartCoroutine(GameManager.Instance.questBehaviour.QuestManageCycle());
    }

    public IEnumerator WaveUpdate1() {
        //
        //temp code
        while (true) {
            Debug.Log(quest);
            if (quest != null) {
                noQuests = false;
                yield return StartCoroutine(GameManager.Instance.questBehaviour.SpawnAndWaitDeathQuest(quest));
                quest = null;
                noQuests = true;
            }
            yield return null;
        }
    }

}
