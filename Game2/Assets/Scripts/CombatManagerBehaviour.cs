using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerBehaviour : MonoBehaviour
{
    Queue<string> turnOrder = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (KeyValuePair<string, Dictionary<string, int>> entry in StaticStorage.getCombatParticipants())
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
