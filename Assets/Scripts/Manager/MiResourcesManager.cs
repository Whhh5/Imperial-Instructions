using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MiManchi
{
    namespace MiResoures
    {
        public class MiResourcesManager : MiBaseClass.MiSingleton<MiResourcesManager>
        {
            public async Task<T> LoadAsync<T>(string filePath, string name,bool isInstantiate = false,RectTransform rectTr = null,Transform trTr = null) where T : Object
            {
                await Task.Delay(System.TimeSpan.Zero);
                string paths = $"{filePath}/{name}";
                T obj = Resources.Load<T>(paths);
                if (isInstantiate)
                {
                    obj = (await MiFactory.MiFactory.Instance.InstantiateAsync(obj, rectTr, trTr)) as T;
                    obj.name = name;
                }
                return obj;
            }
            public async Task<T> loadUIElementAsync<T>(string filePath,string name,MiEnum.CanvasLayer layer) where T : Object
            {
                var parent = await MiUIManager.Instance.GetCanvasRectAsync(layer);
                T obj = await LoadAsync<T>(filePath, name, true,rectTr: parent);

                await Task.Delay(System.TimeSpan.Zero);
                //await Task.Yield();
                return obj;
            }



            public T Load<T>(string filePath, string name, bool isInstantiate = false, RectTransform rectTr = null, Transform trTr = null) where T : Object
            {
                string paths = $"{filePath}/{name}";
                T obj = Resources.Load<T>(paths);
                if (isInstantiate)
                {
                    obj = MiFactory.MiFactory.Instance.Instantiate(obj, rectTr, trTr) as T;
                    obj.name = name;
                }
                return obj;
            }

            public T loadUIElement<T>(string filePath, string name, MiEnum.CanvasLayer layer) where T : Object
            {
                var parent = MiUIManager.Instance.GetCanvasRect(layer);
                T obj = Load<T>(filePath, name, true, rectTr: parent);

                return obj;
            }

            public async Task<AsyncOperation> LoadSceneAsync(string name, LoadSceneMode mode)
            {
                await MiAsync.MiAsyncManager.Instance.Default();
                return SceneManager.LoadSceneAsync(name, mode);
            }
        }
    }
}
