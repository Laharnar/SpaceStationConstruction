using UnityEngine;

public class NestedObject:MonoBehaviour {
    // replaces this game object with target.

    public Transform targetPref;

    private void Awake() {
        Instantiate(targetPref, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
