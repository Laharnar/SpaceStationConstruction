using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestBehaviours {
    public float enemySpawnRate = 1f;
    public Vector2 spawnDirection = Vector2.zero;
    float time;
    int unitCounter = 0;
    int unitCounter2 = 0;

    internal IEnumerator QuestManageCycle() {
        while (true) {

            // load proper set
            //

            // load quests on ui
            GameManager.Instance.ui.ShowQuestChoices(GameManager.Instance.quests.ActiveQuestSet);

            // wait ui input, then close ui
            while (!GameManager.Instance.ui.anySelectedQuest) {
                yield return null;
            }
            GameManager.Instance.ui.ConsumeSelectedQuest();
            GameManager.Instance.ui.HideQuestUI();

            // start quest and for it to end
            QuestReward reward = GameManager.Instance.quests.ActiveQuest.reward;
            GameManager.Instance.quests.StartWaves();
            while (GameManager.Instance.quests.AnyQuest) {
                yield return null;
            }

            // apply quest results
            // temporary solution
            GameManager.Instance.station.BuildNextPiece(GameManager.Instance.station.NextAvaliablePieces()[0]);
            //reward.Apply(GameManager.Instance.station.NextBuildPos);

            Debug.Log("[QUEST] current quest ended");
            yield return null;
        }
    }
    
    // behaviour for spawn quest
    // looks like bad diamond structure already
    internal IEnumerator SpawnAndWaitDeathQuest(SpawnQuest spawnQuest) {
        Debug.Log("[QUESTS] Starting quest");
        Stack<Transform> spawns = new Stack<Transform>();
        for (int i = 0; i < spawnQuest.waves.Length; i++) {
            Debug.Log("[QUESTS/WAVES] starting wave " + i);
            for (int j = 0; j < spawnQuest.waves[i].times; j++) {
                Debug.Log("[QUESTS/WAVES/ENEMY] spawning enemy " + j);
                yield return new WaitForSeconds(enemySpawnRate);
                Vector2 spawnPos = GameManager.Instance.zones.GetRandomSpawnPosInSquareEdge(Vector2.zero, spawnDirection);
                Transform t = spawnQuest.lib.Spawn(spawnQuest.waves[i].itemId, spawnPos);
                spawns.Push(t);
            }
        }
        while (spawns.Count > 0) {
            if (spawns.Peek() == null) {
                spawns.Pop();
            }
            yield return null;
        }
        Debug.Log("[QUESTS] All enemies dead, ending quest");
    }

}
