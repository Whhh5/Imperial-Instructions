using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MiManchi.MiAsync;
using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using MiManchi.MiBaseStruct;
using MiManchi.MiDataStructure;
using MiManchi.MiEnum;
using MiManchi.MiFactory;
using MiManchi.MiInput;
using MiManchi.MiInteraction;
using MiManchi.MiPool;
using MiManchi.MiResoures;
using MiManchi.Tools;


namespace MiManchi
{
    public class MiProperty : MiSingleton<MiProperty>
    {
        //MiBaseClass.MiBaseClass BaseClass => MiBaseClass.MiBaseClass;

        MiAsyncManager AsyncManager => MiAsync.MiAsyncManager.Instance;
        MiInputManager InputManager => MiInputManager.Instance;

        //MiUIDialog Dialog => MiUIDialog;
    }
}
