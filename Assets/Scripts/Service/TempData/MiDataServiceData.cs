using MiManchi.MiBaseStruct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataService", menuName = "ServiceData/New DataService")]
public class MiDataServiceData : ScriptableObject
{
    public Attribute attribute;
    public Knapsack knapsack;
}
