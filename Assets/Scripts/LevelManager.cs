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

    private void OnDestroy()
    {
        UnsubscribeListeners();
    }

    private void SubscribeListeners()
    {
        GameManager.MenuEvents.onGameStarted += SceneChangeToSampleScene;
    }

    private void UnsubscribeListeners()
    {
        GameManager.MenuEvents.onGameStarted -= SceneChangeToSampleScene;
    }

    private void SceneChangeToSampleScene()
    {
        SceneManager.LoadScene("PlayerTestScene");
    }
}
