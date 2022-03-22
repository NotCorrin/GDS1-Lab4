using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Variables
    [SerializeField] private List<Image> background;
    [SerializeField] private Text studio;
    [SerializeField] private Text logo;
    [SerializeField] private Animator ship;
    
    private float colorTimer;

    public enum ColorState
    {
        Orange,
        Yellow,
        Green,
        Blue
    }

    private ColorState colorState = ColorState.Blue;

    private Color orange = new Color(1, 0.5f, 0);
    private Color yellow = new Color(1, 1, 0);
    private Color green = new Color(0, 1, 0);
    private Color blue = new Color(0, 0, 1);

    void Start()
    {
        if (background.Count == 0)
        {
            Debug.LogError("No background objects found!");
        }
        if (!studio)
        {
            Debug.LogError("No studio object found!");
        }
        if (!logo)
        {
            Debug.LogError("No logo object found!");
        }
        if (!ship)
        {
            Debug.LogError("No ship object found!");
        }

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
        ship.SetTrigger("ChangeColor");

        switch (colorState)
        {
            case ColorState.Orange:
                foreach (Image image in background)
                {
                    image.color = yellow;
                }
                studio.color = orange;
                logo.color = blue;
                colorState = ColorState.Yellow;
                break;
            case ColorState.Yellow:
                foreach (Image image in background)
                {
                    image.color = green;
                }
                studio.color = yellow;
                logo.color = orange;
                colorState = ColorState.Green;
                break;
            case ColorState.Green:
                foreach (Image image in background)
                {
                    image.color = blue;
                }
                studio.color = green;
                logo.color = yellow;
                colorState = ColorState.Blue;
                break;
            case ColorState.Blue:
                foreach (Image image in background)
                {
                    image.color = orange;
                }
                studio.color = blue;
                logo.color = green;
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
