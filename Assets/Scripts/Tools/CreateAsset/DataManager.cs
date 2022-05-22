using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public static MasterData Master => MasterData.Instance;
    public static LocalizationData local => LocalizationData.Instance;
    public static CharacterData character => CharacterData.Instance;
}
