using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class QuestBehaviours {
    public float enemySpawnRate = 1f;
    public Vector2 spawnDirection = Vector2.zero;
    float time;
    int unitCounter = 0;
    int unitCounter2 = 0;

    // behaviour for spawn quest
    internal IEnumerator Execute(SpawnQuest spawnQuest) {
        Debug.Log("[QUESTS] Starting quest");
        for (int i = 0; i < spawnQuest.waves.Length; i++) {
            Debug.Log("[QUESTS/WAVES] starting wave " + i);
            for (int j = 0; j < spawnQuest.waves[i].times; j++) {
                Debug.Log("[QUESTS/WAVES/ENEMY] spawning enemy " + j);
                yield return new WaitForSeconds(enemySpawnRate);
                spawnQuest.lib.Spawn(spawnQuest.waves[i].itemId, 
                    GameManager.Instance.zones.GetRandomSpawnPosInSquareEdge(Vector2.zero, spawnDirection));
            }
        }
    }
    
}
