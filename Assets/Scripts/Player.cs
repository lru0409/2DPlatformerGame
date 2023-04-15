using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public GameManager gameManager;
	public StageManager stageManager;

    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;

    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D capsuleCollider;
    Rigidbody2D rigid;
    Animator animator;
    AudioSource audioSource;

    private bool isBounce = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
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
            PlaySound("JUMP");
        }

        if (rigid.velocity.y < 0) {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) {
                animator.SetBool("isJumping", false);
                if (isBounce)
                    isBounce = false;
            }
        }
    }

	public void Reposition()
	{
		spriteRenderer.flipX = false;
		transform.position = new Vector3(0, 0, 0);
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
		stageManager.point += 100;
        rigid.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.OnDamaged();
        PlaySound("ATTACK");
    }

    void OnDamaged(Vector2 enemyPosition)
    {
		stageManager.HpDown();
        animator.SetTrigger("doDamaged");
        PlaySound("DAMAGED");

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
        PlaySound("DIE");
    }

    // ----- Coin, Finish Trigger -----

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") {
            CollectCoin(collision.gameObject);
        } else if (collision.gameObject.tag == "Finish") {
			stageManager.ClearStage();
            PlaySound("FINISH");
        }
    }

    void CollectCoin(GameObject coin)
    {
        if (coin.name == "Bronze")
            stageManager.point += 50;
        else if (coin.name == "Silver")
            stageManager.point += 100;
        else if (coin.name == "Gold")
            stageManager.point += 200;
        
        PlaySound("ITEM");
        coin.SetActive(false);
    }

    // ----- Sound -----

    void PlaySound(string action)
    {
        switch (action) {
            case "JUMP":
                audioSource.clip = audioJump;
                break ;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break ;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break ;
            case "ITEM":
                audioSource.clip = audioItem;
                break ;
            case "DIE":
                audioSource.clip = audioDie;
                break ;
            case "FINISH":
                audioSource.clip = audioFinish;
                break ;
        }
        audioSource.Play();
    }
}