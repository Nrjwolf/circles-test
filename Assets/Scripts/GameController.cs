using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    // Парметры на старте игры
    [SerializeField] Transform circlesParentTransformObject;
    [SerializeField] Color32[] colors;
    [SerializeField] [Range(0f, 1f)] float sizeMin;
    [SerializeField] [Range(0f, 1f)] float sizeMax;
    [SerializeField] float speed;

    // Параметры игры
    private int score;

    // Utils
    private System.DateTime dt;

    // Компоненты
    private GameView view;

    void Start()
    {
        // подписываемся на событие успешной подгрузки
        Main.Instance.OnBundleLoadedSuccessfully += InitGame;
        dt = new System.DateTime();
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    private void InitGame()
    {
        Debug.Log("Init game");
        // находим компоненты
        view = FindObjectOfType<GameView>();

        view.Init(); // инициализация визульной части сцены
        CreateFalingCircle();
    }

    private void UpdateTimer()
    {
        dt = dt.AddSeconds(1);
        view.UpdateTime(dt.ToString("mm:ss"));
    }

    // активируем падение кружочков
    private void CreateFalingCircle()
    {
        Sprite spr = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/whiteOval.png"); // подгружаем спрайт
        Color32 clr = colors[Random.Range(0, colors.Length)]; // выбираем цвет
        float size = Random.Range(sizeMin, sizeMax);
        // расчет позиции
        float sizeSprite = spr.rect.width * size / Main.PIXELS_PER_UNIT;
        float posX = Random.Range(-Main.Instance.cameraSize.x / 2 + sizeSprite / 2, Main.Instance.cameraSize.x / 2 - sizeSprite / 2);
        float posY = Main.Instance.cameraSize.y / 2 + sizeSprite / 2;
        Vector2 pos = new Vector2(posX, posY);
        // расчет 
        float circleSpeed = speed / size;
        // создаем кружок
        GameObject circle = new GameObject("Circle");
        circle.AddComponent<Circle>();
        circle.GetComponent<Circle>().Init(pos, spr, clr, size, circleSpeed);
        circle.transform.SetParent(circlesParentTransformObject);

        DOVirtual.DelayedCall(1f, CreateFalingCircle);
    }
}
