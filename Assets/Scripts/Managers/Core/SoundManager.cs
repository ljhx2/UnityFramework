using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>(); //Caching

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            UnityEngine.Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length -1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string key, Define.Sound type = Define.Sound.Effect, float pitch = 1f)
    {
        key = $"Sounds/{key}";
        GetOrAddAudioClipAsync(key, type, (audioClip) =>
        {
            Play(audioClip, type, pitch);
        });
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void GetOrAddAudioClipAsync(string key, Define.Sound type = Define.Sound.Effect, Action<AudioClip> completed = null)
    {
        if (type == Define.Sound.Bgm)
        {
            Managers.ResourceA.LoadAsync<AudioClip>(key, (audioClip) =>
            {
                completed?.Invoke(audioClip);
            });
        }
        else
        {
            AudioClip audioClip = null;
            if (_audioClips.TryGetValue(key, out audioClip) == false)
            {
                Managers.ResourceA.LoadAsync<AudioClip>(key, (clip) =>
                {
                    _audioClips.Add(key, clip);
                    completed?.Invoke(clip);
                });
            }
            else
            {
                completed?.Invoke(audioClip);
            }
        }
    }
}
