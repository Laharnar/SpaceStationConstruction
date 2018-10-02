using System;
using UnityEngine;

public class UiManagerAccess {
    [SerializeField] UiManager uiManager;
    SelectableObject selectedItem;

    public UiManagerAccess() {
        uiManager = GameObject.FindObjectOfType<UiManager>();
    }

    internal void ShowUI(string tag, Vector3 pos) {
        if (uiManager.activeUI != tag)
            uiManager.ShowUI(tag, pos);
    }

    internal void SetUITarget(SelectableObject selectedItem) {
        this.selectedItem = selectedItem;
    }
}
