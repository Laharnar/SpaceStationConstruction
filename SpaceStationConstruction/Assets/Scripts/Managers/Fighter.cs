using System;
using System.Collections;
using UnityEngine;

public class Fighter:MonoBehaviour {
    public GunInfo gun;
    public FighterData data;
    public Rigidbody2D rig;
    
    private void Awake() {
        GameManager.Instance.targeting.Register(this);
    }

    private void Start() {
        StartCoroutine(FighterBehaviour1());
    }

    private IEnumerator FighterBehaviour1() {
        while (true) {
            yield return StartCoroutine(GameManager.Instance.fighterBehaviour.FighterAttackModules(this));
        }
    }

    private void OnDestroy() {
        GameManager.Instance.targeting.DeRegister(this);
    }

    public void Move() {
        rig.MovePosition(transform.position+transform.up);
    }

    internal void Steering(Vector3 module) {
        // direction with circle based steering >constant change in directoin
        // lerp based steering. >> not constant, depending on degrees.
        // Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(module-transform.position)
        //    , t);
        float length = 2f;
        float circleSize = 1f;
        Vector2 dir = (module - transform.position).normalized*circleSize;
        Vector2 newLookAt = (Vector2)transform.position+(Vector2)transform.up*length + dir;
        transform.up = newLookAt - (Vector2)transform.position;
    }
}
