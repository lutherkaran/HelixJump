using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioClip[] audioClips;
    private AudioSource audioSource;

    public readonly Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Audio/");
        for (int i = 0; i < audioClips.Length; i++)
        {
            if (!audioDict.ContainsKey(audioClips[i].name))
            {
                audioDict.Add(audioClips[i].name, audioClips[i]);
            }
        }
    }
    public void PlaySFX(string _name)
    {
        audioSource.clip = audioDict[_name];
        if (_name == "GameOver")
        {
            audioSource.volume = 0.1f;
        }
        else
        {
            audioSource.volume = 1f;
        }
        audioSource.Play();
    }

    public void ThemeMusic(string _name)
    {
        if (GameSingleton.Instance.ball.bAlive)
        {
            AudioSource audiosource = this.gameObject.AddComponent<AudioSource>();
            audiosource.clip = audioDict[_name];
            audiosource.Play();
            audiosource.loop = true;
        }

    }
}
