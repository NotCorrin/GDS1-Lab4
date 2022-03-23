using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { menu, playing };
    public enum Generator { left, centre, right};
    public enum PowerUpType { antiwall, speed, invul, gunup };
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
    static private float invulDuration = 3;

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
    static private float speedDuration = 3;

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
    static private float gunUpDuration = 3;

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

    static private PlayerState currentPlayerState = PlayerState.normal;
    static public PlayerState CurrentPlayerState
    {
        get => currentPlayerState;
        private set
        {
            currentPlayerState = value;
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

    static private int leftGeneratorMaxHits = 20;
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


    static private int rightGeneratorMaxHits = 20;
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

    static private int centreGeneratorMaxHits = 20;
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
        Debug.Log("subscribe");
        GameEvents.onPlayerHit += OnPlayerHit;
        GameEvents.onGeneratorHit += OnGeneratorHit;
        GameEvents.onGeneratorDestroyed += OnGeneratorDestoryed;
        GameEvents.onPickupEnd += OnPickupEnd;
        GameEvents.onGetPickup += OnPickupGet;
        MenuEvents.onGameStarted += OnGameStart;
        GameEvents.onGameOver += OnGameOver;
        GameEvents.onGameWin += OnGameOver;
        GameEvents.onTopWallShrink += OnTopWallShrink;
        GameEvents.onPlayerDeath += OnPlayerDeath;
    }

    private void UnsubscribeListeners()
    {
        Debug.Log("unsubscribe");
        GameEvents.onPlayerHit -= OnPlayerHit;
        GameEvents.onGeneratorHit -= OnGeneratorHit;
        GameEvents.onGeneratorDestroyed -= OnGeneratorDestoryed;
        GameEvents.onPickupEnd -= OnPickupEnd;
        GameEvents.onGetPickup -= OnPickupGet;
        MenuEvents.onGameStarted -= OnGameStart;
        GameEvents.onGameOver -= OnGameOver;
        GameEvents.onGameWin -= OnGameOver;
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
            if (LeftGeneratorHits < LeftGeneratorMaxHits || RightGeneratorHits < RightGeneratorMaxHits)
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
                    GameEvents.BottomWallShrink();
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
        AudioManager.instance.Play("PlayerBulletImpact");
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
        AudioManager.instance.Play("PlayerExplode");
        CurrentPlayerState = PlayerState.dead;
    }

    private void OnPickupEnd(PowerUpType powerup)
    {
        switch (powerup)
        {
            case PowerUpType.antiwall:
                IsAntiWall = false;
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
                Debug.Log("antiwall");
                break;
            case PowerUpType.invul:
                IsInvul = true;
                gunUpTimer = 0;
                Debug.Log("invul");
                break;
            case PowerUpType.speed:
                IsSpeedUp = true;
                speedTimer = 0;
                Debug.Log("speed");
                break;
            case PowerUpType.gunup:
                IsGunUp = true;
                gunUpTimer = 0;
                Debug.Log("gunup");
                break;
        }
    }

    private void OnTopWallShrink()
    {
        Score -= 90;
    }

    private void OnGeneratorHit( Generator generator )
    {
        AudioManager.instance.Play("GeneratorHit");

        switch (generator)
        {
            case Generator.left:
                LeftGeneratorHits++;
                borderTimer = 0;

                if (LeftGeneratorHits < LeftGeneratorMaxHits) Score += 10 * LeftGeneratorHits;
                else GameEvents.GeneratorDestroyed(Generator.left);
                break;

            case Generator.right:
                RightGeneratorHits++;
                borderTimer = 0;

                if (RightGeneratorHits < RightGeneratorMaxHits) Score += 10 * RightGeneratorHits;
                else GameEvents.GeneratorDestroyed(Generator.right);
                break;

            case Generator.centre:
                CentreGeneratorHits++;
                if (CentreGeneratorHits < CentreGeneratorMaxHits) Score += 10 * CentreGeneratorHits;
                else GameEvents.GeneratorDestroyed(Generator.centre);
                break;
        }
    }

    private void OnGeneratorDestoryed(Generator generator)
    {
        AudioManager.instance.Play("GeneratorExplode");
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
                GameEvents.GameWin();
                break;
        }
    }
}
