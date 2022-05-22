using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.Tools;

public class MiSpriteAlign2D : MiBaseMonoBeHaviourClass
{
    enum Direction
    {
        x,
        y,
        z,
    }
    enum RefernecePoint
    {
        Front,
        Middle,
        Back,
    }
    [SerializeField] SpriteRenderer linkObj;
    [SerializeField] Direction direction;
    [SerializeField] RefernecePoint refernecePoint;
    [SerializeField] float interval;
    [SerializeField,ReadOnly] Vector3 border;
    [SerializeField] bool isLerpMove;
    [SerializeField] float lerpSpeed;

    [SerializeField] Transform temporary;
    protected override void Initialization()
    {
        base.Initialization();
        border = linkObj.bounds.extents;
    }
    public void Sampling(Transform linkTr, float standValue, float value)
    {
        var maxlength = linkTr.localScale.x * standValue + value * standValue;
        var dir = new Vector3();
        float vec = value * standValue;
        switch (direction)
        {
            case Direction.x:
                dir = new Vector3(linkTr.localScale.x * standValue + vec, 0, 0);
                break;
            case Direction.y:
                dir = new Vector3(0, linkTr.localScale.y * standValue + vec, 0);
                break;
            case Direction.z:
                dir = new Vector3(0, 0, linkTr.localScale.z * standValue + vec);
                break;
            default:
                break;
        }
        switch (refernecePoint)
        {
            case RefernecePoint.Front:
                dir = dir;
                break;
            case RefernecePoint.Middle:
                break;
            case RefernecePoint.Back:
                dir = -dir;
                break;
            default:
                break;
        }

    }
    private void Update()
    {

        Temporary();
    }
    void Temporary()
    {
        if (isLerpMove)
        {
            transform.position = Vector3.Lerp(transform.position, temporary.position, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, temporary.rotation, lerpSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = temporary.position;
            transform.rotation = temporary.rotation;
        }
        
    }
    void Main()
    {
        Vector3 dir = new Vector3();
        Vector3 refernece = new Vector3();
        switch (direction)
        {
            case Direction.x:
                dir = new Vector3(interval, 0, 0);
                break;
            case Direction.y:
                dir = new Vector3(0, interval, 0);
                break;
            case Direction.z:
                dir = new Vector3(0, 0, interval);
                break;
            default:
                break;
        }

        switch (refernecePoint)
        {
            case RefernecePoint.Front:
                refernece = linkObj.bounds.extents + dir;
                break;
            case RefernecePoint.Middle:
                refernece = dir;
                break;
            case RefernecePoint.Back:
                refernece = -linkObj.bounds.extents - dir;
                break;
            default:
                break;
        }
        var center = linkObj.bounds.center;
        refernece = new Vector3(refernece.x, refernece.y, 0);
        if (isLerpMove)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0) + refernece, lerpSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0) + refernece;
        }
    }


}
