using MiManchi.MiAsync;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiManchi
{
    namespace MiInteraction
    {
        public class MiButtonObject : MiBaseClass.MiBaseMonoBeHaviourClass
        {
            [SerializeField] Color enterColor;
            [SerializeField] Color downColor;
            [SerializeField] Color dragColor;
            [HideInInspector] public UnityEvent onClickEnter = new UnityEvent();
            [HideInInspector] public UnityEvent onClickOver = new UnityEvent();
            [HideInInspector] public UnityEvent onClickExit = new UnityEvent();
            [HideInInspector] public UnityEvent onClickDown = new UnityEvent();
            [HideInInspector] public UnityEvent onClickDrag = new UnityEvent();
            [HideInInspector] public UnityEvent onClickUp = new UnityEvent();



            bool isExecute = true;
            Color perproColor = new Color();
            Material buttonColor => GetComponent<MeshRenderer>().materials[0];
            protected virtual void OnMouseEnter()
            {
                if (!isExecute) return;
                AddOnMouseEnterClick();
            }
            protected virtual void OnMouseOver()
            {
                if (!isExecute) return;
                AddOnMouseOverClick();
            }
            protected virtual void OnMouseExit()
            {
                if (!isExecute) return;
                AddOnMouseExitClick();
            }


            protected virtual void OnMouseDown()
            {
                if (!isExecute) return;
                AddOnMouseDownClick();
            }
            protected virtual void OnMouseDrag()
            {
                if (!isExecute) return;
                AddOnMouseDragClick();
            }
            protected virtual void OnMouseUp()
            {
                if (!isExecute) return;
                AddOnMouseUpClick();
            }

            public virtual void AddOnMouseEnterClick()
            {
                onClickEnter.SubscribeEventAsync(async () => { await MiAsyncManager.Instance.Default(); Debug.Log($"{this.gameObject.name}  OnPointerClock"); }).SubscribeGC(0);
                perproColor = buttonColor.color;
                buttonColor.color = enterColor;
            }
            public virtual void AddOnMouseOverClick()
            {
                onClickOver.SubscribeEventAsync(async () => { await MiAsyncManager.Instance.Default(); Debug.Log($"{this.gameObject.name}  OnPointerClockDown"); }).SubscribeGC(1);
            }
            public virtual void AddOnMouseExitClick()
            {
                onClickExit.SubscribeEventAsync(async () => { await MiAsyncManager.Instance.Default(); Debug.Log($"{this.gameObject.name}  OnPointerClockUp"); }).SubscribeGC(2);
                buttonColor.color = perproColor;
            }
            public virtual void AddOnMouseDownClick()
            {
                onClickDown.SubscribeEventAsync(async () => { await MiAsyncManager.Instance.Default(); Debug.Log($"{this.gameObject.name}  OnPointerClockEnter"); }).SubscribeGC(3);
                buttonColor.color = downColor;
            }
            public virtual void AddOnMouseDragClick()
            {
                onClickDrag.SubscribeEventAsync(async () => { await MiAsyncManager.Instance.Default(); Debug.Log($"{this.gameObject.name}  OnPointerClockExit"); }).SubscribeGC(4);
                buttonColor.color = dragColor;
            }
            public virtual void AddOnMouseUpClick()
            {
                onClickUp.SubscribeEventAsync(async () => { await MiAsyncManager.Instance.Default(); Debug.Log($"{this.gameObject.name}  OnPointerClockExit"); }).SubscribeGC(4);
                buttonColor.color = perproColor;
            }
        }
    }
}