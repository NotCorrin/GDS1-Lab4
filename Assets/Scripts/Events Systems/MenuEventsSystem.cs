using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEventsSystem : MonoBehaviour
{
    public event Action onGameStarted;
    public void GameStart()
    {
        if (onGameStarted != null)
        {
            onGameStarted();
        }
    }

    public event Action onGameQuit;
    public void GameQuit()
    {
        if (onGameStarted != null)
        {
            onGameQuit();
        }
    }

    public event Action onReturnToMenu;
    public void ReturnToMenu()
    {
        if (onGameStarted != null)
        {
            onReturnToMenu();
        }
    }

    public event Action onColorShift;
    public void ColorShift()
    {
        if (onColorShift != null)
        {
            onColorShift();
        }
    }
}
