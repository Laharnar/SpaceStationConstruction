using System;
using System.Collections;
using UnityEngine;

// TODO : prep turret script for different kind of behaviours and different kinds of guns
// define how it should work
public class Turret:MonoBehaviour, IAiTracking {
    public GunInfo gun;
    public TurretData data;
    public int turretType = 0;

    Coroutine behaviour;

    Vector2 lastAim;
    public string aiState { get; set; }

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

    public void Aim(Vector2 point, Vector2 predictionDir) {
        lastAim = point;
        if (false) {
            transform.up = point - (Vector2)transform.position;
        }else
        transform.up = (point + predictionDir) - (Vector2)transform.position;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, lastAim);
    }

}
