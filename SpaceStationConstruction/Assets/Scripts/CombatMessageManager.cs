using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatMessageManager:MonoBehaviour {
    public static List<CombatOutputInfo> items { get { return GameManager.Instance.combat.messages; } }
    List<CombatOutputInfo> messages = new List<CombatOutputInfo>();

    public static void AddMessage(string msg, int code) {
        //if (window == null)
        //    Init();
        //window.stuff
        GameManager.Instance.combat.messages.Add(new CombatOutputInfo(msg, code));
    }

    public static void Clear() {
        GameManager.Instance.combat.messages.Clear();
    }
}