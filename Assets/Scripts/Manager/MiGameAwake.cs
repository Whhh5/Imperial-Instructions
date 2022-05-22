using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System;
using System.Reflection;

public static class MiGameAwake
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void GameAwake()
    {
        Type type = Type.GetType("BattleTest");
        var obj = Activator.CreateInstance(type);
        type.GetMethod("Takle").Invoke(obj, new object[] { });
    }
}
