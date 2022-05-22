using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using MiManchi.MiEnum;
using MiManchi.MiAsync;
using MiManchi.MiInteraction;

public class Dialog_LoadingScene : MiUIDialog
{
    [SerializeField] MiUISlider slider;
    [SerializeField] MiUIButton nextBtn;

    protected override void Initialization()
    {
        base.Initialization();
    }
    public async Task SetUpShow(AsyncOperation opera)
    {
        await ShowAsync( DialogMode.none);
        Log(color: Color.black, $"{opera != null}");
        if (opera!=null)
        {
            StartCoroutine(LoadScene(opera));
        }
    }

    IEnumerator LoadScene(AsyncOperation opera)
    {
        MiAsyncManager.Instance.StartAsync(async ()=> await ShowAsync(DialogMode.none));
        opera.allowSceneActivation = false;
        while (!opera.isDone)
        {
            var value = opera.progress * 100 / 90.0f;
            slider.SetValue(value);
            yield return null;
        }
        nextBtn.onClick.SubscribeEventAsync(async () => 
        {
            await HideAsync();
            opera.allowSceneActivation = true;
        });

    }
}
