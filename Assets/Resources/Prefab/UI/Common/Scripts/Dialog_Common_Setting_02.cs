using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using MiManchi.MiInteraction;
using System.Threading.Tasks;
using System;
using MiManchi.MiBaseStruct;

public class Dialog_Common_Setting_02 : MiUIDialog
{
    [SerializeField] MiUIButton close2Button;
    [SerializeField] MiUIButton useButton;
    [SerializeField] MiUIButton OnSaleButton;


    [SerializeField] MiUIText title;

    private void Update()
    {
        //if (SetStatus(true).Wait())
        //{

        //}
    }
    public async Task SetUpShowAsync(Func<Task<Valuable>> func)
    {
        var valuable = await func.Invoke();
    }
}
