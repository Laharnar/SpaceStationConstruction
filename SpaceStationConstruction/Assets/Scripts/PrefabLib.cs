
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabLib", menuName = "Data/PrefabLib", order = 1)]
public class PrefabLib : ScriptableObject {
    public Transform[] prefabs;

    internal void Spawn(int itemId, Vector2 vector2) {
        if (itemId < prefabs.Length) {
            Debug.Log("[PREFAB LIB] spawned "+ itemId + " at "+vector2+ " "+prefabs[itemId].name);
            GameObject.Instantiate(prefabs[itemId], vector2, new Quaternion());
        } else
            Debug.Log("Invalid id " + itemId);
    }
}
