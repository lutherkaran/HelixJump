using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    private AudioSource themesource;
    public readonly Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        themesource = this.gameObject.AddComponent<AudioSource>();
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
            themesource.Stop();
            audioSource.volume = 0.2f;
        }
        else
        {
            audioSource.volume = 1f;
        }
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void ThemeMusic(string _name)
    {
        if (GameSingleton.Instance.ball.bAlive)
        {
            themesource.clip = audioDict[_name];
            themesource.Play();
            themesource.loop = true;
        }
    }
}
