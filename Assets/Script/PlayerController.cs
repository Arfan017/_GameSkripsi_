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
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;
    Collider2D swordCollider;
    bool isMoving = false;
    bool canMove = true;
    bool CollisionNPC;
    private GameManager gameManager;
    public DamageableCharacter damageableCharacter;
    Vector3 currentPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
        damageableCharacter = GetComponent<DamageableCharacter>();
    }

    void Update()
    {
        
    }

    // public void SaveDataPosition()
    // {
    //     gameManager.DataPosition = transform.position;
    // }

    // public void LoadDataPosition()
    // {
    //     transform.position = gameManager.DataPosition;
    // }

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

    public void LoadDate(GameData data)
    {
        transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        if (this != null)
        {
            data.playerPosition = transform.position;
        }
    }
}