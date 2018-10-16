
using System;
using UnityEngine;

/// <summary>
/// Wave quest
/// </summary>
[CreateAssetMenu(fileName = "SpawnQuest", menuName = "Quests/SpawnQuest", order = 1)]
public class SpawnQuest:ScriptableObject {
    public PrefabLib lib;
    public WaveItem[] waves;
    public QuestReward reward;
}

interface IQuest {
    bool IsComplete { get; }
    void Execute();
}