using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { menu, playing };
    public enum Generator { left, centre, right};
    public enum PowerUpType { antiwall, invul, speed, gunup };
    public enum PlayerState { normal, dead, antiwall, invul, speed, gunup }

    static public GameEventsSystem GameEvents = new GameEventsSystem();
    static public MenuEventsSystem MenuEvents = new MenuEventsSystem();
    static public ScoreEventsSystem ScoreEvents = new ScoreEventsSystem();

    private GameState currentGameState;
    public GameState CurrentGameState
    {
        get => currentGameState;
        private set
        {
            currentGameState = value;
        }
    }

    private PlayerState currentPlayerState;
    public PlayerState CurrentPlayerState
    {
        get => CurrentPlayerState;
        private set
        {
            CurrentPlayerState = value;
        }
    }

    private int leftGeneratorHit;
    public int LeftGeneratorHit
    {
        get => leftGeneratorHit;
        set
        {
            leftGeneratorHit = value;
        }
    }

    private int rightGeneratorHit;
    public int RightGeneratorHit
    {
        get => rightGeneratorHit;
        set
        {
            rightGeneratorHit = value;
        }
    }

    private int centreGeneratorHit;
    public int CentreGeneratorHit
    {
        get => centreGeneratorHit;
        set
        {
            centreGeneratorHit = value;
        }
    }

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = Mathf.Max(value, 0);
            ScoreEvents.ScoreChanged();
        }
    }

    private int lives;
    public int Lives
    {
        get => lives;
        set
        {
            lives = Mathf.Min(value, 6);
            ScoreEvents.LivesChanged();
        }
    }
    private float topBorderTimer;

    private float livesTimer;

    private void Awake()
    {

        // Subscribing to Events
        GameEvents.onPlayerHit += OnPlayerHit;
        GameEvents.onGeneratorHit += OnGeneratorHit;
        GameEvents.onGeneratorDestroyed += OnGeneratorDestoryed;
            
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentGameState == GameState.playing)
        {
            if (lives < 6 && CurrentPlayerState != PlayerState.dead)
            {
                livesTimer -= Time.deltaTime;
                if (livesTimer <= 0)
                {
                    Lives++;
                    livesTimer = 20;
                }

            }
        }
    }

    private void OnPlayerHit()
    {
        if (Lives == 0)
        {
            Debug.Log("GameOver");
        }
        else
        {
            //Kill Player
        }
    }

    private void OnGeneratorHit( Generator generator )
    {
        switch(generator)
        {
            case Generator.left:
                Score += 10 * LeftGeneratorHit;
                break;
            case Generator.right:
                Score += 10 * RightGeneratorHit;
                break;
            case Generator.centre:
                Score += 10 * CentreGeneratorHit;
                break;
        }
    }

    private void OnGeneratorDestoryed(Generator generator)
    {
        switch (generator)
        {
            case Generator.left:
                Score += 2000;
                break;
            case Generator.right:
                Score += 2000;
                break;
            case Generator.centre:
                Score += 5000;
                break;
        }
    }
}
