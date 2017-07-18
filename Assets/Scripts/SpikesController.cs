using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using ListExtensions;

public class SpikesController : MonoBehaviour
{

    private List<BoxCollider2D> spikes;

    // эффект появления шипов
    public void Init()
    {
        spikes = transform.GetComponentsInChildren<BoxCollider2D>().ToList();
        spikes.Sort((a, b) => string.Compare(a.name, b.name));
        for (int i = 0; i < spikes.Count; i++)
        {
            var item = spikes[i];
            item.GetComponent<Image>().enabled = true;
            item.GetComponent<Image>().sprite = Main.Instance.bundle.LoadAsset<Sprite>("assets/sprites/triangle.png");
            var achorPos = item.GetComponent<RectTransform>().anchoredPosition;
            item.GetComponent<RectTransform>().DOAnchorPosY(achorPos.y * 4, 0.2f).From().SetDelay(i * 0.02f);
        }
    }

    public void SpikeAnimation(BoxCollider2D _box)
    {
        var box = spikes.Find(b => b == _box);
        box.GetComponent<RectTransform>().DOPunchAnchorPos(new Vector2(0, -10), 0.2f, 5, 0.8f);
    }

}
