using UnityEngine;

public class StationModule:MonoBehaviour, IDestructible {
    public UnitStats stats;

    private void Awake() {
        GameManager.Instance.targeting.Register(this);
    }

    public void OnObjDestroyed() {
        GameManager.Instance.targeting.DeRegister(this);
    }
}
