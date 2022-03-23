using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    private int RandomPowerup;

    private Renderer pickupColour;

    // Start is called before the first frame update
    void Start()
    {
        RandomPowerup = Random.Range(0, 2);
        pickupColour = GetComponent<Renderer>();
        ColourPickup();
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

    void ColourPickup()
    {
        if (RandomPowerup == 0)
        {
            pickupColour.material.SetColor("_Color", Color.red);
        }

        if (RandomPowerup == 1)
        {
            pickupColour.material.SetColor("_Color", Color.blue);
        }

        if (RandomPowerup == 2)
        {

        }
    }

    void GetPickup()
    {
        GameManager.PowerUpType poweruptype = (GameManager.PowerUpType)RandomPowerup;
        AudioManager.instance.Play("PickupCollect");
        GameManager.GameEvents.GetPickup(poweruptype);
        Destroy(gameObject);
    }
}
