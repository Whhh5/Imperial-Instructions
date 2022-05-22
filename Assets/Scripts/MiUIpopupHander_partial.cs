using MiManchi.MiEnum;
using MiManchi.MiResoures;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public partial class MiUIpopupHander
{
    //-Backpack
    private Dialog_Backpack dialog_Backpack = null;
    public async Task ShowDialog_Backpack()
    {
        if (dialog_Backpack == null)
        {
            dialog_Backpack =
                (await MiResourcesManager.Instance.loadUIElementAsync<GameObject>
                ($"Prefab/UI/Common", "Dialog_Backpack", CanvasLayer.System))
                .GetComponent<Dialog_Backpack>();
        }
        await dialog_Backpack.SetUpShow();
    }
}
