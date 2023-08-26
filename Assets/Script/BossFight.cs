using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossFight : MonoBehaviour
{
    public Animator animSwordBossAtk;
    public Animator animatorBoss;
    public Image fillHealth;
    private float healthBoss = 5;
    public float demageBoss = 3;
    public GameObject PanelWin;
    public TextMeshProUGUI textNilai;
    private QuizManagar quizManagar;
    public AudioSource audioSwordAtk;

    public float HealthBoss
    {
        set
        {
            healthBoss = value;
            fillHealth.fillAmount = healthBoss / 5f;

            if (healthBoss <= 0f)
            {
                animatorBoss.SetBool("IsDead", true);
                quizManagar.StopAudio();
                PanelWin.SetActive(true);
            }
        }

        get
        {
            return healthBoss;
        }
    }

    void Start()
    {
        PanelWin.SetActive(false);
        quizManagar = FindObjectOfType<QuizManagar>();
    }

    public void BossAttack()
    {
        animSwordBossAtk.SetBool("SwordAttack", true);
        audioSwordAtk.Play();
    }

    public void BackPosition()
    {
        animatorBoss.SetBool("BossAttack", false);
        animatorBoss.SetTrigger("BackPos");
        animSwordBossAtk.SetBool("SwordAttack", false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "SwordPlayer")
        {
            animatorBoss.SetBool("IsDemage", true);
            HealthBoss -= demageBoss;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "SwordPlayer")
        {
            animatorBoss.SetBool("IsDemage", false);
        }
    }
}