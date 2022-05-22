using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System.Reflection;
using System;

public class SkillManager : MiSingleton<SkillManager>
{
    public CharacterSkill character = new CharacterSkill();
    public EnemySkill enemy = new EnemySkill();


    public CommonArrowBase GetSkills(ulong id, params object[] parameter)
    {
        if (!MiDataManager.Instance.master.LocalizeSkillsDataItem.ContainsKey(id))
        {
            Log(Color.red, $" Absent   Skills Id: {id} ");
            return default;
        }
        CommonArrowBase result = default;
        Type type = character.GetType();
        var name = MiDataManager.Instance.master.LocalizeSkillsDataItem[id].prefabName;
        var methodInfo = type.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
        if (methodInfo != null)
        {
            result = methodInfo.Invoke(this, new object[] { id, parameter[0] }) as CommonArrowBase;
        }
        else
        {
            Log(Color.red, $"Method Absent   Id: {id}   Method Name : {name}");
        }
        return result;
    }
}