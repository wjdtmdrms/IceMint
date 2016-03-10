using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TGMap))]
public class TGMapInspector : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Regenerate"))
        {
            TGMap tgMap = (TGMap) target;
            tgMap.BuildMesh();
        }
    }
}
