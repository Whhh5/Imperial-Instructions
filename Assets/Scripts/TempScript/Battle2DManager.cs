using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiBaseStruct;
using System;
using MiManchi.Tools;
using System.Threading.Tasks;

public class Battle2DManager : MiSingletonMonoBeHaviour<Battle2DManager>
{
    public Camera mainCamera;
    public GameObject character;
    public GameObject mainHandle;

    public CharacterControllerInfo controllerInfo => chara_Controller.GetCharacterInfo();

    public CameraController camera_Controller => mainCamera.GetComponent<CameraController>();
    public Rigidbody2D chara_Rigi => character.GetComponent<Rigidbody2D>();
    public Transform chara_Transform => character.GetComponent<Transform>();
    public CharacterController_Rigidbody_2DBox chara_Controller => character.GetComponent<CharacterController_Rigidbody_2DBox>();
    public MainHandle mainHandle_Controller => mainHandle.GetComponent<MainHandle>();
    public HandleControllerInfo mainHandle_Info => mainHandle_Controller.GetInfo();


    [SerializeField] private Action<float> del_SetBloodClick =  (x) => { };
    [SerializeField] private Action del_Finish = () => { };

    //Temp Parameter

    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        AddDelegate_Finish(() => { Log(Color.red, $"Defeated"); });
        chara_Controller.AddBSetBloodClick((value) =>
        {
            Log(Color.black, $"{value.presentBlood}");
            if (value.presentBlood <= 0)
            {
                del_Finish.Invoke();
            }
        });
    }
    protected override void Initialization()
    {
        base.Initialization();
        del_SetBloodClick.Invoke(controllerInfo.bloodProportion);
    }
    public void SetCharacterBlood(float value)
    {
        chara_Controller.SetBlood(value);
        Log(Color.red, controllerInfo.bloodProportion.ToString());
        del_SetBloodClick.Invoke(controllerInfo.bloodProportion);

    }
    public void AddDelegate_SetBloodClick(Action<float> action)
    {
        del_SetBloodClick += action;
    }
    public void AddDelegate_Finish(Action action)
    {
        del_Finish += action;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CommonManager.Instance.prefab.Get_Eff_SelectTarget_1(chara_Transform, 5.0f);
        }
    }
}
