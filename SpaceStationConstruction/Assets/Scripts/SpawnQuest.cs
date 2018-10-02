
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnQuest", menuName = "Quests/SpawnQuest", order = 1)]
public class SpawnQuest:ScriptableObject {
    public PrefabLib lib;
    public WaveItem[] waves;
}

interface IQuest {
    bool IsComplete { get; }
    void Execute();
}