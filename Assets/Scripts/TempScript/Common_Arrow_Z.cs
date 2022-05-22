using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MiManchi.MiBaseClass;
using MiManchi.Tools;
using MiManchi.MiInterface;
using MiManchi;
using System;
using MiManchi.MiEnum;

public class Common_Arrow_Z : CommonArrowBase
{
    [Tooltip("Min Max  >= 0"),SerializeField] Vector2 length;
    [SerializeField] Rigidbody2D rigi;
    [SerializeField] Transform handle;
    [SerializeField,ReadOnly] float nowLength;
    [SerializeField, Range(0, 1)] float _proportion;
    [SerializeField] MiCommonCollider mainCollider;
    [SerializeField] Animation effectsAnima;
    [SerializeField] LayerMask hitLayerMaske;
    [SerializeField, ReadOnly] Transform hitTarget;
    [SerializeField, ReadOnly] Vector3 targetVector;
    [SerializeField, ReadOnly] float durationTime = 3.0f;
    [SerializeField] MethodMode angleMode = MethodMode.Manual;
    [SerializeField] MethodMode moveMode = MethodMode.System;
    float proportion
    {
        get { return _proportion; }
        set
        {
            value = value > 1 ? 1 : value;
            value = value < 0 ? 0 : value;
            _proportion = value;
        }
    }
    [SerializeField] bool isEnabledLerp = false;
    [SerializeField] float lerpSpeed;

    [SerializeField, ReadOnly] bool isPrepare;
    [SerializeField, ReadOnly] bool isMainAngle;
    [SerializeField] float expladeRadius;
    [SerializeField] LayerMask expladeAttackLayerMask;
    [SerializeField] Vector3 lastPosition;
    protected override void Initialization()
    {
        base.Initialization();
        length.x = length.x < 0 ? 0 : length.x;
        length.y = length.y < 0 ? 0 : length.y;
        mainCollider.onColliderEnter += OnColliderEnter2DClick;
    }
    private void Update()
    {
        UpdateLength(proportion);
        HitTarget_Move();
        if (isPrepare)
        {
            proportion += Time.deltaTime;
            if ((angleMode & MethodMode.Manual) != 0)
            {
                main.transform.rotation = Quaternion.Euler(0, 0, Battle2DManager.Instance.mainHandle_Info.handleAngle);
            }
        }
        if(isMainAngle)
        {
            mainAngle();
        }
    }
    void mainAngle()
    {
        if (lastPosition != main.transform.position)
        {
            var vec = main.transform.position - lastPosition;
            var dot = Mathf.Acos(Vector3.Dot(vec.normalized, Vector3.right)) * Mathf.Rad2Deg;
            var cross = Vector3.Cross(Vector3.right, vec.normalized).z;
            cross = cross > 0 ? 1 : cross;
            cross = cross < 0 ? -1 : cross;
            var angle = dot * cross;
            main.transform.rotation = Quaternion.Euler(0, 0, angle);
            lastPosition = main.transform.position;
        }
    }
    void HitTarget_Move()
    {
        if (hitTarget != null)
        {
            var dir = hitTarget.TransformPoint(targetVector);
            main.transform.position = dir;
        }
    }
    public void UpdateLength(float value)
    {

        var unitLength = value;
        unitLength = unitLength > 1 ? 1 : unitLength;
        unitLength = unitLength < 0 ? 0 : unitLength;
        nowLength = length.x + length.y * unitLength;

        if (isEnabledLerp)
        {
            handle.localScale = Vector3.Lerp(handle.localScale,
                new Vector3(nowLength, handle.localScale.y, handle.localScale.z), lerpSpeed * Time.deltaTime);
        }
        else
        {
            handle.localScale = new Vector3(nowLength, handle.localScale.y, handle.localScale.z);
        }
    }

    public override void Active(params object[] value)
    {
        lastPosition = main.transform.position;
        isPrepare = false;
        isMainAngle = true;
        rigi.drag = 1;
        mainCollider.GetComponent<CircleCollider2D>().enabled = true;
        effectsAnima.Play("Common_Arrow_Z_Effects_Hide");
        DOTween.To(() => 2, value => { }, 0, durationTime).OnComplete(() =>
             {
                 if (proportion >= 1)
                 {
                     CommonManager.Instance.prefab.Get_Eff_Common_Aureole_1(
                         transform.TransformPoint(main.transform.localPosition), main.transform.rotation);
                     Explade_Attack();
                 }
                 Destroy();
             });
        if (proportion >= 1)
        {
            // 爆炸范围提示信息
            //CommonManager.Instance.prefab.Get_Common_Line_1
            //    (target: main.transform, radius: expladeRadius, positionCount: 100,durationTime: durationTime);
        }
        switch (moveMode)
        {
            case MethodMode.None:
                break;
            case MethodMode.System:
                rigi.AddRelativeForce(new Vector2(100f, 0), mode: ForceMode2D.Impulse);
                break;
            case MethodMode.Manual:
                return;
            default:
                break;
        }
    }

    private void OnColliderEnter2DClick(Collision2D collision)
    {
        rigi.drag = float.MaxValue;
        Battle2DManager.Instance.camera_Controller.Shake();
        if (proportion < 1)
        {
            //Log(Color.red, $"{proportion}  Defeated");
        }
        else
        {
            //Log(Color.green, $"{proportion}  Victory");
        }
        var collisionLayer = collision.collider.gameObject.layer;
        if (((int)Mathf.Pow(2, collisionLayer) & hitLayerMaske.value) != 0)
        {
            isMainAngle = false;
            hitTarget = collision.transform;
            targetVector = (main.transform.position - hitTarget.position);
            CommonManager.Instance.prefab.Get_Eff_SelectTarget_1(collision.transform, durationTime / 2);
            MiCommonCollider collider = collision.collider.GetComponent<MiCommonCollider>();
            if (collider != null && collider.GetMainObject().TryGetComponent<BaseGameObject_Game>(out BaseGameObject_Game cs))
            {
                MiDataManager.Instance.dataProceccing.BloodChange(
                    MiDataManager.Instance.master.LocalizeSkillsDataItem[100000001].attack, cs, collision.GetContact(0).point);
            }
            else
            {
                Log(Color.green, $"{collision.collider.name}  Absent Cs BaseGameObject_Game ,   Please  Add  Cs");
            }
        }
        mainCollider.GetComponent<CircleCollider2D>().enabled = false;
    }
    public override void Destroy()
    {
        base.Destroy();
    }
    void Explade_Attack()
    {
        List<GameObject> objs = new List<GameObject>();
        var center = main.transform.position;
        Collider2D[] cols = Physics2D.OverlapCircleAll(
            new Vector2(center.x, center.y), radius: expladeRadius, layerMask: expladeAttackLayerMask);


        string str = "";
        foreach (var parameter in cols)
        {
            CommonManager.Instance.prefab.Get_Eff_SelectTarget_1(parameter.transform, 1.0f);
            MiCommonCollider collider = parameter.GetComponent<MiCommonCollider>();
            if (collider != null && collider.GetMainObject().TryGetComponent<BaseGameObject_Game>(out BaseGameObject_Game cs))
            {
                MiDataManager.Instance.dataProceccing.BloodChange(
                    MiDataManager.Instance.master.LocalizeSkillsDataItem[100000001].attack, cs, parameter.ClosestPoint(main.transform.position));
            }
            else
            {
                Log(Color.green, $"{parameter.name}  Absent Cs :  BaseGameObject_Game");
            }
            str += parameter.name + " - ";
        }
        Log(Color.red, $"Explade Scope Detected Object {cols.Length.ToString()} \n{str}");
    }
    public override void Prepare()
    {
        rigi.drag = float.MaxValue;
        isPrepare = true;
        isMainAngle = false;
        proportion = 0;
        hitTarget = null;
        effectsAnima.Play("Common_Arrow_Z_Effects_Show");
        angleMode = MethodMode.Manual;
        moveMode = MethodMode.System;
        mainCollider.GetComponent<CircleCollider2D>().enabled = true;
    }
    /// <summary>
    /// 1.angle controller
    /// 2.move controller
    /// </summary>
    /// <param name="value"></param>
    public override void SetParameter(params object[] mode)
    {
        if (mode.Length >= 2)
        {
            angleMode = (MethodMode)mode[0];
            moveMode = (MethodMode)mode[1];
        }
        else
        {
            Log(Color.red, $"Please Check Status Count ");
        }
    }
}
