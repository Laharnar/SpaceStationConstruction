﻿using System;
using System.Collections;
using UnityEngine;

interface IAiTracking {
    string aiState { get; set; }
}

[System.Serializable]
public class KillReward {
    //[System.Obsolete("Used per quest rewards instead. Better fine tunning.")]
    // experience is still an option.
    //public int reward = 30;
}

public class Fighter:MonoBehaviour, IDestructible, IAiTracking {
    public GunInfo gun;
    public FighterData data;

    Vector2 nextMove;
    float nextRotation;

    Vector2 gizmoTargetPos;

    public Vector2 MovePrediction { get { return Time.deltaTime* data.flySpeed* RotationDir(transform.position, transform.up, gizmoTargetPos, data.rotationSpeed, data.flySpeed); } }
    public string aiState { get; set; }

    const float flyPlane = -1;

    private void Awake() {
        GameManager.Instance.targeting.Register(this);
    }

    private void Start() {
        StartCoroutine(FighterBehaviour1());
    }
    private void Update() {
        //Transform module = GameManager.Instance.targeting.GetClosestModule(f.transform.position);
        //Move();
        //Steering(module.position);
    }
    private IEnumerator FighterBehaviour1() {
        while (true) {
            yield return StartCoroutine(GameManager.Instance.fighterBehaviour.FighterAttackModules(this));
        }
    }

    public void OnObjDestroyed() {
        //GameManager.Instance.building.AddMoney(data.death.reward);
        GameManager.Instance.targeting.DeRegister(this);
    }

    public void Move() {
        transform.Translate(Vector2.up* Time.deltaTime * data.flySpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, flyPlane);
    }

    internal void Steering(Vector3 module) {
        this.gizmoTargetPos = module;
        /*Vector2 unitDir = transform.up;
        Vector2 unitPos = transform.position;
        Vector2 targetPos = module;
        Vector2 sq = Vector2.Perpendicular(unitDir);*/

        // direction with circle based steering >constant change in directoin
        
        //float dot = Vector2.Dot(sq, targetPos);
        //float angle = Mathf.Sign(dot) * Vector2.Angle(unitDir, targetPos);
        // Vector2 newLookAtPt = tpos+upDir+lookDir;
        /*unitDir = Vector2.up;
        unitPos = Vector2.zero;
        sq = Vector2.Perpendicular(unitDir);
        Debug.Log(Vector2.Angle(unitDir, Vector2.right) + " "+ Vector2.Angle(unitDir, Vector2.left ));
        Debug.Log(Vector2.Angle(unitDir, Vector2.one) * Mathf.Sign(Vector2.Dot(sq, Vector2.one)));
        Debug.Log(Vector2.Angle(unitDir, Vector2.left*3) * Mathf.Sign(Vector2.Dot(sq, Vector2.left*3)));*/
        //Debug.Log("anlge: "+angle+" "+ rig.rotation + " "+ Mathf.Clamp(angle * Time.deltaTime, -data.rotationSpeed, data.rotationSpeed));
        //angle = Mathf.Clamp(angle * Time.deltaTime, -data.rotationSpeed, data.rotationSpeed);
        //nextRotation = rig.rotation + angle;
        Vector2 lookDir = RotationDir(transform.position, transform.up, (Vector2)module, data.rotationSpeed, data.flySpeed);
        transform.up = lookDir;
    }

    Vector2 RotationDir(Vector2 tpos, Vector2 up, Vector2 target, float circleSize, float circleDist) {
        //float length = 6f;
        //float circleSize = 1f;
        target += -up;
        Vector2 upDir = (Vector2)up.normalized * circleDist;
        Vector2 circleDir = ((Vector2)target - (tpos + upDir)).normalized * circleSize;
        Vector2 randomDir = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)).normalized;
        float circleVsRandom = 0.6f;
        Vector2 lookDir = (upDir + circleDir* circleVsRandom + randomDir*(1-circleVsRandom)).normalized;
        return lookDir;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, nextMove);
        Gizmos.DrawWireSphere(transform.position+transform.up*2, 1);
        Gizmos.DrawLine(transform.position, gizmoTargetPos);
        Gizmos.DrawLine(transform.position, transform.up);
    }
    
}
