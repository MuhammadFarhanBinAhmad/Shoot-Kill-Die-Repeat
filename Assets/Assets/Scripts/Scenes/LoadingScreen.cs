using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{
    public Image Loading_Bar;
    DontDestroyOnLoad the_DontDestroyOnLoad;

    [SerializeField] List<string> Levels = new List<string>();

    private void Start()
    {
        if (FindObjectOfType<DontDestroyOnLoad>() !=null)
        {
            the_DontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
            the_DontDestroyOnLoad.gameObject.SetActive(false);
        }
        LoadLevel(Levels[LevelManager.CURRENTLEVEL+1]);
    }

    public void LoadLevel(string level_Name)
    {
        StartCoroutine(LoadAsynchronously(level_Name));
    }

    IEnumerator LoadAsynchronously (string level_Name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level_Name);

        while(!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / .9f);
            Loading_Bar.fillAmount = progess;
            print(operation.progress);

            yield return null;
        }
    }
}
