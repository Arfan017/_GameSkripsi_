using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogIlmu : MonoBehaviour
{
    public GameObject PanelDialogIlmu; // Panel yang berisi gambar dan teks
    public Image dialogImage;       // Gambar dialog
    public TextMeshProUGUI dialogText;         // Teks dialog
    public Sprite image;
    public String text;
    // private bool isDialogActive = false;

    private void Start()
    {
        HideDialog();
    }

    // Menampilkan dialog dengan gambar dan teks
    public void ShowDialog(Sprite image, string text)
    {
        // isDialogActive = true;
        PanelDialogIlmu.SetActive(true);

        // Atur gambar dialog
        dialogImage.sprite = image;

        // Atur teks dialog
        dialogText.text = text;
    }

    // Menyembunyikan dialog
    public void HideDialog()
    {
        // isDialogActive = false;
        PanelDialogIlmu.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowDialog(image, text);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HideDialog();
        }
    }
}