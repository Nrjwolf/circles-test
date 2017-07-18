using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;

/**
* Увеличивает размер текстового поля, для реализации динамчиного увеличения иконок
*
*/

public class DynamicTextMeshScale : MonoBehaviour
{

    RectTransform target;
    TextMeshProUGUI text;
    [SerializeField] float SCALE_FACTOR_PX = 10; // шаг увеличения в px
    [SerializeField] int NO_SCALE_CHAR_NUM = 4; // количестов символов без увеличения

    private Vector2 startScale;
    private float ANIMATION_TIME = 0.2f;

    private void Awake()
    {
        target = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
        startScale = target.sizeDelta;
    }

    public void UpdateScale()
    {
        var num = text.text.Length - NO_SCALE_CHAR_NUM;
        if (num < 0)
            num = 0;
        var scaleTo = new Vector2(startScale.x + SCALE_FACTOR_PX * num, startScale.y);
        target.DOSizeDelta(scaleTo, ANIMATION_TIME);
    }

}
