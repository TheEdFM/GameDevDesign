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

    public bool skipTypeOut = false;
    public float timeBetweenLetters = 0.01f;
    public bool newNode = false;

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
            Option[] currentOptions = currentDialogue.currentNode.options;

            characterNameText.text = currentDialogue.currentNode.character.name;

            text = "";
            text += currentDialogue.currentNode.characterText + "\n\n";

            int optionNumber = 0;
            foreach (Option option in currentOptions)
            {
                text += (optionNumber + 1) + ". " + option.optionText + "\n";

                optionNumber++;
            }

            if (newNode)
            {
                StartCoroutine("TypeOut");
                newNode = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skipTypeOut = true;
            }
            

            //If the player chooses an option, change the currentNode,
            //  if the option was an exiting option, hide the dialogue window 
            DialogueNode newCurrentNode = null;
            
            if (Input.GetKeyDown(KeyCode.Alpha1) && currentOptions.Length > 0 && currentOptions[0].IsChooseable())
            {
                newCurrentNode = allDialogueNodes[currentOptions[0].linkedNode];
                currentOptions[0].effects.ApplyEffects();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && currentOptions.Length > 1 && currentOptions[1].IsChooseable())
            {
                newCurrentNode = allDialogueNodes[currentOptions[1].linkedNode];
                currentOptions[1].effects.ApplyEffects();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && currentOptions.Length > 2 && currentOptions[2].IsChooseable())
            {
                newCurrentNode = allDialogueNodes[currentOptions[2].linkedNode];
                currentOptions[2].effects.ApplyEffects();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && currentOptions.Length > 3 && currentOptions[3].IsChooseable())
            {
                newCurrentNode = allDialogueNodes[currentOptions[3].linkedNode];
                currentOptions[3].effects.ApplyEffects();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && currentOptions.Length > 4 && currentOptions[4].IsChooseable())
            {
                newCurrentNode = allDialogueNodes[currentOptions[4].linkedNode];
                currentOptions[4].effects.ApplyEffects();
            }

            if (newCurrentNode != null)
            {
                SetCurrentNode(newCurrentNode, newCurrentNode.exitDialogue);
            }
        }
        
    }

    public void SetCurrentNode(DialogueNode newCurrentNode, bool exitDialogue)
    {
        newNode = true;
        if (newCurrentNode != null)
        {
            currentDialogue.currentNode = newCurrentNode;
            if (exitDialogue)
            {
                dialoguePanel.SetActive(false);
                dialogueName.SetActive(false);
                dialogueText.SetActive(false);
                if (!StaticStorage.inCombat)
                {
                    toaRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
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
        newNode = true;
        dialoguePanel.SetActive(true);
        dialogueName.SetActive(true);
        dialogueText.SetActive(true);
        toaRB.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    IEnumerator TypeOut()
    {
        int i = 0;
        string partialText = "";
        foreach (char c in text)
        {
            if (skipTypeOut)
            {
                textText.text = text;
                skipTypeOut = false;
                break;
            }
            else if (i <= currentDialogue.currentNode.characterText.Length-1) {
                partialText += c;
                textText.text = partialText;
                if (c == ' ')
                {
                    yield return new WaitForSeconds(timeBetweenLetters);
                }
                else if (c == '\n')
                {
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    yield return new WaitForSeconds(timeBetweenLetters);
                }
            }
            else
            {
                textText.text = text;
                break;
            }
            i++;
        }
    }
}
