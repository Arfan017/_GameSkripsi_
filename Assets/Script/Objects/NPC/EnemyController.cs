using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyCountToDefeat = 1;
    private int defeatedEnemyCount = 0;
    public GameObject DialogPanel;

    private void ShowSystemDialog()
    {
       DialogPanel.SetActive(true);
    }

    public void DefeatEnemy()
    {
        defeatedEnemyCount++;

        if (defeatedEnemyCount >= enemyCountToDefeat)
        {
            ShowSystemDialog();
        }
    }
}