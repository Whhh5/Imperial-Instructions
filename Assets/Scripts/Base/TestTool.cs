using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Threading.Tasks;

public class TestTool : Editor
{
    [MenuItem("GameObject/Create UI/Slider",false,0)]
    public static async Task CreateSlider()
    {
        await MiManchi.MiResoures.MiResourcesManager.Instance.LoadAsync<GameObject>("Prefab/Editor/UI", "Slider", true);
    }
}
