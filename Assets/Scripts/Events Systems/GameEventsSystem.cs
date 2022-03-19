using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsSystem : MonoBehaviour
{
    public event Action onPlayerHit;
    public void PlayerHit()
    {
        if (onPlayerHit != null)
        {
            onPlayerHit();
        }
    }

    public event Action<GameManager.Generator> onGeneratorHit;
    public void GeneratorHit( GameManager.Generator generator)
    {
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
}
