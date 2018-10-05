using UnityEngine;

public class StationModule:MonoBehaviour {
    public UnitStats stats;

    private void Awake() {
        GameManager.Instance.targeting.Register(this);
    }

    private void OnDestroy() {
        GameManager.Instance.targeting.DeRegister(this);
    }
}
