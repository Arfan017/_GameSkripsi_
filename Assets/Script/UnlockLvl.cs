using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLvl : MonoBehaviour
{
    private PlayerController playerController;
    int totalCoin;

    private void Awake()
    {
        playerController = new PlayerController();
    }

    private void Start() {
        totalCoin = playerController.CoinsCollected;
    }

    public void UnlockArea()
    {
        if (totalCoin >= 10){
            Debug.Log("");
        }
    }
}
