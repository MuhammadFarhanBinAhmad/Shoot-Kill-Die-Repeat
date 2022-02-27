using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    GameObject player_GO;
    [SerializeField]
    Transform player_Spawn_Pos;
    public List<string> levels = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>() !=null)
        {
            player_GO = other.gameObject;
            player_GO.transform.position = player_Spawn_Pos.transform.position;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(levels[RoomSpawnerV2.current_Level + 1]);
            /*endgame_UI.SetActive(true);
            Time.timeScale = 0;*/

        }
    }
}
