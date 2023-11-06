using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntoManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider SliderLoading;
    public Button button;
    public TextMeshProUGUI dialogText;
    private Coroutine writeSentenceCoroutine = null;
    private Queue<string> sentences;
    public float DialogSpeed = 0.1f;
    private bool isTyping;
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
        // button.gameObject.SetActive(false);
    }

    public void StartDialog(Dialog dialog)
    {
        // dialogPanel.SetActive(true);

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
        // dialogText.text = sentence;
        writeSentenceCoroutine = StartCoroutine(TypeSentence(sentence));
    }

    void EndDialog()
    {
        button.gameObject.SetActive(true);
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

    public void OnClick()
    {
        DataParsistenceManager.instance.NewGame();
        StartCoroutine(LoadAsynchronously(1));
        // SceneManager.LoadSceneAsync("GamePlay");
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            SliderLoading.value = progress;
            yield return null;
        }
    }

}