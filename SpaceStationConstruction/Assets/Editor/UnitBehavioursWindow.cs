using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitBehavioursWindow : EditorWindow{

    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    bool[] fighterToggles = new bool[1000];

    [MenuItem("Window/Unit behaviours")]
    static void Init() {
        // Get existing open window or if none, make a new one:
        UnitBehavioursWindow window = (UnitBehavioursWindow)EditorWindow.GetWindow(typeof(UnitBehavioursWindow));
        window.ShowAuxWindow();
        window.autoRepaintOnSceneChange = true;
    }

    void OnGUI() {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        if (Application.isPlaying) {
            List<Fighter> fighters = GameManager.Instance.targeting.Fighters;
            for (int i = 0; i < fighters.Count && i < fighterToggles.Length; i++) {
                fighterToggles[i] = EditorGUILayout.Foldout(fighterToggles[i], i + " " + fighters[i]);
                if (fighterToggles[i]) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField(fighters[i].transform, typeof(Transform), true);
                    EditorGUILayout.LabelField(fighters[i].aiState);
                    EditorGUI.indentLevel--;
                }
            }
        }
    }
}
