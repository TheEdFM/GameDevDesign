using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStaticStorage;

public class DialogueScript : MonoBehaviour
{
    DialogueController dialogueController;
    public string dialogueName; 

    // Start is called before the first frame update
    void Start()
    {
        dialogueController = GameObject.Find("Dialogue").GetComponent<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        switch (dialogueName)
        {
            case "test":
                dialogueController.SetCurrentDialogue(test);
                break;
            case "testExtraDialogue":
                dialogueController.SetCurrentDialogue(testExtraDialogue);
                break;
            case "narrationStartDialogue":
                dialogueController.SetCurrentDialogue(narrationStartDialogue);
                break;
            case "kingIntroductionDialogue":
                dialogueController.SetCurrentDialogue(kingIntroductionDialogue);
                break;
            case "felixDialogue":
                dialogueController.SetCurrentDialogue(felixDialogue);
                break;
            case "dwayneDialogue":
                dialogueController.SetCurrentDialogue(dwayneDialogue);
                break;
            case "whaihuaDialogue":
                dialogueController.SetCurrentDialogue(whaihuaDialogue);
                break;
            case "kingAttackDialogue":
                dialogueController.SetCurrentDialogue(kingAttackDialogue);
                break;
            case "kingDeathDialogue":
                dialogueController.SetCurrentDialogue(kingDeathDialogue);
                break;
            case "lossDialogue":
                dialogueController.SetCurrentDialogue(lossDialogue);
                break;
            case "kingInterruptFirstDialogue":
                dialogueController.SetCurrentDialogue(kingInterruptFirstDialogue);
                break;
            case "kingInterruptSecondDialogue":
                dialogueController.SetCurrentDialogue(kingInterruptSecondDialogue);
                break;
            case "cherryInfo":
                dialogueController.SetCurrentDialogue(cherryInfoDialogue);
                break;
        }
    }
}
