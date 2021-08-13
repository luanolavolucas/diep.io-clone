using UnityEngine;
using ToolBox.Pools;
using System.Collections;

public class PoolableAudioSource : MonoBehaviour, IPoolable
{
    public AudioSource audioSource;

    public enum PlayingBehaviour
    {
        Default,
        FollowTransform
    }

    public PlayingBehaviour playingBehaviour;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    public void Play(Transform transform)
    {
        print(transform);
        print(audioSource.clip);
        this.transform.position = transform.position;
        audioSource.Play();
        Invoke(nameof(CheckForCompletion), (audioSource.clip.length / audioSource.pitch) + 0.25f);
        if (playingBehaviour == PlayingBehaviour.FollowTransform)
        {
            StartCoroutine(FollowTransform(transform));
        }
    }

    public IEnumerator FollowTransform(Transform transform)
    {
        this.transform.position = transform.position;
        yield return null;
    }

    private void CheckForCompletion()
    {
        if (!audioSource.isPlaying)
        {
            StopAllCoroutines();
            gameObject.Release();
        }

    }
    public void OnGet()
    {
    }

    public void OnRelease()
    {
        audioSource.Stop();
    }

    public void Initialize(Sound sound)
    {
        audioSource.playOnAwake = false;
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = Random.Range(sound.pitch.x, sound.pitch.y);
        audioSource.loop = sound.loop;
        audioSource.spatialBlend = sound.spatialBlend;
        audioSource.minDistance = sound.AttenuationMinDistance;
        audioSource.maxDistance = sound.AttenuationMaxDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0;
        audioSource.priority = sound.priority;
        audioSource.outputAudioMixerGroup = sound.group;
        audioSource.ignoreListenerPause = sound.ignoreListenerPause;
    }
}
