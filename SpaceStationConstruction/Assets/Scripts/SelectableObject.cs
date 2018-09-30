using UnityEngine;
// TODO: selected item type(module, fighter, turret, instalation)
public enum SelectionType {
    Module, Fighter, Turret, Addon
}
public class SelectableObject:MonoBehaviour {

    public SelectionType selectionType;

    /// <summary>
    /// Put on UI button or raycasting.
    /// </summary>
    public void SelectSelf() {
        GameManager.Instance.building.Select(this);
    }

}