using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{   
    // public GameObject ParentCredit;
    // public GameObject PanelCredit;
    public AudioSource audioSource;
    public TMP_Dropdown resolutionDropdown;
    private float musicVolume = 1f;
    Resolution[] resolutions;

    private void Start()
    {

        // Mendapatkan opsi resolusi yang tersedia
        Resolution[] resolutions = Screen.resolutions;

        // Membersihkan opsi yang ada sebelumnya
        resolutionDropdown.ClearOptions();

        // Membuat daftar opsi resolusi baru
        List<string> resolutionOptions = new List<string>();

        // Menambahkan setiap resolusi ke dalam daftar opsi
        foreach (Resolution resolution in resolutions)
        {
            string option = resolution.width + "x" + resolution.height;
            resolutionOptions.Add(option);
        }

        // Menetapkan opsi resolusi pada Dropdown
        resolutionDropdown.AddOptions(resolutionOptions);
    }

    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
    }

    public void SetResolution(int resolutionIndex)
    {
        // Mengambil resolusi yang dipilih dari Dropdown
        Resolution resolution = Screen.resolutions[resolutionIndex];

        // Mengatur resolusi layar
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}