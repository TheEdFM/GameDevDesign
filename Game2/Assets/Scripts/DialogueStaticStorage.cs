using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StaticStorage;

public static class DialogueStaticStorage
{
    //Test dialogue

    public static DialogueNode test0 = new DialogueNode("Welcome to my kingdom," +
        " I hope your journey was manageable?" +
        " Go ahead, say something!",
            new Option[] {
                new Option("It's been a long and tiresome journey, can you help us?",
                    "test1",
                    new Requirements(1,0,1,0,1,0,1,0),
                    new Effects(0,0,0,0,0,0,0,0,new Character[]{ },new Move[]{ },new Character[]{ },new Character[]{ },new Move[]{ },new Item[]{ },new Item[]{ },"")),
                new Option("Who are you?",
                    "test2",
                    new Requirements(1,0,1,0,1,0,1,0),
                    new Effects(0,0,0,0,0,0,0,0,new Character[]{ },new Move[]{ },new Character[]{ },new Character[]{ },new Move[]{ },new Item[]{ },new Item[]{ },""))
            },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test1 = new DialogueNode("Yes I can give you a health potion",
            new Option[] {
                new Option("Thank you!",
                    "test3",
                    new Requirements(1,0,1,0,1,0,1,0),
                    new Effects(0,0,0,0,0,0,0,0,new Character[]{ },new Move[]{ },new Character[]{ },new Character[]{ },new Move[]{ },new Item[]{allItems["Health Potion"]},new Item[]{ },"")) },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test2 = new DialogueNode("I an King Jebediah, ruler of this once powerful kingdom.",
            new Option[] {
                new Option("It's been a long and tiresome journey, can you help us?",
                    "test1",
                    new Requirements(1,0,1,0,1,0,1,0),
                    new Effects(0,0,0,0,0,0,0,0,new Character[]{ },new Move[]{ },new Character[]{ },new Character[]{ },new Move[]{ },new Item[]{},new Item[]{ },"")) },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test3 = new DialogueNode("No problem my young traveller, now be on your way!",
            new Option[] {
                new Option("Bye",
                    "test4",
                    new Requirements(1,0,1,0,1,0,1,0),
                    new Effects(0,0,0,0,0,0,0,0,new Character[]{ },new Move[]{ },new Character[]{ },new Character[]{ },new Move[]{ },new Item[]{},new Item[]{ },"")) },
            allCharacters["King Jebediah"],
            false);

    public static DialogueNode test4 = new DialogueNode("You had best be on your way, traveller.",
            new Option[] {
                new Option("Bye",
                    "test4",
                    new Requirements(1,0,1,0,1,0,1,0),
                    new Effects(0,0,0,0,0,0,0,0,new Character[]{ },new Move[]{ },new Character[]{ },new Character[]{ },new Move[]{ },new Item[]{},new Item[]{ },"")) },
            allCharacters["King Jebediah"],
            true);

    public static Dialogue test = new Dialogue(test0);
    //Test dialogue end

    public static Dictionary<string, DialogueNode> allDialogueNodes = new Dictionary<string, DialogueNode>()
    {
        {"test0", test0 },
        {"test1", test1 },
        {"test2", test2 },
        {"test3", test3 },
        {"test4", test4 }
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
        public readonly float evilGoodRatioMMin;

        public Requirements(float greedyGivingRatioMax, float greedyGivingRatioMin, float cunningHonorRatioMax, float cunningHonorRatioMin, float disdainVigilanceRatioMax, float disdainVigilanceRatioMin, float evilGoodRatioMax, float evilGoodRatioMMin)//and any bools from the story engine
        {
            this.greedyGivingRatioMax = greedyGivingRatioMax;
            this.greedyGivingRatioMin = greedyGivingRatioMin;
            this.cunningHonorRatioMax = cunningHonorRatioMax;
            this.cunningHonorRatioMin = cunningHonorRatioMin;
            this.disdainVigilanceRatioMax = disdainVigilanceRatioMax;
            this.disdainVigilanceRatioMin = disdainVigilanceRatioMin;
            this.evilGoodRatioMax = evilGoodRatioMax;
            this.evilGoodRatioMMin = evilGoodRatioMMin;
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
        public float[] gGCHDVEG = new float[8];
        public readonly Character[] charactersToAddMoves;
        public readonly Move[] movesToAdd;
        public readonly Character[] charactersToAddToParty;
        public readonly Character[] charactersToRemoveMoves;
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
                       Character[] charactersToAddMoves,
                       Move[] movesToAdd,
                       Character[] charactersToAddToParty,
                       Character[] charactersToRemoveMoves,
                       Move[] movesToRemove,
                       Item[] itemsToAdd,
                       Item[] itemsToRemove,
                       string storyEngineSwitchInput)
        {
            this.greedy = greedy;
            gGCHDVEG[0] = greedy;
            this.giving = giving;
            gGCHDVEG[1] = giving;
            this.cunning = cunning;
            gGCHDVEG[2] = cunning;
            this.honor = honor;
            gGCHDVEG[3] = honor;
            this.disdain = disdain;
            gGCHDVEG[4] = disdain;
            this.vigilance = vigilance;
            gGCHDVEG[5] = vigilance;
            this.evil = evil;
            gGCHDVEG[6] = evil;
            this.good = good;
            gGCHDVEG[7] = good;
            this.charactersToAddMoves = charactersToAddMoves;
            this.movesToAdd = movesToAdd;
            this.charactersToAddToParty = charactersToAddToParty;
            this.charactersToRemoveMoves = charactersToRemoveMoves;
            this.movesToRemove = movesToRemove;
            this.itemsToAdd = itemsToAdd;
            this.itemsToRemove = itemsToRemove;
            this.storyEngineSwitchInput = storyEngineSwitchInput;
        }
    }
}
