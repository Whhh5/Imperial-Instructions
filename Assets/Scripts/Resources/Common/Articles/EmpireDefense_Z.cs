using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.Tools;

public class EmpireDefense_Z : Base_BuildingFacilities
{
    [SerializeField] GameObject core;
    [SerializeField] float attackValue;
    [SerializeField] float attackRadius;
    [SerializeField] float attackIntervalTime;
    [SerializeField] MiCommonCollider attackTrigger;
    [SerializeField] LayerMask attackLayerMask;
    [SerializeField] bool isExecute = true;

    [SerializeField, ReadOnly] CommonEffectsBase line_1;
    [SerializeField, ReadOnly] CommonEffectsBase line_2;
    [SerializeField, ReadOnly] Collider2D target;
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        attackTrigger.GetComponent<CircleCollider2D>().radius = attackRadius;
        attackTrigger.onColliderTriggerEnter += OnTriggerTriggerEnter2D;
        line_1 = CommonManager.Instance.prefab.Get_Common_Line_1(core.transform, radius: attackRadius);
        StartCoroutine(EAttackLoop());
    }

    private void Update()
    {
        target = Physics2D.OverlapCircle(main.transform.position, attackRadius, layerMask: attackLayerMask);
        if (target != null)
        {
            if (line_2 == null)
            {
                line_2 = CommonManager.Instance.prefab.Get_Common_Line_2(main.transform, target.transform);
            }
            line_2.gameObject.SetActive(true);
        }
        else if (line_2 != null)
        {
            line_2.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {

    }

    public override void Active(params object[] value)
    {

    }

    public override void Prepare()
    {

    }

    public override void SetParameter(params object[] value)
    {

    }
    public void OnTriggerTriggerEnter2D(Collider2D collider2D)
    {
        base.OnMainTriggerEnter2D(collider2D);
    }
    public override void Destroy()
    {
        StopCoroutine(EAttackLoop());
        if (line_1 != null)
        {
            line_1.Destroy();
            line_1 = null;
        }
        if (line_2 != null)
        {
            line_2.Destroy();
            line_2 = null;
        }
        base.Destroy();
    }
    IEnumerator EAttackLoop()
    {
        while (true)
        {
            if (isExecute)
            {
                if (target != null)
                {
                    CommonManager.Instance.prefab.Get_Bullet_2(main.transform.position, target.transform, attackValue);
                }
            }
            yield return new WaitForSeconds(attackIntervalTime);
        }
    }
}
