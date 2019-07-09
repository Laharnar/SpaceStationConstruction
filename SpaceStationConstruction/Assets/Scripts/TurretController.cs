using UnityEngine;

public class TurretController:MonoBehaviour {

    public TurretStates modes;

    private void Awake() {
        GameManager.Instance.turretManager.RegisterTurret(this);
        modes.Init(this);
    }

}
