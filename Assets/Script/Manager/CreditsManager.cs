// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class CreditsManager : MonoBehaviour
// {
//     public float scrollSpeed = 20f; // Kecepatan scroll teks
//     public float endTime = 50f; // Waktu berapa lama Credit Scene ditampilkan sebelum kembali ke scene utama
//     public GameObject ContentCredit;
//     public GameObject ParentCredit;
//     private RectTransform contentTransform;
//     private Vector3 OriginalPosition;
//     private string SceneName;
//     private QuizManagar quizManagar;

//     private void Awake(){
//         OriginalPosition = ContentCredit.transform.localPosition;
//         contentTransform = ContentCredit.GetComponent<RectTransform>();
//         SceneName = SceneManager.GetActiveScene().name;
//     }

//     private void Update()
//     {
//         if (SceneName == "MainMenu" && ParentCredit.activeSelf)
//         {
//             // Menjalankan coroutine untuk menggerakkan teks secara otomatis
//             StartCoroutine(ScrollCredits());

//             // Memulai countdown untuk kembali ke scene utama setelah waktu yang ditentukan
//             Invoke("DisableCreditScene", endTime);
//         } else
//         {
//             quizManagar = FindObjectOfType<QuizManagar>();
//             StartCoroutine(ScrollCredits());
//             Invoke("goToMainMenu", endTime);
//         }
//     }

//     private IEnumerator ScrollCredits()
//     {
//         float targetPosition = contentTransform.rect.height;

//         while (contentTransform.anchoredPosition.y < targetPosition)
//         {
//             contentTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
//             yield return null;
//         }
//         DisableCreditScene();
//     }

//     private void DisableCreditScene()
//     {
//         // ContentCredit.transform.position = new Vector3(0, -687, 0);
//         contentTransform.position = OriginalPosition;
//         Debug.Log("reset posisi");
//         ParentCredit.SetActive(false);
//     }

//     private void goToMainMenu(){
//         quizManagar.BtnNo(0);
//     }
// }

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public float endTime = 50f;
    public GameObject ContentCredit;
    public GameObject ParentCredit;
    public Vector3 targetPosition;
    private RectTransform contentTransform;
    private Vector3 OriginalPosition;
    private string SceneName;
    private bool isCreditsRunning = false;

    private void Awake()
    {
        OriginalPosition = ContentCredit.transform.localPosition;
        contentTransform = ContentCredit.GetComponent<RectTransform>();
        SceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        
        if (ParentCredit.activeSelf && !isCreditsRunning && SceneName == "MainMenu")
        {
            StartCredits(); // Memulai kredit jika tombol Y ditekan
        }
        // }
        else
        {
            // quizManagar = FindObjectOfType<QuizManagar>();
            StartCredits(); // Memulai kredit jika dalam Gameplay
        }
    }

    private void StartCredits()
    {
        isCreditsRunning = true;
        StartCoroutine(ScrollCredits());
    }

    private void EndCredits()
    {
        contentTransform.localPosition = OriginalPosition;
        isCreditsRunning = false;
        ParentCredit.SetActive(false);
    }

    private IEnumerator ScrollCredits()
    {
        float targetY = targetPosition.y;

        while (contentTransform.anchoredPosition.y < targetY)
        {
            contentTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }
        EndCredits();
    }
}