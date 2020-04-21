using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static StaticStorage;

public class CombatManagerBehaviour : MonoBehaviour
{
    Queue<Character> turnOrder = new Queue<Character>();
    List<Character> combatParticipantsSortList = new List<Character>();
    List<Character> friendlyParty = new List<Character>();
    List<Character> enemyParty = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Character participant in StaticStorage.getCombatParticipants())
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
