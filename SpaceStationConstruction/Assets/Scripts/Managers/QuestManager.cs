
using System.Collections;
using UnityEngine;
/// <summary>
/// Handles quest data
/// </summary>
/// <seealso cref="QuestBehaviours" handles whole quest loading and player interaction/>
/// <seealso cref="QuestManagerAccess" access for ui and other scripts/>
public class QuestManager:MonoBehaviour {

    // currently executed quest.
    public QuestInfo executingQuest;
    [HideInInspector]internal QuestInfo lastExecutingQuest;

    public bool noQuests { get; private set; }

    // currently selected quest set. changes when quest is completed.
    public QuestSet selectedQuests;
    internal OneWayTreeNode allQuestsAsTree;

    private void Start() {
        StartCoroutine(GameManager.Instance.questBehaviour.QuestManageCycle());
    }

    // SPECIFICALLY spawn quest behaviour.
    // executes a SINGLE quest. handle multiple quests elswhere.
    public IEnumerator WaveUpdate1() {
        //
        //temp code
        //while (true) {
        //if (executingQuest != null) {
        lastExecutingQuest = executingQuest;
        noQuests = false;
        yield return StartCoroutine(GameManager.Instance.questBehaviour.SpawnAndWaitDeathQuest(executingQuest));
        Debug.Log("ended quest... is it too fast?");
        if (executingQuest.reward != null)
            executingQuest.reward.Apply(Vector3.zero);
        else {
            Debug.Log("Err: missing reward on current quest " + executingQuest.title);
        }
        executingQuest = null;
        noQuests = true;
        //}
        //  yield return null;
        //}
    }

}
