using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEditor;
using MiManchi.Tools;
using MiManchi.MiResoures;
using MiManchi.MiAsync;

public class GameManager : MiSingletonMonoBeHaviour<GameManager>
{
    [SerializeField,ReadOnly] MiDataServiceData serviceData = null;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Initialization()
    {
        base.Initialization();
        MiAsyncManager.Instance.StartAsync(async () => await MiUIManager.Instance.popup.ShowDialog_Start());
    }

    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();
        DontDestroyOnLoad(gameObject);
        serviceData = AssetDatabase.LoadAssetAtPath<MiDataServiceData>("Assets/Scripts/Service/Data/TempDataService.asset");
        MiDataManager.Instance.character.attribute = serviceData.attribute;

        MiDataManager.Instance.character.knapsack = serviceData.knapsack;

        await MiDataManager.Instance.character.UpdateArticleAsync();

        await MiResourcesManager.Instance.LoadSceneAsync("UI", mode: LoadSceneMode.Additive);
    }

    public async Task GameStart()
    {
        var scene = await MiResourcesManager.Instance.LoadSceneAsync("Battle", mode: LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        await MiUIManager.Instance.popup.Showdialog_Loading(scene, true);
        scene.allowSceneActivation = true; 
        MiAsyncManager.Instance.StartAsync(async () => await MiUIManager.Instance.popup.ShowDialog_System(true));

        await MiManchi.MiInput.MiInputManager.Instance.AddTabDown(async () => await MiUIManager.Instance.popup.ShowDialog_Backpack());
        await MiManchi.MiInput.MiInputManager.Instance.AddCtrlTabDown(async () => { await MiUIManager.Instance.popup.ShowDialog_Meun(); });
    }
}
