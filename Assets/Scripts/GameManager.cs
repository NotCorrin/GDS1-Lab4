using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Generator { left, centre, right};

    static GameEventsSystem GameEvents = new GameEventsSystem();
    static MenuEventsSystem MenuEvents = new MenuEventsSystem();
    static ScoreEventsSystem ScoreEvents = new ScoreEventsSystem();

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            ScoreEvents.ScoreChanged();
        }
    }

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
        
    }

    private void OnPlayerHit()
    {

    }

    private void OnGeneratorHit( Generator generator )
    {
        
    }

    private void OnGeneratorDestoryed(Generator generator)
    {

    }
}
