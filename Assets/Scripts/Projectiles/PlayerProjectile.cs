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

    public Animator animator;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        animator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Checks whether or not we can move the bullet
        // Otherwise just move forward
		if (pc.controllBullet == false && GameManager.CurrentPlayerState == GameManager.PlayerState.normal) {
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

	private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Untagged") {
            return;
		    }
        
        if (isAntiWall && collision.gameObject.tag == "Shield") {
            return;
        }
        
        StartCoroutine(ReloadSprite());
	}

    IEnumerator ReloadSprite()
    {
        animator.SetBool("reload", true);

        AudioManager.instance.Play("PlayerBulletImpact");
        pc.isBulletAlive = false;
        pc.controllBullet = false;
        transform.GetComponentInChildren<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(0.2f);

        animator.SetBool("reload", false);
        Destroy(gameObject);
    }
}
