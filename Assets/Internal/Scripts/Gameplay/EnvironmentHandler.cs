using General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace GamePlay
{
    public class EnvironmentHandler : EventListener
    {
        ///////////////////////////////
        //  INSPECTOR VARIABLES      //
        ///////////////////////////////
        [SerializeField] GameObject[] _trees;
        [SerializeField ]private Transform _treeEndPoint;

        ///////////////////////////////
        //  PRIVATE VARIABLES         //
        ///////////////////////////////

        private bool _createdTweens=false;

        ///////////////////////
        //  PRIVATE METHODS  //
        ///////////////////////
        private void Awake()
        {
            EventConstants.ToEnd.RegisterListener(this);
            EventConstants.StartTweensEvent.RegisterListener(this);
           

        }
        private void KillTweens()
        {
            DOTween.PauseAll();
        }


        private async void StartTweens()
        {
            if (!_createdTweens)
            {
                foreach (GameObject tree in _trees)
                {
                    await Task.Delay(1000);
                    tree.transform.DOMoveX(_treeEndPoint.position.x, 4).SetLoops(-1, LoopType.Restart);

                }
                _createdTweens = true;
            }
            else
            {
                DOTween.PlayAll();
            }
        }

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////


        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                case "ToEnd":
                    KillTweens();
                    break;
                case "StartTweensEvent":
                    StartTweens();
                    break;

            }
        }



    }
}
