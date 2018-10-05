using UnityEngine;

public class UiManager : MonoBehaviour {
    internal string activeUI;

    public string[] tags;
    public Transform[] ui;

    private void Awake() {
        HideUI();
    }

    public void ShowUI(string tag, Vector3 pos) {
        Debug.Log("[SHOWING UI]");
        activeUI = tag;
        for (int i = 0; i < tags.Length; i++) {
            Debug.Log("Comparing tag" + tag + " "+tags[i] + " ui set to "+ (tag == tags[i]) + " "+ui[i]);

            if (tag == tags[i]) {
                if (ui[i] != null) {

                    ui[i].gameObject.SetActive(true);

                    ui[i].transform.position = pos;
                }
            } else {
                if (ui[i] != null) {
                    ui[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void HideUI() {
        Debug.Log("[HIDING UI]");
        for (int i = 0; i < ui.Length; i++) {
            if (ui[i])
                ui[i].gameObject.SetActive(false);
        }
        activeUI = "";
    }
}
