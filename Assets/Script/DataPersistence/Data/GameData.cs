using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float dataHealth;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> EnemyDefeat;
    public SerializableDictionary<string, bool> coinsCollected;
    public SerializableDictionary<string, bool> openArea;

    public GameData()
    {
        this.dataHealth = 5f;
        playerPosition = new Vector3(18.9580002f, -5.80700016f, 0f);
        EnemyDefeat = new SerializableDictionary<string, bool>();
        coinsCollected = new SerializableDictionary<string, bool>();
        openArea = new SerializableDictionary<string, bool>();
    }
}