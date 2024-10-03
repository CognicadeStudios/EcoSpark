using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image npcImage;
    public TextMeshProUGUI dialogueText;
    public Animator anim;

    private Queue<string> sentences;

    private Dictionary<int, List<string>> dialogueDict;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueDict = new();
    }

    private void populateDictionary()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        npcImage.sprite = dialogue.image;
        sentences.Clear();
        List<int> keys = new List<int>(dialogueDict.Keys);
        int dialogueKey = keys[Random.Range(0, keys.Count)];
        List<string> randSent = dialogueDict[dialogueKey];

        dialogueDict.Remove(dialogueKey);
        foreach (string sentence in randSent)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        anim.SetBool("isOpen", false);
    }
}
