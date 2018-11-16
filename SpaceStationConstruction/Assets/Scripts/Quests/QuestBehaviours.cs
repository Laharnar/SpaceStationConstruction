using System;
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
            SaveEmptyTree();
            yield break;
        }
        // load all quests in game from files.
        LoadQuestsFromFiles();
        
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
            yield return null;
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
    public static void SaveEmptyTree(OneWayTreeNode node) {
        LoadJsonFiles.SaveSerializableToJson(
            ExtractQuestSet(node)
                , QUESTS_LOAD_PATH, false);
        LoadJsonFiles.SaveSerializableToJson(
            ExtractQuestDirections(node)
            , QUESTTREE_LOAD_PATH, false);

    }
    public static void SaveEmptyTree(OneWayTreeNode node, int pathId) {
        LoadJsonFiles.SaveSerializableToJson(
            ExtractQuestSet(node)
                , QUESTS_LOAD_PATH + pathId, false);
        LoadJsonFiles.SaveSerializableToJson(
            ExtractQuestDirections(node)
            , QUESTTREE_LOAD_PATH+pathId, false);

    }
    static QuestSet ExtractQuestSet(OneWayTreeNode node) {
        QuestSet set = new QuestSet();
        OneWayTreeNode[] nodes = node.rAsList();
        QuestInfo[] infos = new QuestInfo[nodes.Length];
        for (int i = 0; i < nodes.Length; i++) {
            infos[i] = nodes[i].data;
        }
        set.quests = infos;
        return set;
    }

    static QuestTreeDirections ExtractQuestDirections(OneWayTreeNode node) {
        QuestTreeDirections dirs = new QuestTreeDirections();
        OneWayTreeNode[] nodes = node.rAsList();
        dirs.nodes = new IntSerializableArr[nodes.Length];
        for (int i = 0; i < dirs.nodes.Length; i++) {
            dirs.nodes[i] = new IntSerializableArr();
            dirs.nodes[i].directions = new int[nodes[i].children.Count];
            for (int j = 0; j < dirs.nodes[i].directions.Length; j++) {
                dirs.nodes[i].directions[j] = i+j+1;// untested
            }
        }
        return dirs;
    }

    private void SaveEmptyTree() {
        LoadJsonFiles.SaveSerializableToJson(
                new QuestSet() {
                    quests = new QuestInfo[1] {
                    new QuestInfo() { description = "tet", title = "tett",
                        reward = new QuestReward() { money = 100, mode=0 },
                        waves = new []{
                            new WaveItem() { itemId = WaveSpawnItems.FIGHTER, times = 5 } } } }
                }, QUESTS_LOAD_PATH, true);
        LoadJsonFiles.SaveSerializableToJson(
            new QuestTreeDirections() {
                nodes = new IntSerializableArr[1] {
                        new IntSerializableArr() {
                            directions = new int[1] { 0 } } }
            }, QUESTTREE_LOAD_PATH, true);

    }

    private void LoadQuestsFromFiles() {
        GameManager.Instance.quests.SetQuest(LoadTree());
    }

    public static OneWayTreeNode LoadTree(int variation) {
        return LoadTree(QUESTS_LOAD_PATH+ variation, QUESTTREE_LOAD_PATH + variation);
    }

    public static OneWayTreeNode LoadTree() {
        return LoadTree(QUESTS_LOAD_PATH, QUESTTREE_LOAD_PATH);
    }

    public static OneWayTreeNode LoadTree(string QUESTS_LOAD_PATH, string QUESTTREE_LOAD_PATH) {
        QuestSet qs = LoadJsonFiles.LoadJsonToSerializable<QuestSet>(QUESTS_LOAD_PATH);
        return OneWayTreeNode.ConstructQuestTree(
                qs.quests,
                LoadJsonFiles.LoadJsonToSerializable<QuestTreeDirections>(QUESTTREE_LOAD_PATH)
            );
    }

    // behaviour for spawn quest
    // looks like bad diamond structure already
    internal IEnumerator SpawnAndWaitDeathQuest(QuestInfo spawnQuest) {
        Debug.Log("[QUESTS] Starting quest");
        bool debug = false;
        Stack<Transform> spawns = new Stack<Transform>();
        for (int i = 0; i < spawnQuest.waves.Length; i++) {
            if (debug) Debug.Log("[QUESTS/WAVES] starting wave " + i);
            for (int j = 0; j < spawnQuest.waves[i].times; j++) {
                if (debug) Debug.Log("[QUESTS/WAVES/ENEMY] spawning enemy " + j + " at rate of "+enemySpawnRate);
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
