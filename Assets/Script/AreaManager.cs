using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjArea;
    [SerializeField] private TextMeshProUGUI message;
    private PlayerController playerController;
    private Level level;
    private int totalCoin;

    private string areaName;
    public string AreaName
    {
        get => areaName;
        set => areaName = value;
    }

    private bool openArea;
    public bool OpenArea
    {
        get => openArea;
        set
        {
            openArea = value;
        }
    }

    private int requiredCoin;
    public int RequiredCoin
    {
        get => requiredCoin;
        set => requiredCoin = value;
    }

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        totalCoin = playerController.CoinsCollected;
    }

    public void UnlockArea()
    {
        if (AreaName == "lvl1")
        {
            if (requiredCoin == totalCoin)
            {
                // OpenArea = true;
                level = ObjArea[0].GetComponent<Level>();
                level.Isopen = true;
            }
            else
            {
                message.SetText(requiredCoin + " / " + totalCoin + "\n" + "poin kamu belum cukup");
            }
        }
        else if (AreaName == "lvl2")
        {
            if (requiredCoin == totalCoin)
            {
                level = ObjArea[1].GetComponent<Level>();
                level.Isopen = true;
            }
            else
            {
                message.SetText(requiredCoin + " / " + totalCoin + "\n" + "poin kamu belum cukup");
            }
        }
        else if (AreaName == "lvl3")
        {
            if (requiredCoin == totalCoin)
            {
                level = ObjArea[2].GetComponent<Level>();
                level.Isopen = true;
            }
            else
            {
                message.SetText(requiredCoin + " / " + totalCoin + "\n" + "poin kamu belum cukup");
            }
        }
        else if (AreaName == "lvl4")
        {
            if (requiredCoin == totalCoin)
            {
                level = ObjArea[3].GetComponent<Level>();
                level.Isopen = true;
            }
            else
            {
                message.SetText(requiredCoin + " / " + totalCoin + "\n" + "poin kamu belum cukup");
            }
        }
        else if (AreaName == "lvl5")
        {
            if (requiredCoin == totalCoin)
            {
                level = ObjArea[4].GetComponent<Level>();
                level.Isopen = true;
            }
            else
            {
                message.SetText(requiredCoin + " / " + totalCoin + "\n" + "poin kamu belum cukup");
            }
        }
    }
}