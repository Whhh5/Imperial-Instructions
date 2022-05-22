using MiManchi.MiInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;

public abstract class Base_BuildingFacilities : BaseGameObject_Game, IBuildingFacilities
{
    [SerializeField] MiCommonCollider mainCollider;
    public abstract void Active(params object[] value);
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public override void Destroy()
    {
        base.Destroy();
        main.transform.Normalization(transform);
    }
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        mainCollider.onColliderEnter += OnMainCollisionEnter2D;
        mainCollider.onColliderExit  += OnMainCollisionExit2D;
        mainCollider.onColliderStay += OnMainCollisionStay2D;
        mainCollider. onColliderTriggerEnter += OnMainTriggerEnter2D;
        mainCollider. onColliderTriggerExit += OnMainTriggerExit2D;
        mainCollider.onColliderTriggerStay += OnMainTriggerStay2D;
    }
    public virtual void OnMainCollisionEnter2D(Collision2D collision2D)
    {

    }

    public virtual void OnMainCollisionExit2D(Collision2D collision2D)
    {

    }

    public virtual void OnMainCollisionStay2D(Collision2D collision2D)
    {

    }

    public virtual void OnMainTriggerEnter2D(Collider2D collider2D)
    {

    }

    public virtual void OnMainTriggerExit2D(Collider2D collider2D)
    {

    }

    public virtual void OnMainTriggerStay2D(Collider2D collider2D)
    {

    }
}
