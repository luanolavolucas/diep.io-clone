using System.Linq;
using UnityEngine;
using System.Collections.Generic;
namespace Luan.AudioTools
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "Luan Audio Tools/Sound", order = 0)]
    public class Sound : SoundBase
    {

        [Header("Basic Settings")]
        public AudioClip clip;

        public bool ignoreListenerPause = false;


        [Header("Spatial Settings")]
        [Range(0f, 1.0f)]
        public float spatialBlend = 1;
        public float AttenuationMinDistance = 18.0f;
        public float AttenuationMaxDistance = 32.0f;

        [Header("Other")]
        public AudioClip[] alternateClips;

    }
}