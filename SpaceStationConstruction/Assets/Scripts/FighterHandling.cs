using System.Collections;
using UnityEngine;

public class FighterHandling {
    public void ApplyDmg(int dmg, Fighter f) {
        if (dmg == 0 || f == null) return;
        int nextShield = Mathf.Clamp(f.data.stats.shields - dmg, 0, f.data.stats.shields);
        int nextHp = f.data.stats.health - (int)((Mathf.Clamp(dmg - f.data.stats.shields, 0, dmg)) * f.data.stats.armor);
        f.data.stats.SetShields(nextShield);
        f.data.stats.SetHp(nextHp, f.gameObject);
    }
    public void ApplyDmg(int dmg, StationModule f) {
        if (dmg == 0 || f == null) return;
        int nextShield = Mathf.Clamp(f.stats.shields - dmg, 0, f.stats.shields);
        int nextHp = f.stats.health - (int)((Mathf.Clamp(dmg - f.stats.shields, 0, dmg)) * f.stats.armor);
        f.stats.SetShields(nextShield);
        f.stats.SetHp(nextHp, f.gameObject);
    }
    public IEnumerator FighterAttackModules(Fighter f) {
        while (f != null) {

            Transform module = GameManager.Instance.targeting.GetClosestModule(f.transform.position);
            if (module == null) {
                yield return null;
                continue;
            }
            Debug.Log(f.transform+" "+module);
            while (f != null && module!= null && Vector3.Distance(f.transform.position, module.position) > 1f) {
                f.Move();
                f.Steering(module.position);
                f.gun.Shoot(f.data.shooting);
                yield return null;
            }

            while (f != null && module != null && Vector3.Distance(f.transform.position, module.position) < 10f) {
                f.Move();
                yield return null;
            }

            yield return null;
        }
        yield return null;
    }
}
