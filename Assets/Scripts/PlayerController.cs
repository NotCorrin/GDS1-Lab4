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

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && isBulletAlive == false) {
            Shoot();
		}
    }

	private void FixedUpdate() {
        rb.MovePosition(rb.position + new Vector2(movement.x * moveSpeedX, movement.y * moveSpeedY) * Time.fixedDeltaTime);
	}

    private void Shoot() {
        isBulletAlive = true;
        Instantiate(bullet, bulletSpawnpoint.position, Quaternion.identity);
	}
}
