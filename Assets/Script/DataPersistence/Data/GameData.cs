using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // public int deathCount;
    // public Vector3 playerPosition;
    // public SerializableDictionary<string, bool>  coinsCollected;
    public float dataHealth;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> EnemyDefeat;

    public GameData()
    {
        // this.deathCount = 0;
        // playerPosition = Vector3.zero;
        // coinsCollected = new SerializableDictionary<string, bool>();

        this.dataHealth = 5f;
        playerPosition = new Vector3(18.9580002f, -5.80700016f, 0f);
        EnemyDefeat = new SerializableDictionary<string, bool>();
    }
}