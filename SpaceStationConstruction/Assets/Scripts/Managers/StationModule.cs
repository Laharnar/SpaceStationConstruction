using UnityEngine;

public class StationModule:MonoBehaviour, IDestructible {
    public UnitStats stats;

    private void Awake() {
        GameManager.Instance.targeting.Register(this);
    }

    public void OnDestroyed() {
        GameManager.Instance.targeting.DeRegister(this);
    }
}
