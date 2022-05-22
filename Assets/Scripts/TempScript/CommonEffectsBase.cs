using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using MiManchi.Tools;

public abstract class CommonEffectsBase : MiObjPoolPublicParameter, IEffects
{
    [SerializeField] protected GameObject main;
    [SerializeField] protected ParticleSystem mainParticle;
    [SerializeField, ReadOnly] protected ParticleSystem.MainModule mainMode;
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        mainMode = mainParticle.main;
    }
    public void Pause()
    {
        mainParticle.Pause();
    }

    public void Play()
    {
        mainParticle.Stop();
        mainParticle.Clear();
        mainParticle.time = 0;
        mainParticle.Play();
    }
    /// <summary>
    /// 0.StartDelay   1.StartLifetime
    /// </summary>
    /// <param name="objs"></param>
    public abstract void SetParameter(params object[] objs);

    public void Continue()
    {
        mainParticle.Play();
    }

    public abstract void Active(params object[] objs);

    public abstract void Prepare();


    public object Clone()
    {
        return MemberwiseClone();
    }
    public override void Destroy()
    {
        base.Destroy();
        main.transform.Normalization(gameObject.transform);
    }

    public GameObject GetMain()
    {
        return main;
    }

    public object GetInfo()
    {
        return null;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
