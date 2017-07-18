﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    private Canvas canvas;

    public void Init()
    {
        // находим компоненты
        canvas = FindObjectOfType<Canvas>();
        AddBackground();
    }

    private void AddBackground()
    {
        GameObject bg = new GameObject("Background");
        bg.transform.SetParent(canvas.transform);
        bg.transform.localScale = Vector3.one;
        bg.AddComponent<Image>();
        bg.GetComponent<Image>().sprite = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/whitesquare.png");
        // растягиваем фон на весь экран
        bg.GetComponent<RectTransform>().SetAnchor(AnchorPresets.StretchAll);
        bg.GetComponent<Image>().color = GameUtils.ToColor(0x2F2F2E);
        // фон на задний план
        bg.transform.SetSiblingIndex(0);
    }
}
