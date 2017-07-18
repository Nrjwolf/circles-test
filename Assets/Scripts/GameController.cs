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
    [SerializeField] [Range(0.01f, 0.1f)] float speedStep;
    [SerializeField] [Range(1f, 10f)] float speedMax;

    // Параметры игры
    private bool isGame = false;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseDown();
    }

    // создаем визуальный эффект на клик мыши
    private void OnMouseDown()
    {
        if (!Main.Instance.bundleLoaded)
            return;

        // преобразовываем координаты мыши в игровые координаты
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // создаем кружок
        var go = CreateCircle();
        var circle = go.GetComponent<Circle>();
        circle.Init(pos, Color.white, .05f, 0);
        circle.Destroy();
    }

    private void InitGame()
    {
        // находим компоненты
        view = FindObjectOfType<GameView>();

        view.Init(); // инициализация визульной части сцены
        view.AddPlayButton();
        view.OnPlayButtonClicked += StartGame;
    }

    private void StartGame()
    {
        view.AddScoreItemsOnScene();
        CreateFalingCircle();

        isGame = true;
    }

    // обновляем счетчик времени
    private void UpdateTimer()
    {
        if (!isGame)
            return;
        dt = dt.AddSeconds(1);
        view.UpdateTime(dt.ToString("mm:ss"));

        if (speed < speedMax)
            speed += speedStep;
    }

    private void UpdateScore(int _plus)
    {
        score += _plus;
        view.UpdateScore(score.ToString());
    }

    // создаем падающий кружок с игровыми параметрами
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
        // расчет скорости
        float circleSpeed = speed / size;
        // создаем кружок
        Circle circle = CreateCircle().GetComponent<Circle>();
        circle.Init(pos, clr, size, circleSpeed);
        circle.transform.SetParent(circlesParentTransformObject);
        // подписываемся на событие клика по шарику
        circle.OnClicked += UpdateScore;
        Invoke("CreateFalingCircle", 0.5f);
    }

    private GameObject CreateCircle()
    {
        var go = new GameObject("Circle");
        Circle circle = go.AddComponent<Circle>();
        Sprite spr = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/whiteOval.png"); // подгружаем спрайт
        circle.SetSprite(spr);
        return go;
    }
}
