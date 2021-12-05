using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        InspectorButtonAttribute inspectorButtonAttribute = (InspectorButtonAttribute)attribute;
        Rect buttonRect = new Rect(position.x, position.y, position.width, position.height);
        if (GUI.Button(buttonRect, GetLabel(inspectorButtonAttribute.MethodName)))
        {
            System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
            string eventName = inspectorButtonAttribute.MethodName;

            if (_eventMethodInfo == null)
                _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (_eventMethodInfo != null)
                _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
            else
                Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
        }
    }

    public string GetLabel(string words)
    {
        string label = "";

        for (int i = 0; i < words.Length; i++)
        {
            if (i == 0)
            {
                label += Char.ToUpper(words[i]);
                continue;
            }

            label += Char.IsUpper(words[i]) && i != 0 ? " " : "";
            label += words[i];
        }

        return label;
    }
}
