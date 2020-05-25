using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoryStaticStorage
{
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
            case "acceptKingQuest":
                kingInterruptFirst = true;
                break;
            case "wonFirstFight":
                kingInterruptSecond = true;
                break;
            case "":
                break;
            default:
                Debug.Log("No storyString found");
                break;
                
        }
    }
}
