using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticStorage;

public class EnemyBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        List<string> allParticipants = new List<string>();
        switch (name)
        {
            case "tutorialFight":
                allParticipants.Add("Bewitched Mole");
                break;
            case "firstFight":
                allParticipants.Add("Bewitched Fox");
                allParticipants.Add("Bewitched Mole");
                StoryStaticStorage.storySwitch("felixAttack");
                break;
            case "secondFight":
                allParticipants.Add("Bewitched Grasshopper");
                allParticipants.Add("Bewitched Mole");
                StoryStaticStorage.storySwitch("grasshopperAttack");
                break;
            case "thirdFight":
                allParticipants.Add("Bewitched Squirrel");
                allParticipants.Add("Bewitched Treant");
                break;
            case "bossFight":
                allParticipants.Add("King Jebediah");
                allParticipants.Add("Bewitched Mole");
                allParticipants.Add("Bewitched Treant");
                isLastFight = true;
                break;
        }

        foreach (Character playerCharacter in playerParty)
        {
            allParticipants.Add(playerCharacter.name);
        }
        foreach (string c in allParticipants.ToArray())
        {
            Debug.Log(c);
        }
        
        newEncounter(allParticipants.ToArray());
    }
}
