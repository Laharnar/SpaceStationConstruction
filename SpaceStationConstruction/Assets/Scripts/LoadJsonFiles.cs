using System.IO;
using UnityEngine;

public class LoadJsonFiles {

    public static void SaveSerializableToJson(object obj, string pathDotJson, bool append) {
        Debug.Log("Save json object "+obj.GetType());
        string s = JsonUtility.ToJson(obj, true);
        using (StreamWriter w = new StreamWriter(pathDotJson, append)) {
            w.Write(s);
        }
    }

    public static T LoadJsonToSerializable<T>(string path) where T:class{
        Debug.Log("Load json object "+typeof(T) + " from "+path);
        string json;
        using (StreamReader r = new StreamReader(path)) {
            json = r.ReadToEnd();
        }
        T s = JsonUtility.FromJson<T>(json);
        return s;
    }
}

