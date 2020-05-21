using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueStaticStorage;

public class DialogueScript : MonoBehaviour
{
    DialogueController dialogueController;

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
        dialogueController.SetCurrentDialogue(test);
    }
}
