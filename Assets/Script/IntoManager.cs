using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntoManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider SliderLoading;

    public void OnClick()
    {
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