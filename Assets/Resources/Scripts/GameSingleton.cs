using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton Instance { get; private set; }
    public Input Input { get; private set; }
    public Ball ball { get; private set; }
    public GameMenus GameMenus { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public Disc disc { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        else
        {
            Instance = this;
            GameMenus = this.GetComponentInChildren<GameMenus>();
            ball = this.GetComponentInChildren<Ball>();
            disc = this.GetComponentInChildren<Disc>();
            Input = this.GetComponentInChildren<Input>();
            AudioManager = this.GetComponentInChildren<AudioManager>();
        }
    }
}
