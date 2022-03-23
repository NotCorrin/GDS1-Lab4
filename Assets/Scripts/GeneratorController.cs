using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public Transform[] Emitter;

    private float counter;

    [SerializeField]
    private float emitSpeed = 4;

    [SerializeField]
    private GameObject Bullet;

    private int GeneratorToShoot;
    
    void Awake()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float Ortho = Camera.main.orthographicSize * 2;

        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(Ortho * screenRatio * .5f, Ortho);
        box.offset = new Vector2(Ortho * screenRatio * .25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= emitSpeed)
        {
            Instantiate(Bullet, Emitter[GeneratorToShoot].position, Emitter[GeneratorToShoot].rotation);
            counter = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Player") && GeneratorToShoot != 2)
        {
            GeneratorToShoot = 1;
        }
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("Player") && GeneratorToShoot != 2)
        {
            GeneratorToShoot = 0;
        }
    }
}
