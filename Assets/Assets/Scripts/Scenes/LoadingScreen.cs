using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{
    public Image Loading_Bar;
    DontDestroyOnLoad the_DontDestroyOnLoad;

    AsyncOperation async;

    public List<string> levels = new List<string>();


    private void Start()
    {
        if (FindObjectOfType<DontDestroyOnLoad>() !=null)
        {
            the_DontDestroyOnLoad = FindObjectOfType<DontDestroyOnLoad>();
            the_DontDestroyOnLoad.gameObject.SetActive(false);
        }
        StartCoroutine("LoadScene");
    }
    
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(levels[RoomSpawnerV2.current_Level]);
        async.allowSceneActivation = false;

        while(async.isDone == false)
        {
            Loading_Bar.fillAmount = async.progress;
            if (async.progress == 0.9f)
            {
                Loading_Bar.fillAmount = 1;
                async.allowSceneActivation = true;
                //the_DontDestroyOnLoad.gameObject.SetActive(true);
            }
            yield return null;
        }
        if (async.isDone)
        {
            the_DontDestroyOnLoad.gameObject.SetActive(true);
        }
    }

}
