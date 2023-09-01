using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFight : MonoBehaviour
{
    public Animator animSwordPlayerAtk;
    public Animator animatorPlayer;
    public Image fillHealth;
    private float healthPlayer = 10;
    public float demagePlayer = 3;
    public GameObject PanelLose;
    public TextMeshProUGUI textNilai;
    private QuizManagar quizManagar;
    public AudioSource audioSwordAtk;

    public float HealthPlayer
    {
        set
        {
            healthPlayer = value;
            fillHealth.fillAmount = healthPlayer / 10f;

            if (healthPlayer <= 0f)
            {
                animatorPlayer.SetBool("IsDead", true);
                quizManagar.StopAudio();
                PanelLose.SetActive(true);
            }
        }

        get
        {
            return healthPlayer;
        }
    }

    void Start()
    {
        PanelLose.SetActive(false);
        quizManagar = FindObjectOfType<QuizManagar>();
    }

    public void PlayerAttack()
    {
        animSwordPlayerAtk.SetBool("SwordAttack", true);
        audioSwordAtk.Play();
    }

    public void BackPosition()
    {
        animatorPlayer.SetBool("PlayerAttack", false);
        animatorPlayer.SetTrigger("BackPos");
        animSwordPlayerAtk.SetBool("SwordAttack", false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("trigger dari player");
        if (coll.gameObject.tag == "SwordBoss")
        {
            animatorPlayer.SetBool("IsDemage", true);
            HealthPlayer -= demagePlayer;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "SwordBoss")
        {
            animatorPlayer.SetBool("IsDemage", false);
        }
    }
}