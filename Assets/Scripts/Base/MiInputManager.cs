using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using MiManchi.MiBaseClass;

namespace MiManchi
{
    namespace MiInput
    {
        public class MiInputManager : MiSingletonMonoBeHaviour<MiInputManager>
        {
            UnityEvent clickDownTab = new UnityEvent();
            UnityEvent clickdownCtrlTab = new UnityEvent();

            void Start()
            {
                clickDownTab.SubscribeEventAsync(async () => { Debug.Log($"{GetType()} Get Tab Down"); });
            }

            void Update()
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    clickdownCtrlTab.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    clickDownTab.Invoke();
                }
            }

            public async Task AddTabDown(Func<Task> func)
            {
                clickDownTab.SubscribeEventAsync(func);
                await AsyncDefaule();
            }

            public async Task AddCtrlTabDown(Func<Task> func)
            {
                clickdownCtrlTab.SubscribeEventAsync(func);
                await AsyncDefaule();
            }
        }
    }
}

