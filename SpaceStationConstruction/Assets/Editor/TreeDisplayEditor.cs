using UnityEditor;
using UnityEngine;

public class TreeDisplayEditor:EditorWindow {
    Vector2 scroll;
    OneWayTreeNode node;
    int fileId = -1;
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
            EditorGUILayout.LabelField("Press play...");
            if (GUILayout.Button("Open load path")) {

            }
            fileId = EditorGUILayout.IntField(fileId);
            if (GUILayout.Button("Load quest file")) {
                if (fileId > -1)
                    node = QuestBehaviours.LoadTree(fileId);
                else node = QuestBehaviours.LoadTree();
            }
            if (GUILayout.Button("Save quest file")) {
                if (fileId > -1)
                    QuestBehaviours.SaveEmptyTree(node, fileId);
                else QuestBehaviours.SaveEmptyTree(node);
                
            }

            // edit tree - add items, edit them.
            if (node != null && node.data != null && node.data.reward!= null) {
                scroll = EditorGUILayout.BeginScrollView(scroll);
                DrawEditableTree(node);
                EditorGUILayout.EndScrollView();
            }
        }
        
    }

    public void DrawEditableTree(OneWayTreeNode node) {
        node.data.title = EditorGUILayout.TextField(node.data.title);
        node.data.description = EditorGUILayout.TextField("Description: " + node.data.description, node.data.description);
        node.data.reward.money = EditorGUILayout.IntField("Money: " + node.data.reward.money, node.data.reward.money);

        // waves ...
        for (int i = 0; i < node.data.waves.Length; i++) {
            WaveItem item = node.data.waves[i];
            item.itemId = (WaveSpawnItems)EditorGUILayout.EnumFlagsField("Unit: " + item.itemId, item.itemId);
            item.times = EditorGUILayout.IntField("Times: " + item.times, item.times);
            if (GUILayout.Button("- wave")) {
                node.data.RemoveWave(i);
            }
        }
        if (GUILayout.Button("+ wave")) {
            node.data.AddWave(new WaveItem { itemId = 0, times = 1 });
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
        }
        for (int i = 0; i < node.children.Count; i++) {
            EditorGUILayout.LabelField("L_"+i+"_>");
            EditorGUI.indentLevel++;
            DrawTree(node.children[i]);
            EditorGUI.indentLevel--;
        }
    }
}
