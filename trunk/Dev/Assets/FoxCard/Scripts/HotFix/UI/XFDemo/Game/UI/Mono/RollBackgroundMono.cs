using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;

public class RollBackgroundMono : MonoBehaviour
{
    [SerializeField] public float x, y;
    private RawImage img;

    private void OnEnable()
    {
        Log.Debug($"sdfsaf");
        img = GetComponent<RawImage>();
    }

    void Update()
    {
        //Log.Debug($"sdfsaf");
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }
}