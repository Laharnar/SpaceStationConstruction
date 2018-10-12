using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Quests/Info", order = 1)]
public class QuestInfoScriptableObj : ScriptableObject {
    public QuestInfo info;

    public SpawnQuest AsWaveQuest { get { return info.waveData; } }
}
