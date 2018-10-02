using System;
using UnityEngine;

public class BuildingManagerAccess {
    [SerializeField] BuildingManager buildingManager;

    public BuildingManagerAccess() {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }

    internal void Select(SelectableObject selectableObject) {
        buildingManager.OnSelectItem(selectableObject.transform);
    }

    internal void Build(int turretId, Vector3 position) {
        Transform turret = buildingManager.turrets.Build(turretId, position);
        GameManager.Instance.units.RegisterTurret(SelectableObject.lastSelected, turret);
    }
}
