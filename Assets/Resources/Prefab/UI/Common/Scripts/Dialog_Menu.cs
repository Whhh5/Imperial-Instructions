using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using MiManchi.MiInteraction;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Dialog_Menu : MiUIDialog
{
    [SerializeField] InputField idField;
    [SerializeField] InputField countField;
    [SerializeField] MiUIButton add;
    [SerializeField] MiUIButton remove;

    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();
    }
    public async Task SetUpShowAsync()
    {
        await ShowAsync();

        add.onClick.SubscribeEventAsync(async () => 
        {
            var id = ulong.Parse(idField.text);
            var count = ulong.Parse(countField.text);
            Log(color: Color.black, $"{id} {count}");
            await MiServiceManager.Instance.RequestAriticle_AddAsync(new List<ulong>(new ulong[] { id }), new List<ulong>(new ulong[] { count }));
        });
        remove.onClick.SubscribeEventAsync(async () =>
        {
            var id = ulong.Parse(idField.text);
            var count = ulong.Parse(countField.text);
            await MiServiceManager.Instance.RequestAriticle_RemoveAsync(new List<ulong>(new ulong[] { id }), new List<ulong>(new ulong[] { count }));
        });
    }
}
