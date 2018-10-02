using UnityEngine;

public class WaveManagerAccess {

    WaveManager manager;

    public WaveManagerAccess() {
        manager = GameObject.FindObjectOfType<WaveManager>();
    }

    public void StartWaves() {
        GameManager.Instance.StartCoroutine(manager.WaveUpdate());
    }
}
