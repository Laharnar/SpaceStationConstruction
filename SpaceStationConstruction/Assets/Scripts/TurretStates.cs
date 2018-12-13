using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// modes per turret tells us how many children fit under one mode. not flexible 
/// for upgrades and different modes. swap to tree based.
/// </remarks>
[System.Serializable]
public class TurretStates {

    // where to point from which mode. allows 1 mode swap per turret. [multiple if you go over length...]
    public int mode = 0;// 0-5, 1 for each tower type. 0-2 are basic 3

    public void Init(TurretController turret) {
        SetTurretMode(mode, turret.transform);
    }

    public void ToggleMode(TurretController turret) {
        int[] targetModes = GameManager.Instance.globalModeManager.targetModes;

        if (targetModes.Length == 0) {
            Debug.Log("Increase number of modes.");
            return;
        }
        mode = targetModes[mode%targetModes.Length];
        SetTurretMode(mode, turret.transform);
    }

    // 0: first set 1: seconds set...
    public bool CorrectType(int requiredType) {
        int[] targetModes = GameManager.Instance.globalModeManager.targetModes;
        return mode % targetModes.Length == requiredType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="renderer"></param>
    /// <param name="mode"></param>
    /// <param name="childStates">Modes are set up as enabled/disabled child objects.</param>
    void SetTurretMode(int mode, Transform root) {
        if (mode < root.childCount) {
            for (int i = 0; i < root.childCount; i++) {
                root.GetChild(i).gameObject.SetActive(false);
            }
            root.GetChild(mode).gameObject.SetActive(true);
        } else {
            Debug.Log("fail not enough children for this mode " + mode + " " + root.childCount);
        }
    }

    internal int GetTurretType(TurretController turret) {
        return turret.transform.root.GetChild(mode).GetChild(0).GetComponent<Turret>().turretType;
    }
}
