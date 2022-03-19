using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEventsSystem : MonoBehaviour
{
    public event Action onGammeStarted;
    public void GameStart()
    {
        if (onGammeStarted != null)
        {
            onGammeStarted();
        }
    }

    public event Action onGameQuit;
    public void GameQuit()
    {
        if (onGammeStarted != null)
        {
            onGameQuit();
        }
    }

    public event Action onReturnToMenu;
    public void ReturnToMenu()
    {
        if (onGammeStarted != null)
        {
            onReturnToMenu();
        }
    }
}
