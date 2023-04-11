using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameMenus : MonoBehaviour
{
    public GameObject LoadingImage;

    public GameObject GameOverMenu;
    public Text ScoreText;

    public Slider ProgressBar;
    public Text LoadingText;
    AsyncOperation asyncOperation;

    private void OnEnable()
    {
        Ball.ballDied += PlayerDied;
    }
    public void PlayerDied(bool bAlive)
    {
        if (!bAlive)
        {
            //Debug.Log("Player Died");
            this.GameOverMenu.SetActive(true);
        }
    }
    public void Retry()
    {
        this.GameOverMenu.SetActive(false);
        //GameSingleton.Instance.ball.ResetGame();
        
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Loading()
    {
        if (SceneManager.GetActiveScene().buildIndex < 3)
            asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

    public void LoadingPrevious()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
            asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.N))
        {
            Loading();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            LoadingPrevious();
        }

        if (asyncOperation != null)
        {
            LoadingImage.SetActive(!asyncOperation.isDone);
            if (!asyncOperation.isDone)
            {
                ProgressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                LoadingText.text = Mathf.Round(ProgressBar.value * 100) + "%";
            }
        }
    }

    public void test(int n)
    {
        SceneManager.LoadScene(n);
    }

    private void OnDisable()
    {
        Ball.ballDied -= PlayerDied;
    }
}
