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
        private GameStateManager _gameState { get { return GameStateManager.Instance; } }

        ///////////////////////
        //  PRIVATE METHODS  //
        ///////////////////////
        

        private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
               
                    _gameState.State = GameStateType.Menu;
            }
            else
            {
                throw new Exception("failed to load addressable");
            }
        }

        private void FirstSceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Addressables.LoadSceneAsync(_envScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;
            }
            else
            {
                throw new Exception("failed to load addressable");
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
