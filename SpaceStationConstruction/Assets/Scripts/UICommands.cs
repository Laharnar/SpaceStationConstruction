using UnityEngine;
using UnityEngine.SceneManagement;

public class UICommands:MonoBehaviour {
    public string gameSceneName = "SampleGame";

    public void BuildTurret(int i) {
        if (SelectableObject.lastSelected == null) {
            Debug.Log("Selection is null.");
            return;
        }
        GameManager.Instance.building.BuildTurret(i, SelectableObject.lastSelected.transform.position, null);
    }

    public void MainMenuToGame() {
        SceneManager.LoadScene(gameSceneName);
    }

    public void BuildAddon(int i) {
        if (SelectableObject.lastSelected == null) {
            Debug.Log("Selection is null.");
            return;
        }
        GameManager.Instance.building.BuildAddon(i, SelectableObject.lastSelected.transform.position, null);
    }

    public void SelectQuest(int i) {
        GameManager.Instance.ui.ActivateQuestUI(i);
    }

}
