using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiEnum;
using MiManchi.MiBaseClass;
using System.Threading.Tasks;
using MiManchi.MiDataStructure;
using MiManchi;
using MiManchi.MiAsync;
using System;
using MiManchi.MiInterface;

public class MiUIManager : MiSingletonMonoBeHaviour<MiUIManager>
{
    private Dictionary<CanvasLayer, RectTransform> uiLayer = new Dictionary<CanvasLayer, RectTransform>();
    [SerializeField] public MiUIStack<MiUIDialog> stack = new MiUIStack<MiUIDialog>();


    public MiUIPage page = new MiUIPage();
    public MiUIpopupHander popup = new MiUIpopupHander();
    protected override void Initialization()
    {
        base.Initialization();
        DontDestroyOnLoad(gameObject);
        MiAsyncManager.Instance.StartAsync(async () => await LoadCanvasLayer());
    }
    public async Task LoadCanvasLayer()
    {
        uiLayer.Add(CanvasLayer.First,      GameObject.Find($"Layer-{CanvasLayer.First.ToString()}").GetComponent<RectTransform>());
        uiLayer.Add(CanvasLayer.Second,     GameObject.Find($"Layer-{CanvasLayer.Second.ToString()}").GetComponent<RectTransform>());
        uiLayer.Add(CanvasLayer.Third,      GameObject.Find($"Layer-{CanvasLayer.Third.ToString()}").GetComponent<RectTransform>());
        uiLayer.Add(CanvasLayer.Fourth,     GameObject.Find($"Layer-{CanvasLayer.Fourth.ToString()}").GetComponent<RectTransform>());
        uiLayer.Add(CanvasLayer.Fifth,      GameObject.Find($"Layer-{CanvasLayer.Fifth.ToString()}").GetComponent<RectTransform>());
        uiLayer.Add(CanvasLayer.System,     GameObject.Find($"Layer-{CanvasLayer.System.ToString()}").GetComponent<RectTransform>());
        uiLayer.Add(CanvasLayer.Loading,    GameObject.Find($"Layer-{CanvasLayer.Loading.ToString()}").GetComponent<RectTransform>());

        await Task.Delay(System.TimeSpan.Zero);
    }
    public async Task<RectTransform> GetCanvasRectAsync(CanvasLayer layer)
    {
        await MiAsyncManager.Instance.Default();
        if (uiLayer.ContainsKey(layer))
        {
            return uiLayer[layer];
        }
        else
        {
            Debug.LogError($"{GetType()}  {layer}  {(int)layer}  absent  in  canType");
            return null;
        }
    }
    public RectTransform GetCanvasRect(CanvasLayer layer)
    {
        Debug.Log(uiLayer.ContainsKey(layer));
        if (uiLayer.ContainsKey(layer))
        {
            return uiLayer[layer];
        }
        else
        {
            Debug.LogError($"{GetType()}  {layer}  {(int)layer}  absent  in  canType");
            return null;
        }
    }
}