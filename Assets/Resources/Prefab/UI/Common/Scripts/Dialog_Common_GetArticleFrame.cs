using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi;
using System.Threading.Tasks;

public class Dialog_Common_GetArticleFrame : MiUIDialog
{
    [SerializeField] GameObject articlePrefab;
    [SerializeField] HorVerLayoutGroup layout;
    public async Task SetUpShowAsync(List<ulong> ids, List<ulong> counts = null)
    {
        await ShowAsync();
        await layout.Destroy();
        await layout.SetUpShowAsync(articlePrefab);
        if (counts == null)
        {
            counts = new List<ulong>();
        }
        for (int i = 0; i < ids.Count; i++)
        {
            var count = i < counts.Count ? counts[i] : 1;
            var obj = await ObjPool.GetObjectAsync(articlePrefab);
            var rect = obj.GetComponent<RectTransform>();
            var cs = obj.GetComponent<UICommon_ArticleFrame>();
            await cs.SetUp(articlePrefab, ids[i], count);
            await layout.AddAsync(rect);
        }
        await MiServiceManager.Instance.RequestAriticle_AddAsync(ids, counts);
    }
}
