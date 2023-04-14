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
    public GameObject LevelCompletedMenu;
    public GameObject GameWinMenu;
    public Text ScoreText;

    public Slider ProgressBar;
    public Text LoadingText;
    AsyncOperation asyncOperation;

    private bool bGameWin = false;
    private void OnEnable()
    {
        Ball.ballDied += PlayerDied;
        Ball.GameWin += NextLevel;
    }

    public void PlayerDied(bool bAlive)
    {
        if (!bAlive)
        {
            //Debug.Log("Player Died");
            this.GameOverMenu.SetActive(true);
        }
    }
    public void NextLevel(bool _NextLevel)
    {
        if (_NextLevel)
        {
            if (SceneManager.GetActiveScene().buildIndex <= 2)
            {
                LevelCompletedMenu.SetActive(true);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameWinMenu.SetActive(true);
                bGameWin = true;
            }
        }
    }

    public void Retry()
    {
        if (bGameWin)
        {
            this.GameWinMenu.SetActive(false);
            SceneManager.LoadScene(1);
        }
        else
        {
            this.GameOverMenu.SetActive(false);
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Loading()
    {
        asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadingPrevious()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
            asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Update()
    {
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
        Ball.GameWin -= NextLevel;

    }
}
