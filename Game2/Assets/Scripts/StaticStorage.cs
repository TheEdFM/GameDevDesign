using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStorage
{
    private static Dictionary<string, Move> allMoves = new Dictionary<string, Move>()
    {
        { "Basic Attack", new Move("Basic Attack", 20, new StatusEffect[]{ }, false, "physical") },
        { "Basic Heal", new Move("Basic Heal", -20, new StatusEffect[]{ }, true, "magic") }
    };

    private static Dictionary<string, StatusEffect> allStatusEffects = new Dictionary<string, StatusEffect>()
    {
        {"Stun", new StatusEffect("Stun", 0 , "stun", true, 1) },
        {"Bleed", new StatusEffect("Bleed", 10 , "physical", false, 3) }
    };

    private static Dictionary<string, Character> allCharacters = new Dictionary<string, Character>()
    {
        {"hero", new Character("hero", 10, 0, 100, new Move[]{ allMoves["Basic Attack"], allMoves["Basic Heal"] }, new List<StatusEffect>(){ })}
    };
    
    private static List<Character> combatParticipants = new List<Character>();

    public static void newEncounter(string[] participants)
    {
        combatParticipants.Clear();
        foreach (string name in participants)
        {
            combatParticipants.Add(allCharacters[name]);
        }

    }

    public static List<Character> getCombatParticipants()
    {
        return combatParticipants;
    }

    public class Move
    {
        public string name;
        public int damage;
        public StatusEffect[] statusEffects;
        public bool appliedToTeam;
        public string element;
        public int stun;

        public Move(string name, int damage, StatusEffect[] statusEffects, bool appliedToTeam, string element)
        {
            this.name = name;
            this.damage = damage;
            this.statusEffects = statusEffects;
            this.appliedToTeam = appliedToTeam;
            this.element = element;
        }
    }

    public class Character
    {
        public string name;
        public int initiative;
        public int team;
        public int maxHealth;
        public int currentHealth;
        public Move[] moves;
        public List<StatusEffect> statusEffects;

        public Character(string name, int initiative, int team, int maxHealth, Move[] moves, List<StatusEffect> statusEffects)
        {
            this.name = name;
            this.initiative = initiative;
            this.team = team;
            this.maxHealth = maxHealth;
            this.moves = moves;
            this.statusEffects = statusEffects;
        }
    }

    public class StatusEffect
    {
        public string name;
        public int dot;
        public string element;
        public bool stun;
        public int turnsRemaining;

        public StatusEffect(string name, int dot, string element, bool stun, int turnsRemaining)
        {
            this.name = name;
            this.dot = dot;
            this.element = element;
            this.stun = stun;
            this.turnsRemaining = turnsRemaining;
        }
    }
}
