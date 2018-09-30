using System.Collections.Generic;
using UnityEngine;

public class MyDebug {

    static Queue<object> queueLog = new Queue<object>();

    public static void Log(object message) {
        Debug.Log(message);
    }
    public static void Log(object message, UnityEngine.Object obj) {
        Debug.Log(message, obj);
    }

    public static void QueueLog(object message) {
        queueLog.Enqueue(message);
    }

    public static void QueueLog(object message, UnityEngine.Object obj) {
        queueLog.Enqueue("[" + obj + "]"+ message);
    }

    public static void ReleaseLog() {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        while (queueLog.Count > 0) {
            sb.AppendLine(queueLog.Dequeue().ToString());
        }
        Log(sb.ToString());
    }
}
