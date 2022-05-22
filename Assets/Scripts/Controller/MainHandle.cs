using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiInteraction;
using MiManchi.MiBaseClass;
using MiManchi;
using MiManchi.Tools;
using MiManchi.MiEnum;
using MiManchi.MiInterface;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Reflection;
using MiManchi.MiBaseStruct;

[Serializable]
public class TemporiaryList<T>
{
    public List<T> list = new List<T>();
}
//[Serializable]
public partial class MainHandle : MiUIDialog
{
    [SerializeField] MiUIButton handleLeftButton;
    [SerializeField] MiUIButton handleRightButton;
    [SerializeField] MiUIButton cancelButton;
    [SerializeField] List<TemporiaryList<MiUIButton>> battleControllerFrame = new List<TemporiaryList<MiUIButton>>();
    [Header("RectTansform"),
        SerializeField] RectTransform handleLeft;
    [SerializeField] RectTransform handleRight;
    [SerializeField] RectTransform handleLeftTarget;
    [SerializeField] RectTransform handleRightTarget;

    [Header("Remote Script"),
        SerializeField] MainHandle_BloodController bloodController;

    [Header("Parameter"),
        SerializeField] Vector4 handleSpeedDistance;
    [SerializeField] Vector4 characterSpeedHeight;

    [Header("ReadOnly"),
        SerializeField, ReadOnly] bool temp;

    Action<uint> del_SlotId_SkillFrameDownClick = (x) => { };
    Action<uint> del_SlotId_SkillFrameUpClick = (x) => { };
    Action<uint> del_SlotId_SkillFrameClick = (x) => { };
    Action<uint> del_SlotId_SkillFrameEnterClick = (x) => { };
    Action<uint> del_SlotId_SkillFrameExitClick = (x) => { };
    Action<uint> del_SlotId_SkillFrameLongClick = (x) => { };

    Dictionary<uint, ulong> dic_SlotId_SkillId = new Dictionary<uint, ulong>()
    {
        { 1001, 0},         // jump
        { 1002, 0},         // handle
        { 1003 ,0},         // 
        { 2001, 100000001}, // attack button
        { 2002, 100000002}, // skill 1

    };
    Dictionary<MiUIButton, uint> dic_Button_SlotId = new Dictionary<MiUIButton, uint>();

    [ReadOnly] private HandleControllerInfo controllerInfo;
    //Temp Parameter
    [SerializeField, ReadOnly] CommonArrowBase weapons = null;
    [SerializeField, ReadOnly] bool isCloseButton = false;
    public new Battle2DManager manager => Battle2DManager.Instance;
    public HandleControllerInfo GetInfo()
    {
        return controllerInfo;
    }
    protected override void InitalizationInteriorParameter()
    {
        base.InitalizationInteriorParameter();
        del_SlotId_SkillFrameDownClick += HandleSkillSlotButtonDownClick;
        del_SlotId_SkillFrameUpClick += HandleSkillSlotButtonUpClick;
        del_SlotId_SkillFrameClick += HandleSkillSlotButtonClick;
        del_SlotId_SkillFrameEnterClick += HandleSkillSlotButtonEnterClick;
        del_SlotId_SkillFrameExitClick += HandleSkillSlotButtonExitClick;
        del_SlotId_SkillFrameLongClick += HandleSkillSlotButtonLongClick;
        for (uint i = 0; i < battleControllerFrame.Count; i++)
        {
            var list = battleControllerFrame[(int)i].list;
            for (uint j = 0; j < list.Count; j++)
            {
                dic_Button_SlotId.Add(list[(int)j], (i + 1) * 1000 + j + 1);
                var slotBtn = list[(int)j];
                var slotId = dic_Button_SlotId[slotBtn];
                slotBtn.AddOnPointerDownClick(async () =>
                {
                    del_SlotId_SkillFrameDownClick.Invoke(slotId);
                });
                slotBtn.AddOnPointerUpClick(async () =>
                {
                    del_SlotId_SkillFrameUpClick.Invoke(slotId);
                });
                slotBtn.AddOnPointerClick(async () =>
                {
                    del_SlotId_SkillFrameClick.Invoke(slotId);
                });
                slotBtn.AddOnPointerEnterClick(async () =>
                {
                    del_SlotId_SkillFrameEnterClick.Invoke(slotId);
                });
                slotBtn.AddOnPointerExitClick(async () =>
                {
                    del_SlotId_SkillFrameExitClick.Invoke(slotId);
                });
                slotBtn.AddOnPointerLongDownClick(async () =>
                {
                    del_SlotId_SkillFrameLongClick.Invoke(slotId);
                });
            }
        }
    }

    #region UnityEngine Key Controller
    private void FixedUpdate()
    {
        manager.chara_Controller.Move_Rigidbody(new Vector3(characterSpeedHeight.x, 0, 0));
        if (Input.GetKeyDown(KeyCode.Space) && manager.controllerInfo.isGround)
        {
            manager.chara_Rigi.AddForce(new Vector3(0, characterSpeedHeight.y, 0), mode: ForceMode2D.Impulse);
        }
        var x = Input.GetAxis("Horizontal");
        HandleLeftMove(x);
    }
    void HandleLeftMove(float percentage)
    {
        Vector3 endAnchored;
        if ((handleLeftButton.GetButtonStatus() & ButtonStstus.Down) != 0)
        {
            var direction = Input.mousePosition - handleLeft.position;
            var cross = Vector3.Cross(new Vector3(0, 1, 0), direction).normalized.z;
            var distance = Mathf.Abs(Input.mousePosition.x - handleLeft.position.x);
            var anchoredX = distance > (handleSpeedDistance.y * 0.5f) 
                ? handleSpeedDistance.y * 0.5f * cross * -1 
                : distance * cross * -1;
            endAnchored = new Vector3(anchoredX, 0, 0);
        }
        else
        {
            endAnchored = new Vector3(percentage * handleSpeedDistance.y * 0.5f, 0, 0);
        }
        handleLeftTarget.anchoredPosition3D =
            Vector3.Lerp(handleLeftTarget.anchoredPosition3D, endAnchored, handleSpeedDistance.x * Time.deltaTime);

        controllerInfo.moveController = handleLeftTarget.anchoredPosition3D.x / handleSpeedDistance.y / 2;
    }
    void HandleRightMove()
    {
        float angle;
        Vector3 anchored;
        var mousePos = Input.mousePosition;
        var vec = mousePos - handleRight.position;
        var dot = Mathf.Acos(Vector3.Dot(new Vector3(0, 1, 0), vec.normalized)) * Mathf.Rad2Deg;
        var cross = Vector3.Cross(new Vector3(0, 1, 0), vec.normalized).normalized.z;
        angle = dot * cross + 90.0f;
        var distance = Mathf.Pow( Mathf.Pow(vec.x,2) + Mathf.Pow(vec.y,2),0.5f);
        distance = distance > handleSpeedDistance.w ? handleSpeedDistance.w : distance;
        anchored = (handleRightButton.GetButtonStatus() & ButtonStstus.Down) != 0 ? GetCirclePosition2D(angle,(ulong)distance) : new Vector3(0, 0, 0);

        handleRightTarget.anchoredPosition3D = 
            Vector3.Lerp(handleRightTarget.anchoredPosition3D, anchored, handleSpeedDistance.z * Time.deltaTime);

        controllerInfo.handleAngle = angle;
        controllerInfo.angleRadius = distance;
    }
    #endregion

    #region button click add click

    void HandleSkillSlotButtonDownClick(uint slotId)
    {
        CommonAddCilck(state: ButtonStstus.Down, slotId);
    }
    void HandleSkillSlotButtonUpClick(uint slotId)
    {
        CommonAddCilck(state: ButtonStstus.Up, slotId);
    }
    void HandleSkillSlotButtonClick(uint slotId)
    {
        CommonAddCilck(state: ButtonStstus.Click, slotId);
    }
    void HandleSkillSlotButtonLongClick(uint slotId)
    {
        CommonAddCilck(state: ButtonStstus.Long, slotId);
    }
    void HandleSkillSlotButtonExitClick(uint slotId)
    {
        CommonAddCilck(state: ButtonStstus.Exit, slotId);
    }
    void HandleSkillSlotButtonEnterClick(uint slotId)
    {
        CommonAddCilck(state: ButtonStstus.Enter, slotId);
    }
    void CommonAddCilck(ButtonStstus state, uint slotId)
    {
        var methodName = string.Format("SkillSlot{0}Click_{1}", state, slotId);
        var method = GetType().GetMethod($"{methodName}", BindingFlags.Instance | BindingFlags.NonPublic);
        if (method != null)
        {
            method.Invoke(this, new object[] { slotId });
        }
        else
        {
            //Log(Color.red, $"Absent Method :  {methodName}");
        }
    }
    #endregion

    //commmon mathf
    Vector3 GetCirclePosition2D(float angle, ulong distance)
    {
        var y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        var x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, 0);
    }
}

public partial class MainHandle
{
    void SkillSlotDownClick_(uint slotId)
    {

    }
    void SkillSlotUpClick_(uint slotId)
    {

    }
    void SkillSlotClickClick_(uint slotId)
    {

    }
    void SkillSlotEnterClick_(uint slotId)
    {

    }
    void SkillSlotExitClick_(uint slotId)
    {

    }
    void SkillSlotLongClick_(uint slotId)
    {

    }
    // 1000 + ==============================  Common Controller  =================================================
    void SkillSlotDownClick_1001(uint slotId)
    {
        //DOTween.To(() => 10, value => { Log(Color.green, $"{value}"); }, 0, 10);
    }

    void SkillSlotUpClick_1001(uint slotId)
    {
        handleLeftTarget.DOAnchorPos3DX(0, 0.2f)
            .OnUpdate(() =>
            {
                controllerInfo.moveController =
                handleLeftTarget.anchoredPosition3D.x / handleSpeedDistance.y / 2;
            });
    }
    void SkillSlotLongClick_1001(uint slotId)
    {
        Vector3 endAnchored;
        float percentage = Input.GetAxis("Horizontal");
        if ((handleLeftButton.GetButtonStatus() & ButtonStstus.Down) != 0)
        {
            var direction = Input.mousePosition - handleLeft.position;
            var cross = Vector3.Cross(new Vector3(0, 1, 0), direction).normalized.z;
            var distance = Mathf.Abs(Input.mousePosition.x - handleLeft.position.x);
            var anchoredX = distance > (handleSpeedDistance.y * 0.5f)
                ? handleSpeedDistance.y * 0.5f * cross * -1
                : distance * cross * -1;
            endAnchored = new Vector3(anchoredX, 0, 0);
        }
        else
        {
            endAnchored = new Vector3(percentage * handleSpeedDistance.y * 0.5f, 0, 0);
        }
        handleLeftTarget.anchoredPosition3D =
            Vector3.Lerp(handleLeftTarget.anchoredPosition3D, endAnchored, handleSpeedDistance.x * Time.deltaTime);

        controllerInfo.moveController = handleLeftTarget.anchoredPosition3D.x / handleSpeedDistance.y / 2;
    }
    //
    void SkillSlotClickClick_1002(uint slotId)
    {
        if (manager.controllerInfo.isGround)
        {
            manager.chara_Rigi.AddForce(new Vector3(0, characterSpeedHeight.y, 0), mode: ForceMode2D.Impulse);
        }
    }
    //
    void SkillSlotEnterClick_1003(uint slotId)
    {
        isCloseButton = true;
    }
    void SkillSlotExitClick_1003(uint slotId)
    {
        isCloseButton = false;
    }
    // 2000 + ==============================  Skills  =================================================
    void SkillSlotDownClick_2001(uint slotId)
    {
        weapons = SkillManager.Instance.character.GetSkills(
            dic_SlotId_SkillId[slotId], manager.chara_Rigi.transform.position, MethodMode.Manual, MethodMode.System);
        if (weapons != default)
        {
            manager.chara_Rigi.drag = float.MaxValue;
        }
    }
    void SkillSlotUpClick_2001(uint slotId)
    {
        manager.chara_Rigi.drag = 1;
        handleRightTarget.DOAnchorPos3D(new Vector3(0, 0, 0), 0.2f)
            .OnComplete(() =>
            {
                HandleRightMove();
            });
        if (isCloseButton && weapons != null)
        {
            weapons.Destroy();
            weapons = null;
            return;
        }
        weapons.Active(null);
        weapons = null;
        manager.chara_Rigi.WakeUp();
    }
    void SkillSlotLongClick_2001(uint slotId)
    {
        HandleRightMove();
    }
    //
    void SkillSlotDownClick_2002(uint slotId)
    {
        var startPos = manager.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        weapons = SkillManager.Instance.character.GetSkills(
            dic_SlotId_SkillId[slotId], new Vector3(startPos.x, startPos.y, 0), MethodMode.Manual);
    }
    void SkillSlotUpClick_2002(uint slotId)
    {
        if (isCloseButton && weapons != null)
        {
            weapons.Destroy();
            manager.chara_Rigi.drag = 1;
            weapons = null;
            return;
        }
        weapons.Active(2.0f);
    }
    void SkillSlotClickClick_2003(uint slotId)
    {
        var skill = SkillManager.Instance.character.GetSkills(100000003, Battle2DManager.Instance.chara_Transform.position);
        skill.Active((uint)20, 5.0f, Battle2DManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }
}
