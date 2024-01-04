using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public static class DevMenuButtons
{
    //[MenuItem("Save/EverySO")]
    //public static void SaveSOs()
    //{
    //    var _list = ResourceSystem.AllScriptableObject;
    //    foreach (ScriptableObject _item in _list)
    //    {
    //        if(_item.GetType().IsSubclassOf(typeof(PlaytimeObject)))
    //        {
    //            ScriptableObject _obj = ScriptableObject.CreateInstance(_item.GetType());
    //            (_obj as PlaytimeObject).InitSOCall(_item);
    //            string _path = "Assets/Resources/ScriptableObjects/DataSaving2/" + _item.GetType().ToString() + ".asset";
    //            AssetDatabase.CreateAsset(_obj, _path);
    //            //Selection.activeObject = _obj;
    //        }
    //    }
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //    EditorUtility.FocusProjectWindow();
    //}

}


