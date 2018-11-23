using System.Collections;
using UnityEngine;

public class FighterHandling {
    
    void ApplyDmg(int dmg, UnitStats stats, GameObject source, IDestructible destructibleSource) {
        if (dmg == 0 || stats == null) return;
        int nextShield = Mathf.Clamp(stats.shields - dmg, 0, stats.shields);
        int nextHp = stats.health - Mathf.CeilToInt((Mathf.Clamp(dmg - stats.shields, 0, dmg)) * (1f - stats.armor));
        CombatMessageManager.AddMessage("DMG: "+dmg + " armor(hp):"+(100-stats.armor*100)+
            string.Format("Set shields on {0} {1} -> {2}", source.name, stats.shields, nextShield)+
            "Set hp on " + source.name + " " + stats.health + " -> " + nextHp, 10);
        stats.SetShields(nextShield, source);
        stats.SetHp(nextHp, source, destructibleSource);
    }

    public void ApplyDmg(int dmg, Fighter f) {
        ApplyDmg(dmg, f.data.stats, f.gameObject, f);
    }

    public void ApplyDmg(int dmg, StationModule f) {
        ApplyDmg(dmg, f.stats, f.gameObject, f);
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
