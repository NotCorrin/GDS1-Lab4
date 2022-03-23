using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{
    // Variables
    [SerializeField] private List<Image> background;
    [SerializeField] private Text score;
    
    private float colorTimer;

    public enum ColorState
    {
        Orange,
        Blue
    }

    private ColorState colorState = ColorState.Orange;

    private Color orange = new Color(255 / 255f, 175 / 255f, 64 / 255f);
    private Color blue = new Color(24 / 255f, 220 / 255f, 255 / 255f);

    void Start()
    {
        if (background.Count == 0)
        {
            Debug.LogError("No background objects found!");
        }
        if (!score)
        {
            Debug.LogError("No score object found!");
        }

        score.text = GameManager.Score.ToString();

        GameManager.MenuEvents.ColorShift();
    }

    void Update()
    {
        colorTimer += Time.deltaTime;
        if (colorTimer >= 15f)
        {
            GameManager.MenuEvents.ColorShift();
            colorTimer = 0;
        }
    }

    private void OnColorShift()
    {
        switch (colorState)
        {
            case ColorState.Orange:
                foreach (Image image in background)
                {
                    image.color = orange;
                }
                score.color = blue;
                colorState = ColorState.Blue;
                break;
            case ColorState.Blue:
                foreach (Image image in background)
                {
                    image.color = blue;
                }
                score.color = orange;
                colorState = ColorState.Orange;
                break;
        }
    }

    private void OnEnable() {
        SubscribeListeners();
    }

    private void OnDisable() {
        UnsubscribeListeners();
    }

    private void OnDestroy() {
        UnsubscribeListeners();
    }

    private void SubscribeListeners() {
        GameManager.MenuEvents.onColorShift += OnColorShift;
    }

    private void UnsubscribeListeners() {
        GameManager.MenuEvents.onColorShift -= OnColorShift;
    }
}
