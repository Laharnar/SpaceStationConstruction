using UnityEditor;
using UnityEngine;
namespace UnityMacros {

    public class Transform2T3D {
        [MenuItem("Macro/Convert test 2d to 3d")]
        public static void Test() {
            ConvertObjectTo3D(GameObject.Find("2DSample").transform, true);
        }
        [MenuItem("Macro/Convert test 3d to 2d")]
        public static void Test2() {
            ConvertObjectTo3D(GameObject.Find("2DSample").transform, false);
        }
        [MenuItem("Macro/Convert selected 2d to 3d")]
        public static void ChangeSelected() {
            if (Selection.transforms.Length > 0) {
                foreach (var item in Selection.transforms) {
                    ConvertObjectTo3D(item, true);
                }
            }
        }
        [MenuItem("Macro/Convert selected 3d to 2d")]
        public static void ChangeSelected2() {
            if (Selection.transforms.Length > 0) {
                foreach (var item in Selection.transforms) {
                    ConvertObjectTo3D(item, false);
                }
            }
        }
        [MenuItem("Macro/Convert Scene to 3d")]
        public static void ConvertScene3d() {
            // actually creating a copy of the scene might be better
            foreach (var item in GameObject.FindObjectsOfType<Transform>()) {
                ConvertObjectTo3D(item, true);
            }
        }
        [MenuItem("Macro/Convert Scene to 2d")]
        public static void ConvertScene2d() {
            foreach (var item in GameObject.FindObjectsOfType<Transform>()) {
                ConvertObjectTo3D(item, false);
            }
        }
        // dir: 0 = 2d to 3d, 1 = 3d to 2d
        static void ConvertObjectTo3D(Transform obj, bool dir) {
            // convert in 2 steps. 1: remove comps 2: add them

            // rigidbody
            Rigidbody2D rig2D = obj.GetComponent<Rigidbody2D>();
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            // colliders
            BoxCollider2D collider2D = obj.GetComponent<BoxCollider2D>();
            BoxCollider collider = obj.GetComponent<BoxCollider>();
            // sprites
            SpriteRenderer sprite2d = obj.GetComponent<SpriteRenderer>();
            MeshFilter filter3d = obj.GetComponent<MeshFilter>();
            MeshRenderer mesh3d = obj.GetComponent<MeshRenderer>();

            int addComponents = 0;

            bool boxIsTrigger = true;
            if (collider2D) {
                boxIsTrigger = collider2D.isTrigger;
            }
            if (collider) {
                boxIsTrigger = collider.isTrigger;
            }
            // 2d to 3d. destroy 2d
            if (dir) {
                if (rig2D) {
                    GameObject.DestroyImmediate(rig2D);
                    addComponents += 1;
                }
                if (collider2D) {
                    GameObject.DestroyImmediate(collider2D);
                    addComponents += 2;
                }
                if (sprite2d) {
                    GameObject.DestroyImmediate(sprite2d);
                    addComponents += 4;
                }
            } else { // destroy 3d
                if (rig) {
                    addComponents += 1;
                    GameObject.DestroyImmediate(rig);
                }
                if (collider) {
                    addComponents += 2;
                    GameObject.DestroyImmediate(collider);
                }
                if (mesh3d && filter3d) {
                    GameObject.DestroyImmediate(mesh3d);
                    GameObject.DestroyImmediate(filter3d);
                    addComponents += 4;
                }
            }
            if (dir) {

                if (rig == null && (addComponents & 1) == 1) {
                    rig = obj.gameObject.AddComponent<Rigidbody>();
                    rig.useGravity = false;
                    rig.isKinematic = true;
                }
                if (collider == null && (addComponents & 2) == 2) {
                    collider = obj.gameObject.AddComponent<BoxCollider>();
                    collider.isTrigger = boxIsTrigger;
                }
                if (mesh3d == null && filter3d == null && (addComponents & 4) == 4) {
                    mesh3d = obj.gameObject.AddComponent<MeshRenderer>();
                    filter3d = obj.gameObject.AddComponent<MeshFilter>();
                }
            } else {

                if (rig2D == null && (addComponents & 1) == 1) {
                    obj.gameObject.AddComponent<Rigidbody2D>();
                }
                if (collider2D == null && (addComponents & 2) == 2) {
                    BoxCollider2D bcol = obj.gameObject.AddComponent<BoxCollider2D>();
                    bcol.isTrigger = boxIsTrigger;
                }
                if (sprite2d == null && (addComponents & 4) == 4) {
                    sprite2d = obj.gameObject.AddComponent<SpriteRenderer>();
                }
            }
        }
    }
}