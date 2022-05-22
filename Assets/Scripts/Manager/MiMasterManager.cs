using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiInterface;
using UnityEditor;
using MiManchi;
using MiManchi.Data;

public class MiMasterManager : MiBaseClass
{
    public Dictionary<ulong, MiArticle> Article = new Dictionary<ulong, MiArticle>();

    public Dictionary<ulong, CharacterData> LocalizeCharacterDataItem = new Dictionary<ulong, CharacterData>();// IconDataItem,SkillsDataItem
    public Dictionary<ulong, IconData> LocalizeIconDataItem = new Dictionary<ulong, IconData>();
    public Dictionary<ulong, SkillsData> LocalizeSkillsDataItem = new Dictionary<ulong, SkillsData>();
    public MiMasterManager()
    {
        LocalizeCharacterDataItem = LocalizeCharacterDataItem.LoadData<CharacterData, CharacterDataItem>($"LocalizeCharacterData");
        LocalizeIconDataItem = LocalizeIconDataItem.LoadData<IconData, IconDataItem>($"LocalizeIconData");
        LocalizeSkillsDataItem = LocalizeSkillsDataItem.LoadData<SkillsData, SkillsDataItem>($"LocalizeSkillsData");

        Article = new Dictionary<ulong, MiArticle> {
            { 10001,new MiArticle(10001,"Water",1000)},
            { 10002,new MiArticle(10002,"Run",2000)},
            { 10003,new MiArticle(10003,"Jump",3000)},
        };
    }
    public void Initalization()
    {

    }
}
