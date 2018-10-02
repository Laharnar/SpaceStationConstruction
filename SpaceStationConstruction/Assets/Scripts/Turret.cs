using System;
using System.Collections;
using UnityEngine;

public class Turret:MonoBehaviour {
    float time;
    public TurretData data;
    public Transform spawnPoint;

    Coroutine behaviour;

    private void Start() {
        StartCoroutine(TurretBehaviour1());
    }

    private IEnumerator TurretBehaviour1() {
        while (true) {
            behaviour = StartCoroutine(GameManager.Instance.turretBehaviour.WaitAvaliableTarget(this, null));
            yield return behaviour;
            behaviour = StartCoroutine(GameManager.Instance.turretBehaviour.AttackClosestCycle(this, null));
            yield return behaviour;
        }
    }

    public void Aim(Vector2 point) {
        transform.up = point - (Vector2)transform.position;
    }

    public void Aim() {
        Transform t = GameManager.Instance.targeting.GetClosest((Vector2)transform.position);
        if (t!= null) {
            transform.up = t.position - transform.position;
        }
    }

    public void Shoot() {
        if (Time.time > time) {
            time = data.fireRate + Time.time;
            GameManager.Instance.bullets.SpawnBullet(data, spawnPoint);
        }
    }
}
