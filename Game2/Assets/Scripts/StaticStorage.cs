using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStorage
{
    public static Dictionary<string, Move> allMoves = new Dictionary<string, Move>()
    {
        { "Basic Attack", new Move("Basic Attack", 20, new StatusEffect[]{ }, false, "physical") },
        { "Basic Heal", new Move("Basic Heal", -20, new StatusEffect[]{ }, true, "magic") }
    };

    public static Dictionary<string, StatusEffect> allStatusEffects = new Dictionary<string, StatusEffect>()
    {
        {"Stun", new StatusEffect("Stun", 0 , "physical", true, 1) },
        {"Bleed", new StatusEffect("Bleed", 10 , "physical", false, 3) }
    };

    public static Dictionary<string, Character> allCharacters = new Dictionary<string, Character>()
    {
        {"Toa", new Character("Toa", 10, 0, 100, 100, false, new Move[]{ allMoves["Basic Attack"], allMoves["Basic Heal"] }, new List<StatusEffect>(){ })},
        {"Mole", new Character("Mole", 5, 1, 50, 50, false, new Move[]{ allMoves["Basic Attack"] }, new List<StatusEffect>(){ })}
    };

    public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
    {
        { "Resurrect Potion", new Item("Resurrect Potion", -50, new StatusEffect[]{ }, true, "resurrect") },
        { "Health Potion", new Item("Health Potion", -20, new StatusEffect[]{ }, true, "magic") }
    };

    public static Dictionary<string, ItemAndNumberOwned> playerItems = new Dictionary<string, ItemAndNumberOwned>()
    {
        { "Resurrect Potion", new ItemAndNumberOwned(new Item("Resurrect Potion", -50, new StatusEffect[]{ }, true, "resurrect"), 1) },
        { "Health Potion", new ItemAndNumberOwned(new Item("Health Potion", -20, new StatusEffect[]{ }, true, "magic"), 1) }
    };

    public static List<Character> playerParty = new List<Character>();

    public static Dictionary<string, Character> currentCombatParticipants = new Dictionary<string, Character>();

    public static void newEncounter(string[] participants)
    {
        currentCombatParticipants.Clear();
        foreach (string name in participants)
        {
            resetCharacter(allCharacters[name]);
            currentCombatParticipants.Add(name, allCharacters[name]);
        }

    }

    public static void resetCharacter(Character character)
    {
        character.isDead = false;
        character.currentHealth = character.maxHealth;
        character.statusEffects = new List<StatusEffect>();
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
        public bool isDead;
        public Move[] moves;
        public List<StatusEffect> statusEffects;

        public Character(string name, int initiative, int team, int maxHealth, int currentHealth, bool isDead, Move[] moves, List<StatusEffect> statusEffects)
        {
            this.name = name;
            this.initiative = initiative;
            this.team = team;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.isDead = isDead;
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
    public class Item
    {
        public string name;
        public int damage;
        public StatusEffect[] statusEffects;
        public bool appliedToTeam;
        public string element;
        public int stun;

        public Item(string name, int damage, StatusEffect[] statusEffects, bool appliedToTeam, string element)
        {
            this.name = name;
            this.damage = damage;
            this.statusEffects = statusEffects;
            this.appliedToTeam = appliedToTeam;
            this.element = element;
        }
    }

    public class ItemAndNumberOwned
    {
        public Item item;
        public int numberOwned;

        public ItemAndNumberOwned(Item item, int numberOwned)
        {
            this.item = item;
            this.numberOwned = numberOwned;
        }
    }

    public static void UsePlayerItem(string itemName)
    {
        playerItems[itemName].numberOwned -= 1;
        if (playerItems[itemName].numberOwned <= 0)
        {
            playerItems.Remove(itemName);
        }
    }
}
