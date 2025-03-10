﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticStorage
{
    public static Dictionary<string, StatusEffect> allStatusEffects = new Dictionary<string, StatusEffect>()
    {
        {"Stun", new StatusEffect("STN", 0 , "physical", true, 0, 1, 1) },
        {"Bleed", new StatusEffect("BLD", 10 , "physical", false, 0, 3, 3) },
        {"Strength", new StatusEffect("STR", 0 , "physical", false, 10, 3, 3) },
        {"Weakness", new StatusEffect("WKN", 0 , "physical", false, -5, 3, 3) }
    };

    public static Dictionary<string, Move> allMoves = new Dictionary<string, Move>()
    {
        { "Basic Attack", new Move("Basic Attack", 20, new StatusEffect[]{ }, false, "physical") },
        { "Basic Heal", new Move("Basic Heal", -10, new StatusEffect[]{ }, true, "magic") },
        { "Bleed", new Move("Bleed", 5, new StatusEffect[]{allStatusEffects["Bleed"]}, false, "physical") },
        { "Basic Stun", new Move("Basic Stun", 5, new StatusEffect[]{allStatusEffects["Stun"]}, false, "physical") },
        { "Nuke Cannon", new Move("Nuke Cannon", 10000, new StatusEffect[]{}, false, "physical") },
        { "Strength Potion", new Move("Strength Potion", 0, new StatusEffect[]{allStatusEffects["Strength"] }, true, "magic") },
        { "Weakness Potion", new Move("Weakness Potion", 0, new StatusEffect[]{allStatusEffects["Weakness"] }, false, "magic") },
        { "Healing Potion", new Move("Healing Potion", -50, new StatusEffect[]{ }, true, "magic") },
        { "Claw", new Move("Claw", 20, new StatusEffect[]{ }, false, "physical") },
        { "Sword Slash", new Move("Sword Slash", 20, new StatusEffect[]{ }, false, "physical") },
        { "Restoration", new Move("Restoration", -20, new StatusEffect[]{ }, true, "magic") },
        { "Severing Strike", new Move("Severing Strike", 5, new StatusEffect[]{allStatusEffects["Bleed"]}, false, "physical") },
        { "Rock Bottom", new Move("Rock Bottom", 10, new StatusEffect[]{allStatusEffects["Stun"]}, false, "physical") },
        { "Nut Throw", new Move("Nut Throw", 10, new StatusEffect[]{allStatusEffects["Weakness"] }, false, "physical") },
        { "People's Elbow", new Move("People's Elbow", 20, new StatusEffect[]{ }, false, "physical") },
        { "Petrifying Blast", new Move("Petrifying Blast", 45, new StatusEffect[]{ }, false, "magic") },
        { "Staff Smash", new Move("Staff Smash", 20, new StatusEffect[]{ }, false, "magic") },
        { "Howl", new Move("Howl", 0, new StatusEffect[]{allStatusEffects["Strength"] }, true, "magic") },
        { "Hypnotising Chirp", new Move("Hypnotising Chirp", 10, new StatusEffect[]{allStatusEffects["Stun"] }, false, "magic") },
        { "Rejuvenation", new Move("Rejuvenation", -10, new StatusEffect[]{ }, true, "magic") },
    };

    public static Dictionary<string, Character> allCharacters = new Dictionary<string, Character>()
    {
        {"Toa", new Character("Toa", "SpriteHero", 10, 0, 100, 100, false, false, new List<Move>(){allMoves["Sword Slash"], allMoves["Restoration"], allMoves["Severing Strike"] }, new List<StatusEffect>(){ })},
        {"Bewitched Treant", new Character("Bewitched Treant", "SpriteTreant", 3, 1, 50, 50, false, false, new List<Move>(){allMoves["Rejuvenation"] }, new List<StatusEffect>(){ })},
        {"Mole", new Character("Mole", "SpriteMole", 5, 1, 50, 50, false, false, new List<Move>(){ allMoves["Basic Attack"] }, new List<StatusEffect>(){ })},
        {"Bewitched Mole Slasher", new Character("Bewitched Mole Slasher", "SpriteMole", 5, 1, 50, 50, false, false, new List<Move>(){ allMoves["Bleed"], allMoves["Claw"] }, new List<StatusEffect>(){ })},
        {"Daisy", new Character("Daisy", "SpritePrincess", 5, 0, 50, 50, false, false, new List<Move>(){allMoves["Basic Heal"], allMoves["Basic Stun"], allMoves["Strength Potion"], allMoves["Weakness Potion"] }, new List<StatusEffect>(){ })},
        {"King Jebediah", new Character("King Jebediah", "SpriteOldMan", 9, 1, 120, 120, false, false, new List<Move>(){allMoves["Petrifying Blast"], allMoves["Staff Smash"], allMoves["Staff Smash"], allMoves["Restoration"] }, new List<StatusEffect>(){ })},
        {"Stranger", new Character("Stranger", "SpriteStranger", 5, 0, 5, 5, false, false, new List<Move>(){allMoves["Basic Attack"], allMoves["Nuke Cannon"]}, new List<StatusEffect>(){ })},
        {"King Jebediah Interrupt", new Character("King Jebediah", "SpriteOldMan", 5, 0, 5, 5, false, true, new List<Move>(){allMoves["Basic Heal"]}, new List<StatusEffect>(){ })},
        {"Narrator", new Character("Narrator", "SpriteTreant", 5, 0, 50, 50, false, false, new List<Move>(){allMoves["Basic Attack"]}, new List<StatusEffect>(){ })},
        {"Felix", new Character("Felix", "SpriteFox", 5, 0, 80, 80, false, false, new List<Move>(){allMoves["Claw"], allMoves["Howl"] }, new List<StatusEffect>(){ })},
        {"Dwayne", new Character("Dwayne", "SpriteStranger", 5, 0, 90, 90, false, false, new List<Move>(){allMoves["Rock Bottom"], allMoves["People's Elbow"] }, new List<StatusEffect>(){ })},
        {"Whaihua", new Character("Whaihua", "SpriteSquirrel", 5, 0, 70, 70, false, false, new List<Move>(){allMoves["Claw"], allMoves["Nut Throw"]}, new List<StatusEffect>(){ })},
        {"King Jebediah Interrupt1", new Character("King Jebediah", "SpriteOldMan", 5, 0, 5, 5, false, true, new List<Move>(){allMoves["Strength Potion"] }, new List<StatusEffect>(){ })},
        {"King Jebediah Interrupt2", new Character("King Jebediah", "SpriteOldMan", 5, 0, 5, 5, false, true, new List<Move>(){allMoves["Healing Potion"] }, new List<StatusEffect>(){ })},
        {"Bewitched Mole", new Character("Bewitched Mole", "SpriteMole", 5, 1, 50, 50, false, false, new List<Move>(){allMoves["Claw"] }, new List<StatusEffect>(){ })},
        {"Bewitched Fox", new Character("Bewitched Fox", "SpriteFox", 5, 1, 75, 75, false, false, new List<Move>(){allMoves["Claw"], allMoves["Howl"]  }, new List<StatusEffect>(){ })},
        {"Bewitched Grasshopper", new Character("Bewitched Grasshopper", "SpriteGrasshopper", 5, 1, 80, 80, false, false, new List<Move>(){allMoves["Claw"], allMoves["Hypnotising Chirp"] }, new List<StatusEffect>(){ })},
        {"Bewitched Squirrel", new Character("Bewitched Squirrel", "SpriteSquirrel", 5, 1, 80, 80, false, false, new List<Move>(){allMoves["Claw"], allMoves["Nut Throw"] }, new List<StatusEffect>(){ })}
    };

    public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
    {
        { "Depetrification Crystal", new Item("Depetrification Crystal", -70, new StatusEffect[]{ }, true, "magic") },
        { "Healcherry", new Item("Healcherry", -40, new StatusEffect[]{ }, true, "magic") }
    };

    public static List<Item> playerItems = new List<Item>()
    {
    };

    public static List<Character> playerParty = new List<Character>() { allCharacters["Toa"] };

    public static Dictionary<string, Character> currentCombatParticipants = new Dictionary<string, Character>();

    public static PlayerController player = GameObject.Find("Toa").GetComponent<PlayerController>();

    //Checkpoint stuff
    public static List<Item> checkpointPlayerItems = new List<Item>();

    public static Dictionary<string, bool> playerPartyCheckpointIsDeads = new Dictionary<string, bool>();

    public static bool testInterruptSave;
    //Checkpoint stuff end

    public static bool inCombat = false;

    public static bool isLastFight = false;

    
    //must remember to add story bools to this when I put new ones in
    public static void saveCurrentPlayerItemsAndIsDeads()
    {
        checkpointPlayerItems.Clear();
        foreach (Item item in playerItems) 
        {
            checkpointPlayerItems.Add(item);
        }

        playerPartyCheckpointIsDeads.Clear();
        foreach (Character character in playerParty)
        {
            playerPartyCheckpointIsDeads.Add(character.name, character.isDead);
        }

        testInterruptSave = StoryStaticStorage.testInterrupt;
}

    public static void restorePlayerToSave()
    {
        playerItems.Clear();
        foreach (Item item in checkpointPlayerItems)
        {
            playerItems.Add(item);
        }

        foreach (Character character in playerParty)
        {
            character.isDead = playerPartyCheckpointIsDeads[character.name];
        }

        StoryStaticStorage.testInterrupt = testInterruptSave;
    }

    public static void newEncounter(string[] participants)
    {
        //saveCurrentPlayerItemsAndIsDeads();
        currentCombatParticipants.Clear();
        foreach (string name in participants)
        {
            resetCharacter(allCharacters[name]);
            currentCombatParticipants.Add(name, allCharacters[name]);
        }
        SceneManager.LoadScene("CombatScene", LoadSceneMode.Additive);
        player.mainCamera.SetActive(false);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.depetrificationWindow.SetActive(false);
        inCombat = true;
    }

    public static void resetCharacter(Character character)
    {
        if (!playerParty.Contains(character))
        {
            character.isDead = false;
        }
        if (!character.isDead)
        {
            character.currentHealth = character.maxHealth;
        }
        
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
        public string spriteName;
        public int initiative;
        public int team;
        public int maxHealth;
        public int currentHealth;
        public bool isDead;
        public bool isInterrupt;
        public List<Move> moves;
        public List<StatusEffect> statusEffects;

        public Character(string name, string spriteName, int initiative, int team, int maxHealth, int currentHealth, bool isDead, bool isInterrupt, List<Move> moves, List<StatusEffect> statusEffects)
        {
            this.name = name;
            this.spriteName = spriteName;
            this.initiative = initiative;
            this.team = team;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.isDead = isDead;
            this.isInterrupt = isInterrupt;
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
        public int strength;
        public int maxTurnsRemaining;
        public int currentTurnsRemaining;

        public StatusEffect(string name, int dot, string element, bool stun, int strength, int maxTurnsRemaining, int currentTurnsRemaining)
        {
            this.name = name;
            this.dot = dot;
            this.element = element;
            this.stun = stun;
            this.strength = strength;
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

    public static int GetItemCount(Item item)
    {
        int count = 0;
        foreach (Item comparedItem in playerItems)
        {
            if (comparedItem == item)
            {
                count++;
            }
        }
        return count;
    }

    public static void UsePlayerItem(string itemName)
    {
        playerItems.Remove(allItems[itemName]);
    }
}
