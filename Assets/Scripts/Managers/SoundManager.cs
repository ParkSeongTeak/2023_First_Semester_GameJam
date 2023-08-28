using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{

    
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sounds.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    GameObject root;
    // MP3 Player   -> AudioSource
    // MP3 À½¿ø     -> AudioClip
    // °ü°´(±Í)     -> AudioListener

    public void Init()
    {
        if (root == null)
        {
            root = GameObject.Find("@Sound");

            if (root == null)
            {
                root = new GameObject { name = "@Sound" };
                Object.DontDestroyOnLoad(root);
                string[] soundNames = System.Enum.GetNames(typeof(Define.Sounds));
                for (int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = soundNames[i] };
                    _audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.parent = root.transform;
                }

                _audioSources[(int)Define.Sounds.BGM].loop = true;
            }
            else
            {
                string[] soundNames = System.Enum.GetNames(typeof(Define.Sounds));
                for (int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = root.transform.Find(soundNames[i]).gameObject;
                    _audioSources[i] = go.GetComponent<AudioSource>();
                }

                _audioSources[(int)Define.Sounds.BGM].loop = true;

            }
        }
        
    }
    public void SetVolume(Define.Sounds type, float volume)
    {
        _audioSources[(int)type].volume = volume;
    }
    public float GetVolume(Define.Sounds type)
    {
        return _audioSources[(int)type].volume;
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

    public void Play(string path, Define.Sounds type = Define.Sounds.SFX, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sounds type = Define.Sounds.SFX, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sounds.BGM)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sounds.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sounds.SFX];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void SetAudioSourceVolume(float volume, Define.Sounds sound)
    {
        if (_audioSources[(int)sound] == null) Debug.Log("No _audioSources");
        _audioSources[(int)sound].volume = volume;
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sounds type = Define.Sounds.SFX)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;
        
        if (type == Define.Sounds.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
