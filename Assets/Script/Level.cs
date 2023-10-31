using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    [SerializeField] private int requiredCoin;
    [SerializeField] private GameObject PanelUnlockArea;
    [SerializeField] private TextMeshProUGUI message;
    private Collider2D col;
    private PlayerController playerController;
    private AreaManager areaManager;
    int totalCoin;
    private bool isopen;
    public bool Isopen
    {
        get => isopen;
        set => isopen = value;
    }

    private void Awake()
    {
        col = this.GetComponent<Collider2D>();
        playerController = FindAnyObjectByType<PlayerController>();
        areaManager = FindAnyObjectByType<AreaManager>();
        // areaManager.OnOpenAreaChanged += OnOpenAreaChangedHandler;
    }

    private void Update()
    {
        totalCoin = playerController.CoinsCollected;
        // isopen = areaManager.OpenArea;

        if (Isopen)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PanelUnlockArea.SetActive(true);
            message.SetText("Apakah kamu ingin membuka area " + id + " ?");
            areaManager.AreaName = id;
            areaManager.RequiredCoin = requiredCoin;
            // "Apakah kamu ingin membuka area 1 ?"
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            message.SetText("");
            // "Apakah kamu ingin membuka area ini ?"
            PanelUnlockArea.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        data.openArea.TryGetValue(id, out isopen);
        if (isopen)
        {
            gameObject.SetActive(false);
        }
        else if (!isopen)
        {
            gameObject.SetActive(true);
        }
    }

    public void SaveData(GameData data)
    {

        if (data.openArea.ContainsKey(id))
        {
            data.openArea.Remove(id);
        }
        data.openArea.Add(id, Isopen);
    }
}