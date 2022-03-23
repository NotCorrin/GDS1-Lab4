using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsSystem : MonoBehaviour
{
    public event Action onGameOver;
    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }

    public event Action onGameWin;
    public void GameWin()
    {
        if (onGameWin != null)
        {
            onGameWin();
        }
    }

    public event Action onPlayerHit;
    public void PlayerHit()
    {
        if (onPlayerHit != null)
        {
            onPlayerHit();
        }
    }

    public event Action onPlayerDeath;
    public void PlayerDeath()
    {
        if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }

    public event Action<GameManager.Generator> onGeneratorHit;
    public void GeneratorHit(GameManager.Generator generator)
    {
        Debug.Log("Hit " + generator.ToString());
        if (onGeneratorHit != null)
        {
            onGeneratorHit(generator);
        }
    }

    public event Action<GameManager.Generator> onGeneratorDestroyed;
    public void GeneratorDestroyed(GameManager.Generator generator)
    {
        if (onGeneratorDestroyed != null)
        {
            onGeneratorDestroyed(generator);
        }
    }

    public event Action<int, bool> onWallHit;
    public void GeneratorDestroyed(int wallID, bool isAntiWall)
    {
        if (onWallHit != null)
        {
            onWallHit(wallID, isAntiWall);
        }
    }

    public event Action<GameManager.PowerUpType> onGetPickup;
    public void GetPickup(GameManager.PowerUpType powerupType)
    {
        if (onGetPickup != null)
        {
            onGetPickup(powerupType);
        }
    }

    public event Action<GameManager.PowerUpType> onPickupEnd;
    public void PickupEnd(GameManager.PowerUpType powerupType)
    {
        if (onPickupEnd != null)
        {
            onPickupEnd(powerupType);
        }
    }

    public event Action onTopWallShrink;
    public void TopWallShrink()
    {
        if (onTopWallShrink != null)
        {
            onTopWallShrink();
        }
    }

    public event Action onBottomWallShrink;
    public void BottomWallShrink()
    {
        if (onBottomWallShrink != null)
        {
            onBottomWallShrink();
        }
    }
}
