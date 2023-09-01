using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public BossFight bossFight;
    public PlayerFight playerFight;

    public void bossBackPos(){
        bossFight.BackPosition();
    }

    public void playerBackPos()
    {
        playerFight.BackPosition();
    }

}
