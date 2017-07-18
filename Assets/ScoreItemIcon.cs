using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreItemIcon : MonoBehaviour
{

    private DynamicTextMeshScale dynamicScale;
    private TextMeshProUGUI text;

    // Use this for initialization
    void Start()
    {
        dynamicScale = GetComponentInChildren<DynamicTextMeshScale>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

	// обновляем отображение очков
    public void UpdateScore(string _text)
    {
		text.text = _text;
		dynamicScale.UpdateScale();
    }
}
