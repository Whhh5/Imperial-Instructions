using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using MiManchi.MiBaseClass;
using MiManchi;
using MiManchi.MiAsync;
using MiManchi.Tools;
using MiManchi.MiEnum;
using UnityEngine.UI;

public class HorVerLayoutGroup: MiUIDialog
{
    //VerHorLayout  Horizontal Vertical
    enum Dircetion
    {
        Vertical,
        Horizontal,
    }
    enum Dircetion2
    {
        left,
        right,
        disperse,
    }
    [SerializeField] RectTransform main;
    [SerializeField,ReadOnly] GameObject primary;
    [SerializeField,ReadOnly] List<RectTransform> articleList = new List<RectTransform>();
    [SerializeField] Dircetion dircetion;
    [SerializeField] Dircetion2 dircetion2;
    [SerializeField] Vector2 lineAndCount;
    [SerializeField] Vector2 intervalPositionAndTime;
    [SerializeField] bool autoDirection = false;
    [SerializeField] RectTransform autoSize;
    [Tooltip("Left - Right - Top - Bottom"),SerializeField] Vector4 autoFrame;


    RectTransform firstArticle;
    Vector3 direction;
    Vector3 autoDir;
    protected override async Task InitializationAsync()
    {
        await base.InitializationAsync();
        switch (dircetion)
        {
            case Dircetion.Vertical:
                direction = new Vector3(0, intervalPositionAndTime.x, 0);

                break;
            case Dircetion.Horizontal:
                direction = new Vector3(intervalPositionAndTime.x, 0, 0);
                break;
            default:
                break;
        }
        if (autoSize != null)
        {
            autoSize.sizeDelta = new Vector2(maxWidth, 0);
        }
    }
    public async Task SetUpShowAsync(GameObject primary, List<RectTransform> articleList = null)
    {
        this.primary = primary;
        await Destroy();
        if (articleList == null)
        {
            Log(color: Color.black, "Nont SetUpShow() ,List Is Null Or Count Is Zero");
            articleList = new List<RectTransform>();
        }
        this.articleList = articleList;
        await ShowAsync();
    }
    public override async Task ShowAsync(DialogMode mode = DialogMode.none)
    {
        await base.ShowAsync(mode);
        if (articleList.Count < 1) return;
        await SetFirstArticle(articleList[0]);
        for (int i = 0; i < articleList.Count; i++)
        {
            articleList[i].gameObject.SetActive(true);
            articleList[i].Normalization(main);
            articleList[i].anchoredPosition3D = firstArticle.anchoredPosition3D;
            articleList[i].DOAnchorPos3D(firstArticle.anchoredPosition3D + direction * i, intervalPositionAndTime.y * i, false);
            await AutoSize(articleList[i], ushort.Parse((i + 1).ToString()));
        }
    }
    public async Task AddAsync(RectTransform rect)
    {
        rect.Normalization(main);
        if (firstArticle == null)
        {
            await SetFirstArticle(rect);
        }
        rect.anchoredPosition3D = firstArticle.anchoredPosition3D;
        rect.gameObject.SetActive(true);
        rect.DOAnchorPos3D(firstArticle.anchoredPosition3D + direction * articleList.Count, intervalPositionAndTime.y * articleList.Count, false);
        articleList.Add(rect);
        await AutoSize(rect, ushort.Parse(articleList.Count.ToString()));
    }

    public async Task RemoveAsync(RectTransform rect)
    {
        if (!articleList.Contains(rect))
        {
            Log(color: Color.black, $" -- {rect.name} --  Nont Absent Layout");
            return;
        }
        rect.gameObject.SetActive(false);
        rect.Normalization(null);
        await ObjPool.Repulace(primary,rect.gameObject);
        articleList.Remove(rect);

        if (articleList.Count == 0)
        {
            Log(color: Color.black, $"Article List Count Is Zero");
            return;
        }
        if (firstArticle == null)
        {
            await SetFirstArticle(articleList[0]);
        }

        var index = articleList.IndexOf(rect);

        for (int i = index; i < articleList.Count; i++)
        {
            articleList[i].DOAnchorPos3D(firstArticle.anchoredPosition3D + direction * i, intervalPositionAndTime.y * (i - index + 1), false);
        }
    }
    async Task SetFirstArticle(RectTransform rect)
    {
        firstArticle = rect;
        if (autoDirection)
        {
            firstArticle.anchoredPosition3D = new Vector3(autoFrame.x, -autoFrame.y, 0);
        }
        else
        {
            firstArticle.anchoredPosition3D = Vector3.zero;
        }
        //if (dircetion2 == Dircetion2.disperse)
        //{
        //    await Disperse(firstArticle);
        //}


    }

    public async Task Destroy()
    {
        foreach (var art in this.articleList)
        {
            art.gameObject.SetActive(false);
            art.Normalization(null);
            await ObjPool.Repulace(this.primary, art.gameObject);
        }
        this.articleList = new List<RectTransform>();
    }


    [SerializeField] float maxWidth = 300.0f;
    [SerializeField] float maxHeight;
    public async Task AutoSize(RectTransform rect, ushort count)
    {
        if (autoSize != null && autoDirection)
        {
            var width = rect.rect.width + autoFrame.x + autoFrame.y > autoSize.rect.width ? rect.rect.width + autoFrame.x + autoFrame.y : autoSize.rect.width;
            maxWidth = width;
            maxHeight = count * Mathf.Abs(intervalPositionAndTime.x) + autoFrame.z + autoFrame.w;
            DOTween.To(() => autoSize.sizeDelta, value => { autoSize.sizeDelta = value; }, new Vector2(maxWidth, maxHeight), Mathf.Abs(intervalPositionAndTime.y * articleList.Count)).SetUpdate(false);
        }
    }
    public async Task SetAutoPivot()
    {
        if (autoDirection)
        {
            float x = 0, y = 0;
            var mousePosition = Input.mousePosition;
            if (Mathf.Abs(Camera.main.pixelHeight - mousePosition.y) < maxHeight)
            {
                y = 1;
            }
            if (Mathf.Abs(Camera.main.pixelWidth - mousePosition.x) < maxWidth)
            {
                x = 1;
            }
            autoSize.pivot = new Vector2(x, y);
        }
        else
        {
            Log(Color.black, $"Please Confirm Auto Setting Is Enabled");
        }
    }

    //public async Task Disperse(RectTransform firstArticlePosition)
    //{
    //    float tempInterval = 100;
    //    var count = articleList.Count;

    //    var firstPos = count / 2 * tempInterval;
    //    firstArticlePosition
    //}
}
