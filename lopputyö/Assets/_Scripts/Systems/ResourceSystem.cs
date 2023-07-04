using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public static class ResourceSystem { 
    public static List<ScriptableObject> AllScriptableObject { get; private set; }
    public static Dictionary<string, ScriptableObject> AllScriptableObjectDict { get; private set; }

    

    private static void AssembleResources() {
        AllScriptableObject = Resources.LoadAll<ScriptableObject>("SaveData").ToList();
        AllScriptableObjectDict = AllScriptableObject.ToDictionary(s => s.name, s => s);
    }

    public static ScriptableObject GetScriptableObject(string name) => AllScriptableObjectDict[name];
}   