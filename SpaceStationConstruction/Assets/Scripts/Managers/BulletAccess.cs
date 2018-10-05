using UnityEngine;

public class BulletAccess {
    public void SpawnBullet(TurretData data, Transform spawnPoint) {
        if (data.bulletPref == null) {
            Debug.LogError("Missing bullet pref");
            return;
        }
        GameObject.Instantiate(data.bulletPref, spawnPoint.position, spawnPoint.rotation);
    }
}
