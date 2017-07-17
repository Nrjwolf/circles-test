using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtentions
{
    public static string RemoveLast(this string text, string character)
    {
        if (text.Length < 1) return text;
        return text.Remove(text.ToString().LastIndexOf(character), character.Length);
    }

    public static string GetShortStringOfNumber(decimal number)
    {
        var str = number.ToString();
        if (number >= 1000 && number < 10000m)
            return str.Insert(str.Length - 3, " ");
        if (number >= 10000m && number < 100000m)
            return str.Remove(str.Length - 3, 3) + GetRounded(new char[] { str[3], str[4] }, 1) + 'k';
        if (number >= 100000m && number < 1000000m)
            return str.Remove(str.Length - 3, 3) + GetRounded(new char[] { str[3], str[4] }, 1) + 'k';
        if (number >= 1000000m && number < 1000000000m)
            return str.Remove(str.Length - 6, 6) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + 'M';
        if (number >= 1000000000m && number < 1000000000000m)
            return str.Remove(str.Length - 9, 9) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "B";
        if (number >= 1000000000000m && number < 1000000000000000m)
            return str.Remove(str.Length - 12, 12) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "KB";
        if (number >= 1000000000000000m && number < 1000000000000000000m)
            return str.Remove(str.Length - 15, 15) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "MB";
        if (number >= 1000000000000000000m && number < 1000000000000000000000m)
            return str.Remove(str.Length - 18, 18) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "BB";
        if (number >= 1000000000000000000000m && number < 1000000000000000000000000m)
            return str.Remove(str.Length - 21, 21) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "KBB";
        if (number >= 1000000000000000000000000m && number < 1000000000000000000000000000m)
            return str.Remove(str.Length - 24, 24) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "MBB";
        if (number >= 1000000000000000000000000000m)
            return "infinity";
        // return str.Remove(str.Length - 18, 18) + GetRounded(new char[] { str[1], str[2], str[3] }, 1) + "KBB";
        return str;
    }

    private static string GetRounded(char[] c, int _decimal = 1)
    {
        string str = "";
        foreach (var ch in c)
            str += ch;

        double number = double.Parse("0." + str);
        number = System.Math.Round(number, _decimal);

        if (number == 0)
            str = "";
        else if (number == 1)
            str = ".9";
        else
            str = number.ToString().Remove(0, 1);

        return str;
    }
}
