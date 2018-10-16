using System;
using UnityEngine;

[System.Serializable]
public class QuestReward {
    public Transform spawnObject;

    public void Apply(Vector3 referencePos) {
        GameObject.Instantiate(spawnObject, referencePos, new Quaternion());
    }

}
