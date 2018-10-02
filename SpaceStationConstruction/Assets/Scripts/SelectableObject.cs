using UnityEngine;
// TODO: selected item type(module, fighter, turret, instalation)
public enum SelectionType {
    Module, Fighter, Turret, Addon
}

/// <summary>
/// 
/// </summary>
/// <remarks>This is a bit of a funny thing.</remarks>
public class SelectableObject:MonoBehaviour {

    public static SelectableObject lastSelected;

    public SelectionType selectionType;

    /// <summary>
    /// Put on UI button or raycasting.
    /// </summary>
    public void SelectSelf() {
        GameManager.Instance.building.Select(lastSelected);
    }

    public void BuildTurret(int turretId) {
        GameManager.Instance.building.Build(turretId, lastSelected.transform.position);
    }
}