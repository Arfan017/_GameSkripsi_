using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManagar : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public List<string> options;
        public int correctOptionIndex;
    }

    public List<Question> questions;
    public TextMeshProUGUI questionText;
    public List<Button> optionButtons;
    public Animator animatorPlayer;
    public Animator animatorBoss;
    public TextMeshProUGUI textNilai;
    public AudioSource audioBossFight;
    public GameObject loadingPanel;
    public Slider SliderLoading;
    public float delayBeforeNextQuestion = 2f;
    private DataParsistenceManager dataParsistenceManager;
    private int currentQuestionIndex;
    private bool isAnswered;

    void Start()
    {
        dataParsistenceManager = FindObjectOfType<DataParsistenceManager>();
        currentQuestionIndex = 0;
        isAnswered = false;
        // ShuffleQuestions();
        ShowQuestion();
        audioBossFight.Play();
    }

    // void Update()
    // {
    // currentQuestionIndex = 0;
    // isAnswered = false;
    // ShuffleQuestions();
    // ShowQuestion();
    // textNilai.text = "Nilai: " + CalculateScore();
    // }

    void ShowQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < optionButtons.Count; i++)
            {
                if (i < currentQuestion.options.Count)
                {
                    optionButtons[i].gameObject.SetActive(true);
                    optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
                }
                else
                {
                    optionButtons[i].gameObject.SetActive(false);
                }
            }
            isAnswered = false;
        }
        else
        {
            // Debug.Log("Game Selesai! Skor Akhir: " + CalculateScore());
            // textNilai.text = "Nilai: " + CalculateScore();
        }
    }

    public void OnOptionSelected(int optionIndex)
    {
        if (!isAnswered)
        {
            isAnswered = true;

            Question currentQuestion = questions[currentQuestionIndex];

            if (optionIndex == currentQuestion.correctOptionIndex)
            {
                animatorPlayer.SetBool("PlayerAttack", true);
                // textNilai.text = "Nilai: " + CalculateScore();
            }

            else
            {
                animatorBoss.SetBool("BossAttack", true);
            }
            StartCoroutine(NextQuestionWithDelay());
        }
    }

    private IEnumerator NextQuestionWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextQuestion);

        currentQuestionIndex++;
        ShowQuestion();
    }

    public int CalculateScore()
    {
        int score = 0;

        foreach (Question question in questions)
        {
            if (question.correctOptionIndex == 0)
            {
                score++;
            }
        }
        return score;
    }

    public void BtnYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BtnNo(int sceneIndex)
    {
        // Destroy(dataParsistenceManager);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void StopAudio()
    {
        audioBossFight.Stop();
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

    // public void ShuffleQuestions()
    // {
    //     List<Question> shuffledQuestions = new List<Question>();

    //     List<int> questionIndices = new List<int>();
    //     for (int i = 0; i < questions.Count; i++)
    //     {
    //         questionIndices.Add(i);
    //     }

    //     while (questionIndices.Count > 0)
    //     {
    //         int randomIndex = Random.Range(0, questionIndices.Count);
    //         int questionIndex = questionIndices[randomIndex];
    //         shuffledQuestions.Add(questions[questionIndex]);
    //         questionIndices.RemoveAt(randomIndex);
    //     }

    //     questions = shuffledQuestions;
    // }

}