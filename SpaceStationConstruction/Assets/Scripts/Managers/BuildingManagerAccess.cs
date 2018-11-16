using System;
using UnityEngine;

public class BuildingManagerAccess {
    [SerializeField] BuildingManager buildingManager;

    public int Money { get { return buildingManager.cash; } }

    public BuildingManagerAccess() {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }
    public bool TurretExists(SelectableObject selected) {
        return buildingManager.turrets.IsTaken((Vector2)selected.transform.position);

        // turret is child of selected slot.
        if (selected.transform.childCount == 0)
            return false;
        Transform turret = selected.transform.GetChild(0).transform;
        return GameManager.Instance.units.Exists(turret);
    }

    internal void RemoveMoney(int v) {
        buildingManager.cash -= v;
    }

    internal void AddMoney(int money) {
        buildingManager.cash += money;
    }

    public bool TurretExists(Transform turret) {
        return GameManager.Instance.units.Exists(turret);
    }

    internal void Select(SelectableObject selectableObject) {
        if (!buildingManager.turrets.IsTaken(selectableObject.transform.position))
        buildingManager.OnSelectItem(selectableObject.transform);
    }

    internal void BuildAddon(int i, Vector3 position, object p) {
        Debug.Log("[BUILD] addon, todo");
    }

    internal void BuildTurret(int turretId, Vector3 position, Transform parent) {
        if (!buildingManager.turrets.HasMoney(turretId, GameManager.Instance.building.Money)) {
            Debug.Log("Not enough money for tower "+turretId+".");
            return;
        }
        Debug.Log("[BUILD] turret built, ui should close");
        Transform turret = buildingManager.turrets.Build(turretId, position);

        if (turret) {
            turret.parent = parent;
            GameManager.Instance.units.RegisterTurret(SelectableObject.lastSelected, turret);
        }

        string curUI = GameManager.Instance.ui.activeUI;
        GameManager.Instance.ui.ChangeUI(curUI + "_builtTower", SelectableObject.lastSelected.transform.position);
    }
    
}
