using UnityEngine;

public class UiManagerAccess {
    [SerializeField] UiManager uiManager;

    public UiManagerAccess() {
        uiManager = GameObject.FindObjectOfType<UiManager>();
    }

    internal void ShowUI(string tag) {
        if (uiManager.activeUI != tag)
            uiManager.ShowUI(tag);
    }
}
