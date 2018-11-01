using System.Collections.Generic;

[System.Serializable]
public class TurretList {

    public List<Turret> turrets = new List<Turret>();

    public void RegisterTurret(Turret t) {
        turrets.Add(t);
    }

    public void TryChangeTurret(Turret turret, int requiredType) {
        if (turret.modes.CorrectType(requiredType)) {
            turret.modes.ToggleMode(turret);
        }
    }

}
