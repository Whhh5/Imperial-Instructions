using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiResoures;

public class Instantiate_Prefab : MiSingleton<Instantiate_Prefab>
{
    public GameObject Common_Arrow_Z;


    public void Init()
    {
        //Common_Arrow_Z = MiResourcesManager.Instance.Load<GameObject>(CommonPerfab)
    }
}
