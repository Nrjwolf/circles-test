using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUtils : MonoBehaviour
{
    /* Hex to Color */
    public static Color32 ToColor(int HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
    }

    /* Добавление объекта в канвас */
    public static GameObject AddOnCanvas(GameObject _canvas, GameObject _object)
    {
        GameObject _go = Instantiate(_object) as GameObject;
        Vector3 localScale = _object.transform.localScale; // первоначальный scale
        _go.transform.SetParent(_canvas.transform); // добавление в канвас
        _go.GetComponent<RectTransform>().anchoredPosition = _go.transform.position; // присваивание achor позиции
        _go.GetComponent<RectTransform>().localScale = localScale; // присваивание начального size
        return _go;
    }
}
