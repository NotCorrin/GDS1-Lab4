using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEventsSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action onScoreChanged;
    public void ScoreChanged()
    {
        if (onScoreChanged != null)
        {
            onScoreChanged();
        }
    }

    public event Action onlivesChanged;
    public void LivesChanged()
    {
        if (onlivesChanged != null)
        {
            onlivesChanged();
        }
    }
}
