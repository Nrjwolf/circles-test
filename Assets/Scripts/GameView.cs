using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    [SerializeField] ScoreItemIcon scoreItem;
    [SerializeField] ScoreItemIcon timeItem;

    public void Init()
    {
        // находим компоненты
        scoreItem.Init();
        timeItem.Init();
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
