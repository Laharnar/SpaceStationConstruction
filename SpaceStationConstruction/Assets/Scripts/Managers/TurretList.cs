using System.Collections.Generic;

[System.Serializable]
public class TurretList {

    public List<TurretController> turrets = new List<TurretController>();

    public void RegisterTurret(TurretController t) {
        turrets.Add(t);
    }

    public void ChangeTurretMode(TurretController turret, int expectedTurretType) {
        if (turret.modes.GetTurretType(turret)== expectedTurretType)
            turret.modes.ToggleMode(turret);
    }

}
