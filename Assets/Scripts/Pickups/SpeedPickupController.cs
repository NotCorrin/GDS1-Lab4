using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickupController : PickupController
{
    private void Awake()
    {
        powerupType = GameManager.PowerUpType.speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
