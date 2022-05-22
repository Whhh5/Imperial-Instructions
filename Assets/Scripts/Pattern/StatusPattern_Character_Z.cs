using MiManchi.MiEnum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using System;
using System.Reflection;

public sealed class StatusPattern_Character_Z : MiBaseClass, IStatusPattern
{
    private CharacterStatus status = CharacterStatus.None;
    public uint GetState()
    {
        return (uint)status;
    }
    public void SetState(uint status)
    {
        CharacterStatus state = (CharacterStatus)status;
        switch (state)
        {
            case CharacterStatus.None:
                break;
            case CharacterStatus.Stand:
                break;
            case CharacterStatus.Walk:
                break;
            case CharacterStatus.Jump:
                break;
            case CharacterStatus.Scrunch:
                break;
            case CharacterStatus.Skill_R:
                break;
            case CharacterStatus.skill_F:
                break;
            default:
                Log(color: Color.black, $"Status None Cross The Border: {status}  {state} ");
                return;
        }
        this.status = state;
        Type type = GetType();
        var method = type.GetMethod($"{state}", BindingFlags.Instance | BindingFlags.NonPublic);
        method.Invoke(this, new object[] { });
        Log(color: Color.black, state.ToString());
    }

    private void None()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  None");
    }
    private void Stand()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  Stand");
    }
    private void Walk()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  Walk");
    }
    private void Jump()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  Jump");
    }
    private void Scrunch()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  Scrunch");
    }
    private void Skill_R()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  Skill_R");
    }
    private void skill_F()
    {
        Log(color: Color.black, $"{GetType().FullName}  {GetType().Name}  skill_F");
    }
}
