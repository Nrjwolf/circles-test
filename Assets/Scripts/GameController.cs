using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/**
*   Script of game logic
*
*/

public class GameController : MonoBehaviour
{
    // Start game parametrs
    [SerializeField] Transform circlesParentTransformObject;
    [SerializeField] Color32[] colors;
    [SerializeField] [Range(0f, 1f)] float sizeMin;
    [SerializeField] [Range(0f, 1f)] float sizeMax;
    [SerializeField] float speed;
    [SerializeField] [Range(0.01f, 0.1f)] float speedStep;
    [SerializeField] [Range(1f, 10f)] float speedMax;

    // game parametrs
    private bool isGame = false;
    private int score;
    private int colorAvailable = 2;

    // Utils
    private System.DateTime dt;

    // Components
    private GameView view;

    void Start()
    {
        // subscribe to event of load success
        Main.Instance.OnBundleLoadedSuccessfully += InitGame;
        dt = new System.DateTime();
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseDown();
    }

    // Effect of mouse click
    private void OnMouseDown()
    {
        if (!Main.Instance.bundleLoaded)
            return;

        // converting mouse position to game position
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // creating circle
        var go = CreateCircle();
        var circle = go.GetComponent<Circle>();
        circle.Init(pos, Color.white, .05f, 0);
        circle.Destroy();
    }

    private void InitGame()
    {
        view = FindObjectOfType<GameView>();

        view.Init(); // init scene view
        view.AddPlayButton();
        view.OnPlayButtonClicked += StartGame;

        SoundController.instance.Init();
    }

    private void StartGame()
    {
        view.AddScoreItemsOnScene();
        CreateFalingCircle();

        isGame = true;
    }

    private void UpdateTimer()
    {
        if (!isGame)
            return;
        dt = dt.AddSeconds(1);
        view.UpdateTime(dt.ToString("mm:ss"));

        if (speed < speedMax)
            speed += speedStep;

        if(dt.Second > 0 && dt.Second % 10 == 0 && colorAvailable < colors.Length) 
            colorAvailable ++;
    }

    private void UpdateScore(int _plus)
    {
        score += _plus;
        view.UpdateScore(score.ToString());
    }

    // Creating circle with parametrs
    private void CreateFalingCircle()
    {
        Sprite spr = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/whiteOval.png"); // loading sprite
        Color32 clr = colors[Random.Range(0, colorAvailable)]; // choosing color
        float size = Random.Range(sizeMin, sizeMax);
        // calc of pos
        float sizeSprite = spr.rect.width * size / Main.PIXELS_PER_UNIT;
        float posX = Random.Range(-Main.Instance.cameraSize.x / 2 + sizeSprite / 2, Main.Instance.cameraSize.x / 2 - sizeSprite / 2);
        float posY = Main.Instance.cameraSize.y / 2 + sizeSprite / 2;
        Vector2 pos = new Vector2(posX, posY);
        // calc of speed
        float circleSpeed = speed / size;
        // add circle
        Circle circle = CreateCircle().GetComponent<Circle>();
        circle.Init(pos, clr, size, circleSpeed);
        circle.transform.SetParent(circlesParentTransformObject);
        // subscribe to click event
        circle.OnClicked += UpdateScore;
        Invoke("CreateFalingCircle", 0.5f);
    }

    private GameObject CreateCircle()
    {
        var go = new GameObject("Circle");
        Circle circle = go.AddComponent<Circle>();
        Sprite spr = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/whiteOval.png"); // loading sprite
        circle.SetSprite(spr);
        return go;
    }
}
