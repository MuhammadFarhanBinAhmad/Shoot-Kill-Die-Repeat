using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage : MonoBehaviour
{
    public List<GameObject> page = new List<GameObject>();
    public List<GameObject> page_Weapon = new List<GameObject>();
    [SerializeField]int current_Page;


    public void GoNextPage()
    {
        if (current_Page < page.Count-1)
        {
            page[current_Page].SetActive(false);
            current_Page++;
            page[current_Page].SetActive(true);
        }
        else
        {

            page[current_Page].SetActive(false);
            current_Page = 0;
            page[current_Page].SetActive(true);
        }

    }
}
