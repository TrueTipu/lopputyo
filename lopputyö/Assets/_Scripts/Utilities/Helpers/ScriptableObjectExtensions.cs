using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public abstract class PlaytimeObject : ScriptableObject
{
    public void OnStartLoad()
    {
        if (typeof(IHasDelegates).IsAssignableFrom(GetType()))
        {
            ((IHasDelegates)this).AutoUnsubscribeDelegates();
        }
        LoadInspectorData();
    }
    protected abstract void LoadInspectorData();

    public void OnEnable()
    { 
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
        OnStartLoad();
    }

}
public abstract class EditablePlaytimeObject : PlaytimeObject
{

}


//public abstract class InitializableObject<SO, Del> : ScriptableObject where SO : InitializableObject<SO, Del> where Del : System.Delegate {
//    protected abstract Del GiveConstructor();

//    public static Del CreateInstance(out SO _data)
//    {
//        _data = CreateInstance<SO>();
//        return _data.GiveConstructor();
//    }
//}

public interface IHasDelegates
{
    void AutoUnsubscribeDelegates();
}