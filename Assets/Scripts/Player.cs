using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    CapsuleCollider2D capsuleCollider;

    private bool isBounce = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // ----- Walk -----

        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (h == 0)
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        
        float maxSpeed = 4.5f;
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1))
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        
        if (h != 0)
            spriteRenderer.flipX = rigid.velocity.x < 0;
        
        if (h == 0)
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);
        
        // ----- Jump -----

        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping")) {
            rigid.AddForce(Vector2.up * 17f, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            // Sound
        }

        if (rigid.velocity.y < 0) {
            Debug.DrawRay(rigid.position, Vector3.down, Color.green);
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) {
                if (rayHit.distance > 0.5f)
                    animator.SetBool("isJumping", false);
            }
        }
    }

    // ----- Interact With Enemy -----

    void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Enemy" && rigid.velocity.y < 0 && rigid.position.y > collision.transform.position.y)
                OnAttack(collision.transform);
            else
                OnDamaged(collision.transform.position);
        }
    }

    void OnAttack(Transform enemy)
    {
        // Point
        rigid.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemy.OnDamaged();
        // Sound
    }

    void OnDamaged(Vector2 enemyPosition)
    {
        // Health Down

        // Immortal Active
        gameObject.layer = 8; // PlayerDamaged
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 2);

        // To Bounce
        isBounce = true;
        int direction = transform.position.x - enemyPosition.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(direction*5, 10), ForceMode2D.Impulse);

        // Animation
        animator.SetTrigger("doDamaged");

        // Sound
    }

    void OffDamaged()
    {
        gameObject.layer = 7; // Player
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
