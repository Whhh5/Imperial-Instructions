using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiInterface;
using MiManchi.MiBaseClass;
using DG.Tweening;
using MiManchi;

public abstract class CommonArrowBase : MiObjPoolPublicParameter, ICommon_Weapon
{
    [SerializeField] protected GameObject main;
    [SerializeField] protected Animation mainAnima;
    public virtual GameObject GetMain()
    {
        return main;
    }
    public abstract void Prepare();
    public abstract void Active(params object[] value);

    public virtual object Clone()
    {
        return MemberwiseClone();
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public abstract void SetParameter(params object[] value);
    public override void Destroy()
    {
        float delayDesTime = 0;
        if (mainAnima != null)
        {
            var hideClip = mainAnima.GetClip($"{original.name}_Hide");
            if (hideClip != null)
            {
                mainAnima.Play(mainAnima.name);
                delayDesTime = hideClip.length;
            }
        }
        DOTween.To(() => 2, value => { }, 0, delayDesTime)
            .OnComplete(() =>
            {
                base.Destroy();
                main.transform.Normalization(transform);
            });
    }
}
