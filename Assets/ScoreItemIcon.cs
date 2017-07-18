using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ScoreItemIcon : MonoBehaviour
{

    private Image image;
    private DynamicTextMeshScale dynamicScale;
    private TextMeshProUGUI text;


    // Use this for initialization
    public void Init()
    {
        image = GetComponent<Image>();
        dynamicScale = GetComponentInChildren<DynamicTextMeshScale>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        image.sprite = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/rectangle.png");
    }

    // обновляем отображение очков
    public void UpdateScore(string _text)
    {
        text.text = _text;
        dynamicScale.UpdateScale();
    }
}
