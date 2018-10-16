using System;
using UnityEngine;

public class QuestManagerAccess {

    QuestManager manager;

    public QuestManagerAccess() {
        manager = GameObject.FindObjectOfType<QuestManager>();
    }

    public SpawnQuest ActiveQuest { get { return manager.quest; } }

    public QuestInfo[] ActiveQuestSet {
        get {
            QuestInfo[] info = new QuestInfo[manager.selectedQuests.Length];
            for (int i = 0; i < info.Length; i++) {
                info[i] = manager.selectedQuests[i].info;
            }
            return info;
        }
    }

    public bool AnyQuest { get { return !manager.noQuests; } }

    public void StartWaves() {
        Debug.Log("Starting wave 1");
        GameManager.Instance.StartCoroutine(manager.WaveUpdate1());
    }

    internal void SetActiveQuestFromSet(int i) {
        if (i < manager.selectedQuests.Length) {
            Debug.Log("Setting active quest "+i+ " "+ (i < manager.selectedQuests.Length)+" "+ manager.selectedQuests[i].AsWaveQuest);
            manager.quest = manager.selectedQuests[i].AsWaveQuest;
        } else {
            Debug.Log("Setting active quest "+i+ " "+ (i < manager.selectedQuests.Length));
        }
    }
}
