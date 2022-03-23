using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    private Transform Emitter;
    public Transform Forcefield;

    private float counter;

    [SerializeField]
    private float emitSpeed = 4;

    [SerializeField]
    private GameObject Bullet, Shield;
    
    // Start is called before the first frame update
    void Awake()
    {
        Emitter = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= emitSpeed)
        {
            Instantiate(Bullet, Emitter.position, Emitter.rotation);
            counter = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("PlayerBullet"))
        {
            Instantiate(Shield, new Vector3(transform.position.x, Forcefield.position.y), Quaternion.identity, Forcefield);
        }
    }
}
