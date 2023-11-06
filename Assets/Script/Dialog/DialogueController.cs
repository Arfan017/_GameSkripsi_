// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class DialogueController : MonoBehaviour
// {
//     public TextMeshProUGUI DialogueText;
//     public GameObject DialogPanel;
//     public string[] SentencesDialog;
//     public int Index = 0;
//     public float DialogSpeed;
//     private bool isWriting = false;
//     private Coroutine writeSentenceCoroutine = null;
//     private GameObject obj;
//     public int indexDialog { get => Index; set => Index = value; }

//     void Start()
//     {
//         StartDialogue();
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             if (writeSentenceCoroutine != null)
//             {
//                 StopCoroutine(writeSentenceCoroutine);
//                 writeSentenceCoroutine = null;
//                 DialogueText.text = SentencesDialog[Index];
//             }
//             else
//             {
//                 NextSentenseDialog();
//             }
//         }
//     }

//     void StartDialogue()
//     {
//         if (SentencesDialog.Length > 0)
//         {
//             DialogueText.text = "";
//             writeSentenceCoroutine = StartCoroutine(WriteSentenceText());
//         }
//     }

//     void NextSentenseDialog()
//     {
//         if (Index < SentencesDialog.Length - 1)
//         {
//             Index++;
//             DialogueText.text = "";
//             writeSentenceCoroutine = StartCoroutine(WriteSentenceText());
//         }

//         else
//         {
//             writeSentenceCoroutine = null;
//             Debug.Log("Dialog selesai");
//         }
//     }

//     private IEnumerator WriteSentenceText()
//     {
//         foreach (char Character in SentencesDialog[Index].ToCharArray())
//         {
//             DialogueText.text += Character;
//             yield return new WaitForSeconds(DialogSpeed);
//         }
//         writeSentenceCoroutine = null;
//     }

//     public void RestartDialogue()
//     {
//         Index = 0;
//         StartDialogue();
//     }

//     public void zeroText()
//     {
//         Index = 0;
//         DialogPanel.SetActive(false);
//         Debug.Log("Dialog selesai_1");
//     }
// }