using MiManchi.MiInterface;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MiManchi.MiBaseClass;

public class UIHomeMainSystemPage : MiBaseClass, IUIPage
{
    public async Task Initialization()
    {
        Log(color: Color.black, "Initialization");
    }

    public async Task ShowAsync()
    {
        Log(color: Color.black, "ShowAsync");
    }

    public async Task Distroy()
    {
        Log(color: Color.black, "Distroy");
    }
    public object Clone()
    {
        return (UIHomeMainSystemPage)MemberwiseClone();
    }
}
