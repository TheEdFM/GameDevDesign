using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetListener : MonoBehaviour
{
    private Button button;
    private CombatManagerBehaviour combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManagerBehaviour>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TargetButtonListener);
    }

    private void TargetButtonListener()
    {
        string targetName = name;

        Debug.Log(name);
        combatManager.SetChosenTargetName(name);

        combatManager.StartCoroutine("WaitEndTurn");
        combatManager.StartCoroutine("WaitUseMoveOrItem");
    }
}