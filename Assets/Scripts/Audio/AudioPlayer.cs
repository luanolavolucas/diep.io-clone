using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using ToolBox.Pools;

public class AudioPlayer : MonoBehaviour
{

    private static GameObject audioSourcePrefab;

    private void Awake()
    {
        if (audioSourcePrefab == null)
        {
            audioSourcePrefab = new GameObject("Audio Source", typeof(PoolableAudioSource));
            audioSourcePrefab.hideFlags = HideFlags.HideInHierarchy;
        }
    }

    public void Play(Sound sound)
    {
        print("Inside play.");
        PlayInternal(sound);
    }

    private AudioSource PlayInternal(Sound sound)
    {
        PoolableAudioSource mainSource;

        mainSource = GetPoolableAudioSource();
        mainSource.Initialize(sound);

        if (sound.alternateClips != null)
        {
            int rand = UnityEngine.Random.Range(-1, sound.alternateClips.Length);
            mainSource.audioSource.clip = rand == -1 ? sound.clip : sound.alternateClips[rand];
        }
        print("Trying to play Audio Source!");
        mainSource.Play(transform);

        return mainSource.audioSource;

    }

    private PoolableAudioSource GetPoolableAudioSource()
    {
        return audioSourcePrefab.Get<PoolableAudioSource>(audioSourcePrefab.transform);
    }
}