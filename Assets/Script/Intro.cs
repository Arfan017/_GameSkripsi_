using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public Dialog dialog;
    private IntoManager intoManager;

    private void Awake()
    {
        intoManager = FindObjectOfType<IntoManager>();
    }

    void Start()
    {
        intoManager.StartDialog(dialog);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (intoManager.IsTyping == true)
            {
                return;
            }
            else
            {
                intoManager.DisplayNextSentence();
            }
        }
    }
}
