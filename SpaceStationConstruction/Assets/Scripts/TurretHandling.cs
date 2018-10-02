using System;
using System.Collections;
using UnityEngine;

public class TurretHandling {

    public void ApplyDmg(int dmg, Fighter f) {
        if (dmg == 0 || f == null) return;
        int nextShield = Mathf.Clamp(f.shields - dmg, 0, f.shields);
        int nextHp = f.health - (int)((Mathf.Clamp(dmg - f.shields, 0, dmg)) * f.armor);
        f.SetShields(nextShield);
        f.SetHp(nextHp);
    }

    public IEnumerator WaitAvaliableTarget(Turret turret, Action onDone) {
        Transform target = null;
        do {
            Debug.Log(turret +" waiting target");
            target = GameManager.Instance.targeting.GetClosest(turret.transform.position);
            yield return null;
        } while (target == null);
    }

    public IEnumerator AttackClosestCycle(Turret turret, Action onDone) {
        Debug.Log("attacking target");
        Transform target = GameManager.Instance.targeting.GetClosest(turret.transform.position);
        if (target == null) {
            if (onDone!= null)
                onDone();
            yield return null;
        }
        while (target != null) {
            turret.Aim(target.position);
            turret.Shoot();
            yield return null;
        }
        yield return null;
        if (onDone!= null)
            onDone();
    }
}
