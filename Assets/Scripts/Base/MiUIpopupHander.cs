using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System.Threading.Tasks;
using MiManchi.MiResoures;
using MiManchi.MiEnum;
using MiManchi;

public partial class MiUIpopupHander : MiBaseClass
{
    //-start
    private Dialog_Start dialogStart = null;
    public async Task ShowDialog_Start()
    {
        if (dialogStart == null)
        {
            dialogStart = 
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_Start", CanvasLayer.System))
                .GetComponent<Dialog_Start>();
        }
        await dialogStart.ShowAsync( DialogMode.none);
    }
    private Dialog_System dialogSystem = null;
    public async Task ShowDialog_System(bool isShow = true)
    {
        if (dialogSystem == null)
        {
            dialogSystem = 
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_System", CanvasLayer.System))
                .GetComponent<Dialog_System>();
        }
        if (isShow)
        {
            await dialogSystem.ShowAsync(DialogMode.none);
        }
    }
    private Dialog_LoadingScene dialogLoading = null;
    public async Task Showdialog_Loading(AsyncOperation value, bool isShow = true)
    {
        if (dialogLoading == null)
        {
            dialogLoading = 
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_LoadingScene", CanvasLayer.Loading))
                .GetComponent<Dialog_LoadingScene>();
        }
        if (isShow)
        {
            await dialogLoading.SetUpShow(value);
        }
    }

    //-Menu
    private Dialog_Menu dialog_Menu = null;
    public async Task ShowDialog_Meun()
    {
        if (dialog_Backpack == null)
        {
            dialog_Menu = 
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_Menu", CanvasLayer.System))
                .GetComponent<Dialog_Menu>();
        }
        await dialog_Menu.SetUpShowAsync();
    }

    //-Hint 01
    private Dialog_Common_Hint_01 dialog_Common_Hint_01 = null;
    public async Task ShowDialog_Common_Hint_01Async(string str)
    {
        if (dialog_Common_Hint_01 == null)
        {
            dialog_Common_Hint_01 = 
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_Common_Hint_01", CanvasLayer.System))
                .GetComponent<Dialog_Common_Hint_01>();
        }
        await dialog_Common_Hint_01.SetUpShowAsync(str);
    }
    //-Hint 02
    private Dialog_Common_Hint_02 dialog_Common_Hint_02 = null;
    public async Task ShowDialog_Common_Hint_02Async(GameObject primary, List<RectTransform> rects ,bool active)
    {
        if (dialog_Common_Hint_02 == null)
        {
            dialog_Common_Hint_02 = 
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_Common_Hint_02", CanvasLayer.System))
                .GetComponent<Dialog_Common_Hint_02>();
        }
        await dialog_Common_Hint_02.SetUpShowAsync(primary, rects, active);
    }

    //Get Acticle
    public async Task ShowGetArticle(List<ulong> ids, params ulong[] counts)
    {
        await AsyncDefaule();
    }
}
