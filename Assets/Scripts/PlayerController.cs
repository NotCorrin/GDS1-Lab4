using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public float moveSpeedX = 5f;
    [SerializeField]
    public float moveSpeedY = 3f;

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
    }

	private void FixedUpdate() {
        rb.MovePosition(rb.position + new Vector2(movement.x * moveSpeedX, movement.y * moveSpeedY) * Time.fixedDeltaTime);
	}
}
