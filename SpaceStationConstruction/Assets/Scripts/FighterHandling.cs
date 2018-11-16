using System.Collections;
using UnityEngine;

public class FighterHandling {
    public void ApplyDmg(int dmg, Fighter f) {
        if (dmg == 0 || f == null) return;
        int nextShield = Mathf.Clamp(f.data.stats.shields - dmg, 0, f.data.stats.shields);
        int nextHp = f.data.stats.health - (int)((Mathf.Clamp(dmg - f.data.stats.shields, 0, dmg)) * (1-f.data.stats.armor));
        f.data.stats.SetShields(nextShield);
        f.data.stats.SetHp(nextHp, f.gameObject, f);
    }

    public void ApplyDmg(int dmg, StationModule f) {
        if (dmg == 0 || f == null) return;
        int nextShield = Mathf.Clamp(f.stats.shields - dmg, 0, f.stats.shields);
        int nextHp = f.stats.health - (int)((Mathf.Clamp(dmg - f.stats.shields, 0, dmg)) * (1-f.stats.armor));
        f.stats.SetShields(nextShield);
        f.stats.SetHp(nextHp, f.gameObject, f);
    }

    public IEnumerator FighterAttackModules(Fighter f) {
        Vector3 lastAttackedPos = Vector2.zero;
        while (f != null) {

            Transform module = GameManager.Instance.targeting.GetClosestModule(f.transform.position);
            if (module == null) {
                f.aiState = "Searching for module...";
                yield return null;
                continue;
            }
            lastAttackedPos = (Vector2)module.position;

            while (f != null && Vector2.Distance(f.transform.position, lastAttackedPos) > 1f) {
                f.aiState = "B:Approaching target, shooting.";
                if (module != null)
                    lastAttackedPos = module.position;
                f.Move();
                f.Steering(lastAttackedPos);
                f.aiState = "Approaching target/Move+steer. " + lastAttackedPos;
                if (module != null) {
                    f.gun.Shoot(f.data.shooting);
                    f.aiState = "Approaching target/Move+steer+shoot." + lastAttackedPos;
                }
                yield return null;
            }
            
            while (f != null && Vector2.Distance(f.transform.position, lastAttackedPos) < 5f) {
                f.aiState = "Moving out of target range after getting too close.";
                if (module != null)
                    lastAttackedPos = module.position;
                f.Steering(f.transform.position + f.transform.up*f.data.flySpeed*f.data.rotationSpeed);
                f.Move();
                f.aiState = "Away from target/Move." + lastAttackedPos;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

}
