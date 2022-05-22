using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.Tools;
using DG.Tweening;

public class Common_Line_2 : CommonEffectsBase
{
    [SerializeField] LineRenderer mainLine;
    [SerializeField] float unitLength;
    [SerializeField, ReadOnly] float tiling;
    [SerializeField, ReadOnly] Transform startTarget;
    [SerializeField, ReadOnly] Transform endTarget;

    private void Update()
    {
        if (startTarget != null && endTarget != null)
        {
            mainLine.SetPositions(new Vector3[] { startTarget.position, endTarget.position });
        }
    }
    public override void Active(params object[] objs)
    {

    }

    public override void Prepare()
    {
        mainLine.positionCount = 2;
        startTarget = null;
        endTarget = null;
    }

    public override void SetParameter(params object[] value)
    {
        startTarget = value[0] as Transform;
        endTarget = value[1] as Transform;
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
