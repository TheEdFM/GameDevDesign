using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FleeListener : MonoBehaviour
{
    private Button button;
    private CombatManagerBehaviour combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManagerBehaviour>();
        button = GetComponent<Button>();
        button.onClick.AddListener(FleeButtonListener);
    }

    private void FleeButtonListener()
    {
        Debug.Log("Fleeing has been chosen, but does nothing at the moment :)");

        //TODO: change the state of the combat manager maybe?
    }
}
