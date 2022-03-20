using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public bool isAntiWall;

    [SerializeField]
    float normalSpeed = 1.5f;

    Vector2 movement;

    private PlayerController pc;
    private Rigidbody2D rb;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

	private void FixedUpdate() {
        rb.MovePosition(rb.position + new Vector2(0, normalSpeed) * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            return;
		}

        pc.isBulletAlive = false;
        Destroy(gameObject);
	}
}
