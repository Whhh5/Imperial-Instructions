using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MiManchi.MiBaseClass;
using MiManchi;
using MiManchi.Tools;
using System;

public abstract class MiDialog_World : MiObjPoolPublicParameter
{
    [SerializeField] protected GameObject main;
    [SerializeField] protected Animation anima;
    [SerializeField] protected List<GameObject> hideObj;
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        foreach (var para in hideObj)
        {
            para.SetActive(false);
        }
    }
    protected override void Initialization()
    {
        base.Initialization();
    }
    public virtual void SetUpOriginal(GameObject original)
    {
        this.original = original;
    }
    public void Show()
    {
        //var animaName = $"{original.name}_Hide";
        var animaName = $"{gameObject.name.Split(new string[] { "(clone)" }, StringSplitOptions.RemoveEmptyEntries)[0]}_Hide";
        var click = anima.GetClip(animaName);
        if (click != null)
        {
            anima.Play(animaName);
        }
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        //var animaName = $"{original.name}_Hide";
        var animaName = $"{gameObject.name.Split(new string[] { "(clone)" }, StringSplitOptions.RemoveEmptyEntries)[0]}_Hide";
        var click = anima.GetClip(animaName);
        float hideDelaty = 0;
        if (click != null)
        {
            anima.Play(animaName);
            hideDelaty = click.length;

        }
        DOTween.To(() => 2, value => { }, 0, hideDelaty).OnComplete(() =>
        {
            Destroy();
        });
    }
    public override void Destroy()
    {
        base.Destroy();
    }
}
