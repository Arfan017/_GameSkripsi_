using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageableCharacter : MonoBehaviour, IDemageable, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public GameObject healthText;
    public GameObject panelLose;
    public Animator animator;
    public string nameHealthBar;
    public bool disableSimulated = false;
    public bool canTrunInvicible = false;
    public float invicibilityTime = 0.25f;
    public float _health = 5;
    public bool _targetable = true;
    public bool _invicible = false;
    Rigidbody2D rb;
    Collider2D pysicsCollider;
    bool isAlive = true;
    private float invicibileTimeElapsed = 0f;
    private bool isDie = false;
    private GameManager gameManager;
    private Image imageHealth;
    private GameObject HealthBar;

    public bool IsDie
    {
        get
        {
            return isDie;
        }

        set
        {
            isDie = value;
        }
    }

    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
                RectTransform textTransfrom = Instantiate(healthText).GetComponent<RectTransform>();

                textTransfrom.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

                Canvas canvas = GameObject.FindAnyObjectByType<Canvas>();
                textTransfrom.SetParent(canvas.transform);
            }

            _health = value;
            imageHealth.fillAmount = _health / 5f;

            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                Targetable = false;
                if (gameObject.CompareTag("Player"))
                {
                    panelLose.SetActive(true);
                }
            }
        }
        get
        {
            return _health;
        }
    }

    public bool Targetable
    {
        get
        {
            return _targetable;
        }
        set
        {
            _targetable = value;

            if (disableSimulated)
            {
                rb.simulated = false;
            }
            pysicsCollider.enabled = value;
        }
    }

    public bool Invicible
    {
        get
        {
            return _invicible;
        }

        set
        {
            _invicible = value;

            if (_invicible == true)
            {
                invicibileTimeElapsed = 0f;
            }
        }
    }

    public void Start()
    {
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        pysicsCollider = GetComponent<Collider2D>();
        gameManager = FindObjectOfType<GameManager>();
        // saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }

    void Awake()
    {
        HealthBar = GameObject.Find(nameHealthBar);
        imageHealth = HealthBar.GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    // public void SaveDataHealth()
    // {
    //     gameManager.DataHealth = Health;
    // }

    // public void LoadDataHealth()
    // {
    //     Health = gameManager.DataHealth;
    // }

    public void OnHit(float demage, Vector2 knockback)
    {
        if (!Invicible)
        {
            Health -= demage;

            rb.AddForce(knockback, ForceMode2D.Impulse);

            if (canTrunInvicible)
            {
                Invicible = true;
            }
        }
    }

    public void OnHit(float demage)
    {
        if (!Invicible)
        {
            Health -= demage;

            if (canTrunInvicible)
            {
                Invicible = true;
            }
        }
    }

    public void OnObjectDestroyed()
    {
        // Destroy(gameObject);
        IsDie = true;
        gameObject.SetActive(false);
        // if (gameObject.tag == tagEnemy)
        // {
        //     gameManager.EnemyDefeated(gameObject.tag);
        // }
    }

    public void FixedUpdate()
    {
        if (Invicible)
        {
            invicibileTimeElapsed += Time.deltaTime;
            if (invicibileTimeElapsed > invicibilityTime)
            {
                Invicible = false;
            }
        }
    }

    public void LoadDate(GameData data)
    {
        data.EnemyDefeat.TryGetValue(id, out isDie);

        if (isDie)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.dataHealth = Health;

        if (data.EnemyDefeat.ContainsKey(id))
        {
            data.EnemyDefeat.Remove(id);
        }
        data.EnemyDefeat.Add(id, IsDie);
    }
}