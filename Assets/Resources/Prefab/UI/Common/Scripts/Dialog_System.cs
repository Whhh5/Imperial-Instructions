using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiManchi.MiInteraction;
using MiManchi;
using System.Threading.Tasks;
using MiManchi.MiEnum;

public class Dialog_System : MiUIDialog
{
    [SerializeField] MiUIButton previous;
    [SerializeField] MiUIButton next;
    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();

        previous.onClick.RemoveAllListeners();
        previous.onClick.SubscribeEventAsync(async () => { 
            var dialog = await MiUIManager.Instance.stack.Previous();
            await dialog.ShowAsync(DialogMode.stack);
        });
        next.onClick.RemoveAllListeners();
        next.onClick.SubscribeEventAsync(async () => { 
            var dialog = await MiUIManager.Instance.stack.Next();
            await dialog.ShowAsync(DialogMode.stack);
        });
    }
}
