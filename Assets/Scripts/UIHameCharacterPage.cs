using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIHameCharacterPage : MiBaseClass, IUIPage
{
    public async Task Initialization()
    {
        Log(color: Color.black, "Initialization");
        await AsyncDefaule();
    }
    public async Task ShowAsync()
    {
        Log(color: Color.black, "ShowAsync");
        await AsyncDefaule();
    }
    public async Task Distroy()
    {
        Log(color: Color.black, "Distroy");
        await AsyncDefaule();
    }
    public object Clone()
    {
        return (UIHomeMainSystemPage)MemberwiseClone();
    }
}