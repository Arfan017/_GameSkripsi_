using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits_Manager : MonoBehaviour
{
    private string SceneName;
    private QuizManagar quizManagar;
    public GameObject ParentCredit;
    private void Start()
    {
        SceneName = SceneManager.GetActiveScene().name;
    }

    private void EndCredits()
    {
        if (SceneName == "MainMenu")
        {
            ParentCredit.SetActive(false);
        }
        else
        {
            quizManagar = FindObjectOfType<QuizManagar>();
            quizManagar.BtnNo(0);
        }
    }
}