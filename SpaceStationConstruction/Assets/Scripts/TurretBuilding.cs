using UnityEngine;

[System.Serializable]
public class TurretBuilding {

    public Transform[] turretPrefs;

    public Transform Build(int id, Vector3 pos) {
        if (id < turretPrefs.Length) {
            return SpawnPref(turretPrefs[id], pos);
        } else {
            Debug.LogError("[Turret building]Out of range id "+ id);
            return null;
        }
    }

    public static Transform SpawnPref(Transform pref, Vector3 pos) {
        return GameObject.Instantiate(pref, pos, new Quaternion()).transform;
    }
}
