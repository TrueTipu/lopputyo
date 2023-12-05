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
    static Dictionary<string, ScriptableObject> AllScriptableObjectDict { get; set; }
 

    public static void AssembleResources() {
        Debug.Log("Loaded");
        var _loadManager = Resources.Load<SOLoadManager>("ScriptableObjects/LoadManager");
        AllScriptableObject = Resources.LoadAll<ScriptableObject>("ScriptableObjects/"+ _loadManager.FolderName).ToList();

        AllScriptableObjectDict = AllScriptableObject.ToDictionary(s => s.GetType().ToString(), s => s);
    }

    public static ScriptableObject GetScriptableObject(string name) 
    {

        if (AllScriptableObjectDict == null)
        {
            AssembleResources();
        }
        return AllScriptableObjectDict[name];
    }
}   