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

    private bool isInvul;
    public bool IsInvul
    {
        get => isInvul;
        private set
        {
            isInvul = value;
        }
    }

    private float invulTimer;
    [SerializeField] private float invulDuration;

    private bool isAntiWall;
    public bool IsAntiWall
    {
        get => isAntiWall;
        private set
        {
            isAntiWall = value;
        }
    }

    private bool isSpeedUp;
    public bool IsSpeedUp
    {
        get => isSpeedUp;
        private set
        {
            isSpeedUp = value;
        }
    }

    private float speedTimer;
    [SerializeField] private float speedDuration;

    private bool isGunUp;
    public bool IsGunUp
    {
        get => isGunUp;
        private set
        {
            isGunUp = value;
        }
    }

    private float gunUpTimer;
    [SerializeField] private float gunUpDuration;

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

    // Counting hits to generator

    private int leftGeneratorHits;
    public int LeftGeneratorHits
    {
        get => leftGeneratorHits;
        private set
        {
            leftGeneratorHits = value;
        }
    }

    [SerializeField] private int leftGeneratorMaxHits;
    public int LeftGeneratorMaxHits
    {
        get => leftGeneratorMaxHits;
    }

    private int rightGeneratorHits;
    public int RightGeneratorHits
    {
        get => rightGeneratorHits;
        private set
        {
            rightGeneratorHits = value;
        }
    }


    [SerializeField] private int rightGeneratorMaxHits;
    public int RightGeneratorMaxHits
    {
        get => rightGeneratorMaxHits;
    }

    private int centreGeneratorHits;
    public int CentreGeneratorHits
    {
        get => centreGeneratorHits;
        private set
        {
            centreGeneratorHits = value;
        }
    }

    [SerializeField] private int centreGeneratorMaxHits;
    public int CentreGeneratorMaxHits
    {
        get => centreGeneratorMaxHits;
    }

    private int score;
    public int Score
    {
        get => score;
        private set
        {
            score = Mathf.Max(value, 0);
            ScoreEvents.ScoreChanged();
        }
    }

    private int highScore;
    public int HighScore
    {
        get => highScore;
        private set
        {
            highScore = Mathf.Max(value, 0);
        }
    }

    private int lives;
    public int Lives
    {
        get => lives;
        private set
        {
            lives = Mathf.Min(value, 6);
            ScoreEvents.LivesChanged();
        }
    }
    private float borderTimer;

    private int topBorderMoveCount;
    public int TopBorderMoveCount
    {
        get => topBorderMoveCount;
        private set
        {
            topBorderMoveCount = value;
        }
    }

    private int bottomBorderMoveCount;
    public int BottomBorderMoveCount
    {
        get => bottomBorderMoveCount;
        private set
        {
            bottomBorderMoveCount = value;
        }
    }

    private float livesTimer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    private float secondTimer;

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
        UnsubscribeListeners();
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
        Lives = 4;
        borderTimer = 0;
        TopBorderMoveCount = 0;
        BottomBorderMoveCount = 0;
        LeftGeneratorHits = 0;
        RightGeneratorHits = 0;
        CentreGeneratorHits = 0;
    }

    private void OnGameOver()
    {
        if (Score < HighScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }

    private void OnPlayerHit()
    {
        livesTimer = 0;

        if (Lives == 0)
        {
            GameEvents.GameOver();
            Debug.Log("GameOver");
        }
        else
        {
            Lives--;
        }
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
