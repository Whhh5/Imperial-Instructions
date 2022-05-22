using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Z : BaseGameObject_Game
{
    [SerializeField] Rigidbody2D rigi2D;
    [SerializeField] Collider2D colloder2D;

    public override void Prepare()
    {
        colloder2D.enabled = true;
        if (rigi2D != null)
        {
            rigi2D.WakeUp();
        }
        gameObject.layer = LayerMask.NameToLayer(GetObjectType().ToString());
    }
    public override void SetParameter(params object[] value)
    {

    }
    public override void Destroy()
    {
        colloder2D.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        if (rigi2D != null)
        {
            rigi2D.Sleep();
        }
        base.Destroy();
    }
}
