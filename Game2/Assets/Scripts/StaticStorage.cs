using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticStorage
{
    public static Dictionary<string, StatusEffect> allStatusEffects = new Dictionary<string, StatusEffect>()
    {
        {"Stun", new StatusEffect("STN", 0 , "physical", true, 1, 1) },
        {"Bleed", new StatusEffect("BLD", 10 , "physical", false, 3, 3) }
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
        {"Toa", new Character("Toa", "SpriteHero", 10, 0, 10, 10, false, new List<Move>(){ allMoves["Basic Attack"], allMoves["Basic Bleed"], allMoves["Basic Stun"] }, new List<StatusEffect>(){ })},
        {"Treant", new Character("Treant", "SpriteTreant", 3, 1, 70, 70, false, new List<Move>(){allMoves["Basic Heal"] }, new List<StatusEffect>(){ })},
        {"Mole", new Character("Mole", "SpriteMole", 5, 1, 50, 50, false, new List<Move>(){ allMoves["Basic Attack"] }, new List<StatusEffect>(){ })},
        {"Mole Slasher", new Character("Mole Slasher", "SpriteMole", 5, 1, 50, 50, false, new List<Move>(){ allMoves["Basic Bleed"], allMoves["Basic Attack"] }, new List<StatusEffect>(){ })},
        {"Daisy", new Character("Daisy", "SpritePrincess", 5, 0, 50, 50, false, new List<Move>(){allMoves["Basic Heal"], allMoves["Basic Stun"] }, new List<StatusEffect>(){ })},
        {"King Jebediah", new Character("King Jebediah", "SpriteOldMan", 5, 0, 50, 50, false, new List<Move>(){allMoves["Basic Attack"]}, new List<StatusEffect>(){ })},
        {"Stranger", new Character("Stranger", "SpriteStranger", 5, 0, 500, 500, false, new List<Move>(){allMoves["Basic Attack"], allMoves["Nuke Cannon"]}, new List<StatusEffect>(){ })}
    };

    public static Dictionary<string, Item> allItems = new Dictionary<string, Item>()
    {
        { "Depetrification Crystal", new Item("Depetrification Crystal", -70, new StatusEffect[]{ }, true, "magic") },
        { "Healcherry", new Item("Healcherry", -40, new StatusEffect[]{ }, true, "magic") }
    };

    public static List<Item> playerItems = new List<Item>()
    {
        allItems["Depetrification Crystal"],
        allItems["Healcherry"],
        allItems["Healcherry"],
        allItems["Healcherry"]
    };

    public static List<Character> playerParty = new List<Character>() { allCharacters["Toa"], allCharacters["Daisy"] };

    public static Dictionary<string, Character> currentCombatParticipants = new Dictionary<string, Character>();

    public static PlayerController player = GameObject.Find("Toa").GetComponent<PlayerController>();

    public static void newEncounter(string[] participants)
    {
        currentCombatParticipants.Clear();
        foreach (string name in participants)
        {
            resetCharacter(allCharacters[name]);
            currentCombatParticipants.Add(name, allCharacters[name]);
        }
        SceneManager.LoadScene("CombatScene", LoadSceneMode.Additive);
        player.mainCamera.SetActive(false);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
        public List<Move> moves;
        public List<StatusEffect> statusEffects;

        public Character(string name, string spriteName, int initiative, int team, int maxHealth, int currentHealth, bool isDead, List<Move> moves, List<StatusEffect> statusEffects)
        {
            this.name = name;
            this.spriteName = spriteName;
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
