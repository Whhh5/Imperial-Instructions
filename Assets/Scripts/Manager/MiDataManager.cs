using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiInterface;

public sealed class MiDataManager : MiManchi.MiBaseClass.MiSingleton<MiDataManager>
{
    public MiLocalizeManager localize = new MiLocalizeManager();
    public MiMasterManager master = new MiMasterManager();
    public MiCharacterManager character = new MiCharacterManager();

    public MiDataProcessing dataProceccing = new MiDataProcessing();
}
