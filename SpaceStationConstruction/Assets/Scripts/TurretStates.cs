using UnityEngine;

[System.Serializable]
public class TurretStates {
    const int modesPerTower = 2;
    public const int basicTowerTypes = 3;
    public int mode = 0;// 0-5, 1 for each tower type. 0-2 are basic 3

    public void Init(Turret turret) {
        SetState(mode, turret);
    }

    public void ToggleMode(Turret turret) {
        mode = (mode+basicTowerTypes)%(modesPerTower*basicTowerTypes);
        SetState(mode, turret);
    }

    public bool CorrectType(int requiredType) {
        return mode % TurretStates.basicTowerTypes == requiredType;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="renderer"></param>
    /// <param name="mode"></param>
    /// <param name="childStates">Modes are set up as enabled/disabled child objects.</param>
    void SetState(int mode, Turret turret) {
        if (mode < turret.transform.childCount) {
            for (int i = 0; i < turret.transform.childCount; i++) {
                turret.transform.GetChild(i).gameObject.SetActive(false);
            }
            turret.transform.GetChild(mode).gameObject.SetActive(true);
        } else {
            Debug.Log("fail " + mode + " " + turret.transform.childCount);
        }
    }
}
