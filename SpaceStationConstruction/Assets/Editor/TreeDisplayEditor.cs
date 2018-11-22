using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeDisplayEditor:EditorWindow {
    Vector2 scroll2;
    Vector2 scroll;
    OneWayTreeNode node;
    int fileId = -1;

    List<OneWayTreeNode> allFiles = new List<OneWayTreeNode>();

    [MenuItem("Window/Quest display")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        TreeDisplayEditor window = (TreeDisplayEditor)EditorWindow.GetWindow(typeof(TreeDisplayEditor));
        window.ShowAuxWindow();
        window.autoRepaintOnSceneChange = true;
    }

    private void OnGUI() {
        if (Application.isPlaying) {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            DrawTree(GameManager.Instance.quests.GetQuestTree());
            EditorGUILayout.EndScrollView();
        } else {
            EditorGUILayout.LabelField("Press play... \n 0 is different than DEFAULT. -1 is DEFAULT.");
            scroll = EditorGUILayout.BeginScrollView(scroll);
            if (GUILayout.Button("Open load path")) {
                node = QuestBehaviours.LoadTree(fileId);
            }
            if (GUILayout.Button("Load ALL quest files ")) {
                allFiles.Clear();
                for (int i = 0; i < 100; i++) {
                    OneWayTreeNode node1 = QuestBehaviours.LoadTree(i);
                    if (node1 != null)
                    allFiles.Add(node1);
                }
            }
            if (GUILayout.Button("Load DEFAULT quest file ")) {
                node = QuestBehaviours.LoadTree(-1);
            }
            for (int i = 0; i < allFiles.Count; i++) {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Load quest file " + i)) {
                    node = allFiles[i];
                }
                if (GUILayout.Button("Save as default" + i)) {
                    QuestBehaviours.SaveEmptyTree(allFiles[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
            // atm only for saving.
            fileId = EditorGUILayout.IntField(fileId);
            
            if (GUILayout.Button("Save quest file")) {
                if (fileId > -1)
                    QuestBehaviours.SaveEmptyTree(node, fileId);
                else QuestBehaviours.SaveEmptyTree(node);
                
            }

            // edit tree - add items, edit them.
            if (node != null && node.data != null && node.data.reward!= null) {
                DrawEditableTree(node);
            }
            EditorGUILayout.EndScrollView();
        }

    }

    public void DrawEditableTree(OneWayTreeNode node) {
        node.data.title = EditorGUILayout.TextField("Title", node.data.title);
        node.data.description = EditorGUILayout.TextField("Description: " + node.data.description, node.data.description);
        node.data.reward.money = EditorGUILayout.IntField("Money: " + node.data.reward.money, node.data.reward.money);

        // waves ...
        for (int i = 0; i < node.data.waves.Length; i++) {
            WaveItem item = node.data.waves[i];
            item.itemId = (WaveSpawnItems)EditorGUILayout.EnumFlagsField("Unit: " + item.itemId, item.itemId);
            item.times = EditorGUILayout.IntField("Times: " + item.times, item.times);
            item.spawnRate = EditorGUILayout.FloatField("Spawn rate: " + item.spawnRate, item.spawnRate);
            if (GUILayout.Button("- wave")) {
                node.data.RemoveWave(i);
            }
        }
        if (GUILayout.Button("+ wave")) {
            node.data.AddWave(new WaveItem { itemId = 0, times = 1, spawnRate = GameManager.EnemySpawnRate});
        }
        // end waves...

        if (GUILayout.Button("+ node")) {
            node.children.Add(new OneWayTreeNode() {
                children = new System.Collections.Generic.List<OneWayTreeNode>(),
                data = new QuestInfo() {
                    title = "Not assigned",
                    reward = new QuestReward() { money = 100 },
                    description = "empty",
                    waves = new WaveItem[] { new WaveItem { itemId = 0, times = 1 } }
                }
            }
            );
        }
        for (int i = 0; i < node.children.Count; i++) {
            EditorGUILayout.LabelField("L_" + i + "_>");
            EditorGUI.indentLevel++;
            DrawEditableTree(node.children[i]);
            EditorGUI.indentLevel--;
        }
    }

    public void DrawTree(OneWayTreeNode node) {
        EditorGUILayout.LabelField(node.data.title);
        EditorGUILayout.LabelField("Description: "+ node.data.description);
        EditorGUILayout.LabelField("Money: " +node.data.reward.money);
        foreach (var item in node.data.waves) {
            EditorGUILayout.LabelField("Unit: " + item.itemId);
            EditorGUILayout.LabelField("Times: " + item.times);
            EditorGUILayout.LabelField("Spawn rate: " + item.spawnRate);
        }
        for (int i = 0; i < node.children.Count; i++) {
            EditorGUILayout.LabelField("L_"+i+"_>");
            EditorGUI.indentLevel++;
            DrawTree(node.children[i]);
            EditorGUI.indentLevel--;
        }
    }
}
