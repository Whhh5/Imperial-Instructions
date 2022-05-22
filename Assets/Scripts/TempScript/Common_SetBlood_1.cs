using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MiManchi.MiBaseClass;
using MiManchi;
using MiManchi.MiResoures;

public class Common_SetBlood_1 : MiDialog_World
{
    [SerializeField] Common_Horizontal_Word horizontalList;
    [SerializeField] GameObject textPrefab;
    public void SetUp(string value, Color color)
    {
        gameObject.SetActive(true);
        GameObject obj;
        SpriteRenderer sprite;
        horizontalList.Clear();
        horizontalList.Setting(textPrefab, interval_MoveTime: new Vector2(0.3f, 0.2f));
        for (int i = 0; i < value.Length; i++)
        {
            obj = ObjPool.GetObject(textPrefab);
            sprite = obj.GetComponent<SpriteRenderer>();
            sprite.sprite = MiResourcesManager.Instance.Load<Sprite>(CommonManager.Instance.filePath.ResArt, $"Sprites/DamageNumber/Battle_text_normal_{value[i]}");
            horizontalList.AddElement(obj.transform);
            sprite.color = color;
            obj.SetActive(true);
        }
        AnimationClip clip = anima.GetClip($"{original.name}_Show");
        if (clip != null)
        {
            anima.Play(clip.name);
            DOTween.To(() => 2, value => { }, 0, clip.length).OnComplete(() =>
            {
                Destroy();
            });
        }

    }
}
