using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;

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
        if (capsuleCollider == null)
            Debug.Log("왤깡");
    }

    void Update()
    {
        // ----- Walk -----

        float h = Input.GetAxisRaw("Horizontal");
        if (!isBounce)
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (h == 0 && !isBounce)
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        
        float maxSpeed = 4.5f;
        if (rigid.velocity.x > maxSpeed && !isBounce)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1) && !isBounce)
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        
        if (h != 0)
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        
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
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) {
                if (rayHit.distance > 0.5f) {
                    animator.SetBool("isJumping", false);
                    if (isBounce)
                        isBounce = false;
                }
            }
        }
    }

    // ----- Interact With Enemy -----

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Enemy" && rigid.velocity.y < 0 && rigid.position.y > collision.transform.position.y)
                OnAttack(collision.transform);
            else
                OnDamaged(collision.transform.position);
        }
    }

    void OnAttack(Transform enemyTransform)
    {
        gameManager.stagePoint += 100;
        rigid.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.OnDamaged();
        // Sound
    }

    void OnDamaged(Vector2 enemyPosition)
    {
        gameManager.HpDown();

        // Immortal Active
        gameObject.layer = 7; // PlayerDamaged
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 2);

        // To Bounce
        isBounce = true;
        Debug.Log("Player: " + transform.position.x);
        Debug.Log("Enemy: " + enemyPosition.x);
        int direction = transform.position.x - enemyPosition.x > 0 ? 1 : -1;
        Debug.Log("direction: " + direction);
        rigid.AddForce(new Vector2(direction*5, 10), ForceMode2D.Impulse);

        // Animation
        animator.SetTrigger("doDamaged");

        // Sound
    }

    void OffDamaged()
    {
        gameObject.layer = 6; // Player
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie() // GameManager - HpDown
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        capsuleCollider.enabled = false;
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }

    // ----- Coin Trigger -----

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") {
            CollectCoin(collision.gameObject);
        } else if (collision.gameObject.tag == "Finish") {
            gameManager.NextStage();
        }
    }

    void CollectCoin(GameObject coin)
    {
        if (coin.name == "Bronze")
            gameManager.stagePoint += 50;
        else if (coin.name == "Silver")
            gameManager.stagePoint += 100;
        else if (coin.name == "Gold")
            gameManager.stagePoint += 200;
        
        coin.SetActive(false);
    }
}
