using System.Collections.Generic;
using UnityEngine;
public class InputManager:MonoBehaviour {

    private void Update() {
        List<Turret> turrets = GameManager.Instance.turretManager.turrets;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            for (int i = 0; i < turrets.Count; i++) {
                GameManager.Instance.turretManager.TryChangeTurret(turrets[i], 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            for (int i = 0; i < turrets.Count; i++) {
                GameManager.Instance.turretManager.TryChangeTurret(turrets[i], 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            for (int i = 0; i < turrets.Count; i++) {
                GameManager.Instance.turretManager.TryChangeTurret(turrets[i], 2);
            }
        }


    }

}
