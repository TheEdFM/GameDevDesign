using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticStorage;

public class CombatManagerBehaviour : MonoBehaviour
{
    Queue<Character> turnOrder = new Queue<Character>();
    List<Character> combatParticipantsSortList = new List<Character>();
    List<Character> friendlyParty = new List<Character>();
    List<Character> enemyParty = new List<Character>();

    List<GameObject> participantPrefabs = new List<GameObject>();

    public string state;

    public float moveOrItemWaitTime = 1f;
    public float endTurnWaitTime = 4f;

    public string currentlySelectedMoveOrItem;
    public string currentlySelectedMoveOrItemName;
    public string chosenTargetName;
    public string playerTakingTurnName;

    public Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
    public GameObject turnMenu;
    public GameObject contextMenu;
    public GameObject itemMenu;
    public GameObject moveMenu;
    public GameObject fleeMenu;
    public GameObject targetSelectionMenu;

    public Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;

    public Dictionary<string, GameObject> moves = new Dictionary<string, GameObject>();
    public GameObject move1;
    public GameObject move2;
    public GameObject move3;
    public GameObject move4;

    public Dictionary<string, GameObject> targetsEmptyGameObjects = new Dictionary<string, GameObject>();
    public GameObject friendlyTargetsEmptyGameObject;
    public GameObject enemyTargetsEmptyGameObject;

    public Dictionary<string, GameObject> friendlyTargets = new Dictionary<string, GameObject>();
    public GameObject friendlyTarget1;
    public GameObject friendlyTarget2;
    public GameObject friendlyTarget3;
    public GameObject friendlyTarget4;

    public Dictionary<string, GameObject> enemyTargets = new Dictionary<string, GameObject>();
    public GameObject enemyTarget1;
    public GameObject enemyTarget2;
    public GameObject enemyTarget3;
    public GameObject enemyTarget4;

    // Start is called before the first frame update
    void Start()
    {
        state = "start";

        turnMenu = GameObject.Find("TurnMenu");
        contextMenu = GameObject.Find("ContextMenu");
        itemMenu = GameObject.Find("ItemMenu");
        moveMenu = GameObject.Find("MoveMenu");
        fleeMenu = GameObject.Find("FleeMenu");
        targetSelectionMenu = GameObject.Find("TargetSelectionMenu");
        menus.Add("TurnMenu", turnMenu);
        menus.Add("ContextMenu", contextMenu);
        menus.Add("ItemMenu", itemMenu);
        menus.Add("MoveMenu", moveMenu);
        menus.Add("FleeMenu", fleeMenu);
        menus.Add("TargetSelectionMenu", targetSelectionMenu);

        item1 = GameObject.Find("Item1");
        item2 = GameObject.Find("Item2");
        item3 = GameObject.Find("Item3");
        item4 = GameObject.Find("Item4");
        items.Add("Item1", item1);
        items.Add("Item2", item2);
        items.Add("Item3", item3);
        items.Add("Item4", item4);

        move1 = GameObject.Find("Move1");
        move2 = GameObject.Find("Move2");
        move3 = GameObject.Find("Move3");
        move4 = GameObject.Find("Move4");
        moves.Add("Move1", move1);
        moves.Add("Move2", move2);
        moves.Add("Move3", move3);
        moves.Add("Move4", move4);

        friendlyTargetsEmptyGameObject = GameObject.Find("FriendlyTargets");
        enemyTargetsEmptyGameObject = GameObject.Find("EnemyTargets");
        targetsEmptyGameObjects.Add("FriendlyTargets", friendlyTargetsEmptyGameObject);
        targetsEmptyGameObjects.Add("EnemyTargets", enemyTargetsEmptyGameObject);

        friendlyTarget1 = GameObject.Find("FriendlyTarget1");
        friendlyTarget2 = GameObject.Find("FriendlyTarget2");
        friendlyTarget3 = GameObject.Find("FriendlyTarget3");
        friendlyTarget4 = GameObject.Find("FriendlyTarget4");
        friendlyTargets.Add("FriendlyTarget1", friendlyTarget1);
        friendlyTargets.Add("FriendlyTarget2", friendlyTarget2);
        friendlyTargets.Add("FriendlyTarget3", friendlyTarget3);
        friendlyTargets.Add("FriendlyTarget4", friendlyTarget4);

        enemyTarget1 = GameObject.Find("EnemyTarget1");
        enemyTarget2 = GameObject.Find("EnemyTarget2");
        enemyTarget3 = GameObject.Find("EnemyTarget3");
        enemyTarget4 = GameObject.Find("EnemyTarget4");
        enemyTargets.Add("EnemyTarget1", enemyTarget1);
        enemyTargets.Add("EnemyTarget2", enemyTarget2);
        enemyTargets.Add("EnemyTarget3", enemyTarget3);
        enemyTargets.Add("EnemyTarget4", enemyTarget4);

        //This sets everything to inactive so it must come after finding the gameobjects
        foreach (GameObject menu in menus.Values)
        {
            menu.SetActive(false);
        }
        foreach (GameObject targetsEmptyGameObject in targetsEmptyGameObjects.Values)
        {
            targetsEmptyGameObject.SetActive(false);
        }
        friendlyTargetsEmptyGameObject.SetActive(false);
        enemyTargetsEmptyGameObject.SetActive(false);

        foreach (Character participant in StaticStorage.currentCombatParticipants.Values)
        {
            combatParticipantsSortList.Add(participant);
            if (participant.team == 0)
            {
                friendlyParty.Add(participant);
            }
            else
            {
                enemyParty.Add(participant);
            }
            foreach (Character character in StaticStorage.allCharacters.Values) {
                if (character.name == participant.name)
                {
                    GameObject a = GameObject.Find(participant.name + " combat");
                    if (a != null)
                    {
                        participantPrefabs.Add(a);
                    }
                    Debug.Log(participantPrefabs.Count);
                }
            }
        }
        //Queueing up participants based on their initiative

        combatParticipantsSortList.Sort(CompareInitiative);
        foreach (Character participant in combatParticipantsSortList)
        {
            turnOrder.Enqueue(participant);
        }


    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "end": //end of battle
                SceneManager.LoadScene("Overworld");
                break;
            case "start": // Prepare the player's selection, or prepare and complete the turn for ai
                {
                    contextMenu.SetActive(false);

                    //check to see if all of one team are dead, if all players team dies at the same time as the enemy, they lose
                    bool friendlyAlive = false;
                    foreach (Character character in friendlyParty){
                        if (!character.isDead)
                        {
                            friendlyAlive = true;
                            break;
                        }
                    }
                    bool enemyAlive = false;
                    foreach (Character character in enemyParty)
                    {
                        if (!character.isDead)
                        {
                            enemyAlive = true;
                            break;
                        }
                    }
                    if (!friendlyAlive)
                    {
                        state = "end";
                        Debug.Log("You lost");
                    }
                    else if (!enemyAlive)
                    {
                        state = "end";
                        Debug.Log("You won");
                    }
                    else
                    {

                        Character playerTakingTurn = turnOrder.Peek(); //which character is taking the turn
                        playerTakingTurnName = playerTakingTurn.name;
                        turnOrder.Enqueue(turnOrder.Dequeue()); //put them at the back of the queue

                        if (!playerTakingTurn.isDead)
                        {
                            //check for status effects
                            bool stunned = false; //stun wears off each turn
                            List<StatusEffect> statusEffects = playerTakingTurn.statusEffects;
                            foreach (StatusEffect statusEffect in statusEffects)
                            {
                                playerTakingTurn.currentHealth -= statusEffect.dot;
                                RefreshHealth(playerTakingTurn);

                                if (statusEffect.stun)
                                {
                                    stunned = true;
                                }

                                statusEffect.turnsRemaining -= 1;
                                if (statusEffect.turnsRemaining <= 0)
                                {
                                    statusEffects.Remove(statusEffect);
                                }
                            }
                            if (!playerTakingTurn.isDead)
                            {
                                if (!stunned)
                                {
                                    if (enemyParty.Contains(playerTakingTurn)) //enemy automatically takes action
                                    {
                                        int randomMoveNum = Random.Range(0, playerTakingTurn.moves.Length);
                                        int randomTargetNum;
                                        if (playerTakingTurn.moves[randomMoveNum].appliedToTeam)
                                        {
                                            currentlySelectedMoveOrItem = "move";
                                            currentlySelectedMoveOrItemName = playerTakingTurn.moves[randomMoveNum].name;
                                            while (true)
                                            {
                                                randomTargetNum = Random.Range(0, enemyParty.Count);
                                                string tempChosenTargetName = "EnemyTarget" + (randomTargetNum + 1);
                                                SetChosenTargetName(tempChosenTargetName);
                                                if (!StaticStorage.allCharacters[chosenTargetName].isDead)
                                                {
                                                    break;
                                                }
                                            }
                                            StartCoroutine("WaitEndTurn");
                                            StartCoroutine("WaitUseMoveOrItem");
                                        }
                                        else
                                        {
                                            currentlySelectedMoveOrItem = "move";
                                            currentlySelectedMoveOrItemName = playerTakingTurn.moves[randomMoveNum].name;
                                            while (true)
                                            {
                                                randomTargetNum = Random.Range(0, friendlyParty.Count);
                                                string tempChosenTargetName = "FriendlyTarget" + (randomTargetNum + 1);
                                                SetChosenTargetName(tempChosenTargetName);
                                                if (!StaticStorage.allCharacters[chosenTargetName].isDead)
                                                {
                                                    break;
                                                }
                                            }
                                            StartCoroutine("WaitEndTurn");
                                            StartCoroutine("WaitUseMoveOrItem");
                                        }
                                    }
                                    else //player is given a selection of their possible actions
                                    {
                                        //Adding the the player's items to their selection
                                        int i = 1;
                                        foreach (ItemAndNumberOwned itemAndNumberOwned in StaticStorage.playerItems.Values)
                                        {
                                            TextMeshProUGUI textMeshProUGUI = items["Item" + i].GetComponentInChildren<TextMeshProUGUI>();
                                            textMeshProUGUI.SetText(itemAndNumberOwned.item.name + " (" + itemAndNumberOwned.numberOwned + ")");
                                            i++;
                                        }
                                        for (int j = i; j <= 4; j++) //getting rid of buttons for items we dont have
                                        {
                                            items["Item" + j].SetActive(false);
                                        }

                                        //Adding the the player's moves to their selection
                                        int m = 1;
                                        foreach (Move move in playerTakingTurn.moves)
                                        {
                                            TextMeshProUGUI textMeshProUGUI = moves["Move" + m].GetComponentInChildren<TextMeshProUGUI>();
                                            textMeshProUGUI.SetText(move.name);
                                            m++;
                                        }
                                        for (int j = m; j <= 4; j++) //getting rid of buttons for moves we dont have
                                        {
                                            moves["Move" + j].SetActive(false);
                                        }

                                        turnMenu.SetActive(true);

                                        //all actions are taken by the listeners
                                    }

                                    state = "waiting"; //wait for user or ai to activate button listeners to progress + time for turn to animate etc
                                }
                                else //the ai or player is stunned
                                {

                                }
                            }
                        }
                    }
                    break;
                }

            default:
                break;
        }
    }

    private void UseMove(Move move, Character target)
    {
        target.currentHealth -= move.damage;
        RefreshHealth(target);
        foreach (StatusEffect statusEffect in move.statusEffects)
        {
            target.statusEffects.Add(statusEffect);
        }
    }

    private void UseItem(Item item, Character target)
    {
        target.currentHealth -= item.damage;
        RefreshHealth(target);
        foreach (StatusEffect statusEffect in item.statusEffects)
        {
            target.statusEffects.Add(statusEffect);
        }
        StaticStorage.UsePlayerItem(item.name); //reduced the number of items the player has, if 0 are left the item is removed from choices
    }

    private void RefreshHealth(Character character)
    {
        if (character.currentHealth < 0)
        {
            character.currentHealth = 0;
        }
        else if (character.currentHealth > character.maxHealth)
        {
            character.currentHealth = character.maxHealth;
        }

        if (character.currentHealth <= 0)
        {
            character.isDead = true;
            character.statusEffects = new List<StatusEffect>();
        }

        Debug.Log(character.name+"'s current health: "+character.currentHealth);
    }

    private static int CompareInitiative(Character participantX, Character participantY)
    {
        return -participantX.initiative.CompareTo(participantY.initiative);
    }

    public void SetChosenTargetName(string chosenTargetName)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (chosenTargetName == "FriendlyTarget" + i)
            {
                this.chosenTargetName = friendlyParty[i-1].name;
                break;
            }
            else if (chosenTargetName == "EnemyTarget" + i)
            {
                this.chosenTargetName = enemyParty[i-1].name;
                break;
            }
        }
    }

    public void SetUpTargets(bool rez)
    {
        int i;

        targetSelectionMenu.SetActiveRecursively(true); //we only set things to false in here so need to start with everything true

        // Getting rid of target buttons for characters that aren't there

        i = 1;
        foreach (Character character in friendlyParty)
        {
            i++;
        }
        for (int j = i; j <= 4; j++) //getting rid of buttons for characters that aren't there
        {
            friendlyTargets["FriendlyTarget" + j].SetActive(false);
        }

        i = 1;
        foreach (Character character in enemyParty)
        {
            i++;
        }
        for (int j = i; j <= 4; j++) //getting rid of buttons for characters that aren't there
        {
            enemyTargets["EnemyTarget" + j].SetActive(false);
        }

        //getting rid of buttons for characters who are dead, or vice versa for the rez item

        if (!rez)
        {
            i = 1;
            foreach (Character character in friendlyParty)
            {
                if (character.isDead)
                {
                    friendlyTargets["FriendlyTarget" + i].SetActive(false);
                }
                i++;
            }
            i = 1;
            foreach (Character character in enemyParty)
            {
                if (character.isDead)
                {
                    enemyTargets["EnemyTarget" + i].SetActive(false);
                }
                i++;
            }
        }
        else
        {
            i = 1;
            foreach (Character character in friendlyParty)
            {
                if (!character.isDead)
                {
                    friendlyTargets["FriendlyTarget" + i].SetActive(false);
                }
                i++;
            }
            i = 1;
            foreach (Character character in enemyParty)
            {
                if (!character.isDead)
                {
                    enemyTargets["EnemyTarget" + i].SetActive(false);
                }
                i++;
            }
        }
    }

    IEnumerator WaitEndTurn()
    {
        foreach (GameObject menu in menus.Values)
        {
            menu.SetActive(false);
        }

        TextMeshProUGUI textMeshProUGUI = contextMenu.GetComponentInChildren<TextMeshProUGUI>();
        textMeshProUGUI.SetText(playerTakingTurnName + " used " + currentlySelectedMoveOrItemName + " on " + chosenTargetName);
        contextMenu.SetActive(true);

        yield return new WaitForSeconds(endTurnWaitTime);

        state = "start";
    }

    IEnumerator WaitUseMoveOrItem()
    {
        yield return new WaitForSeconds(moveOrItemWaitTime);
        switch (currentlySelectedMoveOrItem)
        {
            case "move":
                UseMove(StaticStorage.allMoves[currentlySelectedMoveOrItemName], StaticStorage.currentCombatParticipants[chosenTargetName]);
                break;
            default:
                UseItem(StaticStorage.allItems[currentlySelectedMoveOrItemName], StaticStorage.currentCombatParticipants[chosenTargetName]);
                break;
        }
    }
}
