using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupController : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameManager.PowerUpType powerupType;

    private void Awake()
    {
        //GameManager.GameEvents
    }

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.GameEvents.GetPickup(powerupType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
