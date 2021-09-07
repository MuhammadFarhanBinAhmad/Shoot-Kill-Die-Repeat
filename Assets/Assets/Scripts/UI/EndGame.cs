using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    public List<string> levels = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(levels[RoomSpawner.current_Level]);
            /*endgame_UI.SetActive(true);
            Time.timeScale = 0;*/

        }
    }
}
