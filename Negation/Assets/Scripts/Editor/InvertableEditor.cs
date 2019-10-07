using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Invertable))]
public class TagGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("parent"));

        EditorList.Show(serializedObject.FindProperty("myTags"), EditorListOption.Buttons);


        serializedObject.ApplyModifiedProperties();

    }
}
