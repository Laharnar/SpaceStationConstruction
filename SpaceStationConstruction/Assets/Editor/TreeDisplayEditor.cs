using UnityEditor;
using UnityEngine;

public class TreeDisplayEditor:EditorWindow {
    Vector2 scroll;

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
