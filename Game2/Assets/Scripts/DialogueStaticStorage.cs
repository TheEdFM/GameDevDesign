using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticStorage;
using static StoryStaticStorage;

public static class DialogueStaticStorage
{
    //Test dialogue

    public static Option testOption0 = new Option("It's been a long and tiresome journey, can you help us?",
                    "test1",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option testOption1 = new Option("Who are you?",
                    "test2",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option testOption2 = new Option("Thank you!",
                    "test3",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { allItems["Health Potion"] }, new Item[] { }, ""));
    public static Option testOption3 = new Option("Bye",
                    "test4",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode test0 = new DialogueNode("Welcome to my kingdom, " +
        "I hope your journey was manageable? " +
        "Go ahead, say something! " +
        "I'm going to keep speaking to show how this text jus rolls and rolls and rolls and now theres going to be a gap, also hopefully by this time " +
        "you'll be able to see the text wrapping around the whole of the panel oh yeah i have to leave a gap here it comes..\nboom its done its a line break" +
        "wow thats really cool ok say something now alternatively you could have just skipped this trash with the spacebar",
            new Option[] { testOption0, testOption1 },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test1 = new DialogueNode("Yes I can give you a health potion",
            new Option[] { testOption2 },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test2 = new DialogueNode("I am King Jebediah, ruler of this once powerful kingdom.",
            new Option[] { testOption0 },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test3 = new DialogueNode("No problem my young traveller, now be on your way!",
            new Option[] { testOption3 },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test4 = new DialogueNode("You had best be on your way, traveller.",
            new Option[] { testOption3 },
            allCharacters["King Jebediah"],
            true);

    public static Dialogue test = new Dialogue(test0);

    //Test dialogue end

    //testExtraDialogue dialogue start

    public static Option testExtraDialogueOption0 = new Option("Yes",
                    "testExtraDialogue1",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, allCharacters["Toa"], new Move[] { allMoves["Nuke Cannon"] }, allCharacters["Toa"], new Move[] { allMoves["Basic Attack"] }, new Character[] { allCharacters["Stranger"] }, new Character[] { allCharacters["Daisy"] }, new Item[] { }, new Item[] { allItems["Health Potion"] }, ""));
    public static Option testExtraDialogueOption1 = new Option("See you in combat!",
                    "testExtraDialogue1",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode testExtraDialogue0 = new DialogueNode("Hey can I join your adventure?",
            new Option[] { testExtraDialogueOption0 },
            allCharacters["Stranger"],
            false);
    public static DialogueNode testExtraDialogue1 = new DialogueNode("Good to be with you!",
            new Option[] { testExtraDialogueOption1 },
            allCharacters["Stranger"],
            true);

    public static Dialogue testExtraDialogue = new Dialogue(testExtraDialogue0);

    //testExtraDialogue dialogue end

    //This is here so they can all reference themselves and ones which were defined before themselves
    public static Dictionary<string, DialogueNode> allDialogueNodes = new Dictionary<string, DialogueNode>()
    {
        {"test0", test0 },
        {"test1", test1 },
        {"test2", test2 },
        {"test3", test3 },
        {"test4", test4 },
        {"testExtraDialogue0", testExtraDialogue0 },
        {"testExtraDialogue1", testExtraDialogue1 }
    };

    public class Dialogue
    {
        public DialogueNode currentNode;

        public Dialogue(DialogueNode currentNode)
        {
            this.currentNode = currentNode;
        }
    }

    public class DialogueNode
    {
        public readonly string characterText;
        public readonly Option[] options;
        public readonly Character character;
        public readonly bool exitDialogue;

        public DialogueNode(string characterText, Option[] options, Character character, bool exitDialogue)
        {
            this.characterText = characterText;
            this.options = options;
            this.character = character;
            this.exitDialogue = exitDialogue;
        }
    }

    public class Option
    {
        public readonly string optionText;
        public readonly string linkedNode;
        public readonly Requirements requirements;
        public readonly Effects effects;

        public Option(string optionText, string linkedNode, Requirements requirements, Effects effects)
        {
            this.optionText = optionText;
            this.linkedNode = linkedNode;
            this.requirements = requirements;
            this.effects = effects;
        }

        internal bool IsChooseable()
        {
            bool isChooseable = true;

            //check requirements

            float greedyGivingRatio = greedy / (greedy + giving);
            float cunningHonorRatio = cunning / (cunning + honor);
            float disdainVigilanceRatio = disdain / (disdain + vigilance);
            float evilGoodRatio = evil / (evil + good);

            if (requirements.greedyGivingRatioMax < greedyGivingRatio || requirements.greedyGivingRatioMin > greedyGivingRatio)
            {
                isChooseable = false;
            }
            if (requirements.cunningHonorRatioMax < cunningHonorRatio || requirements.cunningHonorRatioMin > cunningHonorRatio)
            {
                isChooseable = false;
            }
            if (requirements.disdainVigilanceRatioMax < disdainVigilanceRatio || requirements.disdainVigilanceRatioMin > disdainVigilanceRatio)
            {
                isChooseable = false;
            }
            if (requirements.evilGoodRatioMax < evilGoodRatio || requirements.evilGoodRatioMin > evilGoodRatio)
            {
                isChooseable = false;
            }
            //end check requirements

            //check storyengine bools eg: do you have an item true/false, have you done something true/false

            /*if (requirements.someStoryBool && !StoryStaticStorage.someStoryBool)
            {
                isChooseable = false;
            }*/

            //end check storyengine bools

            return isChooseable;
        }
    }
    public class Requirements
    {
        public readonly float greedyGivingRatioMax;
        public readonly float greedyGivingRatioMin;
        public readonly float cunningHonorRatioMax;
        public readonly float cunningHonorRatioMin;
        public readonly float disdainVigilanceRatioMax;
        public readonly float disdainVigilanceRatioMin;
        public readonly float evilGoodRatioMax;
        public readonly float evilGoodRatioMin;

        public Requirements(float greedyGivingRatioMax, float greedyGivingRatioMin, float cunningHonorRatioMax, float cunningHonorRatioMin, float disdainVigilanceRatioMax, float disdainVigilanceRatioMin, float evilGoodRatioMax, float evilGoodRatioMin)//and any bools from the story engine
        {
            this.greedyGivingRatioMax = greedyGivingRatioMax;
            this.greedyGivingRatioMin = greedyGivingRatioMin;
            this.cunningHonorRatioMax = cunningHonorRatioMax;
            this.cunningHonorRatioMin = cunningHonorRatioMin;
            this.disdainVigilanceRatioMax = disdainVigilanceRatioMax;
            this.disdainVigilanceRatioMin = disdainVigilanceRatioMin;
            this.evilGoodRatioMax = evilGoodRatioMax;
            this.evilGoodRatioMin = evilGoodRatioMin;
        }
    }
    public class Effects
    {
        public readonly float greedy;
        public readonly float giving;
        public readonly float cunning;
        public readonly float honor;
        public readonly float disdain;
        public readonly float vigilance;
        public readonly float evil;
        public readonly float good;
        public readonly Character characterToAddMoves;
        public readonly Move[] movesToAdd;
        public readonly Character[] charactersToAddToParty;
        public readonly Character[] charactersToRemoveFromParty;
        public readonly Character characterToRemoveMoves;
        public readonly Move[] movesToRemove;
        public readonly Item[] itemsToAdd;
        public readonly Item[] itemsToRemove;
        public readonly string storyEngineSwitchInput;

        public Effects(float greedy,
                       float giving,
                       float cunning,
                       float honor,
                       float disdain,
                       float vigilance,
                       float evil,
                       float good,
                       Character characterToAddMoves,
                       Move[] movesToAdd,
                       Character characterToRemoveMoves,
                       Move[] movesToRemove,
                       Character[] charactersToAddToParty,
                       Character[] charactersToRemoveFromParty,
                       Item[] itemsToAdd,
                       Item[] itemsToRemove,
                       string storyEngineSwitchInput)
        {
            this.greedy = greedy;
            this.giving = giving;
            this.cunning = cunning;
            this.honor = honor;
            this.disdain = disdain;
            this.vigilance = vigilance;
            this.evil = evil;
            this.good = good;
            this.characterToAddMoves = characterToAddMoves;
            this.movesToAdd = movesToAdd;
            this.charactersToAddToParty = charactersToAddToParty;
            this.charactersToRemoveFromParty = charactersToRemoveFromParty;
            this.characterToRemoveMoves = characterToRemoveMoves;
            this.movesToRemove = movesToRemove;
            this.itemsToAdd = itemsToAdd;
            this.itemsToRemove = itemsToRemove;
            this.storyEngineSwitchInput = storyEngineSwitchInput;
        }

        public void ApplyEffects()
        {
            StoryStaticStorage.greedy += greedy;
            StoryStaticStorage.giving += giving;
            StoryStaticStorage.cunning += cunning;
            StoryStaticStorage.honor += honor;
            StoryStaticStorage.disdain += disdain;
            StoryStaticStorage.vigilance += vigilance;
            StoryStaticStorage.evil += evil;
            StoryStaticStorage.good += good;

            if (characterToAddMoves != null)
            {
                foreach (Move move in movesToAdd)
                {
                    characterToAddMoves.moves.Add(move);
                }
            }
            foreach (Character character in charactersToAddToParty)
            {
                playerParty.Add(character);
            }
            foreach (Character character in charactersToRemoveFromParty)
            {
                playerParty.Remove(character);
            }
            if (characterToRemoveMoves != null)
            {
                foreach (Move move in movesToRemove)
                {
                    characterToAddMoves.moves.Remove(move);
                }
            }
            foreach (Item item in itemsToAdd)
            {
                playerItems.Add(item);
            }
            foreach (Item item in itemsToRemove)
            {
                playerItems.Remove(item);
            }
            storySwitch(storyEngineSwitchInput);
        }
    }
}
