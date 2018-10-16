using UnityEngine;

public class NestedObject:MonoBehaviour {
    // replaces this game object with target.

    public Transform targetPref;

    private void Awake() {
        Transform t = Instantiate(targetPref, transform.position, transform.rotation);
        t.parent = transform.parent;
        Destroy(gameObject);
    }
}
