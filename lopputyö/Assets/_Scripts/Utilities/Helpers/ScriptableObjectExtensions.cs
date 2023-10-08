using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

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

    protected virtual void OnEnable()
    { 
        this.hideFlags = HideFlags.DontUnloadUnusedAsset;
        OnStartLoad();
    }
    public void InitSOCall(ScriptableObject _obj)
    {
        InitSO(_obj);
        LoadInspectorData();
    }
    protected virtual void InitSO(ScriptableObject _obj)
    {

    }
}
public abstract class EditablePlaytimeObject : PlaytimeObject
{

}


public abstract class InitializableObject<SO, Func> : ScriptableObject where SO : InitializableObject<SO, Func> where Func : System.Delegate
{
    protected abstract Func GiveConstructor();

    public static Func CreateInstance()
    {
        SO _data = CreateInstance<SO>();
        return _data.GiveConstructor();
    }
}

public interface IHasDelegates
{
    void AutoUnsubscribeDelegates();
}