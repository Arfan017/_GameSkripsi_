using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    [SerializeField] private int requiredCoin;
    [SerializeField] private GameObject PanelUnlockArea;
    private bool open = false;
    private Collider2D col;
    private PlayerController playerController;
    int totalCoin;

    private void Awake()
    {
        col = this.GetComponent<Collider2D>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Start() {
        // totalCoin = ;
    }

    private void Update() {
        totalCoin = playerController.CoinsCollected;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelUnlockArea.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelUnlockArea.SetActive(false);
        }
    }

    public void UnlockArea()
    {
        if (requiredCoin == totalCoin)
        {
            Debug.Log("area terbuka");
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("requiredCoin => " + requiredCoin);
            Debug.Log("playerController.CoinsCollected => " + totalCoin);
            Debug.Log("coin yang dibutuhkan kurang");
        }
    }
}