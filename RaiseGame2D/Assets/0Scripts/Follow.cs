using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        rect = GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GM.Contents.GetPlayer().transform.position);
    }
}
