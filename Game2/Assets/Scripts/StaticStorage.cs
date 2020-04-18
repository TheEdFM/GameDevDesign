using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStorage {

    private static Dictionary<string, Dictionary<string, int>> combatParticipants = new Dictionary<string, Dictionary<string, int>>();

    private static Dictionary<string, Dictionary<string, int>> characterInfo = new Dictionary<string, Dictionary<string, int>>()
    {
        //placeholder stats to formalise formatting
        {"hero", new Dictionary<string, int>
            {
                { "Initiative", 10000},
                { "Team", 0 },
                { "Max Health", 100 },
                { "Current Health", 100 },
                { "move1", 1 },
                { "move2", 2 },
                { "move3", 0 },
                { "move4", 0 }
            }
        },
        {"enemy", new Dictionary<string, int>
            {
                { "Initiative", 1},
                { "Team", 1 },
                { "Max Health", 100 },
                { "Current Health", 100 },
                { "move1",  1},
                { "move2", 0 },
                { "move3", 0 },
                { "move4", 0 }
            }
        }
    };

    public static void newEncounter(string[] participants)
    {
        combatParticipants.Clear();
        foreach (string name in participants)
        {
            combatParticipants.Add(name, characterInfo[name]);
        }
   
    }

    public static Dictionary<string, Dictionary<string, int>> getCombatParticipants()
    {
        return combatParticipants;
    }
    


}
