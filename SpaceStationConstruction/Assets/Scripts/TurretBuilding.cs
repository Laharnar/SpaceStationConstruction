using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Positions that are linked to turret instances can't change.
/// </remarks>
[System.Serializable]
public class TurretBuilding {

    public Transform[] turretPrefs;

    Dictionary<Vector3, Transform> turretInstances = new Dictionary<Vector3, Transform>();

    public bool IsTaken(Vector3 pos) {
        return turretInstances.ContainsKey(pos);
    }

    public Transform Build(int id, Vector3 pos) {
        if (id < turretPrefs.Length) {
            return SpawnPref(turretPrefs[id], pos);
        } else {
            Debug.LogError("[Turret building]Out of range id "+ id);
            return null;
        }
    }

    public Transform SpawnPref(Transform pref, Vector3 pos) {
        if (IsTaken(pos)) {
            Debug.Log("Position " + pos+" taken skipping spawn.");
            return null;
        }
        Transform t= GameObject.Instantiate(pref, pos, new Quaternion()).transform;
        turretInstances.Add(pos, t);
        return t;
    }
}
