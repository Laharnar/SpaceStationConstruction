
using System.Collections;
using UnityEngine;

public class QuestManager:MonoBehaviour {

    [HideInInspector]public SpawnQuest quest;

    // currently selected quest
    public QuestInfoScriptableObj[] selectedQuests;

    private void Start() {
        StartCoroutine(GameManager.Instance.questBehaviour.QuestManagerCycle());
    }

    public IEnumerator WaveUpdate1() {
        //
        //temp code
        while (true) {
            Debug.Log(quest);
            if (quest != null) {
                yield return StartCoroutine(GameManager.Instance.questBehaviour.ExecuteWave(quest));
                quest = null;
            }
            yield return null;
        }
    }

}
