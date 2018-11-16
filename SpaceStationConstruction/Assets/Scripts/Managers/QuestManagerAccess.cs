using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerAccess {

    QuestManager manager;

    public QuestManagerAccess() {
        manager = GameObject.FindObjectOfType<QuestManager>();
    }

    public QuestInfo ActiveQuest { get { return manager.executingQuest; } }

    public QuestInfo[] ActiveQuestSet {
        get {
            // cur implementation for quest set.
            QuestInfo[] info = new QuestInfo[manager.selectedQuests.quests.Length];
            for (int i = 0; i < info.Length; i++) {
                info[i] = manager.selectedQuests.quests[i];
            }
            return info;
        }
    }

    internal void SetQuestTree(OneWayTreeNode root){//QuestInfo[] quests) {
        manager.allQuestsAsTree = root;
    }

    public OneWayTreeNode GetQuestTree() {
        return manager.allQuestsAsTree;
    }

    public bool AnyQuest { get { return !manager.noQuests; } }

    public void StartWaves() {
        GameManager.Instance.StartCoroutine(manager.WaveUpdate1());
    }

    internal void PickQuestFromList(int i) {
        // also pick next quest.
        QuestSet data = manager.selectedQuests;
        if (i < data.quests.Length) {
            Debug.Log("Setting active quest "+i+ " "+ (i < manager.selectedQuests.quests.Length)+" "+ manager.selectedQuests.quests[i]);
            QuestInfo v1 = data.quests[i];
            manager.executingQuest = v1;
        } else {
            Debug.Log("Setting active quest "+i+ " "+ (i < manager.selectedQuests.quests.Length));
        }
    }

    public void SetNextSetOfQuests() {
        manager.selectedQuests = new QuestSet() {
            quests = GetNextSet(manager.lastExecutingQuest)
        };
    }



    

    internal void SetQuest(OneWayTreeNode tree) {
        GameManager.Instance.quests.SetQuestTree(
            tree
        );
    }

    // picks next item, or root.
    QuestInfo[] GetNextSet(QuestInfo src) {
        List<QuestInfo> list = new List<QuestInfo>();
        if (src == null) { // we don't have the root yet
            list.Add(manager.allQuestsAsTree.questData);
        } else {
            for (int i = 0; i < manager.allQuestsAsTree.children.Count; i++) {
                list.Add(manager.allQuestsAsTree.children[i].questData);
            }
        }
        return list.ToArray();
    }
    
}
