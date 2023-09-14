using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject ExitPanel;
    public GameObject DialogPanel;
    public GameObject BossPanel;
    public GameObject loadingPanel;
    public Slider SliderLoading;
    private bool isPaused = false;
    private GameData gameData;
    private Dictionary<string, int> enemyCountNeeded = new Dictionary<string, int>();
    public Dictionary<string, int> EnemyCountNeeded
    {
        get
        {
            return enemyCountNeeded;
        }
        set
        {
            enemyCountNeeded = value;
        }
    }

    private Dictionary<string, int> enemyCount = new Dictionary<string, int>();
    public Dictionary<string, int> EnemyCount
    {
        get
        {
            return enemyCount;
        }
        set
        {
            enemyCount = value;
        }
    }

    private PlayerController player;
    private DamageableCharacter helathPlayer;
    private Boolean isTrigger = false;
    private DataParsistenceManager dataParsistenceManager;
    public bool IsTrigger
    {
        get
        {
            return isTrigger;
        }
        set
        {
            isTrigger = value;
        }
    }

    private float dataHealth;
    public float DataHealth
    {
        get
        {
            return dataHealth;
        }
        set
        {
            dataHealth = value;
        }
    }

    private Vector3 dataPosition;
    public Vector3 DataPosition
    {
        get
        {
            return dataPosition;
        }
        set
        {
            dataPosition = value;
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        helathPlayer = FindObjectOfType<DamageableCharacter>();
        dataParsistenceManager = FindAnyObjectByType<DataParsistenceManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (IsTrigger)
        {
            BossPanel.SetActive(true);
        }
        else
        {
            BossPanel.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        DataPosition = data.playerPosition;
        DataHealth = data.dataHealth;
        // player.LoadPositionPlayer();
        // helathPlayer.LoadHealthPlayer();
    }

    public void SaveData(GameData data)
    {
        data.dataHealth = DataHealth;
        data.playerPosition = DataPosition;
    }

    // private void PrintDictionaryEnemyCountNeeded()
    // {
    //     foreach (var key in EnemyCountNeeded.Keys)
    //     {
    //         // Mencetak key dan value dari setiap elemen dalam dictionary
    //         Debug.Log(key + ": " + EnemyCountNeeded[key]);
    //     }
    // }

    // private void PrintDictionaryEnemyCount()
    // {
    //     foreach (var key in EnemyCount.Keys)
    //     {
    //         // Mencetak key dan value dari setiap elemen dalam dictionary
    //         Debug.Log(key + ": " + EnemyCount[key]);
    //     }
    // }

    public void DestroyDefeatedEnemies()
    {
        // Pastikan enemyCountNeeded dan enemyCount tidak null
        if (EnemyCountNeeded == null)
        {
            EnemyCountNeeded = new Dictionary<string, int>();
        }

        if (EnemyCount == null)
        {
            EnemyCount = new Dictionary<string, int>();
        }

        foreach (KeyValuePair<string, int> entry in EnemyCount)
        {
            string enemyTag = entry.Key;
            int defeatedCount = entry.Value;
            int neededCount = EnemyCountNeeded.ContainsKey(enemyTag) ? EnemyCountNeeded[enemyTag] : 0;

            // Jika jumlah musuh yang dikalahkan mencapai atau melebihi jumlah yang diperlukan
            if (defeatedCount >= neededCount)
            {
                // Hancurkan musuh dengan tag yang sesuai
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }
            }
        }
    }

    public void YesFightBoss(int sceneIndex)
    {
        BossPanel.SetActive(false);
        Destroy(dataParsistenceManager);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void NoFightBoss()
    {
        BossPanel.SetActive(false);
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

    public void showPanelDialog()
    {
        DialogPanel.SetActive(true);
    }

    public void hidePanelDialog()
    {
        DialogPanel.SetActive(false);
        BroadcastMessage("setZero", 0, SendMessageOptions.DontRequireReceiver);
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        MenuPanel.SetActive(true);
    }

    public void save()
    {
        dataParsistenceManager.SaveGame();
    }

    public void load()
    {
        dataParsistenceManager.LoadGame();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        MenuPanel.SetActive(false);
    }

    public void MenuExitIsClick()
    {
        MenuPanel.SetActive(false);
        ExitPanel.SetActive(true);
    }

    public void ExitGameIsYes(int sceneIndex)
    {
        ExitPanel.SetActive(false);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void ExitGameIsNo()
    {
        ExitPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void BtnYes()
    {
        DataParsistenceManager.instance.NewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DataParsistenceManager.instance.LoadGame();
    }

    public void BtnNo(int sceneIndex)
    {
        Destroy(dataParsistenceManager);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
}