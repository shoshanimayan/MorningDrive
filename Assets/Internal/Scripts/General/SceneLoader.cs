using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Window;

namespace General
{
    public class SceneLoader : MonoBehaviour
    {
        /////////////////////////
        // INSPECTOR VARIABLES //
        /////////////////////////
        [SerializeField]
        private AssetReference _firstScene;
        [SerializeField]

        private AssetReference _envScene;


        /////////////////////////
        //  PRIVATE VARIABLES  //
        /////////////////////////
        private AsyncOperationHandle<SceneInstance> _handle;
        private bool _unloaded;
        private GameStateManager _gameState { get { return GameStateManager.Instance; } }

        ///////////////////////
        //  PRIVATE METHODS  //
        ///////////////////////
        

        private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
               // var window = GameObject.Find("window");
             //   if (window)
              //  {
                //    _gameState.SetWindow(window.GetComponent<DrawOnTexture>());
                    _gameState.State = GameState.Menu;
              //  }
            }
        }

        private void FirstSceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {  
                Addressables.LoadSceneAsync(_envScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;
            }
        }

        //////////////////
        //  PUBLIC API  //
        /////////////////

        public void FirstLoad()
        {
            Addressables.LoadSceneAsync(_firstScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += FirstSceneLoadCompleted;
        }

    }
}
