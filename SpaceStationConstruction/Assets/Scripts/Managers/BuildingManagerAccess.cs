using System;
using UnityEngine;

public class BuildingManagerAccess {
    [SerializeField] BuildingManager buildingManager;

    public int Money { get { return buildingManager.cash; } }

    public BuildingManagerAccess() {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }

    internal void RemoveMoney(int v) {
        buildingManager.cash -= v;
    }

    internal void AddMoney(int money) {
        buildingManager.cash += money;
    }
    

    internal void Select(SelectableObject selectableObject) {
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
        GameManager.Instance.ui.Close();
        // closes ui
        // GameManager.Instance.ui.ChangeUI(curUI + "_builtTower", SelectableObject.lastSelected.transform.position);
    }

    internal Transform GetTurret(SelectableObject selected) {
        return buildingManager.turrets.GetTurret(selected.transform.position);
    }
}
