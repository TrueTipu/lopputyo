using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public static class ResourceSystem {
    public static List<ScriptableObject> AllScriptableObject { get; private set; }
    static Dictionary<string, ScriptableObject> AllScriptableObjectDict { get; set; }
    public static string FolderName = "DataSaving2";
 

    public static Dictionary<string, ScriptableObject> AssembleResources() {
        Debug.Log("Loaded");



        AllScriptableObject = Resources.LoadAll<ScriptableObject>("ScriptableObjects/" + FolderName).ToList();

        AllScriptableObjectDict = AllScriptableObject.ToDictionary(s => s.GetType().ToString(), s => s);

        return AllScriptableObjectDict;
    }

    public static ScriptableObject GetScriptableObject(string name) 
    {
        Dictionary<string, ScriptableObject> _allScriptableObjectDict;
        if (AllScriptableObjectDict == null)
        {
            _allScriptableObjectDict = AssembleResources();
        }
        else
        {
            _allScriptableObjectDict = AllScriptableObjectDict;
        }
        try { return _allScriptableObjectDict[name]; }
        catch
        {
            _allScriptableObjectDict = AssembleResources();
            return _allScriptableObjectDict[name];
        }
    }
}   