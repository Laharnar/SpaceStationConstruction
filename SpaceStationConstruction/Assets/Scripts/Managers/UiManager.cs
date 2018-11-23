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

    public Text cashUi;

    private void Awake() {
        HideUI();
    }

    public void UpdateText() {
        cashUi.text = "Cash: " + GameManager.Instance.building.Money;
    }

    public void ShowQuestUI(bool visible) {
        questUi.gameObject.SetActive(visible);
    }

    public void ShowQuestButtons(Button[] btns, Action<int> send, string[] txt) {
        for (int i = 0; i < btns.Length; i++) {
            //btns[i].onClick.RemoveAllListeners();
            if (i < txt.Length) {
                btns[i].gameObject.SetActive(true);
                btns[i].transform.GetChild(0).GetComponent<Text>().text = txt[i];
            } else {
                btns[i].gameObject.SetActive(false);
                Debug.Log("Not enough text data for all buttons");
            }
            Debug.Log("Setting event as "+i);
            //btns[i].onClick.AddListener(delegate { Debug.Log(i); send(i-1); i -= 1; });
        }
    }

    public void ShowUIWithData(string tag, Vector3 pos, string[] buttonsData) {
        Debug.Log("[SHOWING UI]");
        activeUI = tag;
        for (int i = 0; i < tags.Length; i++) {
            Debug.Log("[ShowUI/]Comparing tag " + tag + " "+tags[i] + " ui set to "+ (tag == tags[i]) + " "+ui[i]);

            if (tag == tags[i]) {
                if (ui[i] != null) {

                    for (int k = 0; k < buttonsData.Length && k < ui[i].transform.childCount; k++) {
                        Text txt = ui[i].transform.GetChild(k).GetComponentInChildren<Text>();
                        if (txt) {
                            txt.text = buttonsData[k];
                        }
                    }
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
