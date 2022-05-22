using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using MiManchi.MiBaseClass;

public class MiToRstRequest : MiBaseClass
{
    List<ulong> allArticle = new List<ulong>();
    MiServiceCharacterData miServiceCharacterData;
    public MiToRstRequest()
    {
        Initalization();
    }

    public MiToRstRequest(List<ulong> allArticle)
    {
        this.allArticle = allArticle;
        Initalization();
    }
    void Initalization()
    {
        miServiceCharacterData = AssetDatabase.LoadAssetAtPath<MiServiceCharacterData>("Assets/Scripts/Service/Data/Service Character Data.asset");
        while (miServiceCharacterData.ids.Count > miServiceCharacterData.counts.Count)
        {
            miServiceCharacterData.counts.Add(1);
        }
        while (miServiceCharacterData.ids.Count < miServiceCharacterData.counts.Count)
        {
            miServiceCharacterData.counts.RemoveAt(miServiceCharacterData.counts.Count - 1);
        }
        for (int i = 0; i < miServiceCharacterData.ids.Count; i++)
        {
            var contain = ContainsArticle(miServiceCharacterData.ids[i]);
            if (!contain)
            {
                miServiceCharacterData.ids.RemoveAt(i);
                miServiceCharacterData.counts.RemoveAt(i);
            }
        }
    }

    public void AddArticle(ulong id, ulong count)
    {
        if (miServiceCharacterData.ids.Contains(id))
        {
            var index = miServiceCharacterData.ids.IndexOf(id);
            miServiceCharacterData.counts[index] += count;
        }
        else
        {
            miServiceCharacterData.ids.Add(id);
            miServiceCharacterData.counts.Add(count);
        }
    }
    public void RemoveArticle(ulong id, ulong count)
    {
        Dictionary<ulong, ulong> dic = new Dictionary<ulong, ulong>();
        if (miServiceCharacterData.ids.Contains(id))
        {
            var index = miServiceCharacterData.ids.IndexOf(id);
            if (miServiceCharacterData.counts[index] <= count)
            {
                miServiceCharacterData.ids.RemoveAt(index);
                miServiceCharacterData.counts.RemoveAt(index);
            }
            else
            {
                miServiceCharacterData.counts[index] -= count;
            }
        }
    }
    public void RemoveAllArticle(ulong id, ulong count)
    {
        miServiceCharacterData.ids.Clear();
        miServiceCharacterData.counts.Clear();
    }
    public Dictionary<ulong, ulong> GetAllArticle()
    {
        string str = "\n";
        Dictionary<ulong, ulong> dic = new Dictionary<ulong, ulong>();
        for (int i = 0; i < miServiceCharacterData.ids.Count; i++)
        {
            dic.Add(miServiceCharacterData.ids[i], miServiceCharacterData.counts[i]);
            str += string.Format("{0}<color=#FFFFFF> : {1} -- </color>", miServiceCharacterData.ids[i], miServiceCharacterData.counts[i]);
        }
        Log(Color.cyan, str);
        return dic;
    }

    public bool ContainsArticle(ulong id)
    {
        var article = allArticle.Contains(id);
        return article;
    }
}
