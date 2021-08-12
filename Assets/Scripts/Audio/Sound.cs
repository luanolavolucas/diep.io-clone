using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Sound", menuName = "Scriptable Objects/Sound", order = 0)]
public class Sound : ScriptableObject
{

    [Header("Basic Settings")]
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = .2f;


    [MinMaxSlider(0.1f, 5f)]
    public Vector2 pitch = Vector2.one;

    public bool loop, oneShot;

    public bool ignoreListenerPause = false;


    [Header("Spatial Settings")]
    [Range(0f, 1.0f)]
    public float spatialBlend = 1;
    public float AttenuationMinDistance = 18.0f;
    public float AttenuationMaxDistance = 32.0f;

    [Header("Other")]
    public AudioClip[] alternateClips;

    public string description;


    [Space]
    [Space]
    [Space]
    public int priority = 128;
    public AudioMixerGroup group;
    public static Action<Sound> OnValueChanged;

    void OnValidate()
    {
        if (OnValueChanged != null) OnValueChanged.Invoke(this);
    }
}