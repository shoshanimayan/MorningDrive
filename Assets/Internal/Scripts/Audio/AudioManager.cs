using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Audio
{
    public class AudioManager : EventListener
    {
        ///////////////////////////////
        //  INSPECTOR VARIABLES      //
        ///////////////////////////////
        [Header("Audio Sources")]

        [SerializeField] private AudioSource _carAudio;
        [SerializeField] private AudioSource _speedBumpAudio;
        [SerializeField] private AudioSource _menuAudio;
        [SerializeField] private AudioSource _wipeAudio;

        [Header("Extra Audio Clips")]
        [SerializeField] private AudioClip _carStart;

        ///////////////////////////////
        //  PRIVATE METHODS           //
        ///////////////////////////////
        private void Awake()
        {
            EventConstants.ToMenu.RegisterListener(this);
            EventConstants.ToPlay.RegisterListener(this);
            EventConstants.ToEnd.RegisterListener(this);
            EventConstants.WipeEvent.RegisterListener(this);
            EventConstants.SpeedBumpEvent.RegisterListener(this);

        }
        private async void FadeOutAudio(AudioSource source, float fadeTime)
        {
            float startVolume = source.volume;

            while (source.volume > 0)
            {
                source.volume -= startVolume * Time.deltaTime / fadeTime;

                await Task.Yield();
            }

            source.Stop();
            source.volume = startVolume;
        }

        private void PlayAudio(AudioSource source, bool play, bool fade =false)
        {
            if (play)
            {
                source.Play();
            }
            else
            {
                if (!fade)
                {
                    source.Stop();
                }
                else
                {
                    FadeOutAudio(source, 1);
                }
            }
    
        }

        private async void PlayCarAudio()
        {
            _carAudio.PlayOneShot(_carStart);
            await Task.Delay((int)_carStart.length * 1000);
            PlayAudio(_carAudio, true);
        }

        private void PlaySpeedBumpAudio()
        {
            PlayAudio(_speedBumpAudio, true);
        }

        private void PlayMusicAudio()
        {
            PlayAudio(_menuAudio, true);
        }

        private void StopCarAudio()
        {
            PlayAudio(_carAudio, false, true);
        }

        private void StopMusicAudio()
        {
            PlayAudio(_menuAudio, false, true);
        }

        private void PlayWipeAudio()
        {
            PlayAudio(_wipeAudio, true);
        }

        ///////////////////////////////
        //  PUBLIC API               //
        ///////////////////////////////



        public override void OnEventRaised(string gameEventName)
        {
            switch (gameEventName)
            {
                case "ToMenu":
                    PlayMusicAudio();
                    break;
                case "ToPlay":
                    StopMusicAudio();
                    PlayCarAudio();
                    break;
                case "ToEnd":
                    StopCarAudio();
                    break;

                case "SpeedBumpEvent":
                    PlaySpeedBumpAudio();
                    break;
                case "WipeEvent":
                    PlayWipeAudio();
                    break;
            }
        }
    }
}
