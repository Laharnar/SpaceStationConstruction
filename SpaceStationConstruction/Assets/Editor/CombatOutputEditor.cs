using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CombatOutputEditor : EditorWindow {

    static CombatOutputEditor window;
    GUIStyle style = new GUIStyle();
    Color[] colors = new Color[] {
        Color.black,
        Color.red
    };
    int filter = 0;

    Vector2 scroll;

    [MenuItem("Window/Combat display")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        window = (CombatOutputEditor)EditorWindow.GetWindow(typeof(CombatOutputEditor));
        window.ShowAuxWindow();
        window.autoRepaintOnSceneChange = true;
    }
    
    private void OnGUI() {
        if (Application.isPlaying) {
            if (GUILayout.Button("Clear all")) {
                CombatMessageManager.Clear();
            }
            filter = EditorGUILayout.IntSlider("filter", filter, 0, 10);

            scroll = EditorGUILayout.BeginScrollView(scroll);
            for (int i = 0; i < CombatMessageManager.items.Count; i++) {
                int code = CombatMessageManager.items[i].code;
                if (code >= filter) {
                    style.normal.textColor = colors[code%colors.Length];
                    EditorGUILayout.LabelField("["+code+"]"+CombatMessageManager.items[i].msg, style);
                }
            }
            EditorGUILayout.EndScrollView();
        } else {
            EditorGUILayout.LabelField("Press play...");
        }
    }
}
