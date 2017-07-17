using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameExtensions
{
    //
    // Summary:
    //     ///
    //     Shorthand for writing Vector3(0, 0, 1).
    //     ///
    public static RectTransform RectTransform(this Component c)
    {
        return c.GetComponent<RectTransform>();
    }

    public static Image Image(this Component c)
    {
        return c.GetComponent<Image>();
    }

    public static Button Button(this Component c)
    {
        return c.GetComponent<Button>();
    }

    public static CanvasGroup CanvasGroup(this Component c)
    {
        return c.GetComponent<CanvasGroup>();
    }


	//
    // Summary:
    //     ///
    //     Shorthand for writing Vector3(value, value, value).
    //     ///
    public static Vector3 SetAllAxis(this Vector3 v, float value)
    {
        return new Vector3(value, value, value);
    }
}