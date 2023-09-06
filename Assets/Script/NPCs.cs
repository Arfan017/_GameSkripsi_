using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCs : MonoBehaviour
{
    public Dialog dialog;
    // public string EnemyTag;
    public float DialogSpeed;
    public int enemyCountToDefeat;
    private bool isCollision;
    private int defeatedEnemyCount;
    private GameManager gameManager;
    private DialogManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // defeatedEnemyCount = gameManager.EnemyCount[EnemyTag];

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueManager.IsTyping == true)
            {
                return;
            }
            else
            {
                dialogueManager.DisplayNextSentence();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueManager.StartDialog(dialog);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueManager.EndDialog();
        }
    }
}