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

    public Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
    public GameObject turnMenu;
    public GameObject contextMenu;
    public GameObject itemMenu;
    public GameObject moveMenu;
    public GameObject fleeMenu;

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

    // Start is called before the first frame update
    void Start()
    {
        turnMenu = GameObject.Find("TurnMenu");
        contextMenu = GameObject.Find("ContextMenu");
        itemMenu = GameObject.Find("ItemMenu");
        moveMenu = GameObject.Find("MoveMenu");
        fleeMenu = GameObject.Find("FleeMenu");
        menus.Add("TurnMenu", turnMenu);
        menus.Add("ContextMenu", contextMenu);
        menus.Add("ItemMenu", itemMenu);
        menus.Add("MoveMenu", moveMenu);
        menus.Add("FleeMenu", fleeMenu);

        foreach (GameObject menu in menus.Values)
        {
            menu.SetActive(false);
        }

        turnMenu.SetActive(true);

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

        //Adding the the player's items to their selection
        int i = 1;
        foreach (ItemAndNumberOwned itemAndNumberOwned in StaticStorage.GetPlayerItems())
        {
            TextMeshProUGUI textMeshProUGUI = items["Item" + i].GetComponentInChildren<TextMeshProUGUI>();
            textMeshProUGUI.SetText(itemAndNumberOwned.item.name + " ("+ itemAndNumberOwned.numberOwned+")");
            i++;
        }
        //for (int j = i; j<=3; j++) //getting rid of buttons for items we dont have not sure why not working
        //{
        //    items["Item" + j].SetActive(false);
        //}

        foreach (Character participant in StaticStorage.GetCombatParticipants())
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
            foreach (Character character in StaticStorage.GetAllCharacters().Values) {
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
        combatParticipantsSortList.Sort(CompareInitiative);
        foreach (Character participant in combatParticipantsSortList)
        {
            turnOrder.Enqueue(participant);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (turnOrder.Count == 0) //end of battle
        {
            SceneManager.LoadScene("Overworld");
        }
        Character playerTakingTurn = turnOrder.Peek();

        //at start of turn
        List<StatusEffect> statusEffects = playerTakingTurn.statusEffects;
        bool stunned = false;
        foreach (StatusEffect statusEffect in statusEffects)
        {
            playerTakingTurn.currentHealth -= statusEffect.dot;
            if (statusEffect.stun)
            {
                stunned = true;
            }
        }
        
        foreach (StatusEffect statusEffect in statusEffects)
        {
            statusEffect.turnsRemaining -= 1;
            if (statusEffect.turnsRemaining <= 0)
            {
                statusEffects.Remove(statusEffect);
            }
        }

        if (!stunned)
        {
            if (enemyParty.Contains(playerTakingTurn)) //enemy automatically takes actions
            {
                int randomMoveNum = Random.Range(0, playerTakingTurn.moves.Length);
                int randomTargetNum;
                if (playerTakingTurn.moves[randomMoveNum].appliedToTeam)
                {
                    randomTargetNum = Random.Range(0, enemyParty.Count);
                    UseMove(playerTakingTurn.moves[randomMoveNum], enemyParty[randomTargetNum]);
                }
                else
                {
                    randomTargetNum = Random.Range(0, friendlyParty.Count);
                    UseMove(playerTakingTurn.moves[randomMoveNum], friendlyParty[randomTargetNum]);
                }
            }
            else //TODO: the player takes actions.
            //also pausing and stuff
            {
                int chosenMoveNum = 0;
                int chosenTargetNum = 0;

                //button popups for player choice

                if (playerTakingTurn.moves[chosenMoveNum].appliedToTeam)
                {
                    UseMove(playerTakingTurn.moves[chosenMoveNum], enemyParty[chosenTargetNum]);
                }
                else
                {
                    UseMove(playerTakingTurn.moves[chosenMoveNum], friendlyParty[chosenTargetNum]);
                }
            }

            turnOrder.Enqueue(turnOrder.Dequeue());
        }
    }

    private void UseMove(Move move, Character target)
    {
        target.currentHealth -= move.damage;
        foreach (StatusEffect statusEffect in move.statusEffects)
        {
            target.statusEffects.Add(statusEffect);
        }

        
    }

    private static int CompareInitiative(Character participantX, Character participantY)
    {
        return -participantX.initiative.CompareTo(participantY.initiative);
    }
}
