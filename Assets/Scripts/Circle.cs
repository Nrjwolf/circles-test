using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

/**
*   Circle script
*
*/

public class Circle : MonoBehaviour
{

    private Color32 color;
    private bool isAvailable = false;
    private float speed = 0.1f;
    private float deathPosition; //  lower border, when circle crosses it blows up

    private int scoresOnClick;
    public event Action<int> OnClicked;

    const float ANIMATION_TIME = 0.2f;

    // Some parametrs for game
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

    // explosive reaction for other circles
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

    // For animation of spikes (punch down)
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

    // Checking : is circle crossed the border
    private void CheckDeathPosition()
    {
        if (transform.position.y < deathPosition)
        {
            Destroy();
        }
    }

    // When clicked on circle — destroy it and give some scores
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

    // Destroy with animation
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
        SoundController.instance.PlaySound(SoundName.BLOB);
    }

}
