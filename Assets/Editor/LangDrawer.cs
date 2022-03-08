using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    [CustomPropertyDrawer (typeof(LangAttribute))]
    public class LangDrawer : PropertyDrawer
    {
        private const float TextLineHeight = 18f;
        private const float TextLabelHeight = 20f;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            GUIContent enumLabel = new GUIContent (((Lang)pos).ToString());
 
            EditorGUI.BeginProperty (position, label, property);
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.PrefixLabel(position, enumLabel);
                position.y += TextLabelHeight;
                position.height = GetPropertyHeight(property, label) - TextLabelHeight;
                string text = EditorGUI.TextArea(position, property.stringValue);
                property.stringValue = text;
            }
            else
                EditorGUI.PropertyField(position, property, enumLabel);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                return TextLineHeight * (((LangAttribute)attribute).lines + 1);
            }
            return base.GetPropertyHeight(property, label);
        }
    }
}

