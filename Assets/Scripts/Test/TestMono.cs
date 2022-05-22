using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using MiManchi.MiResoures;
using System.Threading.Tasks;
using MiManchi.MiPool;
using MiManchi.MiEnum;
using MiManchi.MiAsync;
using MiManchi;

public class TestMono : MiManchi.MiBaseClass.MiBaseMonoBeHaviourClass
{

    [SerializeField] GameObject article;
    [SerializeField] HorVerLayoutGroup list;

    [SerializeField] Dialog_Common_GetArticleFrame dialog_Common_GetArticleFrame;
    [SerializeField] List<ulong> ids = new List<ulong>();
    [SerializeField] List<ulong> counts = new List<ulong>();
    private void Start()
    {

    }
    protected override async Task InitializationAsync()
    {
        await AsyncDefaule();
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    MiAsyncManager.Instance.StartAsync(async () => await MiUIpopupHander.Instance.ShowDialog_1());
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    MiAsyncManager.Instance.StartAsync(async () => await MiUIpopupHander.Instance.ShowDialog_2());
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad3))
        //{
        //    MiAsyncManager.Instance.StartAsync(async () => await MiUIpopupHander.Instance.ShowDialog_3());
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad4))
        //{
        //    MiAsyncManager.Instance.StartAsync(async () => await MiUIpopupHander.Instance.ShowDialog_4());
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad5))
        //{
        //    MiAsyncManager.Instance.StartAsync(async () => await MiUIpopupHander.Instance.ShowDialog_5());
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad6))
        //{
        //    MiAsyncManager.Instance.StartAsync(async () => await MiUIpopupHander.Instance.ShowDialog_6());
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dialog_Common_GetArticleFrame.SetUpShowAsync(ids, counts).Wait();
        }
    }
}

