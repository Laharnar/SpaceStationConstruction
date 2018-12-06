
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabLib", menuName = "Data/PrefabLib", order = 1)]
public class PrefabLib : ScriptableObject {
    public Transform[] prefabs;

    // creates 1 instance of all prefs
    internal Transform[] SpawnTempLib() {
        Transform[] ts = new Transform[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++) {
            ts[i] = Instantiate(prefabs[i], new Vector3(-10000,-10000,-10000), new Quaternion());
        }
        return ts;
    }
}
