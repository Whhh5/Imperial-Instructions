using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiManchi.MiBaseClass;
using MiManchi.MiInteraction;
using System.Threading.Tasks;
using MiManchi.MiInterface;
using MiManchi.Tools;

namespace MiManchi
{
    public class MiUIDialog : MiBaseMonoBeHaviourClass, IUIDialog
    {
        [Header("Start Game Hide Obj"),
            SerializeField] List<GameObject> hideObjs = new List<GameObject>();
        [Header("Animation"),
            SerializeField,ReadOnly] Animation anima;
        [SerializeField] AnimationClip showClip;
        [SerializeField] AnimationClip hideClip;
        [Header("Buttton"),
            SerializeField] MiUIButton close;

        protected MiUIManager manager => MiUIManager.Instance;
        protected override async Task InitializationAsync()
        {
            await base.InitializationAsync();

            if (close != null)
            {
                close.onClick.SubscribeEventAsync(async () => { await HideAsync(); await MiAsync.MiAsyncManager.Instance.Default(); }).SubscribeGC(1);
            }
            if (GetComponent<Animation>() != null && anima == null)
            {
                anima = GetComponent<Animation>();
            }

            foreach (var item in hideObjs)
            {
                item.SetActive(false);
            }
        }
        public virtual async Task ShowAsync(MiEnum.DialogMode mode = MiEnum.DialogMode.none)
        {
            await OnShowAsync();
            switch (mode)
            {
                case MiEnum.DialogMode.stack:
                    await MiUIManager.Instance.stack.Push(this);
                    break;
                case MiEnum.DialogMode.none:
                    break;
                default:
                    break;
            }
            if (anima != null && showClip != null)
            {
                anima.GetClip(showClip.name).events = null;
                anima.GetClip(showClip.name).AddEvent(new AnimationEvent
                {
                    functionName = "OnShow",
                    time = anima.GetClip(showClip.name).length,
                });
                await PlayClip(anima, showClip);
            }
        }
        public virtual async Task HideAsync(MiEnum.DialogMode mode = MiEnum.DialogMode.stack)
        {
            if (anima != null && hideClip != null)
            {
                anima.GetClip(hideClip.name).events = null;
                anima.GetClip(hideClip.name).AddEvent(new AnimationEvent
                {
                    functionName = "OnHide",
                    time = anima.GetClip(hideClip.name).length,
                });
                await PlayClip(anima, hideClip);
            }
            else
            {
                await OnHide();
            }
        }

        protected async Task OnShowAsync()
        {
            gameObject.SetActive(true);
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            await AsyncDefaule();
        }
        private async Task OnHide()
        {
            gameObject.SetActive(false);
            await AsyncDefaule();
        }

        public async Task PlayClip(Animation anima,AnimationClip clip)
        {
            if (anima != null && clip != null)
            {
                anima.Play(clip.name);
            }
            await Task.Delay(System.TimeSpan.Zero);
        }
    }
}
