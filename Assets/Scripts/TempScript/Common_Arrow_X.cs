using DG.Tweening;
using MiManchi.MiEnum;
using MiManchi.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_Arrow_X : CommonArrowBase
{
    [SerializeField,ReadOnly] List<CommonArrowBase> objs = new List<CommonArrowBase>();
    [SerializeField, ReadOnly] Transform target = null;
    [SerializeField] float attackRadius = 10.0f;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float angleSpeed = 5.0f;
    [SerializeField] LayerMask attackLayerMask;
    [Tooltip("heigth - Move Time") ,SerializeField] Vector2 higthTime;
    [SerializeField] float attackInterval = 0.3f;
    [SerializeField] uint arrowCount;
    [SerializeField] float duration;

    // Temp Parameter
    Transform mainTr;
    Vector3 vec;
    float dot, cross, angle;
    CommonEffectsBase line_1 = null;

    /// <summary>
    /// Parameter
    /// 1.Arrow Count 
    /// 2.Duration Time 
    /// 3.Point
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override void Active(params object[]  value)
    {
        main.transform.DOMoveY(higthTime.x, higthTime.y);
        StartCoroutine(ELoop(arrowCount, duration));
    }
    private void Update()
    {
        var clo = Physics2D.OverlapCircle(
            new Vector2(main.transform.position.x, main.transform.position.y), radius: attackRadius, layerMask: attackLayerMask);
        var endPos = new Vector3(Battle2DManager.Instance.chara_Transform.position.x, main.transform.position.y, 0);
        main.transform.position = Vector3.Lerp(main.transform.position, endPos, moveSpeed * Time.deltaTime);
        if (clo != null)
        {
            target = clo.transform;
        }
        else
        {
            target = null;
        }
        foreach (var item in objs)
        {
            if (target != null)
            {
                mainTr = item.GetMain().transform;
                vec = target.position - mainTr.position;
                dot = Mathf.Acos(Vector3.Dot(vec.normalized, Vector3.right)) * Mathf.Rad2Deg;
                cross = Vector3.Cross(Vector3.right, vec.normalized).z;
                cross = cross > 0 ? 1 : cross;
                cross = cross < 0 ? -1 : cross;
                angle = dot * cross;
                mainTr.rotation = Quaternion.Lerp(mainTr.rotation, Quaternion.Euler(0, 0, angle), angleSpeed * Time.deltaTime);
            }
            item.transform.position = main.transform.position;
        }
    }

    /// <summary>
    /// Update Show Execute
    /// </summary>
    public override void Prepare()
    {
        line_1 = CommonManager.Instance.prefab.Get_Common_Line_1(main.transform, attackRadius);
    }

    public override void SetParameter(params object[] value)
    {

    }

    IEnumerator ELoop(uint count , float duration)
    {
        objs.Clear();
        int index = 0;
        while (index < count)
        {
            var arrow = SkillManager.Instance.character.GetSkills(100000001, main.transform.position);
            arrow.SetParameter(MethodMode.System, MethodMode.System);
            objs.Add(arrow);
            index++;
            yield return new WaitForSeconds(duration / count);
        }
        yield return new WaitForSeconds(2.0f);
        while (objs.Count != 0)
        {
            while (target == null)
            {
                yield return new WaitForSeconds(1.0f);
            }
            var obj = objs[0];
            obj.Active();
            objs.Remove(obj);
            yield return new WaitForSeconds(0.3f);
        }
        Destroy();
    }
    public override void Destroy()
    {
        if (line_1 != null)
        {
            line_1.Destroy();
        }
        line_1 = null;
        base.Destroy();
        main.transform.localPosition = new Vector3(0, 0, 0);
    }
}
