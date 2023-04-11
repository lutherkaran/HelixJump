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
    public Vector3 initialPosition;
    public Transform SplashParent;
    public bool bAlive = false;

    public static event Action<bool> ballDied;

    private void Awake()
    {
        SplashPrefab = Resources.Load<GameObject>("Prefabs/Splash");
    }
    private void Start()
    {
        initialPosition = transform.position;
        ResetGame();
        GameSingleton.Instance.AudioManager.ThemeMusic("ThemeMusic");
    }

    public void ResetGame()
    {
        this.transform.position = initialPosition;
        bAddForce = bAlive = true;
        score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bAlive)
        {
            GameSingleton.Instance.AudioManager.PlaySFX("BallBounce");
            GameObject.Instantiate(SplashPrefab, this.transform.position - new Vector3(0f, +0.18f, 0), Quaternion.AngleAxis(90, Vector3.right), SplashParent);
        }
        if (collision.gameObject.tag == "Gameover")
        {
            GameSingleton.Instance.AudioManager.PlaySFX("GameOver");
            bAlive = false;
            bAddForce = false;
            ballDied?.Invoke(bAlive);
        }

        if (bAddForce)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1f, 0f) * Time.deltaTime * force);
            bAddForce = false;
            Invoke("RestoringForce", 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Score")
        {
            score++;
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
        bAddForce = true;
    }
}
