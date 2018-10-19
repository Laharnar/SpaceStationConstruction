using System;
using UnityEngine;

public class UiManagerAccess {
    [SerializeField] UiManager uiManager;
    SelectableObject selectedItem;
    public string activeUI;
    public bool anySelectedQuest { get; private set; }

    public UiManagerAccess() {
        uiManager = GameObject.FindObjectOfType<UiManager>();
        uiManager.ShowQuestUI(false);
    }

    internal void ShowUI(string tag, Vector3 pos) {
        activeUI = tag;
        uiManager.ShowUI(tag, pos);
    }

    internal void ConsumeSelectedQuest() {
        anySelectedQuest = false;
    }

    internal void ShowQuestChoices(QuestInfo[] activeQuestSet) { 
        // show choice of buttons.
        uiManager.ShowQuestUI(true);
        // load data on them.
        //
        uiManager.ShowQuestButtons(uiManager.questUi.GetComponentsInChildren<UnityEngine.UI.Button>(),
            null,// ActivateQuestUI
            GetData(activeQuestSet));
    }

    public void HideQuestUI() {
        uiManager.ShowQuestUI(false);
    }

    private string[] GetData(QuestInfo[] activeQuestSet) {
        if (activeQuestSet == null) {
            Debug.Log("Null active quest set.");
            return new string[activeQuestSet.Length];
        }
        string[] s = new string[activeQuestSet.Length];
        for (int i = 0; i < activeQuestSet.Length; i++) {
            s[i] = activeQuestSet[i].title;
        }
        return s;
    }

    /// <summary>
    /// Sets active quest. QuestManager handles what should happen with it.
    /// </summary>
    /// <param name="ui"></param>
    public void ActivateQuestUI(int ui) {
        GameManager.Instance.quests.SetActiveQuestFromSet(ui);
        anySelectedQuest = true;
    }

    internal void HideUI() {
        uiManager.HideUI();
    }

    internal void ChangeUI(string newUi, Vector3 position) {
        // assumes the slot doesn't have built ui.
        string curUI = GameManager.Instance.ui.activeUI;
        Debug.Log("changeUI " + curUI + " to " + newUi);
        GameManager.Instance.ui.HideUI();
        GameManager.Instance.ui.ShowUI(newUi, SelectableObject.lastSelected.transform.position);

    }
}
