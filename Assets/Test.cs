using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using MiManchi.MiBaseClass;
using MiManchi.MiResoures;
using System.Reflection;

public class Test : MonoBehaviour
{
    public int num;
    public int i;
    UnityEvent e = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetTy();
            //MiResourcesManager.MiInstance.MiLoadPrefab("", "Sphere");
        }
    }

    private void dun(int h)
    {
        Debug.Log(h);
    }
    public void GetTy()
    {
        Type type = typeof(Test);
        MemberInfo[] fileInfo = type.GetMember("num");
        var temp = type.GetField(fileInfo[0].Name);
        var t = type.GetProperty("num");
        //var temp = fileInfo.GetValue(type.Name) as string;
        Debug.Log($"{fileInfo[0]}  {fileInfo.Length}   {temp}  ");
    }
}
