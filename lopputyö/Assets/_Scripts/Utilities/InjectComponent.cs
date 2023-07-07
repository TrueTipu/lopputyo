using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class InjectComponent 
{

    public static void InjectGetComponent(this MonoBehaviour _object)
    {
        var fields = GetFieldsWithAttribute(typeof(GetComponentAttribute), _object.GetType());
        foreach (var field in fields)
        {
            var type = field.FieldType;
            var component = _object.GetComponent(type);
            if (component == null)
            {
                Debug.LogWarning("GetComponent typeof(" + type.Name + ") in game object '" + _object.gameObject.name + "' is null");
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
public class GetComponentAttribute : System.Attribute
{
}