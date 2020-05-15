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

        string itemName = textMeshProUGUI.text; //name of the item that has been selected

        Debug.Log(itemName);

        //TODO: start target selection etc... change the state of the combat manager maybe?
    }
}

