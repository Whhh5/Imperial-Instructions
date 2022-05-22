using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseStruct;
using System.Threading.Tasks;
using MiManchi.MiBaseClass;

public class MiCharacterManager : MiBaseClass
{
    public Attribute attribute = new Attribute();
    public Knapsack knapsack = new Knapsack();

    Dictionary<ulong, ulong> articles = new Dictionary<ulong, ulong>();
    public MiCharacterManager()
    {
        
    }

    private void AddAsync(ulong id,ulong count = 1)
    {
        if (articles.ContainsKey(id))
        {
            articles[id] += count;
        }
        else
        {
            articles.Add(id, count);
        }
        Log(color: Color.black, $"Add Acticle {id}, Number {count}, Now Count{articles[id]}");
        GetAllArticle().Wait();
    }
    private void RemoveAsync(ulong id, ulong count = 1)
    {
        if (articles.ContainsKey(id))
        {
            if (articles[id] <= count)
            {
                articles.Remove(id);
            }
            else
            {
                articles[id] -= count;
            }
        }
        else
        {
            Log(color: Color.black, $"Please Reference Dictionary : Article None Id : {id}");
        }
        GetAllArticle().Wait();
    }
    public async Task UpdateArticleAsync()
    {
        articles = await MiServiceManager.Instance.GetAllArticles();
    }
    public async Task<List<ulong>> GetAllArticle()
    {
        string str = "";
        List<ulong> ids = new List<ulong>();
        foreach (var parameter in articles)
        {
            ids.Add(parameter.Key);
            str += $"{parameter.Key} : {parameter.Value}  -  ";
        }
        Log(color: Color.black, $"Get Acticle Count :{ids.Count} \n {str}");
        return ids;
    }
    public async Task<bool> ContainsAsync(ulong id)
    {
        var isHave = articles.ContainsKey(id);
        return isHave;
    }


}
