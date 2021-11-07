using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    [SerializeField]
    float speed_y = 2;
    [SerializeField]
    float speed_x = 2;

    float offset_y, offset_x;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void FixedUpdate()
    {
        offset_y += (Time.deltaTime * speed_y) / 10f;
        offset_x += (Time.deltaTime * speed_x) / 10f;

        mat.mainTextureOffset = new Vector2(offset_x, offset_y);
    }
}
