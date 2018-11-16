using System;
using System.Collections.Generic;
using UnityEngine;
public class Targeting {
    List<Transform> fighters= new List<Transform>();
    List<Transform> modules = new List<Transform>();

    List<Fighter> fighterScripts = new List<Fighter>();

    public List<Fighter> Fighters { get { return fighterScripts; } }

    internal void DeRegister(StationModule stationModule) {
        modules.Remove(stationModule.transform);
    }

    // Note: register functions should be called from awake.
    public void Register(Fighter target) {
        fighters.Add(target.transform);
        fighterScripts.Add(target);
    }
    public void Register(StationModule target) {
        modules.Add(target.transform);
    }
    public Transform GetClosest(Vector2 position, List<Transform> list) {
        float min = float.MaxValue;
        Transform closest = null;
        for (int i = 0; i < list.Count; i++) {
            if (list[i].gameObject.activeSelf == false) {
                continue;
            }
            float dist = Vector2.Distance(position, list[i].transform.position);
            if (dist > 0 && dist < min) {
                min = dist;
                closest = list[i];
            }
        }
        return closest;
    }

    public Transform GetClosestTarget(Vector2 position) {
        return GetClosest(position, fighters);
    }

    public Transform GetClosestModule(Vector2 position) {
        return GetClosest(position, modules);
    }

    internal void DeRegister(Fighter fighter) {
        fighters.Remove(fighter.transform);
        fighterScripts.Remove(fighter);
    }
}
