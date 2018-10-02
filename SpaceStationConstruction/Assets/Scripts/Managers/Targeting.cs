using System;
using System.Collections.Generic;
using UnityEngine;

public class Targeting {
    List<Transform> targets = new List<Transform>();

    public void Register(Fighter target) {
        targets.Add(target.transform);
    }
    public Transform GetClosest(Vector2 position) {
        float min = float.MaxValue;
        Transform closest = null;
        for (int i = 0; i < targets.Count; i++) {
            float dist = Vector2.Distance(position, targets[i].transform.position);
            if (dist > 0 && dist < min) {
                min = dist;
                closest = targets[i];
            }
        }
        return closest;
    }

    internal void DeRegister(Fighter fighter) {
        targets.Remove(fighter.transform);
    }
}
