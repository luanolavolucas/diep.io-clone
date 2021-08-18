using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luan.AudioTools
{
    [CreateAssetMenu(fileName = "New Music", menuName = "Luan Audio Tools/Music", order = 1)]
    public class Music : SoundBase
    {
        public AudioClip intro, loop;

        public bool HasIntro => intro != null;
        

        public double IntroDuration
        {
            get
            {
                if (!intro)
                    return 0;

                return (double)intro.samples / intro.frequency;
            }
        }
    }
}
