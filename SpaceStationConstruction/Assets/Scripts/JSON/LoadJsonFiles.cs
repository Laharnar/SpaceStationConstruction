using System.IO;
using UnityEngine;

public class LoadJsonFiles {

    // TODO Note: some parts of code, don't use this yet.
    public const string JSONPATH = "Content/";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="pathDotJson">x.json</param>
    /// <param name="append"></param>
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
        try {
            using (StreamReader r = new StreamReader(path)) {
                json = r.ReadToEnd();
            }
        } catch (System.Exception) {

            return null;
        }
        T s = JsonUtility.FromJson<T>(json);
        return s;
    }
}

