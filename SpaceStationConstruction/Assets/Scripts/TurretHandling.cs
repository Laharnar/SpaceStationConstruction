using System;
using System.Collections;
using UnityEngine;
public class TurretHandling {

    public IEnumerator WaitAvaliableTarget(Turret turret, Action onDone) {
        Transform target = null;
        do {
            Debug.Log(turret +" waiting target");
            target = GameManager.Instance.targeting.GetClosestTarget(turret.transform.position);
            yield return null;
        } while (target == null);
    }

    public IEnumerator AttackClosestCycle(Turret turret, Action onDone) {
        Debug.Log("attacking target");
        Transform target = GameManager.Instance.targeting.GetClosestTarget(turret.transform.position);
        if (target == null) {
            if (onDone!= null)
                onDone();
            yield return null;
        }
        while (target != null) {
            turret.Aim(target.position, target.GetComponent<Fighter>().MovePrediction);
            turret.gun.Shoot(turret.data);
            yield return null;
        }
        yield return null;
        if (onDone!= null)
            onDone();
    }
}
