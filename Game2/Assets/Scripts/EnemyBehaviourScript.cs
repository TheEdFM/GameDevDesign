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
        allParticipants.Add("Mole");
        allParticipants.Add("Mole Slasher");
        allParticipants.Add("Treant");
        foreach (Character playerCharacter in playerParty)
        {
            allParticipants.Add(playerCharacter.name);
        }
        newEncounter(allParticipants.ToArray());
    }
}
