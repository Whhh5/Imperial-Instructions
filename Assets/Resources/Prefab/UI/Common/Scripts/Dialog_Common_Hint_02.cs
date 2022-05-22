using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using System.Threading.Tasks;
using DG.Tweening;

public class Dialog_Common_Hint_02 : MiUIDialog
{
    [SerializeField] CanvasGroup mainGroup;
    [SerializeField] RectTransform rect;
    [SerializeField] HorVerLayoutGroup layout;
    [SerializeField, Range(0, 20)] float hintMoveSpeed;
    [SerializeField] bool isEnabled = true;
    [SerializeField] Vector2 showAndHideTime;
    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();

        mainGroup.alpha = 0;
    }
    private void Update()
    {
        if (isEnabled)
        {
            Move().Wait();
            layout.SetAutoPivot().Wait();
        }
    }

    private async Task Move()
    {
        await AsyncDefaule();
        rect.anchoredPosition3D = Vector3.Lerp(rect.anchoredPosition3D, Input.mousePosition - new Vector3(Camera.main.pixelWidth * 0.5f, Camera.main.pixelHeight * 0.5f, 0), hintMoveSpeed * Time.deltaTime);
    }

    public async Task SetMove(bool enabled)
    {
        await AsyncDefaule();
        isEnabled = enabled;
    }

    public async Task SetUpShowAsync(GameObject primary,List<RectTransform> rects,bool active)
    {
        await base.OnShowAsync();
        Vector2 endAlphaAndTime;
        if (active)
        {
            await layout.SetUpShowAsync(primary, rects);
            await SetActive(active);
            endAlphaAndTime.x = 1;
            endAlphaAndTime.y = showAndHideTime.x;
        }
        else
        {
            await SetActive(active);
            endAlphaAndTime.x = 0;
            endAlphaAndTime.y = showAndHideTime.y;
        }
        DOTween.To(() => mainGroup.alpha, value => { mainGroup.alpha = value; }, endAlphaAndTime.x, showAndHideTime.y).OnComplete(() => { }).SetUpdate(false);
    }
    async Task SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
