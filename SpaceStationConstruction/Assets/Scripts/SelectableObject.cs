﻿using UnityEngine;
/// <summary>
/// 
/// </summary>
/// <remarks>This is a bit of a funny thing.</remarks>
public class SelectableObject:MonoBehaviour {

    // last selected is different than this, because this can also act as a manager.
    public static SelectableObject lastSelected;

    public SelectionType selectionType;

    /// <summary>
    /// Put on UI button or raycasting.
    /// </summary>
    public void SelectSelf() {
        GameManager.Instance.building.Select(lastSelected);
    }
    
}