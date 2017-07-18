﻿using System;
using System.Collections;
using UnityEngine;
using ListExtensions;

public class Main : MonoBehaviour
{
    public static Main Instance;

    public AssetBundle bundle { get; protected set; }
    private bool bundleLoaded = false;
    public event Action OnBundleLoadedSuccessfully = () => { };
    [HideInInspector] public Vector2 cameraSize;

    public const int PIXELS_PER_UNIT = 100;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        var h = Camera.main.orthographicSize * 2;
        cameraSize = new Vector2(Camera.main.aspect * h, h);

        
    }

    // перед стартом игры подгружаем бандл
    IEnumerator Start()
    {
        WWW www = new WWW("http://nrjwolf.com/bundle/circlegamebundle");
        yield return www;

        if (www.assetBundle != null)
        {
            bundle = www.assetBundle;
            bundleLoaded = true;
            Debug.Log("Asset loaded successfully : \n" + bundle.GetAllAssetNames().JoinToString(",\n"));
            OnBundleLoadedSuccessfully(); // event сообщает об успешной закгрузке
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}