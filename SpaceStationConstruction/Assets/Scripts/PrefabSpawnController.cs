using UnityEngine;
public class PrefabSpawnController:MonoBehaviour {
    public PrefabLib lib;

    // prefabs that should be spawned.
    // Transform[] prefabInstances;

    // for prefab templates
    public int templateRWMode = 0; // R = 0 W = 1

    bool init = false;
    JSONFighterData data;

    private void Awake() {
        //prefabInstances = lib.SpawnTempLib();
        // load json prefab data

        // how to use: save once to get template, edit file, then always load
        if (templateRWMode == 1) {
            SavePrefabs();
        } else if (templateRWMode == 0) {
            LoadPrefabs();
        }

        init = true;
    }

    private void LoadPrefabs() {
        // assume prefabs are fighters - load their data as from json
        JSONFighterData data = LoadJsonFiles.LoadJsonToSerializable<JSONFighterData>(LoadJsonFiles.JSONPATH + "/prefabData.json"); ;
        this.data = data;
    }

    private void SavePrefabs() {
        // assume prefabs are fighters - save their data as a template
        FighterData[] t = new FighterData[lib.prefabs.Length];
        for (int i = 0; i < lib.prefabs.Length; i++) {
            Fighter f = lib.prefabs[i].GetComponent<Fighter>();
            if (f) {
                t[i] = f.data;
            } else {
                Debug.Log("[Prefab JSON save] no fighter script " + f.name);
            }
        }
        JSONFighterData data = new JSONFighterData(t);

        LoadJsonFiles.SaveSerializableToJson(data, LoadJsonFiles.JSONPATH + "prefabData.json", false);
    }

    // spawn instances instead of direct prefab
    internal Transform Spawn(int itemId, Vector2 vector2) {
        if (!init) return null;
        if (!lib) return null;
        if (itemId < lib.prefabs.Length) {
            if (false)
                Debug.Log("[PREFAB LIB] spawned " + itemId + " at " + vector2 + " " + lib.prefabs[itemId].name);
            Transform obj = GameObject.Instantiate(lib.prefabs[itemId], vector2, new Quaternion());
            Fighter f = obj.GetComponent<Fighter>();
            if (f) {
                f.data = data.data[itemId];
            }
        } else
            Debug.Log("Invalid id " + itemId);
        return null;
    }
}
