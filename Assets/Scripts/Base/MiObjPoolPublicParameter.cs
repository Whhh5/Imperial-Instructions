using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.Tools;
using MiManchi;

public class MiObjPoolPublicParameter : MiBaseMonoBeHaviourClass
{
    [SerializeField, ReadOnly] protected ulong id;
    [SerializeField, ReadOnly] protected GameObject original;

    public virtual void SettingOriginal(GameObject original)
    {
        this.original = original;
    }
    public virtual void SettingId(ulong id)
    {
        this.id = id;
    }
    public virtual void Destroy()
    {
        gameObject.SetActive(false);
        transform.Normalization(null);
        ObjPool.Repulace(original, gameObject).Wait();
    }
}
