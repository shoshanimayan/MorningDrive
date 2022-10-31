using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Sources")]

        [SerializeField] private AudioSource _carAudio;
        [SerializeField] private AudioSource _speedBumpAudio;
        [SerializeField] private AudioSource _menuAudio;
        [SerializeField] private AudioSource _wipeAudio;

        [Header("Extra Audio Clips")]
        [SerializeField] private AudioClip _carStart;

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


        public async void PlayCarAudio()
        {
            _carAudio.PlayOneShot(_carStart);
            await Task.Delay((int)_carStart.length*1000);
            PlayAudio(_carAudio,true);
        }

        public void PlaySpeedBumpAudio()
        {
            PlayAudio(_speedBumpAudio,true);
        }

        public void PlayMusicAudio()
        {
            PlayAudio(_menuAudio, true);
        }

        public void StopCarAudio()
        {
            PlayAudio(_carAudio,false,true);
        }

        public void StopMusicAudio()
        {
            PlayAudio(_menuAudio, false,true);
        }

        public void PlayWipeAudio()
        {
            PlayAudio(_wipeAudio, true);
        }

       

    }
}
