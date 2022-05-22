using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using MiManchi.Tools;
using System;
using MiManchi.MiEnum;
using MiManchi.MiBaseStruct;
using MiManchi;
using DG.Tweening;

public abstract class BaseGameObject_Game : MiObjPoolPublicParameter, ICommon_Object
{
    [SerializeField, ReadOnly] string objName;
    [SerializeField] protected GameObject main;
    [SerializeField] Animation mainAnima;
    [SerializeField] ObjectType objectType;
    [SerializeField] protected CommonGameObjectInfo objInfo = new CommonGameObjectInfo(0);
    [SerializeField] private float bloodMax;
    [SerializeField] private float _blood;
    [SerializeField] private Action<CommonGameObjectInfo> setBloodClick = (x) => { };
    private float blood
    {
        get
        {
            return _blood;
        }
        set
        {
            value = value > bloodMax ? _blood : value;
            value = value < 0 ? 0 : value;
            _blood = value;
        }
    }
    [SerializeField, ReadOnly] protected float bloodProportion;
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        setBloodClick += (objInfo) =>
        {
            if (objInfo.proportionBlood <= 0)
            {
                Destroy();
            } 
        };
        objInfo.MaxBlood = bloodMax;
        objInfo.lastBlood = blood;
        SetBlood(bloodMax);
    }
    protected override void Initialization()
    {
        base.Initialization();
    }
    public ObjectType GetObjectType()
    {
        return objectType;
    }
    public GameObject GetMain()
    {
        return main;
    }

    public CommonGameObjectInfo GetInfo()
    {
        return objInfo;
    }
    public ulong GetId()
    {
        return id;
    }
    public abstract void Prepare();
    public abstract void SetParameter(params object[] value);
    public void AddBloodValue(float value)
    {
        blood += value;
        SetProportion();
    }
    public virtual void SetBlood(float value)
    {
        blood = value;
        SetProportion();
    }
    void SetProportion()
    {
        bloodProportion = blood / bloodMax;
        objInfo.SetBlood(blood);
        setBloodClick.Invoke(objInfo);
    }

    public void AddBSetBloodClick(Action<CommonGameObjectInfo> action)
    {
        setBloodClick += action;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override void Destroy()
    {
        var delayTime = 0.0f;
        if (mainAnima != null)
        {
            var hideClip = mainAnima.GetClip($"{original.name}_Hide");
            if (hideClip != null)
            {
                mainAnima.Play(hideClip.name);
                delayTime = hideClip.length;
            }
        }
        DOTween.To(() => 2, value => { }, 0, delayTime).OnComplete(() =>
               {
                   base.Destroy();
                   main.transform.Normalization(transform);
               });
    }
}
