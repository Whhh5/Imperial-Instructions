using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiInterface;
using System;
using MiManchi.MiResoures;
using MiManchi;
using DG.Tweening;
using MiManchi.MiBaseClass;

public class CommonPerfab : MiBaseClass
{
    public GameObject GetButtle(GameObject primitive, Transform point,GameObject buttle)
    {
        var gb = ObjPool.GetObject(buttle);
        gb.transform.SetPositionAndRotation(point.position, point.rotation);
        var main = gb.GetComponent<IWeapon>().GetMain(primitive);
        gb.AddComponent<CubeMove_Sin>().SetUp(main);
        gb.SetActive(true);
        return gb;
    }

    private GameObject eff_SelectTarget_1 = null;
    public CommonEffectsBase Get_Eff_SelectTarget_1(Transform target, float durationTime)
    {
        if (eff_SelectTarget_1 == null)
        {
            eff_SelectTarget_1 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Eff_SelectTarget_1");
        }
        var obj = ObjPool.GetObject(eff_SelectTarget_1);
        obj.transform.Normalization(null);
        var cs = obj.GetComponent<CommonEffectsBase>();
        cs.SetParameter(durationTime, target);
        cs.Active();
        obj.SetActive(true);
        return cs;
    }
    private GameObject common_Line_1 = null;
    public Common_Line_1 Get_Common_Line_1(Transform target, float radius)
    {
        if (common_Line_1 == null)
        {
            common_Line_1 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Common_Line_1");
        }
        var obj = ObjPool.GetObject(common_Line_1);
        var cs = obj.GetComponent<Common_Line_1>();
        cs.Prepare();
        cs.SetParameter(target, radius);
        cs.Active();
        obj.SetActive(true);
        return cs;
    }
    private GameObject common_Line_2 = null;
    public CommonEffectsBase Get_Common_Line_2( Transform startPoint, Transform endPoint)
    {
        if (common_Line_2 == null)
        {
            common_Line_2 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Common_Line_2");
        }
        var obj = ObjPool.GetObject(common_Line_2);
        var cs = obj.GetComponent<CommonEffectsBase>();
        cs.Prepare();
        cs.SetParameter(startPoint, endPoint);
        cs.Active();
        obj.SetActive(true);
        return cs;
    }
    private GameObject Eff_Common_Aureole_1 = null;
    public Eff_Common_Aureole_1 Get_Eff_Common_Aureole_1(Vector3 position,Quaternion rotation)
    {
        if (Eff_Common_Aureole_1 == null)
        {
            Eff_Common_Aureole_1 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Eff_Common_Aureole_1");
        }
        var obj = ObjPool.GetObject(Eff_Common_Aureole_1);
        var cs = obj.GetComponent<Eff_Common_Aureole_1>();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        cs.Play();
        return cs;
    }
    private GameObject common_SetBlood_1 = null;
    public Common_SetBlood_1 Get_Common_SetBlood_1(string value, Vector3 position, Color color)
    {
        if (common_SetBlood_1 == null)
        {
            common_SetBlood_1 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Common_SetBlood_1");
        }
        var obj = ObjPool.GetObject(common_SetBlood_1);
        var cs = obj.GetComponent<Common_SetBlood_1>();
        obj.transform.Normalization(null).position = position;
        cs.SetUp(value, color);
        return cs;
    }
    private GameObject eff_Common_Cloud_1 = null;
    public CommonEffectsBase Get_Eff_Common_Cloud_1( Vector3 position, float duration)
    {
        if (eff_Common_Cloud_1 == null)
        {
            eff_Common_Cloud_1 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Eff_Common_Cloud_1");
        }
        var obj = ObjPool.GetObject(eff_Common_Cloud_1);
        var cs = obj.GetComponent<CommonEffectsBase>();
        obj.transform.Normalization(null).position = position;
        cs.SetParameter((float)0.0f, duration);
        obj.SetActive(true);
        cs.Active();
        return cs;
    }
    private GameObject eff_Common_MotionTrail_1 = null;
    public CommonEffectsBase Get_Eff_Common_MotionTrail_1(Transform target)
    {
        if (eff_Common_MotionTrail_1 == null)
        {
            eff_Common_MotionTrail_1 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Eff_Common_MotionTrail_1");
        }
        var obj = ObjPool.GetObject(eff_Common_MotionTrail_1);
        var cs = obj.GetComponent<CommonEffectsBase>();
        obj.transform.Normalization(null).position = target.position;
        cs.Prepare();
        cs.SetParameter(target);
        obj.SetActive(true);
        cs.Active();
        return cs;
    }
    private GameObject eff_Common_Aureole_2 = null;
    public CommonEffectsBase Get_Eff_Common_Aureole_2(Vector3 startPosition)
    {
        if (eff_Common_Aureole_2 == null)
        {
            eff_Common_Aureole_2 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Effects/Eff_Common_Aureole_2");
        }
        var obj = ObjPool.GetObject(eff_Common_Aureole_2);
        var cs = obj.GetComponent<CommonEffectsBase>();
        obj.transform.Normalization(null).position = startPosition;
        cs.Prepare();
        cs.SetParameter();
        obj.SetActive(true);
        cs.Active();
        return cs;
    }
    private GameObject common_Bullet_2 = null;
    public CommonArrowBase Get_Bullet_2(Vector3 startPosition, Transform target, float attackValue)
    {
        if (common_Bullet_2 == null)
        {
            common_Bullet_2 = MiResourcesManager.Instance.Load<GameObject>(
                CommonManager.Instance.filePath.PreComPath, "Arrticles/Common_Bullet_2");
        }
        var obj = ObjPool.GetObject(common_Bullet_2);
        var cs = obj.GetComponent<CommonArrowBase>();
        obj.transform.Normalization(null).position = startPosition;
        cs.Prepare();
        cs.SetParameter(target, attackValue);
        obj.SetActive(true);
        cs.Active();
        return cs;
    }




    public void Put(GameObject primitive, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = new Vector3(0, 0, 0);
        ObjPool.Repulace(primitive, obj).Wait();
    }
}
