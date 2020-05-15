using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationListener : MonoBehaviour
{
    private Button button;
    private CombatManagerBehaviour combatManager;

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManagerBehaviour>();
        button = GetComponent<Button>();
        button.onClick.AddListener(NavigationButtonListener);
    }

    private void NavigationButtonListener()
    {
        foreach (GameObject menu in combatManager.menus.Values)
        {
            menu.SetActive(false);
        }

        combatManager.menus[name+"enu"].SetActive(true);
    }
}
