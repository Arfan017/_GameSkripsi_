using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public float moveSpeed = 500f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    public GameObject swordHitbox;
    public DamageableCharacter damageableCharacter;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;
    Collider2D swordCollider;
    bool isMoving = false;
    bool canMove = true;
    private GameManager gameManager;
    private int coinsCollected = 0;
    public int CoinsCollected
    {
        get => coinsCollected;
        set => coinsCollected = value;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
        damageableCharacter = GetComponent<DamageableCharacter>();
        GameEventsManager.instance.onCoinCollected += OnCoinCollected;
    }

    void FixedUpdate()
    {
        if (canMove == true && moveInput != Vector2.zero)
        {

            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > maxSpeed)
            {
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }

            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }

            IsMoving = true;
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("MoveScene"))
        {
            gameManager.IsTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("MoveScene"))
        {
            gameManager.IsTrigger = false;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void LoadData(GameData data)
    {
        if (this != null)
        {
            this.transform.position = data.playerPosition;
        }
    }

    public void SaveData(GameData data)
    {
        if (this != null)
        {
            data.playerPosition = this.transform.position;
        }
    }

    private void OnCoinCollected()
    {
        CoinsCollected++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("lockarea"))
        {
            Debug.Log(CoinsCollected);
        }
    }
}