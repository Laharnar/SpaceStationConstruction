using System;
using UnityEngine;

public class UiManagerAccess {
    [SerializeField] UiManager uiManager;
    SelectableObject selectedItem;
    public string activeUI;

    public UiManagerAccess() {
        uiManager = GameObject.FindObjectOfType<UiManager>();
    }

    internal void ShowUI(string tag, Vector3 pos) {
        if (uiManager.activeUI != tag) {
            uiManager.ShowUI(tag, pos);
            activeUI = tag;
        }
    }

    internal void SetUITarget(SelectableObject selectedItem) {
        this.selectedItem = selectedItem;
    }

    internal void HideUI() {
        uiManager.HideUI();
    }
}
