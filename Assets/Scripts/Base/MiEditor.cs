using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using MiManchi.MiInput;
using EditorTool;

[CustomEditor(typeof(MiUISlider))]
public class MiEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();
        MiUISlider myScript = (MiUISlider)target;
        if (GUILayout.Button("Reset"))
        {
            myScript.OnInspectorGUI();
        }
    }

    [MenuItem("Game Start/Start Active")]
    public static void GameStart()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "",false);
        EditorSceneManager.OpenScene("Assets/Scenes/Active.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("CustomEditor/Test")]
    public static void Test()
    {
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "", false);
        EditorSceneManager.OpenScene("Assets/Scenes/Active.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}