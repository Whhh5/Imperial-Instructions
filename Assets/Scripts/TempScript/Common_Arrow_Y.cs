using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiInterface;
using DG.Tweening;
using System.Threading.Tasks;
using MiManchi.MiEnum;
using MiManchi;

public class Common_Arrow_Y : CommonArrowBase
{
    [SerializeField, Range(0, 10)] float moveSpeed; 
    [SerializeField] Vector3 launch_Count_duration_Speed;
    [SerializeField] Vector2 rangeX;
    [SerializeField] Vector2 rangeY;
    [SerializeField] Vector2 rangeAngle;
    [SerializeField, Range(-180, 180)] float amendmentAngle = -90;
    [SerializeField] MethodMode methodMode = MethodMode.Manual;
    [SerializeField] bool isPrepare = true;
    public override void Active(params object[] delay)
    {
        isPrepare = false;
        CommonManager.Instance.prefab.Get_Eff_Common_Cloud_1(main.transform.position, (float)delay[0] + launch_Count_duration_Speed.y);
        DOTween.To(() => 2, value => { }, 0, (float)delay[0])
            .OnComplete(() =>
            {
                StartCoroutine(ELoop());
            });
        //DOTween.To(() => 2, value => { }, 0, launch_Count_duration_Speed.y + (float)delay)
        //    .OnComplete(() =>
        //    {
        //        Destroy();
        //    });
    }

    private void Update()
    {
        if (isPrepare && (methodMode & MethodMode.Manual) != 0)
        {
            var pos = Battle2DManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //Log(Color.green, $"{pos}");
            var worldPoint = new Vector3(pos.x, pos.y, 0);
            main.transform.position = Vector3.Lerp(main.transform.position, worldPoint, moveSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ELoop());
        }
    }
    public override void Prepare()
    {
        isPrepare = true;
        this.methodMode = MethodMode.Manual;
    }

    public override void SetParameter(params object[] methodMode)
    {
        if (methodMode.Length >= 1)
        {
            this.methodMode = (MethodMode)methodMode[0];
        }
    }
    IEnumerator ELoop()
    {
        int count = 0;

        while (count < launch_Count_duration_Speed.x)
        {
            CommonArrowBase cs;
            float intervalAngle = Random.Range(rangeAngle.x, rangeAngle.y);
            cs = SkillManager.Instance.character.GetSkills(100000001, main.transform.position);
            cs.SetParameter(MethodMode.System, MethodMode.System);
            var x = Random.Range(rangeX.x, rangeX.y);
            var y = Random.Range(rangeY.x, rangeY.y);
            cs.transform.position += new Vector3(x, y, 0);
            cs.gameObject.transform.rotation = Quaternion.Euler(0, 0, amendmentAngle + intervalAngle);
            DOTween.To(() => 2, value => { }, 0, 2)
                .OnComplete(() =>
                {
                    cs.Active(null);
                });
            count++;
            yield return new WaitForSeconds(launch_Count_duration_Speed.y / launch_Count_duration_Speed.x);
        }
        Destroy();
    }
    public override void Destroy()
    {
        base.Destroy();
        main.transform.Normalization(transform);
    }
}
