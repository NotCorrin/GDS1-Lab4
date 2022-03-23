using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        SubscribeListeners();
    }

    private void OnDisable()
    {
        UnsubscribeListeners();
    }

    private void SubscribeListeners()
    {
        GameManager.MenuEvents.onGameStarted += SceneChangeToSampleScene;
        GameManager.GameEvents.onGameOver += OnGameOver;
        GameManager.GameEvents.onGameWin += OnGameWin;
        GameManager.MenuEvents.onReturnToMenu += OnReturnToMenu;
    }

    private void UnsubscribeListeners()
    {
        GameManager.MenuEvents.onGameStarted -= SceneChangeToSampleScene;
        GameManager.GameEvents.onGameOver -= OnGameOver;
        GameManager.GameEvents.onGameWin -= OnGameWin;
        GameManager.MenuEvents.onReturnToMenu -= OnReturnToMenu;
    }

    private void SceneChangeToSampleScene()
    {
        SceneManager.LoadScene("PlayerTestScene");
    }

    private void OnGameOver()
    {
        SceneManager.LoadScene("LoseScene");
    }

    private void OnGameWin()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void OnReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
