using UnityEngine;

public class UiManager : MonoBehaviour {
    internal string activeUI;

    public string[] tags;
    public Transform[] ui;

    private void Awake() {
        HideUI();
    }

    public void ShowUI(string tag, Vector3 pos) {
        for (int i = 0; i < tags.Length; i++) {
            if (tag == tags[i]) {
                if (i < ui.Length) {
                    ui[i].gameObject.SetActive(true);
                    ui[i].transform.position = pos;
                }
            } else {
                if (i < ui.Length) {
                    ui[i].gameObject.SetActive(false);
                }
            }
        }
        activeUI = tag;
    }

    public void HideUI() {
        for (int i = 0; i < ui.Length; i++) {
            ui[i].gameObject.SetActive(false);
        }
    }
}
