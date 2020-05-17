using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveListener : MonoBehaviour
{
    private Button button;
    private CombatManagerBehaviour combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManagerBehaviour>();
        button = GetComponent<Button>();
        button.onClick.AddListener(MoveButtonListener);
    }

    private void MoveButtonListener()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();

        string moveName = textMeshProUGUI.text; //name of the move that has been selected

        Debug.Log(moveName);
        combatManager.currentlySelectedMoveOrItemName = moveName;
        combatManager.currentlySelectedMoveOrItem = "move";

        combatManager.SetUpTargets(false);

        combatManager.targetSelectionMenu.SetActive(true);
        if (StaticStorage.allMoves[moveName].appliedToTeam)
        {
            combatManager.targetsEmptyGameObjects["FriendlyTargets"].SetActive(true);
            combatManager.targetsEmptyGameObjects["EnemyTargets"].SetActive(false);
        }
        else
        {
            combatManager.targetsEmptyGameObjects["FriendlyTargets"].SetActive(false);
            combatManager.targetsEmptyGameObjects["EnemyTargets"].SetActive(true);
        }
        
    }
}