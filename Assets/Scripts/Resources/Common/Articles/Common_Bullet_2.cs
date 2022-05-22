using MiManchi.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_Bullet_2 : CommonArrowBase
{
    [SerializeField] float maxDistanceDelta;
    [SerializeField] MiCommonCollider mainCollider2D;
    [SerializeField ,ReadOnly] float temporaryValue;
    [SerializeField] LayerMask attackLayerMask;
    [SerializeField, ReadOnly] Transform target = null;
    [SerializeField, ReadOnly] CommonEffectsBase tuoWei = null;
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        mainCollider2D.onColliderTriggerEnter += OnMainColliderEnter2D;
    }
    private void Update()
    {
        if (target != null)
        {
            main.transform.position = Vector3.MoveTowards(main.transform.position, target.position, maxDistanceDelta * Time.deltaTime);
        }
    }
    public override void Active(params object[] value)
    {

    }

    public override void Prepare()
    {
        target = null;
        mainCollider2D.enabled = true;
        tuoWei = CommonManager.Instance.prefab.Get_Eff_Common_MotionTrail_1(main.transform);
    }

    public override void SetParameter(params object[] value)
    {
        target = value[0] as Transform;
        temporaryValue = (float)value[1];
    }

    public void OnMainColliderEnter2D(Collider2D collider2D)
    {
        if (((int)Mathf.Pow(2, collider2D.gameObject.layer) & attackLayerMask) != 0)
        {
            mainCollider2D.enabled = false;
            if (collider2D.TryGetComponent<BaseGameObject_Game>(out BaseGameObject_Game cs))
            {

                MiDataManager.Instance.dataProceccing.BloodChange(
                    temporaryValue, cs, (Vector3)collider2D.ClosestPoint(main.transform.position), MiManchi.MiEnum.HarmType.Remove);
            }
            CommonManager.Instance.prefab.Get_Eff_Common_Aureole_2(main.transform.position);
            Destroy();
        }
    }
    public override void Destroy()
    {
        tuoWei.Destroy();
        base.Destroy();
    }
}
