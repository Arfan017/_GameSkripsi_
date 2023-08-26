using System.Collections;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    public float scrollSpeed = 20f; // Kecepatan scroll teks
    public float endTime = 50f; // Waktu berapa lama Credit Scene ditampilkan sebelum kembali ke scene utama
    public GameObject PanelCredit;
    public GameObject ParentCredit;
    private RectTransform contentTransform;
    private Vector3 OriginalPosition;

    private void Start()
    {
        OriginalPosition = PanelCredit.transform.position;

        contentTransform = PanelCredit.GetComponent<RectTransform>();

        // Menjalankan coroutine untuk menggerakkan teks secara otomatis
        StartCoroutine(ScrollCredits());

        // Memulai countdown untuk kembali ke scene utama setelah waktu yang ditentukan
        Invoke("DisableCreditScene", endTime);
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
}
