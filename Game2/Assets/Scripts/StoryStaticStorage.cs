using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StoryStaticStorage
{
    public static DialogueController dialogueController = GameObject.Find("Dialogue").GetComponent<DialogueController>();

    //Player dialogue stats
    public static float greedy = 50;
    public static float giving = 50;
    public static float cunning = 50;
    public static float honor = 50;
    public static float disdain = 50;
    public static float vigilance = 50;
    public static float evil = 50;
    public static float good = 50;

    //Story engine bools
    public static bool testInterrupt = false;
    public static bool kingInterruptFirst = false;
    public static bool kingInterruptSecond = false;
    public static bool lastDialogue = false;

    public static void storySwitch(string storyString)
    {
        switch (storyString)
        {
            case "testInterrupt":
                //and any other stuff which will happen in the scene
                testInterrupt = true;
                break;
            case "testTest":
                break;
            case "felixAttack":
                kingInterruptFirst = true;
                break;
            case "grasshopperAttack":
                kingInterruptSecond = true;
                break;
            case "wonFirstFight":
                kingInterruptSecond = true;
                break;
            case "wonLastFight":
                dialogueController.SetCurrentDialogue(DialogueStaticStorage.kingDeathDialogue);
                break;
            case "wonGame":
                SceneManager.LoadScene("WinScene");
                break;
            case "":
                break;
            default:
                Debug.Log("No storyString found");
                break;
                
        }
    }
}
