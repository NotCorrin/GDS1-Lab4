using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 2, homingSpeed = 2;
    
    private Transform player;

    private float border;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        border = Camera.main.orthographicSize;

        fallSpeed = (transform.position.y - (Camera.main.transform.position.y - border)) / fallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float _homing = Mathf.Sign(transform.position.x - player.position.x);
        if(Mathf.Abs(transform.position.x - player.position.x) < homingSpeed * Time.deltaTime) 
        {
            transform.position = new Vector3(player.position.x, transform.position.y);
            transform.position += new Vector3(0, -fallSpeed) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(_homing * -homingSpeed, -fallSpeed) * Time.deltaTime;
        }
    }
    
    void LateUpdate()
    {
        if(Mathf.Abs(transform.position.y) > border) Destroy(gameObject);
    }

    protected virtual void UpdateScores(string otherTag)
    {
        if(otherTag.Equals("Player"))
        {
            //GameManager.GameEvents.PlayerHit();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        UpdateScores(other.gameObject.tag);
    }
}
