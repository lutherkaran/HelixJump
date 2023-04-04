using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float offset = 0.17f;
    private float force = 12000;
    bool bAddForce = false;
    GameObject SplashPrefab;
    int score;
    public Text scoreText;
    public GameObject GameoverMenu;
    public Transform SplashParent;
    public bool bPlayer = false;

    public static Ball Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SplashPrefab = Resources.Load<GameObject>("Prefabs/Splash");
        bAddForce = true;
        bPlayer = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (bPlayer)
        {
            GameObject.Instantiate(SplashPrefab, this.transform.position - new Vector3(0f, +0.19f, 0), Quaternion.AngleAxis(90, Vector3.right), SplashParent);

            if (collision.gameObject.tag == "Gameover")
            {
                bPlayer = false;
                bAddForce = false;
                force = 0;
                GameoverMenu.SetActive(true);
            }
            if (bAddForce)
            {
                this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1f, 0f) * Time.deltaTime * force);
                bAddForce = false;
                Invoke("Fix", 0.5f);
            }

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (bPlayer)
        {
            if (other.gameObject.tag == "Score")
            {
                score++;
                scoreText.text = "Score: " + score;
            }
        }
    }
    private void Update()
    {
        if (bPlayer)
        {
            if (transform.position.y + offset < Camera.main.transform.position.y)
            {
                Vector3 pos = Camera.main.transform.position;
                pos.y = this.transform.position.y + offset;
                Camera.main.transform.position = pos;
            }
        }
    }
    public void Fix()
    {
        bAddForce = true;
    }
}
