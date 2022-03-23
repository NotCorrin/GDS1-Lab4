using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider gen1Health;
    [SerializeField] private Slider gen2Health;
    [SerializeField] private Slider gen31Health;
    [SerializeField] private Slider gen32Health;

    [SerializeField] private Text score;
    [SerializeField] private List<Image> lives;

    void Start()
    {
        VerifyVariables();


    }

    void VerifyVariables()
    {
        if (gen1Health == null)
        {
            Debug.LogError("No gen1Health object found!");
        }
        if (gen2Health == null)
        {
            Debug.LogError("No gen2Health object found!");
        }
        if (gen31Health == null)
        {
            Debug.LogError("No gen31Health object found!");
        }
        if (gen32Health == null)
        {
            Debug.LogError("No gen32Health object found!");
        }
        if (score == null)
        {
            Debug.LogError("No score object found!");
        }
        if (lives.Count == 0)
        {
            Debug.LogError("No lives object found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onScoreChanged()
    {
        score.text = GameManager.Score.ToString();
    }

    private void onLivesChanged()
    {
        Debug.Log("Lives changed");
        for (int i = 0; i < lives.Count; i++)
        {
            Debug.Log("i: " + i);
            Debug.Log("GameManager.Lives: " + GameManager.Lives);
            if (i < GameManager.Lives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    private void onGeneratorHit(GameManager.Generator generator)
    {
        if (GameManager.LeftGeneratorHits < 20)
        {
            gen1Health.value = 20 - GameManager.LeftGeneratorHits;
        }
        else
        {
            gen1Health.value = 0;
        }
        if (GameManager.RightGeneratorHits < 20)
        {
            gen2Health.value = 20 - GameManager.RightGeneratorHits;
        }
        else
        {
            gen2Health.value = 0;
        }
        if (GameManager.CentreGeneratorHits < 20)
        {
            gen31Health.value = 20 - GameManager.CentreGeneratorHits / 2;
            gen32Health.value = 20 - GameManager.CentreGeneratorHits / 2;
        }
        else
        {
            gen31Health.value = 0;
            gen32Health.value = 0;
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
        GameManager.ScoreEvents.onScoreChanged += onScoreChanged;
        GameManager.ScoreEvents.onlivesChanged += onLivesChanged;
        GameManager.GameEvents.onGeneratorHit += onGeneratorHit;
    }

    private void UnsubscribeListeners() {
        GameManager.ScoreEvents.onScoreChanged -= onScoreChanged;
        GameManager.ScoreEvents.onlivesChanged -= onLivesChanged;
        GameManager.GameEvents.onGeneratorHit -= onGeneratorHit;
    }
}
