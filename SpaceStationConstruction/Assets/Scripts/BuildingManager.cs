using System;
using UnityEngine;
public class BuildingManager : MonoBehaviour {

    SelectableObject selectedItem;

    private void Update() {
        GetCurrentSelection();
        //Debug.Assert(selectedItem != null, "NULL selected item, select something.");
        if (selectedItem != null) {
            GameManager.Instance.ui.ShowUI(selectedItem.selectionType.ToString());
        }
    }

    private void GetCurrentSelection() {
        if (Input.GetMouseButtonDown(0)) { // if left button pressed...
            GetHovered();
        }
    }

    private void GetHovered() {
        // Note: requires collision.
        Camera camera = Camera.main;
        Vector3 ray = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector3.forward, Mathf.Infinity);
        if (hit.transform != null) {
            if (SelectionFilter(hit.transform))
                OnSelectItem(hit.transform);
        }
    }

    /// <summary>
    /// Select buildings, modules, units.
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    private bool SelectionFilter(Transform t) {
        return t.GetComponent<SelectableObject>();
    }

    public void OnSelectItem(Transform t) {
        selectedItem = t.GetComponent<SelectableObject>();
    }
}
