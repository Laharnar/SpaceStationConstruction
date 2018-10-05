using UnityEngine;

[System.Serializable]
public class GunInfo {
    float time;
    public Transform spawnPoint;

    public void Shoot(TurretData data) {
        if (Time.time > time) {
            time = data.fireRate + Time.time;
            GameManager.Instance.bullets.SpawnBullet(data, spawnPoint);
        }
    }
}
