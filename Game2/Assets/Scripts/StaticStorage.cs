using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticStorage
{
    public static Dictionary<string, StatusEffect> allStatusEffects = new Dictionary<string, StatusEffect>()
    {
        {"Stun", new StatusEffect("Stun", 0 , "physical", true, 1, 1) },
        {"Bleed", new StatusEffect("Bleed", 10 , "physical", false, 3, 3) }
    };

    public static Dictionary<string, Move> allMoves = new Dictionary<string, Move>()
    {
        { "Basic Attack", new Move("Basic Attack", 20, new StatusEffect[]{ }, false, "physical") },
        { "Basic Heal", new Move("Basic Heal", -20, new StatusEffect[]{ }, true, "magic") },
        { "Basic Bleed", new Move("Basic Bleed", 5, new StatusEffect[]{allStatusEffects["Bleed"]}, false, "physical") },
        { "Basic Stun", new Move("Basic Stun", 5, new StatusEffect[]{allStatusEffects["Stun"]}, false, "physical") },
        { "Nuke Cannon", new Move("Nuke Cannon", 10000, new StatusEffect[]{}, false, "physical") }
    };

    public static Dictionary<string, Character> allCharacters = new Dictionary<string, Character>()
    {
        {"Toa", new Character("Toa", "ImageHero", 10, 0, 100, 100, false, new Move[]{ allMoves["Basic Attack"], allMoves["Nuke Cannon"], allMoves["Basic Bleed"], allMoves["Basic Stun"] }, new List<StatusEffect>(){ })},
        {"Treant", new Character("Treant", "ImageTreant", 3, 1, 70, 70, false, new Move[]{allMoves["Basic Heal"] }, new List<StatusEffect>(){ })},
        {"Mole", new Character("Mole", "ImageMole", 5, 1, 50, 50, false, new Move[]{ allMoves["Basic Attack"] }, new List<StatusEffect>(){ })},
        {"Mole Slasher", new Character("Mole Slasher", "ImageMole", 5, 1, 50, 50, false, new Move[]{ allMoves["Basic Bleed"], allMoves["Basic Attack"] }, new List<StatusEffect>(){ })},
        {"Daisy", new Character("Daisy", "ImageDaisy", 5, 0, 50, 50, false, new Move[]{allMoves["Basic Heal"], allMoves["Basic Stun"] }, new List<StatusEffect>(){ })}
    };

    public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
    {
        { "Resurrect Potion", new Item("Resurrect Potion", -70, new StatusEffect[]{ }, true, "magic") },
        { "Health Potion", new Item("Health Potion", -40, new StatusEffect[]{ }, true, "magic") }
    };

    public static Dictionary<string, ItemAndNumberOwned> playerItems = new Dictionary<string, ItemAndNumberOwned>()
    {
        { "Resurrect Potion", new ItemAndNumberOwned(allItems["Resurrect Potion"], 1) },
        { "Health Potion", new ItemAndNumberOwned(allItems["Health Potion"], 3) }
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
        public string imageName;
        public int initiative;
        public int team;
        public int maxHealth;
        public int currentHealth;
        public bool isDead;
        public Move[] moves;
        public List<StatusEffect> statusEffects;

        public Character(string name, string imageName, int initiative, int team, int maxHealth, int currentHealth, bool isDead, Move[] moves, List<StatusEffect> statusEffects)
        {
            this.name = name;
            this.imageName = imageName;
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
        public int maxTurnsRemaining;
        public int currentTurnsRemaining;

        public StatusEffect(string name, int dot, string element, bool stun, int maxTurnsRemaining, int currentTurnsRemaining)
        {
            this.name = name;
            this.dot = dot;
            this.element = element;
            this.stun = stun;
            this.maxTurnsRemaining = maxTurnsRemaining;
            this.currentTurnsRemaining = currentTurnsRemaining;
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
