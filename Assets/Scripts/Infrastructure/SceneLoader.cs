using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private AsyncOperation _wait;

        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action callback = null) =>
            _coroutineRunner.StartCoroutine(LoadLevel(name, callback));

        
        private IEnumerator LoadLevel(string name, Action callback = null)
        {
            _wait = SceneManager.LoadSceneAsync(name);
            while (!_wait.isDone)
            {
                yield return null;
            }
            callback?.Invoke();
        }
       
        public void UnLoad(int index, Action callback = null) =>
            _coroutineRunner.StartCoroutine(UnLoadLevel(index));
        

        private IEnumerator UnLoadLevel(int index, Action callback = null)
        {
            _wait = SceneManager.UnloadSceneAsync(index);
            while (!_wait.isDone)
            {
                yield return null;
            }
            callback?.Invoke();
        }
    }
}
