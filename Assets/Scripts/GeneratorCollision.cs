using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject Shield;

    [SerializeField]
    private Transform Forcefield;
    public GameManager.Generator generator;
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if(collider2D.gameObject.CompareTag("PlayerBullet"))
        {
            GameManager.GameEvents.GeneratorHit(generator);
            if(Forcefield) Instantiate(Shield, new Vector3(transform.position.x, Forcefield.position.y), Quaternion.identity);
        }
    }
}
