using UnityEngine;

public class UiManager : MonoBehaviour {
    internal string activeUI;

    public string[] tags;
    public Transform[] ui;

    public void ShowUI(string tag) {
        for (int i = 0; i < tags.Length; i++) {
            if (tag == tags[i]) {
                if (i < ui.Length) {
                    ui[i].gameObject.SetActive(true);
                }
            } else {
                if (i < ui.Length) {
                    ui[i].gameObject.SetActive(false);
                }
            }
        }
        activeUI = tag;
    }
}
