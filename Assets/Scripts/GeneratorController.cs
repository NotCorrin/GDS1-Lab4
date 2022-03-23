using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    private Transform Emitter;

    private float counter;

    [SerializeField]
    private float emitSpeed = 4;

    [SerializeField]
    private GameObject Bullet;
    
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
}
