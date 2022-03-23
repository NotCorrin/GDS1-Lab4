using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject Shield;

    [SerializeField]
    private Transform Forcefield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("PlayerBullet") && Forcefield)
        {
            Instantiate(Shield, new Vector3(transform.position.x, Forcefield.position.y), Quaternion.identity);
        }
    }
}
