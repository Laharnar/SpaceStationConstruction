using System;
using UnityEngine;

public class Fighter:MonoBehaviour {
    public int shields = 0;
    public int health = 1;
    public float armor = 1f;

    private void Awake() {
        GameManager.Instance.targeting.Register(this);
    }

    internal void SetShields(int nextShield) {
        shields = nextShield;
    }

    internal void SetHp(int nextHp) {
        health = nextHp;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        GameManager.Instance.targeting.DeRegister(this);
    }
}
