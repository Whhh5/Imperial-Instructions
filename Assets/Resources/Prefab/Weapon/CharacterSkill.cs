using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi;
using System;
using MiManchi.MiInterface;
using MiManchi.MiResoures;
using System.Reflection;
using MiManchi.MiEnum;

public class CharacterSkill : MiBaseClass
{
    Dictionary<ulong,GameObject> skills = new Dictionary<ulong, GameObject>();
    Dictionary<ulong, GameObject> effects = new Dictionary<ulong, GameObject>();
    public CommonArrowBase GetSkills(ulong id, Vector3 startPosition, params object[] status)
    {
        ref readonly var data = ref DataManager.Master.GetTableData<LocalizeSkillsData>(id);


        if (!MiDataManager.Instance.master.LocalizeSkillsDataItem.ContainsKey(id))
        {
            Log(Color.red, $" Absent   Skills Id: {id} ");
            return default;
        }
        var prefabName = MiDataManager.Instance.master.LocalizeSkillsDataItem[id].prefabName;
        GameObject original = null;
        if (skills.ContainsKey(id))
        {
            original = skills[id];
        }
        else
        {
            original = MiResourcesManager.Instance.Load<GameObject>(CommonManager.Instance.filePath.PreComPath, $"Skills/{prefabName}");
            if (original != null)
            {
                skills.Add(id, original);
            }
            else
            {
                Log(Color.red, $" Absent   Prefab   Path   {CommonManager.Instance.filePath.PreComPath}/Skills/{prefabName}");
                return null;
            }
        }
        var obj = ObjPool.GetObject(original);
        obj.transform.Normalization(null);
        obj.transform.position = startPosition;
        CommonArrowBase result = obj.GetComponent<CommonArrowBase>();
        if (result != null)
        {
            result.GetMain().transform.Normalization(obj.transform);
            result.SettingId(id);
            result.Prepare();
            result.SetParameter(status);
            obj.SetActive(true);
        }
        return result;
    }

    public CommonEffectsBase GetEffects(ulong id, Vector3 startPosition, params object[] parameter)
    {
        if (!MiDataManager.Instance.master.LocalizeSkillsDataItem.ContainsKey(id))
        {
            Log(Color.red, $" Absent   Skills Id: {id} ");
            return default;
        }
        var prefabName = MiDataManager.Instance.master.LocalizeSkillsDataItem[id].prefabName;
        GameObject original = null;
        if (skills.ContainsKey(id))
        {
            original = skills[id];
        }
        else
        {
            original = MiResourcesManager.Instance.Load<GameObject>(CommonManager.Instance.filePath.PreComPath, $"Effcets/{prefabName}");
            if (original != null)
            {
                skills.Add(id, original);
            }
            else
            {
                Log(Color.red, $" Absent   Prefab   Path   {CommonManager.Instance.filePath.PreComPath}/Effcets/{prefabName}");
                return null;
            }
        }
        var obj = ObjPool.GetObject(original);
        obj.transform.Normalization(null);
        obj.transform.position = startPosition;
        CommonEffectsBase result = obj.GetComponent<CommonEffectsBase>();
        if (result != null)
        {
            result.GetMain().transform.Normalization(obj.transform);
            result.SettingId(id);
            result.Prepare();
            result.SetParameter(parameter);
            obj.SetActive(true);
        }
        return result;
    }
}