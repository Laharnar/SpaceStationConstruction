using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitList {
    List<SelectableObject> slots = new List<SelectableObject>();
    List<Transform> turrets = new List<Transform>();

    public void RegisterTurret(SelectableObject selected, Transform t) {
        slots.Add(selected);
        turrets.Add(t);
    }

}
