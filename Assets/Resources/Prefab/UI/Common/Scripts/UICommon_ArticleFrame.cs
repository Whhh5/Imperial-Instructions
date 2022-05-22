using MiManchi.MiInteraction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using MiManchi.MiPool;
using MiManchi.MiBaseClass;
using UnityEngine.Events;
using MiManchi.Tools;
using MiManchi;
using MiManchi.MiResoures;

public class UICommon_ArticleFrame : MiBaseMonoBeHaviourClass,ICloneable
{
    [SerializeField, ReadOnly] GameObject original;
    [SerializeField, ReadOnly] ulong id;
    [SerializeField] CanvasGroup main;
    [SerializeField] MiUIText countText;
    [SerializeField] MiUIButton button;
    [SerializeField] Image icon;
    [SerializeField] Image frame;
    public async Task SetUp(GameObject original, ulong id, Sprite sprite, bool isRaycast = true)
    {
        this.id = id;
        this.original = original;
        icon.sprite = sprite;

        main.blocksRaycasts = isRaycast;
        await AsyncDefaule();
    }
    public async Task SetUp(GameObject original, ulong id,ulong count = 1, bool isRaycast = true)
    {
        this.id = id;
        this.original = original;
        icon.sprite = await MiResourcesManager.Instance.LoadAsync<Sprite>("Sprite/Icon", id.ToString());

        await countText.SetRawText(count.ToString());
        main.blocksRaycasts = isRaycast;
        await AsyncDefaule();
    }
    public async Task SetUp(Func<Task<GameObject>> original, Func<Task<ulong>> id, Func<Task<Sprite>> sprite, bool isRaycast = true)
    {
        this.id = await id.Invoke();
        this.original = await original.Invoke();
        icon.sprite = await sprite.Invoke();

        main.blocksRaycasts = isRaycast;
        await AsyncDefaule();
    }
    public async Task Follow(Vector3 anch)
    {
        await AsyncDefaule();
    }
    public ulong GetId()
    {
        return id;
    }
    public async Task Destroy()
    {
        await ObjPool.Repulace(original, gameObject);
        var rect = GetComponent<RectTransform>();
        rect.Normalization(null);
        gameObject.SetActive(false);
    }

    public object Clone()
    {
        return (UICommon_ArticleFrame)MemberwiseClone();
    }
}
