using MiManchi.MiEnum;
using MiManchi.MiInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPattern_Enemy_Z : MonoBehaviour, IStatusPattern
{
    private Z_EnemyStatus status = Z_EnemyStatus.None;
    public uint GetState()
    {
        return ((uint)status);
    }
    public void SetState(uint state)
    {
        var t = (Z_EnemyStatus)state;
        switch (t)
        {
            case Z_EnemyStatus.None:
                break;
            case Z_EnemyStatus.Stand:
                break;
            case Z_EnemyStatus.Walk:
                break;
            case Z_EnemyStatus.Jump:
                break;
            case Z_EnemyStatus.Scrunch:
                break;
            case Z_EnemyStatus.Skill_R:
                break;
            case Z_EnemyStatus.skill_F:
                break;
            default:
                break;
        }

        Type type = GetType();

    }

    private void None()
    {

    }
    private void Stand()
    {

    }
    private void Walk()
    {

    }
    private void Jump()
    {

    }
    private void Scrunch()
    {

    }
    private void Skill_R()
    {

    }
    private void skill_F()
    {

    }
}
