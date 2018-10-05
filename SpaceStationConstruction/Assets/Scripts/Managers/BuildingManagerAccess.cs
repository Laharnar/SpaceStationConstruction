using System;
using UnityEngine;

public class BuildingManagerAccess {
    [SerializeField] BuildingManager buildingManager;

    public BuildingManagerAccess() {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }
    public bool TurretExists(SelectableObject selected) {
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
        buildingManager.OnSelectItem(selectableObject.transform);
    }

    internal void Build(int turretId, Vector3 position, Transform parent) {
        Debug.Log("[BUILD] turret built, ui should close");
        Transform turret = buildingManager.turrets.Build(turretId, position);
        turret.parent = parent;
        GameManager.Instance.units.RegisterTurret(SelectableObject.lastSelected, turret);

        // assumes the slot doesn't have built ui.
        string curUI = GameManager.Instance.ui.activeUI;
        Debug.Log("changeUI "+curUI + " to "+ "+_buildTower");
        GameManager.Instance.ui.HideUI();
        //GameManager.Instance.ui.ShowUI(curUI+ "_builtTower", SelectableObject.lastSelected.transform.position);



    }
    
}
