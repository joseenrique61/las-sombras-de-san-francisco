using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace UI
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField, TextArea(4, 8)] private string[] dialogueLines;
        [SerializeField] private bool activeOnStart = false;
        public bool finished { get; private set; } = false;
        private bool isDialogueActive;
        private int lineIndex;
        private float typingTime = 0.05f;

        void Start()
        {
            input = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();

            dialogueText.text = string.Empty;

            if (activeOnStart)
            {
                StartDialogue();
            }
        }

        void Update()
        {
            InputAction dialogueAction = input.actions["Dialogue"];
            if (dialogueAction != null && dialogueAction.WasPressedThisFrame())
            {
                if (!isDialogueActive)
                {
                    StartDialogue();
                }
                else if (dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                }
            }
        }

        private void StartDialogue()
        {
            lineIndex = 0;
            isDialogueActive = true;
            dialogueBox.SetActive(true);
            StartCoroutine(ShowLine());
        }

        private void NextDialogueLine()
        {
            lineIndex++;
            if (lineIndex < dialogueLines.Length)
            {
                StartCoroutine(ShowLine());
            }
            else
            {
                isDialogueActive = false;
                dialogueBox.SetActive(false);
                finished = true;
            }
        }
        private IEnumerator ShowLine()
        {
            dialogueText.text = string.Empty;
            foreach (char ch in dialogueLines[lineIndex])
            {
                dialogueText.text += ch;
                yield return new WaitForSecondsRealtime(typingTime);
            }
        }
    }
}