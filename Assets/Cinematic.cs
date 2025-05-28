using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] private GameObject TittlePanel;
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private LevelLoader levelLoader;

    private Dialogue dialogue;
    private TMP_Text chapterTittle;
    private TMP_Text chapterNumber;

    void Start()
    {
        dialogue = DialogPanel.GetComponent<Dialogue>();
        chapterTittle = TittlePanel.transform.Find("ChapterTittle").GetComponent<TMP_Text>();
        chapterNumber = TittlePanel.transform.Find("ChapterNumber").GetComponent<TMP_Text>();

        if (dialogue == null)
        {
            Debug.LogError($"El componente {DialogPanel.name} no cuenta con un componente {dialogue.name}");
        }

        if (chapterTittle == null)
        {
            Debug.LogError($"El componente {chapterTittle.name} no cuenta con un componente {chapterTittle.name}");
        }

        if (chapterNumber == null)
        {
            Debug.LogError($"El componente {chapterNumber.name} no cuenta con un componente {chapterNumber.name}");
        }

        TittlePanel.SetActive(false);
    }

    void Update()
    {
        if (dialogue.finished)
        {
            TittlePanel.SetActive(true);

            PrintInScreen(chapterTittle);

            levelLoader.LoadNextLevel();
        }
    }

    
    private void PrintInScreen(TMP_Text text)
    {
        StartCoroutine(ShowLine(text));
    }

    private IEnumerator ShowLine(TMP_Text text)
    {
        string line = text.text;
        text.text = string.Empty;
        foreach (char ch in line)
        {
            text.text += ch;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
