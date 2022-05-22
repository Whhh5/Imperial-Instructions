using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Collections;
using DG.Tweening;

[ExecuteInEditMode]
public class CubeMove_Tan : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] float _gameTime = 1;
    float gameTime
    {
        get { return _gameTime; }
        set { _gameTime = value; }
    }

    [SerializeField] [Header("segments - height - segmentsWidth")] Vector3 parameter;

    [SerializeField] GameObject useObj;
    [SerializeField] GameObject usePrefab;
 
    float segments => parameter.x;
    float height => parameter.y;
    float segmentsWidth => parameter.z;

    bool isLoop;
    private void Awake()
    {
        gameTime = 0;
        Loop(0);
    }

    void Loop(float gameTime)
    {
        var moveTime = (segments + height + segmentsWidth) / 3;
        float endValue = 1;
        DOTween.To(() => gameTime, nowTime => { if(isLoop) this.gameTime = nowTime; }, endValue, moveTime).OnComplete(() => { Loop(0); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isLoop = !isLoop;
        }
        Launch();
    }

    void Launch()
    {
        //设置曲线
        z = segments * Mathf.PI * gameTime;
        y = Mathf.Tan(z);

        //设置 宽度 高度
        float posX, posY, posZ;
        posX = x;
        posY = y * height;
        posZ = z * segmentsWidth;
        //赋值 位置
        usePrefab.transform.localPosition = new Vector3(posX, posY, posZ);

        //设置  前一个点  后一个点
        minnumPrior = gameTime == 0 ? 0 : 0.01f;
        minnumNext = gameTime == 1 ? 0 : 0.01f;
        //前一个点
        posXT2 = posX;
        posYT2 = Mathf.Tan(segments * Mathf.PI * (gameTime + minnumNext)) * height;
        posZT2 = segments * Mathf.PI * (gameTime + minnumNext) * segmentsWidth;

        //后一个点
        posXT3 = posX;
        posYT3 = Mathf.Tan(segments * Mathf.PI * (gameTime - minnumPrior)) * height;
        posZT3 = segments * Mathf.PI * (gameTime - minnumPrior) * segmentsWidth;
        //点成 求角度
        var dirction = Mathf.Acos(Vector3.Dot(new Vector3(0, 0, 5).normalized, new Vector3(posXT2 - posXT3, posYT2 - posYT3, posZT2 - posZT3).normalized));
        var dir = Vector3.Cross(useObj.transform.TransformDirection(useObj.transform.forward), new Vector3(posXT2 - posXT3, posYT2 - posYT3, posZT2 - posZT3)).x / Mathf.Abs(Vector3.Cross(useObj.transform.TransformDirection(useObj.transform.forward), new Vector3(posXT2 - posXT3, posYT2 - posYT3, posZT2 - posZT3)).x);
        //赋值 角度
        usePrefab.transform.localRotation = Quaternion.Euler(new Vector3(dirction * Mathf.Rad2Deg * dir, 0, 0));


        //划线
        for (float i = 0; i <= 1; i += interval)
        {

            posXT1 = posX;
            posYT1 = Mathf.Tan(segments * Mathf.PI * i) * height;
            posZT1 = segments * Mathf.PI * i * segmentsWidth;

            posXT2 = posX;
            posYT2 = Mathf.Tan(segments * Mathf.PI * (i + interval)) * height;
            posZT2 = segments * Mathf.PI * (i + interval) * segmentsWidth;

            Debug.DrawLine(useObj.transform.TransformPoint(new Vector3(posXT1, posYT1, posZT1)), useObj.transform.TransformPoint(new Vector3(posXT2, posYT2, posZT2)));

            posXT3 = posX;
            posYT3 = Mathf.Tan(segments * Mathf.PI * (i - interval)) * height;
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
