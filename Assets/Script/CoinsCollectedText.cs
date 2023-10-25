using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCollectedText : MonoBehaviour
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
    }

    void Update()
    {
        coinsCollectedText.text = coinsCollected + " " ;
    }
}
