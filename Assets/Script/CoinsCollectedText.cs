using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCollectedText : MonoBehaviour, IDataPersistence
{
    private int coinsCollected = 0;
    private TextMeshProUGUI coinsCollectedText;

    private void Awake()
    {
        coinsCollectedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // subscribe to events
        GameEventsManager.instance.onCoinCollected += OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        coinsCollected++;
        UpdateCoinsCollectedText();
    }

    void UpdateCoinsCollectedText()
    {
        coinsCollectedText.text = coinsCollected + "";
    }

    public void LoadData(GameData data)
    {
        coinsCollected = 0;
        foreach (KeyValuePair<string, bool> pair in data.coinsCollected)
        {
            if (pair.Value)
            {
                coinsCollected++;
            }
        }
        UpdateCoinsCollectedText();
    }

    public void SaveData(GameData data)
    {
        // throw new System.NotImplementedException();
    }
}
