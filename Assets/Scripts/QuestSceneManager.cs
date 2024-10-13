using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueManager dialogueManager;
    public QuestSystem questSystem;
    public Sprite npcSprite;
    void Start()
    {
        //StartCoroutine(StartDialogueWithDelay());
    }
        
  /*  private IEnumerator StartDialogueWithDelay()
    {
        yield return new WaitForSeconds(2f);
        Dialogue dialogue = new Dialogue
       {
           name = "Govenor",
           image = npcSprite
       };

        dialogueManager.StartDialogue(dialogue);

        if(questSystem.CurrentTask.Count == 0)
        {
            Debug.Log("NO Tasks Currentely");
            return;
        }

        foreach (var ts in questSystem.CurrentTask)
        {
            Debug.Log($"Current Task ID: {ts.ID}, Completed: {ts.Completed}");
        }


        
    }
    // Update is called once per frame
  */
}
