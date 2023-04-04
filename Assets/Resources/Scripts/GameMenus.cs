using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameMenus : MonoBehaviour
{

    public GameObject GameMenu;
    public GameObject LoadingImage;
    public Slider ProgressBar;
    public Text LoadingText;
    AsyncOperation asyncOperation;

    public void Retry()
    {
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
        //StartCoroutine(LoadAsc(n));
    }
    public void LoadingPrevious()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
            asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        //StartCoroutine(LoadAsc(n));
    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            Loading();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadingPrevious();
        }
        if (asyncOperation != null)
        {
            LoadingImage.SetActive(true);
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
}
