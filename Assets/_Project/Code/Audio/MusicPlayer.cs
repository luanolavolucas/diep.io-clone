using UnityEngine;

namespace Luan.AudioTools
{
    public class MusicPlayer : MonoBehaviour
    {
        public Music music;
        public bool playOnAwake;


        private AudioSource intro, loop;
        private void Awake()
        {
            intro = gameObject.AddComponent<AudioSource>();
            loop = gameObject.AddComponent<AudioSource>();
            intro.hideFlags = HideFlags.HideInInspector;
            loop.hideFlags = HideFlags.HideInInspector;
            loop.loop = true;

            if(music!=null && playOnAwake){
                Play();
            }

        }

        public void Play()
        {
            Initialize(music);
            PlayInternal();
        }

        public void Play(Music music)
        {
            Initialize(music);
            PlayInternal();
        }

        private void PlayInternal()
        {
            double startTime = AudioSettings.dspTime + 0.05;
            intro.PlayScheduled(startTime);
            loop.PlayScheduled(startTime + music.IntroDuration);

        }

        private void Initialize(Music music)
        {
            this.music = music;
            void Init(Music sound, AudioSource audioSource)
            {

                audioSource.playOnAwake = false;
                audioSource.volume = sound.volume;
                audioSource.pitch = Random.Range(sound.pitch.x, sound.pitch.y);
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0;
                audioSource.priority = sound.priority;
                audioSource.outputAudioMixerGroup = sound.group;
            }
            Init(music, intro);
            Init(music, loop);
            intro.clip = music.intro;
            loop.clip = music.loop;
        }
    }
}
