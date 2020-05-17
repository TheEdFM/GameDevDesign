using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemListener : MonoBehaviour
{
    private Button button;
    private CombatManagerBehaviour combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManagerBehaviour>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ItemButtonListener);
    }

    private void ItemButtonListener()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();

        string[] nameParts = textMeshProUGUI.text.Split(" ".ToCharArray()); //item names are eg: "item1 (3)" as shown to the user
        string itemName = "";
        for (int i = 0; i < nameParts.Length-1; i++)
        {
            if (itemName == "")
            {
                itemName = nameParts[i];
            }
            else
            {
                itemName = itemName + " " + nameParts[i];
            }
        } 

        Debug.Log(itemName);
        combatManager.currentlySelectedMoveOrItemName = itemName;
        combatManager.currentlySelectedMoveOrItem = "item";

        if (itemName == "Resurrect Potion")
        {
            combatManager.SetUpTargets(true);
        }
        else
        {
            combatManager.SetUpTargets(false);
        }

        combatManager.targetSelectionMenu.SetActive(true);
        if (StaticStorage.allItems[itemName].appliedToTeam)
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

