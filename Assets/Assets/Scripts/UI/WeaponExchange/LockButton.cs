using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    [SerializeField]
    Sprite image_Unlock_Button;
    [SerializeField]
    Image image_Button;

    public Button button_Weapon;

    public void ChangeButtonImage()
    {
        button_Weapon.image.sprite = image_Unlock_Button;
        button_Weapon.interactable = false;
    }
}
