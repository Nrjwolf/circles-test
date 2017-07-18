using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

/**
*   Скрипт круожочка
*
*/

public class Circle : MonoBehaviour
{

    private Color32 color;
    private bool isAvailable = false;
    private float speed = 0.1f;
    private float deathPosition; // нижняя граница, при пересечении шарик лопается

    private int scoresOnClick;
    public event System.Action<int> OnClicked;

    const float ANIMATION_TIME = 0.2f;

    // Присваивание параметров для игры
    public void Init(Vector2 _postition, Color32 _color, float _size, float _speed)
    {
        var spr = gameObject.GetComponent<SpriteRenderer>();

        transform.position = _postition;
        transform.localScale = new Vector2(_size, _size);
        spr.color = _color;
        speed = _speed;

        float sizeSprite = spr.sprite.rect.width * _size / Main.PIXELS_PER_UNIT;
        // расчет позиции уничтожения кружочка
        var spikeSize = 0.35f;
        deathPosition = -Main.Instance.cameraSize.y / 2 + sizeSprite / 2 + spikeSize;

        // колайдер
        var cldr = gameObject.AddComponent<CircleCollider2D>();
        cldr.isTrigger = true;
        cldr.radius = 1.15f;

        var _score = (int)(_speed / _size);
        scoresOnClick = _score > 0 ? _score : 1;

        isAvailable = true;
    }

    
    void Update()
    {
        if (!isAvailable)
            return;
        transform.position += Vector3.down * speed * Time.deltaTime;
        CheckDeathPosition();
    }

    // Взрывная цепь для других шаров
    private void ExplosionDamage(Vector3 center, float radius)
    {
        var hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var item in hitColliders)
        {
            var c = item.GetComponent<Circle>();
            if (c)
                c.Destroy();
        }
    }

    // Для эффекта проседания шипов
    private void SpikeEffect(Vector3 center, float radius)
    {
        var hitColliders = Physics2D.OverlapCircleAll(center, radius);
        foreach (var item in hitColliders)
        {
            var box = item.GetComponent<BoxCollider2D>();
            if (box)
            {
                FindObjectOfType<SpikesController>().SpikeAnimation(box);
            }
        }
    }

    // Проверка позиции, если перешел границу - взрываем
    private void CheckDeathPosition()
    {
        if (transform.position.y < deathPosition)
        {
            Destroy();
        }
    }

    // На клик по шарику - взрываем, прибавляем очки
    public void OnMouseDown()
    {
        if (!isAvailable)
            return;

        OnClicked(scoresOnClick);
        Destroy();
    }

    // Задаем нужный спрайт
    public void SetSprite(Sprite spr)
    {
        if (GetComponent<SpriteRenderer>() == null)
            gameObject.AddComponent<SpriteRenderer>();

        GetComponent<SpriteRenderer>().sprite = spr;
    }

    // Уничтоаем, анимируем, вызываем эффекты
    public void Destroy(float _delay = 0)
    {
        if (!isAvailable)
            return;

        isAvailable = false;
        GetComponent<SpriteRenderer>().DOFade(0, ANIMATION_TIME).SetDelay(_delay);
        SpikeEffect(transform.position, GetComponent<CircleCollider2D>().bounds.size.x / 2);
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.1f), ANIMATION_TIME, 2, 1).SetDelay(_delay).OnComplete(() =>
        {
            // эффект взрывной цепи
            ExplosionDamage(transform.position, GetComponent<CircleCollider2D>().bounds.size.x / 2);
            if (_delay != 0) // если взрыв вызван цепью, то даем очки
                OnClicked(scoresOnClick);
            Destroy(gameObject);
        });
    }

}
