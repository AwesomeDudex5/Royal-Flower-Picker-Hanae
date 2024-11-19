using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameValues
{
    private static GameValues _current;

    public static int currentLevel { get; set; }
    public static int currentPinkFlowers { get; set; }
    public static int currentRedFlowers { get; set; }
    public static int currentBlueFlowers { get; set; }
    public static int currentPurpleFlowers { get; set; }
    public static int currentYellowFlowers { get; set; }
    public static int currentWhiteFlowers { get; set; }


    public GameValues()
    {
        _current = this;
    }
}
