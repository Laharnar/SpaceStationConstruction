using System;
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

    // coordinates work in 2d.
    Dictionary<Vector3, Transform> turretInstances = new Dictionary<Vector3, Transform>();

    public bool TurretExistsAt(Vector3 pos) {
        return turretInstances.ContainsKey((Vector2)pos);
    }

    public Transform Build(int id, Vector3 pos) {
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

    public Transform SpawnPref(Transform pref, Vector3 pos) {
        if (TurretExistsAt(pos)) {
            Debug.Log("Position " + pos+" taken skipping spawn.");
            return null;
        }
        Transform t= GameObject.Instantiate(pref, pos, new Quaternion()).transform;
        turretInstances.Add((Vector2)pos, t);
        return t;
    }

    public bool HasMoney(int i, int money) {
        if (i < price.Length) {
            return money >= price[i];
        }
        Debug.Log("Id "+i+" out of range.");
        return false;
    }

    internal Transform GetTurret(Vector3 position) {
        if (TurretExistsAt(position)) {
            return turretInstances[(Vector2)position];
        }
        return null;
    }
}
