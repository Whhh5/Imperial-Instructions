using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Collections;
using DG.Tweening;
using MiManchi.MiBaseClass;
using MiManchi.MiInterface;

[ExecuteInEditMode]
public class CubeMove_Cos : MiBaseMonoBeHaviourClass
{
    [SerializeField] [Range(0, 1)] float _gameTime = 1;
    [SerializeField] bool auto = true;
    [SerializeField] bool endIsDes = true;
    float gameTime
    {
        get { return _gameTime; }
        set { _gameTime = value; }
    }

    [SerializeField] [Header("segments - height - segmentsWidth")] Vector3 parameter = new Vector3(10, 1, 1);

    [SerializeField] GameObject character;
    [SerializeField] GameObject product;

    [SerializeField, Range(-180, 180)] int angleAmend = 180;

    [SerializeField] float _moveTime;
    float moveTime
    {
        get { return (segments + height + segmentsWidth) / 3 + _moveTime; }
        set { _moveTime = value; }
    }

    float segments => parameter.x;
    float height => parameter.y;
    float segmentsWidth => parameter.z;

    bool isLoop;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Initialization<T0>(T0 t0)
    {
        base.Initialization(t0);
        product = t0 as GameObject;
        character = gameObject;
        if (product != null) Loop();
    }
    public void SetUp(GameObject main)
    {
        Initialization<GameObject>(main);
    }

    void Loop()
    {
        DOTween.To(() => 0.0f, nowTime => { gameTime = nowTime; }, 1.0f, moveTime).SetEase(ease: Ease.Linear)
            .OnUpdate(() =>
            {
                Launch();
            })
            .OnComplete(() => {
                if (endIsDes)
                    this.GetComponent<IWeapon>().Put();
                else
                    Loop();
            });
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Loop();
        }
        Launch();
    }

    void Launch()
    {
        if (product == null)
        {
            Debug.Log($"{GetType()}  please check it  product:{product}");
            return;
        }
        //设置曲线
        z = segments * Mathf.PI * gameTime;
        y = Mathf.Cos(z);

        //设置 宽度 高度
        float posX, posY, posZ;
        posX = x;
        posY = y * height;
        posZ = z * segmentsWidth;
        //赋值 位置
        product.transform.localPosition = new Vector3(posX, posY, posZ);

        //设置  开始点  结束点
        minnumPrior = gameTime == 0 ? 0 : 0.01f;
        minnumNext = gameTime == 1 ? 0 : 0.01f;
        //前一个点
        posXT2 = posX;
        posYT2 = Mathf.Cos(segments * Mathf.PI * (gameTime + minnumNext)) * height;
        posZT2 = segments * Mathf.PI * (gameTime + minnumNext) * segmentsWidth;

        //后一个点
        posXT3 = posX;
        posYT3 = Mathf.Cos(segments * Mathf.PI * (gameTime - minnumPrior)) * height;
        posZT3 = segments * Mathf.PI * (gameTime - minnumPrior) * segmentsWidth;
        //点成 求角度
        Debug.DrawLine(character.transform.TransformVector(new Vector3(posXT2, posYT2, posZT2)), character.transform.TransformVector(new Vector3(posXT3, posYT3, posZT3)), color: Color.black);

        Vector3 v32 = character.transform.TransformVector(new Vector3(posXT2, posYT2, posZT2));
        Vector3 v33 = character.transform.TransformVector(new Vector3(posXT3, posYT3, posZT3));
        Vector3 direction = (v33 - v32).normalized;

        var dirction = Mathf.Acos(Vector3.Dot(character.transform.TransformDirection(new Vector3(0, 0, 1)).normalized, direction)) * Mathf.Rad2Deg;
        var dir = Vector3.Cross(character.transform.TransformDirection(new Vector3(0, 0, 1)),
            direction).x / Mathf.Abs(Vector3.Cross(character.transform.TransformDirection(new Vector3(0, 0, 1)), direction).x);
        //赋值 角度
        product.transform.localRotation = Quaternion.Euler(new Vector3(dirction * dir + angleAmend, 0, 0));


        //划线
        for (float i = 0; i <= 1; i += interval)
        {

            posXT1 = posX;
            posYT1 = Mathf.Cos(segments * Mathf.PI * i) * height;
            posZT1 = segments * Mathf.PI * i * segmentsWidth;

            posXT2 = posX;
            posYT2 = Mathf.Cos(segments * Mathf.PI * (i + interval)) * height;
            posZT2 = segments * Mathf.PI * (i + interval) * segmentsWidth;

            Debug.DrawLine(character.transform.TransformPoint(new Vector3(posXT1, posYT1, posZT1)), character.transform.TransformPoint(new Vector3(posXT2, posYT2, posZT2)));

            posXT3 = posX;
            posYT3 = Mathf.Cos(segments * Mathf.PI * (i - interval)) * height;
            posZT3 = segments * Mathf.PI * (i - interval) * segmentsWidth;
        }
    }

#region 临时变量
    float x = 0, y = 0, z = 0;

    float posXT1, posYT1, posZT1, posXT2, posYT2, posZT2, posXT3, posYT3, posZT3;

    float minnumPrior, minnumNext;

    float interval = 0.01f;
#endregion
}
