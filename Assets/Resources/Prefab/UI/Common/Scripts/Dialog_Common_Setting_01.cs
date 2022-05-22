using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System.Threading.Tasks;
using MiManchi;
using UnityEngine.UI;
using MiManchi.MiInteraction;
using TMPro;

public class Dialog_Common_Setting_01 : MiUIDialog
{
    [SerializeField] MiUIButton confirm;
    [SerializeField] MiUIButton reduction;
    [SerializeField] MiUIButton reduceCount;
    [SerializeField] MiUIButton addCount;

    [Header("RactTransform"), 
        SerializeField] RectTransform information;

    [Header("Image"),
        SerializeField] Image icon;

    [Header("Input Field")
        ,SerializeField] InputField countField;
    [SerializeField] InputField testId;

    [Header("Prefab"),
        SerializeField] GameObject textPro;

    [SerializeField] HorVerLayoutGroup layoutList;
    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();
    }
    public async Task SetUpShow(ulong id)
    {
        await ShowAsync();
        await layoutList.SetUpShowAsync(textPro,new List<RectTransform>());
        confirm.onClick.RemoveAllListeners();
        reduction.onClick.RemoveAllListeners();
        reduceCount.onClick.RemoveAllListeners();
        addCount.onClick.RemoveAllListeners();


        confirm.onClick.SubscribeEventAsync(async () =>
        {
            if (countField.text != string.Empty)
            {
                List<ulong> ids = new List<ulong>();
                List<ulong> counts = new List<ulong>();

                ids.Add(id);
                counts.Add(ulong.Parse(countField.text));
                var value = Mathf.Abs(int.Parse(countField.text));
                await MiServiceManager.Instance.RequestAriticle_RemoveAsync(ids, counts);
                await SetInformation(ids[0]);
            }
            else
            {
                Log(color: Color.black, $"Please Input Count");
                await MiUIManager.Instance.popup.ShowDialog_Common_Hint_01Async("Please Input Count ! ! ! ");
            }
        });

        reduction.onClick.SubscribeEventAsync(async () =>
        {
            countField.text = "";
        });

        reduceCount.onClick.SubscribeEventAsync(async () =>
        {
            countField.text = (ulong.Parse(countField.text) - 1).ToString();
        });

        addCount.onClick.SubscribeEventAsync(async () =>
        {
            countField.text = (ulong.Parse(countField.text) + 1).ToString();
        });
    }


    private async Task SetInformation(ulong id)
    {
        var t = MiDataManager.Instance.master.Article[id];

        var fidldsInfo = t.GetType().GetFields();

        await layoutList.Destroy();

        foreach (var info in fidldsInfo)
        {
            var obj = (await ObjPool.GetObjectAsync(textPro));

            RectTransform rect = obj.GetComponent<RectTransform>();
            TextMeshProUGUI text = obj.GetComponent<TextMeshProUGUI>();

            text.text = $"{info.Name}:{info.GetValue(t)}";

            await layoutList.AddAsync(rect);

            obj.SetActive(true);

        }
    }
}
