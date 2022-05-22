using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MiManchi
{
    namespace MiPool
    {
        public class MiPool : MiBaseClass.MiSingleton<MiPool>
        {
            public MiTPool<GameObject> PoolObj = new MiTPool<GameObject>();
        }
        public class MiTPool<T> : MiBaseClass.MiBaseClass where T : UnityEngine.Object
        {
            private Dictionary<T, List<T>> pool = new Dictionary<T, List<T>>();
            public MiTPool()
            {
                //Log(Color.green, pool.Count.ToString());
            }
            public async Task<T> GetObjectAsync(T par)
            {
                T o = null;
                if (pool.ContainsKey(par) && pool[par].Count != 0)
                {
                    o = pool[par][0];
                    pool[par].Remove(o);
                }
                else
                {
                    o = await MiFactory.MiFactory.Instance.InstantiateAsync(par) as T;
                }
                if (o == null)
                {
                    o = await MiFactory.MiFactory.Instance.InstantiateAsync(par) as T;
                }
                await Task.Delay(TimeSpan.FromSeconds(0));
                Log(Color.green, pool.Count.ToString());
                return o;
            }
            public async Task Repulace(T par,T obj)
            {
                if (obj == null || par == null) return;
                if (pool.ContainsKey(par))
                {
                    pool[par].Add(obj);
                }
                else
                {
                    pool.Add(par, new List<T>(new T[] { obj}));
                }
                await Task.Delay(TimeSpan.Zero);
            }
            public T GetObject(T obj)
            {
                T o = null;
                if (pool.ContainsKey(obj) && pool[obj].Count != 0)
                {
                    o = pool[obj][0];
                    pool[obj].Remove(o);
                }
                else
                {
                    o = MiFactory.MiFactory.Instance.Instantiate(obj) as T;
                }
                if (o == null)
                {
                    o = MiFactory.MiFactory.Instance.Instantiate(obj) as T;
                }
                
                if (o.GetType().Name == "GameObject")
                {
                    var e = o as GameObject;
                    var cs = e.GetComponent<MiObjPoolPublicParameter>();
                    if (cs != null)
                    {
                        cs.SettingOriginal(obj as GameObject);
                    }
                    else
                    {
                        Log(Color.red, $"{o.name}  {obj.name}  Absent  cs  {cs.GetType()}");
                    }
                }
                else
                {
                    Log(Color.red, $"{o.name}   {obj.name}  Absent  Type   Is  Nont  GameObject");
                }
                return o;
            }
            public async Task Clear()
            {
                pool.Clear();
                await Task.Delay(delay: TimeSpan.FromSeconds(0));
            }

            public async Task<string> ReadPool()
            {
                string str = "";
                foreach (var item in pool)
                {
                    str += $"{item.Key}:";
                    foreach (var parm in item.Value)
                    {
                        str += $"{parm} - ";
                    }
                    str += '\n';
                }
                await Task.Delay(TimeSpan.Zero);
                Debug.Log(str);
                return str;
            }
        }
    }
}