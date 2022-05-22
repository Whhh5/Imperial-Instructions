using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System.Threading.Tasks;
using MiManchi.MiInterface;
using System;

public sealed class MiUIPage : MiBaseClass
{
    IUIPage nowPage = null;

    Type nowType = null;
    public async Task OpenPage<T>() where T : class, IUIPage, new()
    {
        var obj = Activator.CreateInstance<T>();
        if (nowPage != null)
        {
            Log(color: Color.black, $"{nowType.Name}  {obj.GetType().Name}  {nowType.Name != obj.GetType().Name}");
            if (nowType.Name != obj.GetType().Name)
            {
                await nowPage.Distroy();
                nowPage = null;
                await obj.Initialization();
            }
            else
            {
                obj = nowPage as T;
            }
        }
        else
        {
            await obj.Initialization();
        }
        nowPage = obj;
        nowType = obj.GetType();
        await obj.ShowAsync();
    }
    public async Task ClosePage<T>()
    {
        if (nowPage != null)
        {
            await nowPage.Distroy();
            nowPage = null;
        }
    }
}