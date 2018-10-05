
using UnityEngine;

[System.Serializable]
public class UnitStats {
    public int alliance = 0;
    public int shields = 0;
    public int health = 1;
    public float armor = 1f;

    internal void SetShields(int nextShield) {
        shields = nextShield;
    }

    internal void SetHp(int nextHp, GameObject source) {
        health = nextHp;
        if (health <= 0) {
            GameObject.Destroy(source);
        }
    }
}
