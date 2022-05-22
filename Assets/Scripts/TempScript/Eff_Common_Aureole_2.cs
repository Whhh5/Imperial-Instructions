using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MiManchi.Tools;

public class Eff_Common_Aureole_2 : CommonEffectsBase
{
    [SerializeField, ReadOnly] bool isDestroy = false;
    public override void Active(params object[] objs)
    {
        Play();
        DOTween.To(() => 2, value => { }, 0, 0.5f)
            .OnComplete(() =>
            {
                isDestroy = true;
            });
    }
    private void Update()
    {
        if (isDestroy && mainParticle.particleCount <= 0)
        {
            Destroy();
        }
    }
    public override void Prepare()
    {
        isDestroy = false;
    }

    public override void SetParameter(params object[] objs)
    {

    }
    public override void Destroy()
    {
        base.Destroy();
    }
}
