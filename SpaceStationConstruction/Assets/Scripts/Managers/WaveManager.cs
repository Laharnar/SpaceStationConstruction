
using System.Collections;
using UnityEngine;

public class WaveManager:MonoBehaviour {

    public SpawnQuest quest;

    public IEnumerator WaveUpdate() {
        //
        //temp code
        while (true) {
            if (quest != null) {
                yield return StartCoroutine(GameManager.Instance.questBehaviour.Execute(quest));
                quest = null;
            }
            yield return null;
        }
    }

}
