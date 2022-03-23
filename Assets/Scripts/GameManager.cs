using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { menu, playing };
    public enum Generator { left, centre, right};
    public enum PowerUpType { antiwall, invul, speed, gunup };
    public enum PlayerState { normal, dead }

    // Powerups bools

    static private bool isInvul;
    static public bool IsInvul
    {
        get => isInvul;
        private set
        {
            isInvul = value;
        }
    }

    static private float invulTimer;
    [SerializeField] static private float invulDuration;

    static private bool isAntiWall;
    static public bool IsAntiWall
    {
        get => isAntiWall;
        private set
        {
            isAntiWall = value;
        }
    }

    static private bool isSpeedUp;
    static public bool IsSpeedUp
    {
        get => isSpeedUp;
        private set
        {
            isSpeedUp = value;
        }
    }

    static private float speedTimer;
    [SerializeField] static private float speedDuration;

    static private bool isGunUp;
    static public bool IsGunUp
    {
        get => isGunUp;
        private set
        {
            isGunUp = value;
        }
    }

    static private float gunUpTimer;
    [SerializeField] static private float gunUpDuration;

    static public GameEventsSystem GameEvents = new GameEventsSystem();
    static public MenuEventsSystem MenuEvents = new MenuEventsSystem();
    static public ScoreEventsSystem ScoreEvents = new ScoreEventsSystem();

    static private GameState currentGameState = GameState.menu;
    static public GameState CurrentGameState
    {
        get => currentGameState;
        private set
        {
            currentGameState = value;
        }
    }

    static private PlayerState currentPlayerState;
    static public PlayerState CurrentPlayerState
    {
        get => CurrentPlayerState;
        private set
        {
            CurrentPlayerState = value;
        }
    }

    // Counting hits to generator

    static private int leftGeneratorHits;
    static public int LeftGeneratorHits
    {
        get => leftGeneratorHits;
        private set
        {
            leftGeneratorHits = value;
        }
    }

    [SerializeField] static private int leftGeneratorMaxHits = 20;
    static public int LeftGeneratorMaxHits
    {
        get => leftGeneratorMaxHits;
    }

    static private int rightGeneratorHits;
    static public int RightGeneratorHits
    {
        get => rightGeneratorHits;
        private set
        {
            rightGeneratorHits = value;
        }
    }


    [SerializeField] static private int rightGeneratorMaxHits = 20;
    static public int RightGeneratorMaxHits
    {
        get => rightGeneratorMaxHits;
    }

    static private int centreGeneratorHits;
    static public int CentreGeneratorHits
    {
        get => centreGeneratorHits;
        private set
        {
            centreGeneratorHits = value;
        }
    }

    [SerializeField] static private int centreGeneratorMaxHits = 20;
    static public int CentreGeneratorMaxHits
    {
        get => centreGeneratorMaxHits;
    }

    static private int score;
    static public int Score
    {
        get => score;
        private set
        {
            score = Mathf.Max(value, 0);
            ScoreEvents.ScoreChanged();
        }
    }

    static private int highScore;
    static public int HighScore
    {
        get => highScore;
        private set
        {
            highScore = Mathf.Max(value, 0);
        }
    }

    static private int lives;
    static public int Lives
    {
        get => lives;
        private set
        {
            lives = Mathf.Min(value, 6);
            ScoreEvents.LivesChanged();
        }
    }

    static private float borderTimer;

    static private int topBorderMoveCount;
    static public int TopBorderMoveCount
    {
        get => topBorderMoveCount;
        private set
        {
            topBorderMoveCount = value;
        }
    }

    static private int bottomBorderMoveCount;
    static public int BottomBorderMoveCount
    {
        get => bottomBorderMoveCount;
        private set
        {
            bottomBorderMoveCount = value;
        }
    }

    static private float livesTimer;

    static private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    static private float secondTimer;

    static private float deathTimer;

    private void OnEnable()
    {

        // Subscribing to Events
        SubscribeListeners();
            
    }

    private void OnDisable()
    {
        UnsubscribeListeners();
    }

    private void OnDestroy()
    {
        //UnsubscribeListeners();
    }

    private void SubscribeListeners()
    {
        GameEvents.onPlayerHit += OnPlayerHit;
        GameEvents.onGeneratorHit += OnGeneratorHit;
        GameEvents.onGeneratorDestroyed += OnGeneratorDestoryed;
        GameEvents.onPickupEnd += OnPickupEnd;
        GameEvents.onGetPickup += OnPickupGet;
        GameEvents.onGameStart += OnGameStart;
        GameEvents.onGameOver += OnGameOver;
        GameEvents.onTopWallShrink += OnTopWallShrink;
        GameEvents.onPlayerDeath += OnPlayerDeath;
    }

    private void UnsubscribeListeners()
    {
        GameEvents.onPlayerHit -= OnPlayerHit;
        GameEvents.onGeneratorHit -= OnGeneratorHit;
        GameEvents.onGeneratorDestroyed -= OnGeneratorDestoryed;
        GameEvents.onPickupEnd -= OnPickupEnd;
        GameEvents.onGetPickup -= OnPickupGet;
        GameEvents.onGameStart -= OnGameStart;
        GameEvents.onGameOver -= OnGameOver;
        GameEvents.onTopWallShrink -= OnTopWallShrink;
        GameEvents.onPlayerDeath -= OnPlayerDeath;
    }
    /// <summary>
    /// Above: Setting up variables and listeners. Below: GameManager responding to and calling listeners.
    /// </summary>
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
                if ((livesTimer += Time.deltaTime) >= 20)
                {
                    Lives++;
                    livesTimer = 0;
                }

            }

            if (CurrentPlayerState == PlayerState.dead)
            {
                if ((deathTimer += Time.deltaTime) >= 2) GameEvents.GameOver();
            }


            //Not entirely sure if += works properly here
            if (LeftGeneratorHits < LeftGeneratorMaxHits && RightGeneratorHits < RightGeneratorMaxHits)
            {
                if ((borderTimer += Time.deltaTime) >= 15)
                {
                    TopBorderMoveCount++;
                    GameEvents.TopWallShrink();
                    borderTimer = 0;
                }
            }
            else
            {
                if ((borderTimer += Time.deltaTime) >= 2)
                {
                    BottomBorderMoveCount++;
                    GameEvents.TopWallShrink();
                    borderTimer = 0;
                }
            }

            if (IsInvul)
            {
                if ((invulTimer -= Time.deltaTime) >= invulDuration) GameEvents.PickupEnd(PowerUpType.invul);
            }

            if (IsSpeedUp)
            {
                if ((speedTimer -= Time.deltaTime) >= speedDuration) GameEvents.PickupEnd(PowerUpType.speed);
            }

            if (IsGunUp)
            {
                if ((gunUpTimer -= Time.deltaTime) >= gunUpDuration) GameEvents.PickupEnd(PowerUpType.gunup);
            }

            if ((secondTimer += Time.deltaTime) >= 1)
            {
                Score += Lives;
                secondTimer -= 1;
            }
        }
        

    }

    private void OnGameStart()
    {
        Score = 0;
        livesTimer = 0;
        deathTimer = 0;
        Lives = 6;
        borderTimer = 0;
        TopBorderMoveCount = 0;
        BottomBorderMoveCount = 0;
        LeftGeneratorHits = 0;
        RightGeneratorHits = 0;
        CentreGeneratorHits = 0;
        CurrentPlayerState = PlayerState.normal;
        CurrentGameState = GameState.playing;
    }

    private void OnGameOver()
    {
        if (Score < HighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }

        CurrentGameState = GameState.menu; ;
    }

    private void OnPlayerHit()
    {
        livesTimer = 0;

        if (Lives == 0)
        {
            GameEvents.PlayerDeath();
            Debug.Log("GameOver");
        }
        else
        {
            Lives--;
        }
    }

    private void OnPlayerDeath()
    {
        CurrentPlayerState = PlayerState.dead;
    }

    private void OnPickupEnd(PowerUpType powerup)
    {
        switch (powerup)
        {
            case PowerUpType.antiwall:
                IsAntiWall = true;
                break;
            case PowerUpType.invul:
                IsInvul = false;
                break;
            case PowerUpType.speed:
                IsSpeedUp = false;
                break;
            case PowerUpType.gunup:
                IsGunUp = false;
                break;
        }
    }

    private void OnPickupGet(PowerUpType powerup)
    {
        switch (powerup)
        {
            case PowerUpType.antiwall:
                IsAntiWall = true;
                break;
            case PowerUpType.invul:
                IsInvul = true;
                gunUpTimer = 0;
                break;
            case PowerUpType.speed:
                IsSpeedUp = true;
                speedTimer = 0;
                break;
            case PowerUpType.gunup:
                IsGunUp = true;
                gunUpTimer = 0;
                break;
        }
    }

    private void OnTopWallShrink()
    {
        Score -= 90;
    }

    private void OnGeneratorHit( Generator generator )
    {


        switch(generator)
        {
            case Generator.left:
                LeftGeneratorHits++;
                borderTimer = 0;

                if (LeftGeneratorHits < LeftGeneratorMaxHits) Score += 10 * LeftGeneratorHits;
                else OnGeneratorDestoryed(Generator.left);
                break;

            case Generator.right:
                RightGeneratorHits++;
                borderTimer = 0;

                if (RightGeneratorHits < RightGeneratorMaxHits) Score += 10 * RightGeneratorHits;
                else OnGeneratorDestoryed(Generator.right);
                break;

            case Generator.centre:
                CentreGeneratorHits++;

                if (CentreGeneratorHits < CentreGeneratorMaxHits) Score += 10 * CentreGeneratorHits;
                else OnGeneratorDestoryed(Generator.centre);
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
