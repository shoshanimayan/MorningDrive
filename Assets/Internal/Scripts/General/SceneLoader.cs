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
    public class SceneLoader : EventListener
    {
        /////////////////////////
        // INSPECTOR VARIABLES //
        /////////////////////////
        [SerializeField]
        private AssetReference _firstScene;
        [SerializeField]
        private AssetReference _envScene;


        ///////////////////////
        //  PRIVATE METHODS  //
        ///////////////////////
        private void Awake()
        {
            EventConstants.LoadSceneEvent.RegisterListener(this);
        }

        private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {

                EventConstants.ToMenu.Raise();
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

        private void FirstLoad()
        {
            Addressables.LoadSceneAsync(_firstScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += FirstSceneLoadCompleted;
        }
        //////////////////
        //  PUBLIC API  //
        /////////////////



        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                case "LoadSceneEvent":
                    FirstLoad();
                    break;
                
            }
        }
    }
}
