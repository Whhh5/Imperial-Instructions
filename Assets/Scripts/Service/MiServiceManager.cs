using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;
using UnityEditor;

public class MiServiceManager : MiManchi.MiBaseClass.MiSingleton<MiServiceManager>
{
    MiToRstRequest toRstRequest = new MiToRstRequest(new List<ulong> 
    {
        10001,
        10002,
        10003,
        10004,
    });
    public async Task Initialization()
    {
    }
    public async Task RequestAriticle_AddAsync(List<ulong> ids,List<ulong> number)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            ulong count = i < number.Count ? number[i] : 1;
            var value = await ContainsArticle(ids[i]);
            if (value)
            {
                toRstRequest.AddArticle(ids[i], count);
            }
            else
            {
                Log(color: Color.black, $"Error : Article id : - {ids[i]} - , Absence, Please Check");
            }
        }
        await MiDataManager.Instance.character.UpdateArticleAsync();
    }

    public async Task RequestAriticle_RemoveAsync(List<ulong> ids, List<ulong> number)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            ulong count = i < number.Count ? number[i] : 1;
            var value = await ContainsArticle(ids[i]);
            if (value)
            {
                toRstRequest.RemoveArticle(ids[i], count);
            }
            else
            {
                Log(color: Color.black, $"Error : Article id : - {ids[i]} - , Absence, Please Check");
            }
        }
        await MiDataManager.Instance.character.UpdateArticleAsync();
    }

    public async Task<Dictionary<ulong, ulong>> GetAllArticles()
    {
        await AsyncDefaule();
        return toRstRequest.GetAllArticle();
    }

    public async Task<bool> ContainsArticle(ulong id)
    {
        return toRstRequest.ContainsArticle(id);
    }
}
