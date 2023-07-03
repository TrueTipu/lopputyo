using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PlaytimeObject : ScriptableObject
{
    protected abstract void LoadInspectorData();
    private void OnEnable()
    {
        LoadInspectorData();
    }
}

public abstract class InitializableObject<SO, Del> : ScriptableObject where SO : InitializableObject<SO, Del> where Del : System.Delegate {
    protected abstract Del GiveConstructor();

    public static Del CreateInstance(out SO _data)
    {
        _data = CreateInstance<SO>();
        return _data.GiveConstructor();
    }
}
