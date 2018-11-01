using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestTreeDirections {
    public IntSerializableArr[] nodes;
}

[System.Serializable]
public class IntSerializableArr {
    public int[] directions;
}
[System.Serializable]
public class QuestBehaviours {

    public float enemySpawnRate = 1f;
    public Vector2 spawnDirection = Vector2.zero;
    float time;
    int unitCounter = 0;
    int unitCounter2 = 0;

    public const string QUESTS_LOAD_PATH = "Content/quests.json";
    public const string QUESTTREE_LOAD_PATH = "Content/tree.json";

    public int mode = 0;
    internal IEnumerator QuestManageCycle() {
        if (mode == 0) {
            LoadJsonFiles.SaveSerializableToJson(
                new QuestSet(){ quests = new QuestInfo[1] {
                    new QuestInfo() { description = "tet", title = "tett",
                        reward = new QuestReward() { money = 100, mode=0 },
                        waves = new []{
                            new WaveItem() { itemId = WaveSpawnItems.FIGHTER1, times = 5 } } } } }, QUESTS_LOAD_PATH, true);
            LoadJsonFiles.SaveSerializableToJson(
                new QuestTreeDirections() {
                    nodes = new IntSerializableArr[1] {
                        new IntSerializableArr() {
                            directions = new int[1] { 0 } } } }, QUESTTREE_LOAD_PATH, true);
            yield break;
        }
        // load all quests in game from files.
        GameManager.Instance.quests.LoadQuestsFromFiles(QUESTS_LOAD_PATH, QUESTTREE_LOAD_PATH);
        
        while (true) {

            // load proper set
            GameManager.Instance.quests.SetNextSetOfQuests();

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
            GameManager.Instance.station.BuildNextPiece(GameManager.Instance.station.NextAvaliablePiece());
            //reward.Apply(GameManager.Instance.station.NextBuildPos);

            Debug.Log("[QUEST] current quest ended");
            yield return null;
        }
    }
    
    // behaviour for spawn quest
    // looks like bad diamond structure already
    internal IEnumerator SpawnAndWaitDeathQuest(QuestInfo spawnQuest) {
        Debug.Log("[QUESTS] Starting quest");
        Stack<Transform> spawns = new Stack<Transform>();
        for (int i = 0; i < spawnQuest.waves.Length; i++) {
            Debug.Log("[QUESTS/WAVES] starting wave " + i);
            for (int j = 0; j < spawnQuest.waves[i].times; j++) {
                Debug.Log("[QUESTS/WAVES/ENEMY] spawning enemy " + j);
                yield return new WaitForSeconds(enemySpawnRate);
                Vector2 spawnPos = GameManager.Instance.zones.GetRandomSpawnPosInSquareEdge(Vector2.zero, spawnDirection);
                
                Transform t = GameManager.Instance.spawnLib.Spawn((int)spawnQuest.waves[i].itemId, spawnPos);
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
