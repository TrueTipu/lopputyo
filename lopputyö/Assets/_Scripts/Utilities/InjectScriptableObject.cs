using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class InjectScriptableObject
{
    public static void InjectGetSO(this UnityEngine.Object _object)
    {
        var fields = GetFieldsWithAttribute(typeof(GetSOAttribute), _object.GetType());
        foreach (var field in fields)
        {
            var type = field.FieldType;
            var component = ResourceSystem.GetScriptableObject(type.ToString());
            if (component == null)
            {
                Debug.LogWarning("GetComponent typeof(" + type.Name + ") in game object '" + _object.name + "' is null");
                continue;
            }
            field.SetValue(_object, component);
        }
    }

    private static IEnumerable<FieldInfo> GetFieldsWithAttribute(Type _attributeType, Type _objectType)
    {
        var fields = _objectType
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(field => field.GetCustomAttributes(_attributeType, true).FirstOrDefault() != null);

        return fields;
    }
}
[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
public class GetSOAttribute : System.Attribute
{
}