using System;
using UnityEngine;

public class BuildingManagerAccess {
    [SerializeField] BuildingManager buildingManager;

    public BuildingManagerAccess() {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }
    public bool TurretExists(SelectableObject selected) {
        return buildingManager.turrets.IsTaken(selected.transform.position);

        // turret is child of selected slot.
        if (selected.transform.childCount == 0)
            return false;
        Transform turret = selected.transform.GetChild(0).transform;
        return GameManager.Instance.units.Exists(turret);
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
        Debug.Log("[BUILD] turret built, ui should close");
        Transform turret = buildingManager.turrets.Build(turretId, position);

        if (turret) {
            turret.parent = parent;
            GameManager.Instance.units.RegisterTurret(SelectableObject.lastSelected, turret);
        }

        // assumes the slot doesn't have built ui.
        string curUI = GameManager.Instance.ui.activeUI;
        Debug.Log("changeUI "+curUI + " to "+ "+_buildTower");
        GameManager.Instance.ui.HideUI();
        GameManager.Instance.ui.ShowUI(curUI+ "_builtTower", SelectableObject.lastSelected.transform.position);
    }
    
}
