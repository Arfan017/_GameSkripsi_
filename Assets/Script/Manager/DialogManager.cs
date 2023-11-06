using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private bool isTyping;
    public TextMeshProUGUI dialogText;
    private Queue<string> sentences;
    private Coroutine writeSentenceCoroutine = null;
    public float DialogSpeed = 0.1f;
    private GameManager gameManager;

    public bool IsTyping
    {
        get
        {
            return isTyping;
        }
        set
        {
            isTyping = value;
        }
    }

    void Start()
    {
        sentences = new Queue<string>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartDialog(Dialog dialog)
    {
        gameManager.showPanelDialog();
        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        writeSentenceCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        IsTyping = true;
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(DialogSpeed);
        }
        IsTyping = false;
    }

    public void EndDialog()
    {
        gameManager.hidePanelDialog();
    }
}