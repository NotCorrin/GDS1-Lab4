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
        transform.position += new Vector3(_homing * -homingSpeed, -fallSpeed) * Time.deltaTime;
    }
    
    void LateUpdate()
    {
        if(Mathf.Abs(transform.position.y) > border) Destroy(gameObject);
    }

    protected virtual void UpdateScores(string otherTag)
    {
        if(otherTag == "Player")
        {
            GameManager.GameEvents.PlayerHit();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UpdateScores(other.tag);
    }
}
