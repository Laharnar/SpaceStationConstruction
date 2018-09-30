using UnityEngine;

public class BuildingManagerAccess {
    [SerializeField] BuildingManager buildingManager;

    public BuildingManagerAccess() {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }

    internal void Select(SelectableObject selectableObject) {
        buildingManager.OnSelectItem(selectableObject.transform);
    }

}
