using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    GameObject player_GO;
    [SerializeField]
    Transform player_Spawn_Pos;
    public List<string> levels = new List<string>();

    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            player_GO = other.gameObject;
            StartCoroutine("LoadNextLevel");
            anim.SetTrigger("FadeIn");

        }
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1);
        player_GO.SetActive(false);
        player_GO.transform.position = player_Spawn_Pos.transform.position;
        //insert fade in scene here
        player_GO.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        RoomSpawnerV2.current_Level++;
        SceneManager.LoadScene("LoadingScreen");
        /*endgame_UI.SetActive(true);
        Time.timeScale = 0;*/
    }
}
