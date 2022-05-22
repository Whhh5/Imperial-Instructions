using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiEnum;
using System;

public class MiDataProcessing : MiBaseClass
{
    public Action<BaseGameObject_Game> CharacterBloodEvent = (x) => { };
    public void BloodChange(float value, BaseGameObject_Game obj, Vector3 point = default, HarmType type = HarmType.Remove)
    {
        obj.AddBloodValue(value * ((int)type));
        Color color = Color.gray;
        if (point == default)
        {
            point = obj.transform.position;
        }
        switch (type)
        {
            case HarmType.Noen:
                ColorUtility.TryParseHtmlString("#FFFFFFCC", out color);
                break;
            case HarmType.Add:
                ColorUtility.TryParseHtmlString("#CCFF00CC", out color);
                break;
            case HarmType.Remove:
                ColorUtility.TryParseHtmlString("#FF7F00CC",out color);
                break;
            default:
                break;
        }
        CharacterBloodEvent.Invoke(obj);
        CommonManager.Instance.prefab.Get_Common_SetBlood_1(value.ToString(), point, color);
    }
}
