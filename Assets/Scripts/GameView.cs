﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/**
*   Script of scene view
*
*/

public class GameView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] RectTransform leftBottomPanel;
    [SerializeField] ScoreItemIcon scoreItem;
    [SerializeField] ScoreItemIcon timeItem;
    [SerializeField] SpikesController spikes;

    private GameObject btnPlay;
    public event System.Action OnPlayButtonClicked = () => { };

    public void Init()
    {
        scoreItem.Init();
        timeItem.Init();
    }

    // Animations of counters
    public void AddScoreItemsOnScene()
    {
        leftBottomPanel.DOAnchorPosX(-leftBottomPanel.anchoredPosition.x, 0.2f).SetEase(Ease.OutBack);
    }

    // Play button add on scene
    public void AddPlayButton()
    {
        RemoveLoadingText();
        btnPlay = GameUtils.AddOnCanvas(FindObjectOfType<Canvas>().gameObject, Main.Instance.bundle.LoadAsset<GameObject>("Assets/Prefabs/StartButton.prefab"));
        TMP_FontAsset font = Resources.Load("LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
        btnPlay.GetComponentInChildren<TextMeshProUGUI>().font = font;
        btnPlay.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.InBounce);
        btnPlay.GetComponent<Button>().onClick.AddListener(OnPlayButton);
    }

    // Play button action
    private void OnPlayButton()
    {
        SoundController.instance.PlaySound(SoundName.CLICK);
        btnPlay.GetComponent<Button>().onClick.RemoveAllListeners();
        var time = 0.2f;
        btnPlay.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), time, 8, 0.8f);
        btnPlay.GetComponent<Image>().DOFade(0, time);
        btnPlay.GetComponentInChildren<TextMeshProUGUI>().DOFade(0, time);
        OnPlayButtonClicked();

        // add spikes
        spikes.Init();
    }

    //  Loading text update
    public void UpdateLoadingText(string _text)
    {
        loadingText.text = string.Format("Loading {0}%", _text);
    }

    public void RemoveLoadingText()
    {
        Destroy(loadingText);
    }

    // Update score text
    public void UpdateScore(string _score)
    {
        scoreItem.UpdateScore(_score);
    }

    // Update timer text
    public void UpdateTime(string _time)
    {
        timeItem.UpdateScore(_time);
    }

}
