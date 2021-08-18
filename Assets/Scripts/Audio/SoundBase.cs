using UnityEngine;
using UnityEngine.Audio;
using System;
using NaughtyAttributes;
namespace Luan.AudioTools
{
    public abstract class SoundBase : ScriptableObject
    {

        [SerializeField]
        private string description;

        [Range(0f, 1f)]
        public float volume = .2f;

        [MinMaxSlider(0.1f, 5f)]
        public Vector2 pitch = Vector2.one;

        public int priority = 128;
        public AudioMixerGroup group;

        public static Action<SoundBase> OnValueChanged;
        private void OnValidate()
        {
            if (OnValueChanged != null) OnValueChanged.Invoke(this);
        }

    }
}