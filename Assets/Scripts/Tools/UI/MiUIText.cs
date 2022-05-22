using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using System.Threading.Tasks;
using TMPro;
using MiManchi.Tools;

public class MiUIText : MiBaseMonoBeHaviourClass
{
    [SerializeField, ReadOnly] TextMeshProUGUI textPro = null;
    protected override async Task InitalizationInteriorParameterAsync()
    {
        await base.InitalizationInteriorParameterAsync();

        textPro = GetComponent<TextMeshProUGUI>();
        if (textPro == null)
        {
            textPro = gameObject.AddComponent<TextMeshProUGUI>();
        }
    }
    public async Task SetRawText(string str)
    {
        textPro.text = str;
    }
    public string GetRawText()
    {
        return textPro.text;
    }
}
