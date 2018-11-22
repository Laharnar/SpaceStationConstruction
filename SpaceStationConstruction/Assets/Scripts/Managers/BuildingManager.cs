﻿//#define TwoDMode

using System;
using System.Collections;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    SelectableObject selectedItem { get { return SelectableObject.lastSelected; } set { SelectableObject.lastSelected = value; } }

    public TurretBuilding turrets;
    public int cash = 0;

    private void Update() {
        GetCurrentSelection();
        //Debug.Assert(selectedItem != null, "NULL selected item, select something.");
        
    }


    private void GetCurrentSelection() {
        if (Input.GetMouseButtonDown(0)) { // if left button pressed...
            GetHoveredOrDeselect();

            // open ui
            if (selectedItem != null) {
                string additionalTag = "";
                if (GameManager.Instance.building.TurretExists(selectedItem))
                    additionalTag += "_builtTower";
                GameManager.Instance.ui.ShowUI(selectedItem.selectionType.ToString() + additionalTag, (Vector2)selectedItem.transform.position);
            }
        }
    }

    private void GetHoveredOrDeselect() {
        // Note: requires collision.
        Camera camera = Camera.main;
        Vector3 ray = camera.ScreenToWorldPoint(Input.mousePosition);
#if TwoDMode
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector3.forward, Mathf.Infinity);
        if (hit.transform != null) {
            if (SelectionFilter(hit.transform))
                OnSelectItem(hit.transform);
        } else {
            //selectedItem = null;
        }
#else 
        RaycastHit hit;
        if (Physics.Raycast(ray, Vector3.forward, out hit, Mathf.Infinity)) {
            if (hit.transform != null) {
                if (SelectionFilter(hit.transform))
                    OnSelectItem(hit.transform);
            } else {
                //selectedItem = null;
            }
        }
#endif
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
        SelectableObject.lastSelected = t.GetComponent<SelectableObject>();
    }

}
