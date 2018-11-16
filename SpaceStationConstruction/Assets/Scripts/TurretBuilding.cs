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
    public int[] price;

    Dictionary<Vector2, Transform> turretInstances = new Dictionary<Vector2, Transform>();

    public bool IsTaken(Vector2 pos) {
        return turretInstances.ContainsKey(pos);
    }

    public Transform Build(int id, Vector2 pos) {
        if (id < price.Length) {
            GameManager.Instance.building.RemoveMoney(price[id]);
        } else {
            Debug.LogError("[Turret price]Out of range id " + id);
        }
        if (id < turretPrefs.Length) {
            return SpawnPref(turretPrefs[id], pos);
        } 
        Debug.LogError("[Turret building]Out of range id "+ id);
        return null;
    }

    public Transform SpawnPref(Transform pref, Vector2 pos) {
        if (IsTaken(pos)) {
            Debug.Log("Position " + pos+" taken skipping spawn.");
            return null;
        }
        Transform t= GameObject.Instantiate(pref, pos, new Quaternion()).transform;
        turretInstances.Add(pos, t);
        return t;
    }

    public bool HasMoney(int i, int money) {
        if (i < price.Length) {
            return money >= price[i];
        }
        Debug.Log("Id "+i+" out of range.");
        return false;
    }
}
