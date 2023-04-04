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
    AsyncOperation AsyncO;

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
        AsyncO = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(LoadAsc(n));
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Loading();
        }
        if (AsyncO != null)
        {
            LoadingImage.SetActive(true);
            ProgressBar.value = Mathf.Clamp01(AsyncO.progress / 0.9f);
            LoadingText.text = Mathf.Round(ProgressBar.value * 100) + "%";
        }
    }



    public void test(int n)
    {
        SceneManager.LoadScene(n);
    }

    IEnumerator LoadAsc(int n)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(n);

        while (!async.isDone)
        {
            if (!LoadingImage.activeSelf)
            {
                LoadingImage.SetActive(true);
                ProgressBar.value = Mathf.Clamp01(async.progress / 0.9f);
                LoadingText.text = Mathf.Round(ProgressBar.value * 100) + "%";
            }

            yield return null;
        }

    }

}
