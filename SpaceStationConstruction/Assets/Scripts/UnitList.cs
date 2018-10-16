using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitList {
    List<SelectableObject> slots = new List<SelectableObject>();
    List<Transform> turrets = new List<Transform>();

    public void RegisterTurret(SelectableObject selected, Transform t) {
        Debug.Log("[REGISTER TURRET]"+selected + " " + t);
        slots.Add(selected);
        turrets.Add(t);
    }
    public bool Exists(Transform turret) {
        for (int i = 0; i < turrets.Count; i++) {
            if (turrets[i].gameObject == turret.gameObject) {
                return true;
            }
        }
        return false;
    }
}
