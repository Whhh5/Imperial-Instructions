using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Service Character Data",menuName = "ServiceData/New Service Character Data")]
public class MiServiceCharacterData : ScriptableObject
{
    public List<ulong> ids;
    public List<ulong> counts;
}
