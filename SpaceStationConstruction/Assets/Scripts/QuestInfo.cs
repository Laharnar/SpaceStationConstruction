using System;
using UnityEngine;
[System.Serializable]
public class QuestInfo {
    public string title;
    public string description;

    public WaveItem[] waves;
    public QuestReward reward;

    //public SpawnQuest waveData;

    public void AddWave(WaveItem item) {
        WaveItem[] items = new WaveItem[waves.Length+1];
        for (int i = 0; i < waves.Length; i++) {
            items[i] = waves[i];
        }
        items[items.Length - 1] = item;
        this.waves = items;
    }


    public void RemoveWave(int id) {
        WaveItem[] items = new WaveItem[waves.Length-1];
        int iid = 0;
        for (int i = 0; i < waves.Length; i++) {
            if (i != id) {
                items[iid] = waves[i];
                iid++;
            }
        }
        this.waves = items;
    }
}

