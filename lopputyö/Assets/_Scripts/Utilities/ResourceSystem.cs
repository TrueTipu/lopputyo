using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public static class ResourceSystem {
    static List<ScriptableObject> AllScriptableObject { get; set; }
    static Dictionary<string, ScriptableObject> AllScriptableObjectDict { get; set; }



    private static void AssembleResources() {
        AllScriptableObject = Resources.LoadAll<ScriptableObject>("ScriptableObjects/DataSaving").ToList();
        AllScriptableObjectDict = AllScriptableObject.ToDictionary(s => s.GetType().ToString(), s => s);
    }

    public static ScriptableObject GetScriptableObject(string name) 
    {
        if(AllScriptableObjectDict == null)
        {
            AssembleResources();
        }
        return AllScriptableObjectDict[name];
    }
}   