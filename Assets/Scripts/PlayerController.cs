using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float moveSpeedX = 5f;
    [SerializeField]
    float moveSpeedY = 3f;

    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform bulletSpawnpoint;

    private bool hasShotBullet;
    private Vector2 movement;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump")) {

		}
    }

	private void FixedUpdate() {
        rb.MovePosition(rb.position + new Vector2(movement.x * moveSpeedX, movement.y * moveSpeedY) * Time.fixedDeltaTime);
	}

    private void Shoot() {
        Instantiate(bullet, bulletSpawnpoint.position, Quaternion.identity);
	}
}
