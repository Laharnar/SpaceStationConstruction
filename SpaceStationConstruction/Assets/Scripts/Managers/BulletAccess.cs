using UnityEngine;

public class BulletAccess {
    public void SpawnBullet(TurretData data, Transform spawnPoint) {
        GameObject.Instantiate(data.bulletPref, spawnPoint.position, spawnPoint.rotation);
    }
}
