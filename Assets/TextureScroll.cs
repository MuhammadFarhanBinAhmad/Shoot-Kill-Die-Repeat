using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    float speed = 2;
    float offset;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    private void FixedUpdate()
    {
        offset += (Time.deltaTime * speed) / 10f;
        mat.mainTextureOffset = new Vector2(0, offset);
    }
}
