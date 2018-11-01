using UnityEngine;

public class Transform2T3D {
    public static void Test() {
        ConvertObjectTo3D(GameObject.Find("ModuleSample").transform, true);
    }

    // dir: 0 = 2d to 3d, 1 = 3d to 2d
    public static void ConvertObjectTo3D(Transform obj, bool dir) {
        // rigidbody
        Rigidbody2D rig2D = obj.GetComponent<Rigidbody2D>();
        Rigidbody rig = obj.GetComponent<Rigidbody>();
        if (dir) {
            if (rig2D) {
                GameObject.Destroy(rig2D);
            }
            if (rig == null) {
                obj.gameObject.AddComponent<Rigidbody>();
            }
        } else {
            if (rig) {
                GameObject.Destroy(rig);
            }
            if (rig2D == null) {
                obj.gameObject.AddComponent<Rigidbody2D>();
            }
        }
        // colliders
        BoxCollider2D collider2D = obj.GetComponent<BoxCollider2D>();
        BoxCollider collider = obj.GetComponent<BoxCollider>();
        if (dir) {
            bool isTrigger = true;
            if (collider == null) {
                BoxCollider bcol = obj.gameObject.AddComponent<BoxCollider>();
                if (collider2D) {
                    bcol.isTrigger = collider2D.isTrigger;
                    isTrigger = collider2D.isTrigger;
                }
            }
            if (collider2D) {
                GameObject.Destroy(collider2D);
            }
        } else {
            if (collider2D == null) {
                BoxCollider2D bcol = obj.gameObject.AddComponent<BoxCollider2D>();
                if (collider)
                    bcol.isTrigger = collider.isTrigger;
            }
            if (collider) {
                GameObject.Destroy(collider);
            }
        }
    }
}
