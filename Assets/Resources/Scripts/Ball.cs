using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    private float offset = 0.17f;
    private float force = 12000;
    bool bAddForce = false;
    GameObject SplashPrefab;

    int score;
    int highScore;

    public Vector3 initialPosition;
    public Transform SplashParent;
    public bool bAlive = false;

    public float splashDistance = 0.1f;
    public static event Action<bool> ballDied;
    public static event Action<bool> GameWin;
    public bool hasBounced = false;

    private void Awake()
    {
        SplashPrefab = Resources.Load<GameObject>("Prefabs/Splash");
    }
    private void Start()
    {
        initialPosition = transform.position;
        ResetGame();
        GameSingleton.Instance.AudioManager.ThemeMusic("ThemeMusic");
        GameSingleton.Instance.GameMenus.HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void ResetGame()
    {
        this.transform.position = initialPosition;
        bAddForce = bAlive = true;
        score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bAlive && !hasBounced)
        {
            GameSingleton.Instance.AudioManager.PlaySFX("BallBounce");
            Vector3 splashPosition = collision.contacts[0].point;//collision.contacts[0].point + collision.contacts[0].normal.normalized * splashDistance;
            splashPosition.y += 0.035f;
            GameObject.Instantiate(SplashPrefab, splashPosition, Quaternion.LookRotation(collision.contacts[0].normal), SplashParent);

            if (collision.gameObject.tag == "Gameover")
            {
                GameSingleton.Instance.AudioManager.PlaySFX("GameOver");
                bAlive = false;
                bAddForce = false;
                ballDied?.Invoke(bAlive);
            }
            if (collision.gameObject.tag == "GameWin")
            {
                GameWin?.Invoke(true);
                GameSingleton.Instance.Input.bInput = false;
            }

            if (bAddForce)
            {
                this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1f, 0f) * Time.deltaTime * force);
                bAddForce = false;
                Invoke("RestoringForce", 0.5f);
            }
            hasBounced = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            score++;
            highScore = PlayerPrefs.GetInt("HighScore");
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            GameSingleton.Instance.GameMenus.ScoreText.text = "Score: " + score;
        }
    }

    private void Update()
    {
        if (bAlive)
        {
            MoveCameraDownward();
        }
    }

    private void MoveCameraDownward()
    {
        if (transform.position.y + offset < Camera.main.transform.position.y)
        {
            Vector3 pos = Camera.main.transform.position;
            pos.y = this.transform.position.y + offset;
            Camera.main.transform.position = pos;
        }
    }

    public void RestoringForce()
    {
        hasBounced = false;
        bAddForce = true;
    }
}
