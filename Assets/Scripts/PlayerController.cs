using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float moveSpeedX = 5f;
    [SerializeField]
    float moveSpeedY = 3f;

    [Space]

    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletSpawnpoint;

    private Vector2 movement;
    
    [HideInInspector]
    public bool isBulletAlive;
    [HideInInspector]
    public bool controllBullet;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        // If there is a bullet active and you press spacebar
        // Stop the ship movement and controll the bullet
		if (Input.GetButton("Jump") && isBulletAlive) {
            controllBullet = true;
            movement = Vector2.zero;
            return;
		}

        controllBullet = false;
        
        // Move the ship
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Shoot a bullet
        if (Input.GetButtonDown("Jump") && isBulletAlive == false) {
            Shoot();
		}
    }

	private void FixedUpdate() {
        // Move the ship
        rb.MovePosition(rb.position + new Vector2(movement.x * moveSpeedX, movement.y * moveSpeedY) * Time.fixedDeltaTime);
	}

    private void Shoot() {
        // Spawn a bullet and let everyone know about it
        isBulletAlive = true;
        AudioManager.instance.Play("PlayerShoot");
        Instantiate(bullet, bulletSpawnpoint.position, Quaternion.identity);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		//Check for border
        if(collision.transform.tag == "Border") {
            // Tell the gamemanager
            // Die
		}

		//Check for enemy bullet
	}
}
