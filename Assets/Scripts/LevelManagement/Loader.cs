using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectX
{
    public static class Loader
    {
        private class LoadingMonoBehaviour : MonoBehaviour
        {
            
        }
        
        public enum Scene
        {
            GameScene,
            Loading,
            MainMenu
        }

        private static Action onLoaderCallback;
        private static AsyncOperation asyncOperation;
        public static void Load(Scene scene)
        {
            onLoaderCallback = () =>
            {
                GameObject loadingGameObject = new GameObject("Loading Game Object");
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));

            };
            
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        private static IEnumerator LoadSceneAsync(Scene scene)
        {
            yield return null;
            asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }

        public static float GetLoadingProgress()
        {
            if (asyncOperation != null)
            {
                Debug.Log(asyncOperation.progress);
                return asyncOperation.progress;
            }
            else
            {
                return 1f;
            }
        }

        public static void LoaderCallBack()
        {
            if (onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }
    }
}