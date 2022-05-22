using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;

public class CommonManager : MiSingleton<CommonManager>
{
    public CommonPerfab prefab = new CommonPerfab();
    public CommonFilePath filePath = new CommonFilePath();
}
