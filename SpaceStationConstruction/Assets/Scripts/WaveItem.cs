
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves/Wave", order = 1)]
// can point to item in for exmaple, prefab lib instance.
public class WaveItem:ScriptableObject {
    public int itemId=0;
    public int times=1;
}
