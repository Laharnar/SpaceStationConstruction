using System;
using UnityEngine;

[System.Serializable]
public class QuestReward {
    public int mode = 0;
    public int money = 100; 
    
    [NonSerialized]public Transform spawnObject;

    public void Apply(Vector3 referencePos) {
        if (mode == 0) {
            GameManager.Instance.building.AddMoney(money);
        } else {
            GameObject.Instantiate(spawnObject, referencePos, new Quaternion());
        }
    }

}
