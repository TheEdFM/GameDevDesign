using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStaticStorage;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public Rigidbody2D toaRB;

    public GameObject dialoguePanel;
    public GameObject dialogueText;
    public GameObject dialogueName;

    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI textText;

    public Dialogue currentDialogue;
    public string text;

    // Start is called before the first frame update
    void Start()
    {
        toaRB = GameObject.Find("Toa").GetComponent<Rigidbody2D>();

        dialoguePanel = GameObject.Find("DialoguePanel");
        dialogueText = GameObject.Find("DialogueText");
        dialogueName = GameObject.Find("DialogueName");

        characterNameText = GameObject.Find("DialogueName").GetComponent<TextMeshProUGUI>();
        textText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();

        dialoguePanel.SetActive(false);
        dialogueName.SetActive(false);
        dialogueText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDialogue != null)
        {

            characterNameText.text = currentDialogue.currentNode.character.name;

            text = "";
            text += currentDialogue.currentNode.characterText + "\n\n";

            int optionNumber = 0;
            foreach (Option option in currentDialogue.currentNode.options)
            {
                text += (optionNumber + 1) + ". " + currentDialogue.currentNode.options[optionNumber].optionText + "\n";

                optionNumber++;
            }

            textText.text = text;

            //If the player chooses an option, change the currentNode,
            //  if the option was an exiting option, hide the dialogue window 
            DialogueNode newCurrentNode = null;
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentDialogue.currentNode.options.Length > 0)
            {
                Debug.Log("hi");
                newCurrentNode = allDialogueNodes[currentDialogue.currentNode.options[0].linkedNode];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentDialogue.currentNode.options.Length > 1)
            {
                newCurrentNode = allDialogueNodes[currentDialogue.currentNode.options[1].linkedNode];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && currentDialogue.currentNode.options.Length > 2)
            {
                newCurrentNode = allDialogueNodes[currentDialogue.currentNode.options[2].linkedNode];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && currentDialogue.currentNode.options.Length > 3)
            {
                newCurrentNode = allDialogueNodes[currentDialogue.currentNode.options[3].linkedNode];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && currentDialogue.currentNode.options.Length > 4)
            {
                newCurrentNode = allDialogueNodes[currentDialogue.currentNode.options[4].linkedNode];
            }
            if (newCurrentNode != null)
            {
                Debug.Log("poop");
                SetCurrentNode(newCurrentNode, newCurrentNode.exitDialogue);
            }
        }
        
    }

    public void SetCurrentNode(DialogueNode newCurrentNode, bool exitDialogue)
    {
        if (newCurrentNode != null)
        {
            currentDialogue.currentNode = newCurrentNode;
            if (exitDialogue)
            {
                dialoguePanel.SetActive(false);
                dialogueName.SetActive(false);
                dialogueText.SetActive(false);
                toaRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                dialoguePanel.SetActive(true);
                dialogueName.SetActive(true);
                dialogueText.SetActive(true);
            }
        }
    }

    public void SetCurrentDialogue(Dialogue newCurrentDialogue)
    {
        currentDialogue = newCurrentDialogue ?? throw new System.ArgumentNullException(nameof(newCurrentDialogue));
        dialoguePanel.SetActive(true);
        dialogueName.SetActive(true);
        dialogueText.SetActive(true);
        toaRB.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
