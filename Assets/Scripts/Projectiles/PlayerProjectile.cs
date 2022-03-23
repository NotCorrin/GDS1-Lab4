using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public bool isAntiWall;
    
    [Space]
    
    [SerializeField]
    float normalSpeed = 1.5f;
    [SerializeField]
    float controlSpeed = 3;

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
        // Checks whether or not we can move the bullet
        // Otherwise just move forward
		if (pc.controllBullet == false) {
            movement = Vector2.zero;
            return;
        }

        // Move the bullet 
        // Making sure that the bullet cannot move backwards
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Mathf.Clamp(Input.GetAxisRaw("Vertical"), 0, Mathf.Infinity);
    }

	private void FixedUpdate() {
        // Moves the bullet
        rb.MovePosition(rb.position + new Vector2(movement.x * controlSpeed, ((movement.y * controlSpeed * 0.7f) + normalSpeed)) * Time.fixedDeltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            return;
		}
		if (isAntiWall && collision.tag == "Shield") {
            return;
		}
        pc.isBulletAlive = false;
        pc.controllBullet = false;
        Destroy(gameObject);
	}
}
