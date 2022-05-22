using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using MiManchi.MiInteraction;
using System.Threading.Tasks;
using MiManchi.MiFactory;
using MiManchi.MiResoures;
using DG.Tweening;
using UnityEngine.UI;
using MiManchi.Tools;
using UnityEngine.Events;
using System;

public class Dialog_Backpack : MiUIDialog
{
    [SerializeField] MiUIButton shopping;
    [SerializeField] MiUIButton onSale;
    [SerializeField] MiUIButton installBtn;

    [Header("GameObject"),
        SerializeField] GameObject article;
    [SerializeField] GameObject lattic;
    [SerializeField] GameObject hint2Primary;

    //[Header("UI Text"),
    //    SerializeField] MiUIText hint2Primary;


    [Header("RectTransform"),
        SerializeField] RectTransform main;
    [SerializeField] RectTransform articleParent;

    [Header("Other CS"),
        SerializeField]
    HorVerLayoutGroup installList = new HorVerLayoutGroup();

    [Header("List"),
        SerializeField] List<Dialog_Backpack_Lattic> latticList = new List<Dialog_Backpack_Lattic>();


    [Header("ReadOnly"),
        SerializeField,ReadOnly] bool isArticleMove = false;
    [SerializeField,ReadOnly] RectTransform selectedArticle;
    [SerializeField,ReadOnly] Dialog_Backpack_Lattic articleLattic;
    UnityEvent articleClick = new UnityEvent();


    [Header("Temp Parameter"),
        SerializeField,ReadOnly] List<RectTransform> hint2Rects = new List<RectTransform>();
    [SerializeField, ReadOnly] string str = "";
    public async Task SetUpShow()
    {
        await InitLattic(10);
        await InitArticle();
        await ShowAsync(MiManchi.MiEnum.DialogMode.none);
    }

    async Task InitLattic(int value)
    {
        foreach (var item in latticList)
        {
            await item.Destroy();
        }
        latticList = new List<Dialog_Backpack_Lattic>();
        for (int i = 0; i < value; i++)
        {
            var obj = await ObjPool.GetObjectAsync(lattic);
            var rect = obj.GetComponent<RectTransform>();
            rect.SetParent(articleParent);
            rect.localScale = Vector3.one;
            rect.rotation = Quaternion.Euler(Vector3.zero);
            rect.anchoredPosition3D = Vector3.zero;
            var cs = obj.GetComponent<Dialog_Backpack_Lattic>();
            await cs.SetUp(lattic);
            obj.name = i.ToString();
            cs.click.AddOnPointerEnterClick(async () =>
            {
                articleClick.RemoveAllListeners();
                articleClick.SubscribeEventAsync(async () =>
                {
                    var item = await cs.GetArticle();
                    if (selectedArticle == null)
                    {
                        selectedArticle = null;
                        return;
                    }
                    if (item == null)
                    {
                        //调换位置
                        await cs.Put(selectedArticle);
                        Log(color: Color.black, $"{cs.name} -> {selectedArticle.name}");
                    }
                    else
                    {
                        await articleLattic.Put((await cs.TakeOut()).GetComponent<RectTransform>());
                        await cs.Put(selectedArticle);
                    }
                    selectedArticle = null;
                });




                foreach (var parameter in hint2Rects)
                {
                    await ObjPool.Repulace(hint2Primary, parameter.gameObject);
                }
                hint2Rects = new List<RectTransform>();
                if (cs.GetArticle() != null)
                {
                    MiArticle data = MiDataManager.Instance.master.Article[(await cs.GetArticle()).GetId()];
                    var type = Type.GetType(data.GetType().ToString());
                    MiUIText miUIText;

                    str = "";

                    foreach (var parameter in type.GetFields())
                    {
                        miUIText = (await ObjPool.GetObjectAsync(hint2Primary)).GetComponent<MiUIText>();
                        await miUIText.SetRawText($"{parameter.Name}: {parameter.GetValue(data)}");
                        hint2Rects.Add(miUIText.GetComponent<RectTransform>());
                    }
                    await MiUIManager.Instance.popup.ShowDialog_Common_Hint_02Async(hint2Primary, hint2Rects, true);
                }
            });
            cs.click.AddOnPointerExitClick(async () =>
            {
                await MiUIManager.Instance.popup.ShowDialog_Common_Hint_02Async(null,null,false);
            });
            cs.click.AddOnPointerDownClick(async () =>
            {
                var art = await cs.GetArticle();
                if (art != null)
                {
                    var artic = await cs.TakeOut();
                    selectedArticle = artic.GetComponent<RectTransform>();
                    selectedArticle.SetParent(main);
                    articleLattic = cs;
                    isArticleMove = true;
                }
            });
            cs.click.AddOnPointerUpClick(async () =>
            {
                if (selectedArticle != null)
                {
                    articleClick.Invoke();
                }
                isArticleMove = false;
            });

            cs.click.AddOnPointerClick(async () =>
            {
                
            });

            latticList.Add(cs);
            obj.SetActive(true);
        }

        installBtn.AddOnPointerEnterClick(async () => 
        {
            articleClick.RemoveAllListeners();
            articleClick.SubscribeEventAsync(async () =>
            {
                if (selectedArticle != null)
                {
                    await installList.AddAsync(selectedArticle);
                    isArticleMove = true;
                }
                selectedArticle = null;
            });
        });
        installBtn.AddOnPointerUpClick(async () => 
        {
            if (selectedArticle != null)
            {
                articleClick.Invoke();
            }
            isArticleMove = false;
        });
    }

    public async Task InitArticle()
    {
        var characterActicles = MiDataManager.Instance.master.Article;

        foreach (var item in characterActicles)
        {
            var obj = await ObjPool.GetObjectAsync(article);
            var rect = obj.GetComponent<RectTransform>();

            foreach (var parameter in latticList)
            {
                if (await parameter.Put(rect))
                    break;
            }
            var cs = obj.GetComponent<UICommon_ArticleFrame>();
            var sprite = await MiResourcesManager.Instance.LoadAsync<Sprite>("Sprite/Icon", item.Value.id.ToString());
            await cs.SetUp(article, item.Value.id, sprite, false);
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        if (isArticleMove && selectedArticle != null)
        {
            var mousePos = Input.mousePosition;
            selectedArticle.anchoredPosition3D = Vector3.Lerp(selectedArticle.anchoredPosition3D,new Vector3(mousePos.x - Camera.main.pixelWidth / 2, mousePos.y - Camera.main.pixelHeight * 0.5f), 10.0f * Time.deltaTime);
        }
    }
}
