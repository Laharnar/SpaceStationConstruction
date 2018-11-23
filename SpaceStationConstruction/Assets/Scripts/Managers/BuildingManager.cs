//#define TwoDMode

using System;
using System.Collections;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    SelectableObject selectedItem { get { return SelectableObject.lastSelected; } set { SelectableObject.lastSelected = value; } }

    public TurretBuilding turrets;
    public int cash = 0;

    private void Update() {
        GetCurrentSelection();
    }

    private void GetCurrentSelection() {
        if (Input.GetMouseButtonDown(0)) { // if left button pressed...
            GetHoveredOrDeselect();

            if (selectedItem != null) {
                // on select turret
                OnSelectBuildSlot();
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
        //if (!turrets.TurretExistsAt(t.position))
        SelectableObject.lastSelected = t.GetComponent<SelectableObject>();
    }

    public void OnSelectBuildSlot() {
        // open ui, or turret ui
        string additionalTag = "";
        // open correct ui based on tag. temporary disabled, use other version.
        /*if (GameManager.Instance.building.TurretExists(selectedItem))
            additionalTag += "_builtTower";
            */
        GameManager.Instance.ui.ShowTurretChoices(
            GameManager.Instance.building.GetTurret(selectedItem), 
            selectedItem.selectionType.ToString() + additionalTag, 
            (Vector2)selectedItem.transform.position);

        // ui should be renamed, by current turret choices
    }
}
public class BuildingTurrets {
 

    public void OnPressedBuild(int choice) {
        // execute choice at id
    }
}
