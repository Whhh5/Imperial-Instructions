using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;
using MiManchi.MiEnum;

public class Dialog_Common_Hint_01 : MiUIDialog
{
    [SerializeField] MiUIText title;
    [SerializeField] CanvasGroup group;
    [Tooltip("Show Time - Stay Time - Hide Time"),SerializeField] Vector3 time;
    public async Task SetUpShowAsync(string str)
    {
        await title.SetRawText(str);
        await ShowAsync();
        group.alpha = 0;
        DOTween.To(() => group.alpha, value => { group.alpha = value; }, 1, time.x)
            .OnComplete(()=> {
                DOTween.To(() => 2, value => { }, 1, time.y)
                    .OnComplete(() => {
                        DOTween.To(() => group.alpha, value => { group.alpha = value; }, 0, time.z)
                            .OnComplete(() => { HideAsync().Wait(); });
                    });
            })
            .SetUpdate(false);
    }
    public new async Task ShowAsync(DialogMode mode = DialogMode.none)
    {
        gameObject.SetActive(true);
        transform.SetSiblingIndex(transform.parent.childCount - 1);
        await AsyncDefaule();
    }
}
