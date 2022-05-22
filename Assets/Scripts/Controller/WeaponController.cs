using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] Transform playerTr;
    [SerializeField] RectTransform rect;
    [SerializeField] GameObject buttle;
    [SerializeField] Transform buttlePoint;
    [SerializeField] float speed;
    [SerializeField] AnimationClip animaClip;

    Transform gunTr => gun.GetComponent<Transform>();
    void Update()
    {
        GunAngle();

        if (Input.GetMouseButtonDown(0))
        {
            CommonManager.Instance.prefab.GetButtle(buttle,buttlePoint, buttle);
        }
    }

    void GunAngle()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(gunTr.position);
        //Debug.Log($"{worldPos}  {Screen.width}  {Screen.height}");
        rect.anchoredPosition3D = new Vector3( -worldPos.z, worldPos.y, worldPos.x);
        Vector3 v2 = (Input.mousePosition - worldPos - new Vector3(Camera.main.pixelRect.width, Camera.main.pixelRect.height, 0) / 2).normalized;
        var angle = Mathf.Acos(Vector3.Dot(new Vector3(1,0,0), v2)) * Mathf.Rad2Deg;
        var around = Vector3.Cross(new Vector3(1, 0, 0), v2).z > 0 ? -1 : 1;
        gunTr.rotation = Quaternion.Euler(new Vector3(angle * around + 90.0f, 0, 0));
        rect.rotation = Quaternion.Euler(new Vector3(0, 0, - angle * around));
    }
}