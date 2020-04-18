using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerBehaviour : MonoBehaviour
{
    Queue<string> turnOrder = new Queue<string>();
    List<KeyValuePair<string, Dictionary<string, int>>> combatParticipantsSortList = new List<KeyValuePair<string, Dictionary<string, int>>>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (KeyValuePair<string, Dictionary<string, int>> entry in StaticStorage.getCombatParticipants())
        {
            combatParticipantsSortList.Add(entry);
        }
        combatParticipantsSortList.Sort(CompareInitiative);
        foreach (KeyValuePair<string, Dictionary<string, int>> entry in combatParticipantsSortList)
        {
            turnOrder.Enqueue(entry.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static int CompareInitiative(KeyValuePair<string, Dictionary<string, int>> participantX, KeyValuePair<string, Dictionary<string, int>> participantY)
    {
        return -(participantX.Value["Initiative"].CompareTo(participantY.Value["Initiative"]));
    }
}
