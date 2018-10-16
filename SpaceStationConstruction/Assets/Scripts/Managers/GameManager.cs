using UnityEngine;
public class GameManager:MonoBehaviour {

    static GameManager instance;
    public BuildingManagerAccess building;
    public UiManagerAccess ui;
    public UnitList units;
    [HideInInspector]public SpawnZones zones;
    public QuestBehaviours questBehaviour;
    public QuestManagerAccess quests;
    public BulletAccess bullets;
    public Targeting targeting;
    public TurretHandling turretBehaviour;
    public FighterHandling fighterBehaviour;
    public Station station;
    //testc if stuff gets disabled and then properly enabled.
    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null) {
                    Debug.Log("Missing game manager in scene.");
                } else {
                    instance.units = new UnitList();
                    instance.building = new BuildingManagerAccess();
                    instance.ui = new UiManagerAccess();
                    instance.GetComponent<SpawnZones>();
                    instance.quests = new QuestManagerAccess();
                    instance.zones = instance.GetComponent<SpawnZones>();
                    instance.bullets = new BulletAccess();
                    instance.turretBehaviour = new TurretHandling();
                    instance.targeting = new Targeting();
                    instance.fighterBehaviour = new FighterHandling();

                }
            }
            return instance;
        }
    }

    private void Start() {
        instance.station.Init();
    }
}
