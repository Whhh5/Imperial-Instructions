using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiManchi
{
    public class MiSceneManager : MiBaseClass.MiSingletonMonoBeHaviour<MiSceneManager>
    {




        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        }

        protected override async Task InitializationAsync()
        {
            await base.InitializationAsync();
        }
        #region Load Scene
        public async Task LoadScene(string sceneName, bool isDesOld = false)
        {
            LoadSceneMode mode = isDesOld ? LoadSceneMode.Single : LoadSceneMode.Additive;
            SceneManager.LoadSceneAsync(sceneName, mode);
            await Task.Delay(System.TimeSpan.Zero);
        }
        public async Task LoadScene(Scene scene, bool isDesOld = false)
        {
            LoadSceneMode mode = isDesOld ? LoadSceneMode.Single : LoadSceneMode.Additive;
            SceneManager.LoadSceneAsync(scene.buildIndex, mode);
            await Task.Delay(System.TimeSpan.Zero);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            throw new System.NotImplementedException();
        }

        private void SceneManager_sceneUnloaded(Scene arg0)
        {
            throw new System.NotImplementedException();
        }

        private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

