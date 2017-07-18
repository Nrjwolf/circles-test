using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    private Canvas canvas;

    [SerializeField] ScoreItemIcon scoreItem;
    [SerializeField] ScoreItemIcon timeItem;

    public void Init()
    {
        // находим компоненты
        canvas = FindObjectOfType<Canvas>();
        AddBackground();
        scoreItem.Init();
        timeItem.Init();
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

    public void UpdateScore(string _score)
    {
        scoreItem.UpdateScore(_score);
    }

    public void UpdateTime(string _time)
    {
        timeItem.UpdateScore(_time);
    }

}
