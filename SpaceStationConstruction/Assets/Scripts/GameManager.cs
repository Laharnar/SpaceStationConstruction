using UnityEngine;

public class GameManager:MonoBehaviour {

    static GameManager instance;
    public BuildingManagerAccess building;
    public UiManagerAccess ui;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null) {
                    Debug.Log("Missing game manager in scene.");
                } else {
                    instance.building = new BuildingManagerAccess();
                    instance.ui = new UiManagerAccess();
                }
            }
            return instance;
        }
    }

}
