using System.Collections;
using System.Collections.Generic;
using SpaceEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(FRange))]
[CustomPropertyDrawer(typeof(CRange))]
public class RangeDrawer : PropertyDrawer
{
    // public override VisualElement CreatePropertyGUI(SerializedProperty property)
    // {
    //     // Create property container element.
    //     var container = new VisualElement();

    //     // Create property fields.
    //     var fromField = new PropertyField(property.FindPropertyRelative("From"));
    //     var toField = new PropertyField(property.FindPropertyRelative("To"));
    //     var valueField = new PropertyField(property.FindPropertyRelative("Value"));
    //     // var textField = new PropertyField();

    //     // Add fields to the container.
    //     container.Add(fromField);
    //     container.Add(toField);
    //     container.Add(valueField);
    //     // container.Add(textField);

    //     return container;
    // }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position = EditorGUI.PrefixLabel(position, label);

        // Don't make child fields be indented
        // var indent = EditorGUI.indentLevel;

        EditorGUI.indentLevel = 0;

        // Debug.Log(indent);

        // Calculate rects
        var fromRect = new Rect(position.x, position.y, 30, position.height);
        var toRect = new Rect(position.x + 35, position.y, 50, position.height);
        var valueRect = new Rect(position.x + 90, position.y + position.height, position.width - 90, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(fromRect, property.FindPropertyRelative("From"), GUIContent.none);
        EditorGUI.PropertyField(toRect, property.FindPropertyRelative("To"), GUIContent.none);
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("Value"), GUIContent.none);

        // position.position += Vector2.one * 15;

        // var textRect = new Rect(position.x, position.y + 25, position.width, position.height);
        // EditorGUI.TextField(textRect, "dffgdg");

        // Set indent back to what it was
        // EditorGUI.indentLevel = indent;
        EditorGUI.indentLevel++;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // return 30f;
        return base.GetPropertyHeight(property, label) * 2f;
        // return EditorGUI.GetPropertyHeight(property, label) / 3f;
    }
}