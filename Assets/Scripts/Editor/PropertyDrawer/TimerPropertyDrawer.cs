using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Timer))]
public class TimerPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        var timeRect = new Rect(position.x, position.y, position.width / 2f, position.height);
        EditorGUI.PropertyField(timeRect, property.FindPropertyRelative("time"), GUIContent.none);
        var typeRect = new Rect(position.x + position.width / 2f, position.y, position.width / 2f, position.height);
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
