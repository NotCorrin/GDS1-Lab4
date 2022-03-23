using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    int RandomPowerup;
    // Start is called before the first frame update
    void Start()
    {
        RandomPowerup = Random.Range(0, 4);
        //RandomisePowerup();
        //GameManager.GameEvents.GetPickup += GetPickup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetPickup();
        }
    }

    void GetPickup()
    {
        GameManager.GameEvents.GetPickup((GameManager.PowerUpType)1);
        Destroy(gameObject);
    }
}
