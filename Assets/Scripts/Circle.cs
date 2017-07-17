using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Circle : MonoBehaviour
{

    private Color32 color;
    private bool isAvailable = false;
    private float speed = 0.1f;
    private float deathPosition;

    public void Init(Vector2 _postition, Sprite _sprite, Color32 _color, float _size, float _speed)
    {
        var spr = gameObject.AddComponent<SpriteRenderer>();

        transform.position = _postition;
        transform.localScale = new Vector2(_size, _size);
        spr.sprite = _sprite;
        spr.color = _color;
        spr.sortingOrder = 1;
        speed = _speed;

        float sizeSprite = spr.sprite.rect.width * _size / Main.PIXELS_PER_UNIT;
        // расчет позиции уничтожения кружочка
        deathPosition = -Main.Instance.cameraSize.y / 2 + sizeSprite / 2;

        // колайдер
        var cldr = gameObject.AddComponent<CircleCollider2D>();
        cldr.isTrigger = true;
		cldr.radius = 1.15f;
    }

    void Start()
    {
        isAvailable = true;
    }

    public void OnMouseDown()
    {
        Destroy();
    }

    void Update()
    {
        if (!isAvailable)
            return;
        transform.position += Vector3.down * speed * Time.deltaTime;
        CheckDeathPosition();
    }

    private void CheckDeathPosition()
    {
        if (transform.position.y < deathPosition)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        isAvailable = false;
        float time = 0.2f;
        GetComponent<SpriteRenderer>().DOFade(0, time);
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.1f), time, 2, 1).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

}
