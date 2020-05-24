using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DepetrificationListener : MonoBehaviour
{
    private Button button;
    private CombatManagerBehaviour combatManager;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Toa").GetComponent<PlayerController>();
        button = GetComponent<Button>();
        button.onClick.AddListener(DepetrificationButtonListener);
    }

    private void DepetrificationButtonListener()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();

        string characterName = textMeshProUGUI.text;

        Debug.Log(characterName);

        if (StaticStorage.GetItemCount(StaticStorage.allItems["Depetrification Crystal"]) > 0)
        {
            StaticStorage.UsePlayerItem("Depetrification Crystal");
            StaticStorage.allCharacters[characterName].isDead = false;
            StaticStorage.allCharacters[characterName].currentHealth = StaticStorage.allCharacters[characterName].maxHealth;
            playerController.UpdateDepetrificationWindow();
        }
    }
}

