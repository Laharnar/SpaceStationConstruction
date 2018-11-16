
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves/Wave", order = 1)]
// can point to item in for exmaple, prefab lib instance.
public class WaveItemSO:ScriptableObject {
    public WaveItem wave;
}
[System.Serializable]
public class WaveItem {
    public WaveSpawnItems itemId = WaveSpawnItems.FIGHTER;
    public int times = 1;
}
public enum WaveSpawnItems {
    DRONE,
    FIGHTER,
    MEDIUMCRAFT,
    MOTHERSHIP
}