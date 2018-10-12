using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    internal string activeUI;

    public string[] tags;
    public Transform[] ui;

    // buttons that activate quests.
    public Transform questUi;

    private void Awake() {
        HideUI();

    }

    public void ShowQuestUI(bool visible) {
        questUi.gameObject.SetActive(visible);
    }

    public void ShowQuestButtons(Button[] btns, Action<int> send, string[] txt) {
        for (int i = 0; i < btns.Length; i++) {
            //btns[i].onClick.RemoveAllListeners();
            if (i <txt.Length)
                btns[i].transform.GetChild(0).GetComponent<Text>().text = txt[i];
            else {
                Debug.Log("Not enough text data for all buttons");
            }
            Debug.Log("Setting event as "+i);
            //btns[i].onClick.AddListener(delegate { Debug.Log(i); send(i-1); i -= 1; });
        }
    }

    public void ShowUI(string tag, Vector3 pos) {
        Debug.Log("[SHOWING UI]");
        activeUI = tag;
        for (int i = 0; i < tags.Length; i++) {
            Debug.Log("[ShowUI/]Comparing tag" + tag + " "+tags[i] + " ui set to "+ (tag == tags[i]) + " "+ui[i]);

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
