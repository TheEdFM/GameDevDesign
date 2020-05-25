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
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { allItems["Healcherry"] }, new Item[] { }, ""));
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

    public static DialogueNode test1 = new DialogueNode("Yes I can give you a Healcherry",
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
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, allCharacters["Toa"], new Move[] { allMoves["Nuke Cannon"] }, allCharacters["Toa"], new Move[] { allMoves["Basic Attack"] }, new Character[] { allCharacters["Stranger"] }, new Character[] { allCharacters["Daisy"] }, new Item[] { }, new Item[] { allItems["Healcherry"] }, ""));
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

    //testInterrupt dialogue

    public static Option testInterruptOption0 = new Option("Close dialogue",
                "testInterrupt0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option testInterruptOption1 = new Option("shouldn't show",
                    "testInterrupt1",
                    new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                    new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode testInterrupt0 = new DialogueNode("Hey I'm here to interrupt",
            new Option[] { testInterruptOption0 },
            allCharacters["King Jebediah Interrupt"],
            true);
    public static DialogueNode testInterrupt1 = new DialogueNode("shouldn't show",
            new Option[] { testInterruptOption1 },
            allCharacters["King Jebediah Interrupt"],
            true);

    public static Dialogue testInterruptDialogue = new Dialogue(testInterrupt0);

    //testInterrupt dialogue end

    //narrationStart dialogue

    public static Option narrationStartOption0 = new Option("Continue...",
                "narrationStart0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode narrationStart0 = new DialogueNode("Our hero, Toa, on his adventure for the pursuit of fame," +
        " glory, and riches has just escaped an unruly town mob overthrowing their mayor - which he had absolutely no part " +
        "in stirring up. None whatsoever. Definitely not. Now as he explores the wilderness, he comes across a clearing" +
        " with an odd air about it…",
            new Option[] { narrationStartOption0 },
            allCharacters["Narrator"],
            true);

    public static Dialogue narrationStartDialogue = new Dialogue(narrationStart0);

    //narrationStart dialogue end

    //kingIntroduction dialogue

    public static Option kingIntroductionOption0 = new Option("Continue",
                "kingIntroduction1",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option kingIntroductionOption1 = new Option("Continue",
                "kingIntroduction2",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option kingIntroductionOption2 = new Option("Of course I will!",
                "kingIntroduction4",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, "acceptKingQuest"));
    public static Option kingIntroductionOption3 = new Option("That sounds like a lot of work, what’s in it for me?",
            "kingIntroduction3",
            new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
            new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option kingIntroductionOption4 = new Option("Sorry, I fell asleep during your tale, can you repeat that for me?",
            "kingIntroduction0",
            new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
            new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option kingIntroductionOption5 = new Option("Goodbye.",
            "kingIntroduction5",
            new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
            new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode kingIntroduction0 = new DialogueNode("*cough cough* Hel-*cough* HELLO THERE! *cough* Sorry about the cough, my old vocal chords" +
        " haven’t been used in years! Not many people to talk to around here nowadays, not since the incident anyways. I’m sorry, allow me to introduce myself," +
        " I am King Jeremiah, the proud ruler of this kingdom you see around us. Many years ago when, shortly after I began my reign I had grown this place" +
        " into a prosperous and well respected kingdom. One strong enough to repel any, that made sure every belly went to bed full. Peaceful and free, a " +
        "place for people to truly become their best selves. If ever there was a perfect place, then it was to be found here, within these walls, within my" +
        " people, and within the bonds we shared.",
            new Option[] { kingIntroductionOption0 },
            allCharacters["King Jebediah"],
            false);
    public static DialogueNode kingIntroduction1 = new DialogueNode("It's not much of a kingdom anymore, not since that evil wizard cursed it and petrified " +
        "every last one of my doting, loyal subjects. Even the cattle and wild animals were hit by his spell, some turning to granite in the blink of an eye, " +
        "some simply being turned to madness - husks of themselves. He was a lonely and  jealous old fool, envious of our unity - although I may now be older " +
        "than he was when he stepped foot here. Keeping track of the years is hard by yourself…",
            new Option[] { kingIntroductionOption1 },
            allCharacters["King Jebediah"],
            false);
    public static DialogueNode kingIntroduction2 = new DialogueNode("He was nothing like you in your heroic armour, with your spry and powerful body still " +
        "filled with the power of youth. Foolishly he bound the curse to a sword, plunged deep into a stone at the center of the kingdom, telling me that " +
        "until it was lifted from that stone I would forever remain the only inhabitant of the kingdom, while the rest were sealed in their stone forms. " +
        "I tried many a time to lift the sword but as a frail old man my strength has failed me time and time again, if I only I could turn back the years " +
        "to my days when I was like you. So with great humility I must as you, will you help me get my people back?",
            new Option[] { kingIntroductionOption2, kingIntroductionOption3, kingIntroductionOption4 },
            allCharacters["King Jebediah"],
            false);
    public static DialogueNode kingIntroduction3 = new DialogueNode("When my kingdom is restored I will reward you with titles and wealth beyond your wildest " +
        "imagination, I ask again will you aid me good sir?",
            new Option[] { kingIntroductionOption2 },
            allCharacters["King Jebediah"],
            false);
    public static DialogueNode kingIntroduction4 = new DialogueNode("Praises upon you! The sword at the center of the kingdom lies to the <direction> of this " +
        "castles ruin. On your journey many animals may threaten you, in any time of peril when you, or an ally need it most, these Depetrification Crystals " +
        "will get you back to fighting fit in absolutely no time at all! {Press R to open a Depetrification menu outside of combat}\nIf you do happen to find " +
        "any of those crazed animals, deliver the finishing blow to them with this Rejuvenating McGuffin to Save the Animals.\nNow fly, and rid us all of this " +
        "wretched curse!",
            new Option[] { kingIntroductionOption5 },
            allCharacters["King Jebediah"],
            false);
    public static DialogueNode kingIntroduction5 = new DialogueNode("Hero, I cannot express my gratitude enough to you. Come back to me with the sword and I will " +
        "bestow glory upon you. Until then I wish you all the luck good sir!",
            new Option[] { kingIntroductionOption5 },
            allCharacters["King Jebediah"],
            true);

    public static Dialogue kingIntroductionDialogue = new Dialogue(kingIntroduction0);

    //kingIntroduction dialogue end

    //felix dialogue

    public static Option felixOption0 = new Option("Sure thing, I’m trying to free everyone from the curse, but I’ve got time enough to help you out.",
                "felix1",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option felixOption1 = new Option("Continue.",
                "felix2",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option felixOption2 = new Option("Let's go!",
                "felix4",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { allCharacters["Felix"] }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option felixOption3 = new Option("No way! Every second I waste looking for them is one second I’m not rich and famous from freeing them. " +
        "Besides when I lift the curse they’ll be free anyway.",
                "felix5",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option felixOption4 = new Option("Goodbye.",
                "felix6",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option felixOption5 = new Option("Continue.",
                "felix3",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode felix0 = new DialogueNode("And that Felicia is how we- " +
        "ohmygodwhoareyouwhereamIwhydoesthisplacelookfamiliarandwhydoesmyheadhurstsobad. Oh wow! My head hurts real bad. Like kicked in the head for " +
        "hundreds of years bad. Wow my memories are blurry as a bard’s. I can remember bits of the last…well...I don’t even know how long, mostly felt " +
        "like someone else was in my head, like a hand puppet moving my body. I can remember that crazy old kook mumbling something while I was talking " +
        "to my wife then- poof. Puppet.\nNow that I’m free though I feel like I should  try and find my wife, Francine and our son Francis.Can you spare " +
        "a few hours to help me look for them, or at least their stone statues?",
            new Option[] { felixOption0, felixOption3 },
            allCharacters["Felix"],
            false);
    public static DialogueNode felix1 = new DialogueNode("You spend a couple of hours searching the surrounding are for Felix’s family to no avail, and " +
        "you meet him back in the clearing later.",
            new Option[] { felixOption1 },
            allCharacters["Narrator"],
            false);
    public static DialogueNode felix2 = new DialogueNode("Well thanks for that! I managed to find their stone forms not too far from here, they’re in a safe " +
        "spot so they’ll be fine when they eventually get free. Well stranger, thanks for your help - I never did catch your name? Toa huh ? Earlier I heard " +
        "you say you were gonna try and free everyone, now I know the fate of my family for sure, I suppose I should help you out on your quest!",
            new Option[] { felixOption5 },
            allCharacters["Felix"],
            false);
    public static DialogueNode felix3 = new DialogueNode("Felix joined the party!",
            new Option[] { felixOption2 },
            allCharacters["Narrator"],
            false);
    public static DialogueNode felix4 = new DialogueNode("Come on lets go, my family is counting on us.",
            new Option[] { felixOption2 },
            allCharacters["Felix"],
            true);
    public static DialogueNode felix5 = new DialogueNode("Well thanks for freeing me from that spell all the same! I guess I’ll look for them if it’s all the " +
        "same to you sir, I just want to know they’re still out there...",
            new Option[] { felixOption4 },
            allCharacters["Felix"],
            false);
    public static DialogueNode felix6 = new DialogueNode("Well thanks for freeing me from that spell all the same! I guess I’ll look for them if it’s all the " +
        "same to you sir, I just want to know they’re still out there...",
            new Option[] { felixOption4 },
            allCharacters["Felix"],
            true);

    public static Dialogue felixDialogue = new Dialogue(felix0);

    //felix dialogue end

    //dwayne dialogue

    public static Option dwayneOption0 = new Option("Pour the potion over the statue",
                "dwayne1",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option dwayneOption1 = new Option("Walk away, keen to preserve the potions for when they could best help you, " +
        "and you alone. They’ll be free soon anyway.",
                "dwayne0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option dwayneOption2 = new Option("Join me in my quest to free the kingdom from this curse!",
                "dwayne2",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option dwayneOption3 = new Option("I guess I could use some help pulling the sword from the stone…",
                "dwayne2",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option dwayneOption4 = new Option("Lets go!",
                "dwayne4",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option dwayneOption5 = new Option("Continue",
                "dwayne3",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { allCharacters["Dwayne"] }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option dwayneOption6 = new Option("Lets go!",
                "dwayne3",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode dwayne0 = new DialogueNode("The statue speaks to you, emanating a great power, perhaps a Depetrification Crystal may free them " +
        "from their stony bonds. Will you use one?",
            new Option[] { dwayneOption0, dwayneOption1 },
            allCharacters["Narrator"],
            true);
    public static DialogueNode dwayne1 = new DialogueNode("*spluttering* Thank you for freeing me! That crazy wackjob may have sealed me and the rest of my " +
        "brothers, friends, family and everyone I know away years upon years ago but I can tell you that you’re still awake in there! My name’s Dwayne! " +
        "Who might you be, fearless liberator?\nThat’s an odd name, although I’m sure it must have some meaning where you’re from.You’ve done me a great favour " +
        "in unleashing me from the Rock. However can I repay you?",
            new Option[] { dwayneOption2, dwayneOption3 },
            allCharacters["Dwayne"],
            false);
    public static DialogueNode dwayne2 = new DialogueNode("Absolutely Toa! These monsters aren’t ready for what I’ve got cooking!!",
            new Option[] { dwayneOption4 },
            allCharacters["Dwayne"],
            false);
    public static DialogueNode dwayne3 = new DialogueNode("That sword's not going to pull itself!",
            new Option[] { dwayneOption6 },
            allCharacters["Dwayne"],
            true);
    public static DialogueNode dwayne4 = new DialogueNode("Dwayne joined the party!",
            new Option[] { dwayneOption5 },
            allCharacters["Narrator"],
            false);

    public static Dialogue dwayneDialogue = new Dialogue(dwayne0);

    //dwayne dialogue end

    //whaihua dialogue

    public static Option whaihuaOption0 = new Option("Of course, I can’t even imagine what it’s like going years without food!",
                "whaihua1",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption1 = new Option("I can feel how close I am to that sword, sorry but I can’t waste any time.",
                "whaihua4",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption2 = new Option("Continue",
                "whaihua2",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption3 = new Option("Thank you.",
                "whaihua3",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption4 = new Option("Continue",
                "whaihua6",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { allCharacters["Whaihua"] }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption5 = new Option("Goodbye.",
                "whaihua5",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption6 = new Option("Goodbye.",
                "whaihua5",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option whaihuaOption7 = new Option("Let's go!",
                "whaihua6",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode whaihua0 = new DialogueNode("-and that’s the recipe for my chestnut risott-OH. Oh this is something new. Oh that’s a new pain. " +
        "Oh that’s...that’s…that’s just nuts… HAHA GEDDIT? NUTS? NUTS. NUTS COS IM A SQUIRREL, AND I EAT NUTS. Man I really do ace this comedy thing. The name’s " +
        "Whaihua. And that talk about nuts has got me crazy hungry. It feels like I ain’t eaten in yoooooooonks friend. Any chance you could help me gather up " +
        "some food?",
            new Option[] { whaihuaOption0, whaihuaOption1 },
            allCharacters["Whaihua"],
            true);
    public static DialogueNode whaihua1 = new DialogueNode("Together you and Whaihua collect up enough nuts and berries to fill up an army! You and your allies " +
        "feast together filling your bellies ahead of what may be your toughest challenge yet.",
            new Option[] { whaihuaOption2 },
            allCharacters["Narrator"],
            false);
    public static DialogueNode whaihua2 = new DialogueNode("Well thanks Toa! On a full stomach I’m a more than competent fighter and adventurer, so I’d love " +
        "nothing more than to help you on your quest to lift this horrible curse!",
            new Option[] { whaihuaOption3 },
            allCharacters["Whaihua"],
            false);
    public static DialogueNode whaihua3 = new DialogueNode("Whaihua joined the party!",
            new Option[] { whaihuaOption4 },
            allCharacters["Narrator"],
            false);
    public static DialogueNode whaihua4 = new DialogueNode("Well shucks. I totally understand though man, but I gotta get me some eats before I collapse!",
            new Option[] { whaihuaOption5 },
            allCharacters["Whaihua"],
            false);
    public static DialogueNode whaihua5 = new DialogueNode("See you around...",
            new Option[] { whaihuaOption6 },
            allCharacters["Whaihua"],
            true);
    public static DialogueNode whaihua6 = new DialogueNode("Come on, we need to save the kingdom, although holy... I am full.",
            new Option[] { whaihuaOption7 },
            allCharacters["Whaihua"],
            true);

    public static Dialogue whaihuaDialogue = new Dialogue(whaihua0);

    //whaihua dialogue end

    //kingAttack dialogue

    public static Option kingAttackOption0 = new Option("Continue",
                "kingAttack0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode kingAttack0 = new DialogueNode("Curses. Looks like those worthless animals under my spell were unable to even wear you down correctly!" +
        "\nHeavens that uninspired look on your face informs me you really haven’t pieced it together yet have you ? Imbecile.\nI cursed this kingdom and its " +
        "people. Constantly begging from me, leeching off me and my wealth. So of course when that wizard showed up offering to extend my lifespan and " +
        "eternally rid me of those pests, I lept at the opportunity. In fact, for every petrified person or animal their remaining lifespan was added to mine. " +
        "And now, I will add yours to mine! Prepare to die peasant!",
            new Option[] { kingAttackOption0 },
            allCharacters["King Jebediah"],
            true);

    public static Dialogue kingAttackDialogue = new Dialogue(kingAttack0);

    //kingAttack dialogue end

    //kingDeath dialogue

    public static Option kingDeathOption0 = new Option("Continue",
                "kingDeath1",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));
    public static Option kingDeathOption1 = new Option("Continue",
                "kingDeath0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode kingDeath0 = new DialogueNode("Bleugh, you may have beaten me but you’ll never...oh you’ve pulled the sword out with ease…\nBUT " +
        "THAT MEANS.\nNO.\nNO.\nI’m not ready to d - ...",
            new Option[] { kingDeathOption0 },
            allCharacters["King Jebediah"],
            true);
    public static DialogueNode kingDeath1 = new DialogueNode("Shortly after you pull the sword of the stone, the king turns to stone and almost immediately " +
        "crumbles to dust. All that remains of the selfish king is a small mound of fine gray matter.",
            new Option[] { kingDeathOption1 },
            allCharacters["Narrator"],
            false);

    public static Dialogue kingDeathDialogue = new Dialogue(kingDeath0);

    //kingDeath dialogue end

    //loss dialogue

    public static Option lossOption0 = new Option("Continue",
                "loss0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode loss0 = new DialogueNode("Energy depleted, you are dragged to the sword, and similar to the kingdoms citizens, you become " +
        "petrified, your body turned to stone.",
            new Option[] { lossOption0 },
            allCharacters["Narrator"],
            true);

    public static Dialogue lossDialogue = new Dialogue(loss0);

    //loss dialogue end

    //kingInterruptFirst dialogue

    public static Option kingInterruptFirstOption0 = new Option("Continue",
                "kingInterruptFirst0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode kingInterruptFirst0 = new DialogueNode("Greetings hero, I’m glad I encountered you here! I found this attack boosting potion " +
        "knocking around my throne room!\nHere!\nCatch!",
            new Option[] { kingInterruptFirstOption0 },
            allCharacters["King Jebediah Interrupt1"],
            true);

    public static Dialogue kingInterruptFirstDialogue = new Dialogue(kingInterruptFirst0);

    //kingInterruptFirst dialogue end

    //kingInterruptSecond dialogue

    public static Option kingInterruptSecondOption0 = new Option("Continue",
                "kingInterruptSecond0",
                new Requirements(1, 0, 1, 0, 1, 0, 1, 0),
                new Effects(0, 0, 0, 0, 0, 0, 0, 0, null, new Move[] { }, null, new Move[] { }, new Character[] { }, new Character[] { }, new Item[] { }, new Item[] { }, ""));

    public static DialogueNode kingInterruptSecond0 = new DialogueNode("Salutations hero, what a chance meeting! You’re looking a bit beaten up there, " +
        "let me heal you up!",
            new Option[] { kingInterruptSecondOption0 },
            allCharacters["King Jebediah Interrupt2"],
            true);

    public static Dialogue kingInterruptSecondDialogue = new Dialogue(kingInterruptSecond0);

    //kingInterruptSecond dialogue end

    //This is here so they can all reference themselves and ones which were defined before themselves
    public static Dictionary<string, DialogueNode> allDialogueNodes = new Dictionary<string, DialogueNode>()
    {
        {"test0", test0 },
        {"test1", test1 },
        {"test2", test2 },
        {"test3", test3 },
        {"test4", test4 },
        {"testExtraDialogue0", testExtraDialogue0 },
        {"testExtraDialogue1", testExtraDialogue1 },
        {"testInterrupt0", testInterrupt0 },
        {"testInterrupt1", testInterrupt1 },
        {"narrationStart0", narrationStart0 },
        {"kingIntroduction0", kingIntroduction0 },
        {"kingIntroduction1", kingIntroduction1 },
        {"kingIntroduction2", kingIntroduction2 },
        {"kingIntroduction3", kingIntroduction3 },
        {"kingIntroduction4", kingIntroduction4 },
        {"kingIntroduction5", kingIntroduction5 },
        {"felix0", felix0 },
        {"felix1", felix1 },
        {"felix2", felix2 },
        {"felix3", felix3 },
        {"felix4", felix4 },
        {"felix5", felix5 },
        {"felix6", felix6 },
        {"dwayne0", dwayne0 },
        {"dwayne1", dwayne1 },
        {"dwayne2", dwayne2 },
        {"dwayne3", dwayne3 },
        {"dwayne4", dwayne4 },
        {"whaihua0", whaihua0 },
        {"whaihua1", whaihua1 },
        {"whaihua2", whaihua2 },
        {"whaihua3", whaihua3 },
        {"whaihua4", whaihua4 },
        {"whaihua5", whaihua5 },
        {"whaihua6", whaihua6 },
        {"kingDeath0", kingDeath0 },
        {"kingDeath1", kingDeath1 },
        {"kingAttack0", kingAttack0 },
        {"loss0", loss0 },
        {"kingInterruptFirst0", kingInterruptFirst0 },
        {"kingInterruptSecond0", kingInterruptSecond0 }
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
