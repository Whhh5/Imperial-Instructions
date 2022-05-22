using MiManchi.MiInteraction;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MiManchi;

public class Dialog_Start : MiUIDialog
{
    [SerializeField] MiUIButton startBtn;
    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();
        startBtn.onClick.SubscribeEventAsync(async () =>
        {
            await GameManager.Instance.GameStart();
            await HideAsync();
        });
    }
}
