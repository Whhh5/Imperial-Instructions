using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UILoopList : MiBaseMonoBeHaviourClass
{
    [SerializeField] RectTransform main;
    [SerializeField] List<RectTransform> list = new List<RectTransform>();

    [SerializeField] Vector3 endQua;
    [SerializeField] uint nowLoopIndex = 0;
    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            var tounch = Input.mouseScrollDelta;

            //Log(tounch.ToString());

            Log(color: Color.black, Input.GetAxis("Mouse ScrollWheel").ToString());
        }

        //main.localRotation = Quaternion.Lerp(main.localRotation, Quaternion.Euler(main.localRotation.eulerAngles.x + 0, main.localRotation.eulerAngles.y + 0, main.localRotation.eulerAngles.z + Input.mouseScrollDelta.y * 5), 100.0f * Time.deltaTime);

        if (Input.mouseScrollDelta.y != 0)
        {
            SetLoopPosition(main.localRotation.eulerAngles + new Vector3(0, 0, 20 * Input.mouseScrollDelta.y));
        }
    }

    private void OnPostRender()
    {
        
    }
    void SetLoopPosition(Vector3 qua)
    {
        //main.localRotation = Quaternion.Lerp(main.localRotation, qua, 100.0f * Time.deltaTime);
        main.DOLocalRotate(qua, 1);
    }
}

