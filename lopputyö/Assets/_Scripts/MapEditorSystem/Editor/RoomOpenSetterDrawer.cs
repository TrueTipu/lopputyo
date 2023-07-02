using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;




[CustomPropertyDrawer(typeof(RoomOpenData))]
public class RoomOpenDataDrawer : PropertyDrawer
{

    private readonly float lineHeight = EditorGUIUtility.singleLineHeight;

    private const float firstLineMargin = 1f;
    private const float lastLineMargin = 2f;

    private readonly Vector2 cellSpacing = new Vector2(5f, 5f);


    private readonly Vector2Int defaultCellSizeValue = new Vector2Int(16, 16);

    private SerializedProperty gridSizeProperty;
    private SerializedProperty cellSizeProperty;
    private SerializedProperty cellsProperty;

    #region SerializedProperty getters

    private void GetGridSizeProperty(SerializedProperty property) =>
        TryFindPropertyRelative(property, "gridSize", out gridSizeProperty);

    private void GetCellSizeProperty(SerializedProperty property) =>
        TryFindPropertyRelative(property, "cellSize", out cellSizeProperty);

    private void GetCellsProperty(SerializedProperty property) =>
        TryFindPropertyRelative(property, "cells", out cellsProperty);

    #endregion


    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {

        // Initialize properties
        GetGridSizeProperty(_property);
        GetCellSizeProperty(_property);
        GetCellsProperty(_property);

        // Don't draw anything if we miss a property
        if (gridSizeProperty == null || cellSizeProperty == null || cellsProperty == null)
        {
            return;
        }

        // Initialize cell size to default value if not already done
        if (cellSizeProperty.vector2IntValue == default)
        {
            cellSizeProperty.vector2IntValue = defaultCellSizeValue;
        }

        _position = EditorGUI.IndentedRect(_position);

        // Begin property drawing
        EditorGUI.BeginProperty(_position, _label, _property);

        // Display foldout
        var foldoutRect = new Rect(_position)
        {
            height = lineHeight
        };

        // We're using EditorGUI.IndentedRect to draw our Rects, and it already takes the indentLevel into account, so we must set it to 0.
        // This allows the PropertyDrawer to handle nested variables correctly.
        // More info: https://answers.unity.com/questions/1268850/how-to-properly-deal-with-editorguiindentlevel-in.html
        EditorGUI.indentLevel = 0;

        _property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, _property.isExpanded, _label);
        EditorGUI.EndFoldoutHeaderGroup();

        // Go to next line
        _position.y += lineHeight;

        if (_property.isExpanded)
        {
            _position.y += firstLineMargin;

            DisplayGrid(_position);
        }

        EditorGUI.EndProperty();
    }



    public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label)
    {
        float height = base.GetPropertyHeight(_property, _label);

        GetGridSizeProperty(_property);
        GetCellSizeProperty(_property);

        if (_property.isExpanded)
        {
            height += firstLineMargin;

            height += gridSizeProperty.vector2IntValue.y * (cellSizeProperty.vector2IntValue.y + cellSpacing.y) - cellSpacing.y; // Cells lines

            height += lastLineMargin;
        }

        return height;
    }

    private void DisplayGrid(Rect _position)
    {
        var _cellRect = new Rect(_position.x, _position.y, cellSizeProperty.vector2IntValue.x,
            cellSizeProperty.vector2IntValue.y);

        for (var y = 0; y < gridSizeProperty.vector2IntValue.y; y++)
        {
            for (var x = 0; x < gridSizeProperty.vector2IntValue.x; x++)
            {
                var pos = new Rect(_cellRect)
                {
                    x = _cellRect.x + (_cellRect.width + cellSpacing.x) * x,
                    y = _cellRect.y + (_cellRect.height + cellSpacing.y) * y
                };

                SerializedProperty _cell = GetCellAt(y, x);

                SerializedProperty _property;
                if (GetEnumValue(_cell, x).intValue != (int)Direction.Null)
                {
                    _property = GetBoolValue(_cell, x);
                    EditorGUI.PropertyField(pos, _property, GUIContent.none);
                }
            }
        }
    }

    private SerializedProperty GetCellAt(int y, int x)
    {
        return cellsProperty.GetArrayElementAtIndex(y).FindPropertyRelative("row").GetArrayElementAtIndex(x);
    }
    private SerializedProperty GetBoolValue(SerializedProperty _cell, int x)
    {
        return _cell.FindPropertyRelative("isOpen");
    }
    private SerializedProperty GetEnumValue(SerializedProperty _cell, int x)
    {
        return _cell.FindPropertyRelative("dir");
    }

    private void TryFindPropertyRelative(SerializedProperty _parent, string _relativePropertyPath, out SerializedProperty _prop)
    {
        _prop = _parent.FindPropertyRelative(_relativePropertyPath);

        if (_prop == null)
        {
            Debug.LogError($"Couldn't find variable \"{_relativePropertyPath}\" in {_parent.name}");
        }
    }

}
