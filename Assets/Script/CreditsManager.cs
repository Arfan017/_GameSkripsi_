using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public float scrollSpeed = 20f; // Kecepatan scroll teks
    public float endTime = 50f; // Waktu berapa lama Credit Scene ditampilkan sebelum kembali ke scene utama
    public GameObject PanelCredit;
    public GameObject ParentCredit;
    private RectTransform contentTransform;
    private Vector3 OriginalPosition;
    private string SceneName;
    private QuizManagar quizManagar;

    private void Start()
    {
        OriginalPosition = PanelCredit.transform.position;
        contentTransform = PanelCredit.GetComponent<RectTransform>();
        SceneName = SceneManager.GetActiveScene().name;

        if (SceneName == "MainMenu")
        {
            // Menjalankan coroutine untuk menggerakkan teks secara otomatis
            StartCoroutine(ScrollCredits());

            // Memulai countdown untuk kembali ke scene utama setelah waktu yang ditentukan
            Invoke("DisableCreditScene", endTime);
        } else
        {
            quizManagar = FindObjectOfType<QuizManagar>();
            StartCoroutine(ScrollCredits());
            Invoke("goToMainMenu", endTime);
        }
    }

    private IEnumerator ScrollCredits()
    {
        float targetPosition = contentTransform.rect.height;

        while (contentTransform.anchoredPosition.y < targetPosition)
        {
            contentTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }
        DisableCreditScene();
    }

    private void DisableCreditScene()
    {
        ParentCredit.SetActive(false);
        PanelCredit.transform.position = OriginalPosition;
    }

    private void goToMainMenu(){
        quizManagar.BtnNo(0);
    }
}