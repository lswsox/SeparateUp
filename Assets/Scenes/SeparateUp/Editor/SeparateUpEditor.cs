using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SeparateUp))][CanEditMultipleObjects]
public class SeparateUpEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SeparateUp su = target as SeparateUp;

        EditorGUILayout.HelpBox("Space 키로 발사하려면 Game View에서 누르세요.", MessageType.None);
        if (GUILayout.Button("Fire"))
        {
            su.Fire();
        }
    }
}
