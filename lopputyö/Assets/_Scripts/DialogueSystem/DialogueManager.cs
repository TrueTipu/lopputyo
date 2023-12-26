using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : Singleton<DialogueManager>
{
    Queue<Line> lines;

    [SerializeField] TextMeshProUGUI dialog;
    [SerializeField] Image portraitim;
    [SerializeField] Image tausta;
    [SerializeField] Animator animator;

    bool dialogueActive = false;
  
    // Start is called before the first frame update
    void Start()
    {
        lines = new Queue<Line>();
    }

    public void StartDialogue(Dialogue _dialogue)
    {
        dialogueActive = true;
        animator.SetBool("isopen", true);
        lines.Clear();
        foreach (Line _newLine in _dialogue.Lines)
        {
            lines.Enqueue(_newLine);
        }
        DisplayNextLine();
    }
    public void DisplayNextLine()
    {
 
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        Line _newLine = lines.Dequeue();

        dialog.rectTransform.localPosition = new Vector3(-134.5f, -127.8f, 0f);
        portraitim.rectTransform.localPosition = new Vector3(404f, -127f, 0f);
        portraitim.sprite = _newLine.Image;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(_newLine.Sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialog.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialog.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("isopen", false);
        dialogueActive = false;
    }
}
    

