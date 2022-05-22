using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using MiManchi.Tools;
using MiManchi.MiInterface;

namespace MiManchi
{

    namespace MiBaseClass
    {
        public abstract partial class MiBaseClass
        {
            //protected MiProperty Property = new MiProperty();
            protected MiPool.MiTPool<GameObject> ObjPool => MiPool.MiPool.Instance.PoolObj;
            protected void Log(Color color = default, params string[] parameter)
            {
                string str = "";
                foreach (var para in parameter)
                {
                    str += para;
                }
                var col = ColorUtility.ToHtmlStringRGBA(color);
                Debug.Log($"{string.Format("<color=#FFFF00FF>{0}</color>", GetType())}:  {string.Format("<color=#{0}>{1}</color>", col, str)}");
            }
            protected void LogError(string str)
            {
                Debug.LogError($"{GetType()} -- {str}");
            }
            protected async Task AsyncDefaule()
            {
                await Task.Delay(TimeSpan.Zero);
            }
        }
        public abstract partial class MiBaseMonoBeHaviourClass : MonoBehaviour 
        {
            //protected MiProperty Property = new MiProperty();
            protected MiPool.MiTPool<GameObject> ObjPool => MiPool.MiPool.Instance.PoolObj;
            protected virtual void Awake()
            {
                InitalizationInteriorParameterAsync().Wait();
                InitalizationInteriorParameter();
            }
            protected virtual void Start()
            {
                //MiAsync.MiAsyncManager.Instance.StartAsync(InitializationAsync);
                InitializationAsync().Wait();
                Initialization();
            }
            #region Async Initialization
            protected virtual async Task InitalizationInteriorParameterAsync()
            {
                await MiAsync.MiAsyncManager.Instance.Default();
            }
            protected virtual async Task InitializationAsync()
            {
                await MiAsync.MiAsyncManager.Instance.Default();
            }
            #endregion
            #region Initialization
            protected virtual void InitalizationInteriorParameter()
            {
                
            }
            protected virtual void Initialization()
            {
            }
            protected virtual void Initialization<T0>(T0 t0)
            {
            }
            protected void Log(Color color = default, params string[] parameter)
            {
                string str = "";
                foreach (var para in parameter)
                {
                    str += para;
                }
                var col = ColorUtility.ToHtmlStringRGBA(color);
                Debug.Log($"{string.Format("<color=#FFFF00FF>{0}</color>", GetType())}:  {string.Format("<color=#{0}>{1}</color>", col, str)}");
            }

            protected void LogError(string str)
            {
                Debug.LogError($"{GetType()} -- {str}");
            }
            protected async Task AsyncDefaule()
            {
                await Task.Delay(TimeSpan.Zero);
            }
            #endregion
        }
        public abstract class MiSingleton<T> : MiBaseClass where T : new()
        {
            public static T Instance = new T();
        }
        public abstract class MiSingletonMonoBeHaviour<T> : MiBaseMonoBeHaviourClass where T : MiSingletonMonoBeHaviour<T>
        {
            public static T Instance = null;
            protected override void Awake()
            {
                base.Awake();
                if(Instance == null) Instance = (T)this;
            }
        }

        public abstract class Dataitembase : ScriptableObject, IDataItemMothodBase
        {
            public abstract List<object> GetData();
        }
    }






    public class MiParamentSystem : MiBaseClass.MiSingletonMonoBeHaviour<MiParamentSystem>
    {
        public static float MiinterFrameSpaceTime = 0;
        private void Update()
        {
            MiinterFrameSpaceTime = Time.deltaTime;
        }
    }
    namespace MiResoures { }
}

