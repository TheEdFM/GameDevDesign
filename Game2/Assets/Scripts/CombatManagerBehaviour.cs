using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public bool currentlyStunned;

    public Vector2 spawnPosFriendly = new Vector2(-119, 126);
    public Vector2 spawnPosEnemy = new Vector2(116, 126);
    public Vector2 spawnPosVerticalChange = new Vector2(0, -53.7f);
    public Vector2 spawnPosOffset = new Vector2(400, 225);
    public float offsetMultiplier = 1.25f;

    public GameObject canvas;

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

    public Dictionary<string, GameObject> friendlyStatuses = new Dictionary<string, GameObject>();
    public GameObject friendlyStatus1;
    public GameObject friendlyStatus2;
    public GameObject friendlyStatus3;
    public GameObject friendlyStatus4;

    public Dictionary<string, GameObject> enemyStatuses = new Dictionary<string, GameObject>();
    public GameObject enemyStatus1;
    public GameObject enemyStatus2;
    public GameObject enemyStatus3;
    public GameObject enemyStatus4;

    public Dictionary<string, GameObject> images = new Dictionary<string, GameObject>();
    public GameObject imageHero;
    public GameObject imageDaisy;
    public GameObject imageOldMan;
    public GameObject imageGirl;
    public GameObject imageGuy;
    public GameObject imageTreant;
    public GameObject imageMole;

    public Dictionary<string, GameObject> friendlyImages = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> enemyImages = new Dictionary<string, GameObject>();

    public GameObject damageIndicatorPrefab;

    public TextMeshProUGUI turnMenuText;
    public TextMeshProUGUI turnOrderText;

    // Start is called before the first frame update
    void Start()
    {
        state = "start";

        canvas = GameObject.Find("Canvas");

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

        friendlyStatus1 = GameObject.Find("FriendlyStatus1");
        friendlyStatus2 = GameObject.Find("FriendlyStatus2");
        friendlyStatus3 = GameObject.Find("FriendlyStatus3");
        friendlyStatus4 = GameObject.Find("FriendlyStatus4");
        friendlyStatuses.Add("FriendlyStatus1", friendlyStatus1);
        friendlyStatuses.Add("FriendlyStatus2", friendlyStatus2);
        friendlyStatuses.Add("FriendlyStatus3", friendlyStatus3);
        friendlyStatuses.Add("FriendlyStatus4", friendlyStatus4);

        enemyStatus1 = GameObject.Find("EnemyStatus1");
        enemyStatus2 = GameObject.Find("EnemyStatus2");
        enemyStatus3 = GameObject.Find("EnemyStatus3");
        enemyStatus4 = GameObject.Find("EnemyStatus4");
        enemyStatuses.Add("EnemyStatus1", enemyStatus1);
        enemyStatuses.Add("EnemyStatus2", enemyStatus2);
        enemyStatuses.Add("EnemyStatus3", enemyStatus3);
        enemyStatuses.Add("EnemyStatus4", enemyStatus4);

        imageHero = GameObject.Find("ImageHero");
        imageDaisy = GameObject.Find("ImageDaisy");
        imageOldMan = GameObject.Find("ImageOldMan");
        imageGirl = GameObject.Find("ImageGirl");
        imageGuy = GameObject.Find("ImageGuy");
        imageTreant = GameObject.Find("ImageTreant");
        imageMole = GameObject.Find("ImageMole");
        images.Add("ImageHero", imageHero);
        images.Add("ImageDaisy", imageDaisy);
        images.Add("ImageOldMan", imageOldMan);
        images.Add("ImageGirl", imageGirl);
        images.Add("ImageGuy", imageGuy);
        images.Add("ImageTreant", imageTreant);
        images.Add("ImageMole", imageMole);

        turnMenuText = GameObject.Find("PlayerTakingTurnText").GetComponent<TextMeshProUGUI>();
        turnOrderText = GameObject.Find("TurnOrderText").GetComponent<TextMeshProUGUI>();

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

        //Adding participants to party
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
            foreach (Character character in StaticStorage.allCharacters.Values) { //searching for prefabs
                if (character.name == participant.name)
                {
                    GameObject a = GameObject.Find(participant.name + " combat");
                    if (a != null)
                    {
                        participantPrefabs.Add(a);
                    }
                }
            }
        }

        //Queueing up participants based on their initiative
        combatParticipantsSortList.Sort(CompareInitiative);
        foreach (Character participant in combatParticipantsSortList)
        {
            turnOrder.Enqueue(participant);
        }

        int i;
        // Getting rid of status panels for characters that aren't there, and setting them up

        i = 1;
        foreach (Character character in friendlyParty)
        {
            TextMeshProUGUI[] nameAndNumericalHealth =  friendlyStatuses["FriendlyStatus" + i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in nameAndNumericalHealth)
            {
                if (text.text == "Name")
                {
                    text.text = character.name;
                }
                else if (text.text == "Status Effects")
                {
                    text.text = "";
                }
                else
                {
                    text.text = character.currentHealth+" / "+character.maxHealth;
                }
            }
            Image[] healthAndHealthBackground = friendlyStatuses["FriendlyStatus" + i].GetComponentsInChildren<Image>();
            foreach (Image image in healthAndHealthBackground)
            {
                if (image.type == Image.Type.Filled)
                {
                    image.fillAmount = (float)character.currentHealth/character.maxHealth;
                }
            }
            GameObject friendlyImage = Instantiate(images[character.imageName], (spawnPosFriendly + (spawnPosVerticalChange * (i - 1))) * offsetMultiplier + new Vector2(canvas.transform.position.x, canvas.transform.position.y), new Quaternion(0, 0, 0, 0));
            friendlyImage.transform.SetParent(canvas.transform);
            friendlyImages.Add("FriendlyImage" + i, friendlyImage);

            i++;
        }
        for (int j = i; j <= 4; j++) //getting rid of status panels for characters that aren't there
        {
            friendlyStatuses["FriendlyStatus" + j].SetActive(false);
        }

        i = 1;
        foreach (Character character in enemyParty)
        {
            TextMeshProUGUI[] nameAndNumericalHealth = enemyStatuses["EnemyStatus" + i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in nameAndNumericalHealth)
            {
                if (text.text == "Name")
                {
                    text.text = character.name;
                }
                else if (text.text == "Status Effects")
                {
                    text.text = "";
                }
                else
                {
                    text.text = character.currentHealth + " / " + character.maxHealth;
                }
            }
            Image[] healthAndHealthBackground = enemyStatuses["EnemyStatus" + i].GetComponentsInChildren<Image>();
            foreach (Image image in healthAndHealthBackground)
            {
                if (image.type == Image.Type.Filled)
                {
                    image.fillAmount = (float)character.currentHealth / character.maxHealth;
                }
            }
            GameObject enemyImage = Instantiate(images[character.imageName], (spawnPosEnemy + (spawnPosVerticalChange * ( i - 1 ))) * offsetMultiplier + new Vector2(canvas.transform.position.x, canvas.transform.position.y), new Quaternion(0,0,0,0));
            enemyImage.transform.SetParent(canvas.transform);
            enemyImages.Add("EnemyImage" + i, enemyImage);
            
            i++;
        }
        for (int j = i; j <= 4; j++) //getting rid of status panels for characters that aren't there
        {
            enemyStatuses["EnemyStatus" + j].SetActive(false);
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

                        string tempTurnOrderText = "";
                        int turnOrderI = 0;
                        foreach (Character c in turnOrder)
                        {
                            if (turnOrderI == turnOrder.Count-1)
                            {
                                tempTurnOrderText += c.name;
                            }
                            else
                            {
                                tempTurnOrderText += c.name + ", ";
                            }
                            turnOrderI++;
                        }
                        turnOrderText.text = tempTurnOrderText;

                        turnOrder.Enqueue(turnOrder.Dequeue()); //put them at the back of the queue

                        if (!playerTakingTurn.isDead)
                        {
                            //check for status effects
                            bool stunned = false; //stun wears off each turn
                            currentlyStunned = false;
                            List<StatusEffect> statusEffects = playerTakingTurn.statusEffects;
                            foreach (StatusEffect statusEffect in statusEffects)
                            {
                                if (!(statusEffect.currentTurnsRemaining <= 0))
                                {
                                    playerTakingTurn.currentHealth -= statusEffect.dot;

                                    //for damageIndicator
                                    GameObject image;
                                    if (playerTakingTurn.team == 0) //friendly
                                    {
                                        int imageIndex = friendlyParty.IndexOf(playerTakingTurn);
                                        image = friendlyImages["FriendlyImage" + (imageIndex + 1)];
                                    }
                                    else //enemy
                                    {
                                        int imageIndex = enemyParty.IndexOf(playerTakingTurn);
                                        image = enemyImages["EnemyImage" + (imageIndex + 1)];
                                    }
                                    GameObject damageIndicator = Instantiate(damageIndicatorPrefab, image.transform.position, new Quaternion(0, 0, 0, 0));
                                    damageIndicator.transform.SetParent(canvas.transform);
                                    TextMeshProUGUI textMeshProUGUI = damageIndicator.GetComponent<TextMeshProUGUI>();
                                    textMeshProUGUI.text = System.Math.Abs(statusEffect.dot).ToString();
                                    if (statusEffect.dot > 0)
                                    {
                                        textMeshProUGUI.color = new Color(1, 0, 0, textMeshProUGUI.color.a);
                                    }
                                    else if (statusEffect.dot < 0)
                                    {
                                        textMeshProUGUI.color = new Color(0, 1, 0, textMeshProUGUI.color.a);
                                    }
                                    else
                                    {
                                        textMeshProUGUI.color = new Color(1, 1, 0, textMeshProUGUI.color.a);
                                    }
                                    //end damageIndicator

                                    statusEffect.currentTurnsRemaining -= 1;

                                    RefreshHealth(playerTakingTurn);
                                    if (playerTakingTurn.isDead)
                                    {
                                        break;
                                    }

                                    if (statusEffect.stun)
                                    {
                                        stunned = true;
                                        currentlyStunned = true;
                                    }

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
                                                List<string> targetableCharacters = new List<string>();
                                                if (playerTakingTurn.moves[randomMoveNum].statusEffects.Length == 0) // if the applied to team move has no lasting effects eg: HoT or a buff, the ai will never heal a full health friend
                                                {
                                                    int i = 1;
                                                    foreach (Character character in enemyParty) //finding characters in need of healing
                                                    {
                                                        string targetName = "EnemyTarget" + i;
                                                        if (character.currentHealth < character.maxHealth && !character.isDead)
                                                        {
                                                            targetableCharacters.Add(targetName);
                                                        }
                                                        i++;
                                                    }
                                                    if (targetableCharacters.Count == 0) //if no one needs healing, heal a random alive character
                                                    {
                                                        int j = 1;
                                                        foreach (Character character in enemyParty)
                                                        {
                                                            if (!character.isDead)
                                                            {
                                                                string targetName = "EnemyTarget" + j;
                                                                targetableCharacters.Add(targetName);
                                                            }
                                                            j++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    int i = 1;
                                                    foreach (Character character in enemyParty) //cast on a random allied character instead of specifically people who need healing
                                                    {
                                                        if (!character.isDead)
                                                        {
                                                            string targetName = "EnemyTarget" + i;
                                                            targetableCharacters.Add(targetName);
                                                        }
                                                        i++;
                                                    }
                                                }
                                                randomTargetNum = Random.Range(0, targetableCharacters.Count);
                                                string tempChosenTargetName = targetableCharacters[randomTargetNum];
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
                                        turnMenuText.text = playerTakingTurn.name;
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
                                            moves["Move" + m].SetActive(true);
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
                                    state = "waiting";
                                    StartCoroutine("WaitEndTurn");
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

        //for damageIndicator
        GameObject image;
        int index;
        if (target.team == 0) //friendly
        {
            index = friendlyParty.IndexOf(target);
            image = friendlyImages["FriendlyImage" + (index + 1)];
        }
        else //enemy
        {
            index = enemyParty.IndexOf(target);
            image = enemyImages["EnemyImage" + (index + 1)];
        }
        GameObject damageIndicator = Instantiate(damageIndicatorPrefab, image.transform.position, new Quaternion(0, 0, 0, 0));
        damageIndicator.transform.SetParent(canvas.transform);
        TextMeshProUGUI textMeshProUGUI = damageIndicator.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = System.Math.Abs(move.damage).ToString();
        if (move.damage > 0)
        {
            textMeshProUGUI.color = new Color(1, 0, 0, textMeshProUGUI.color.a);
        }
        else if (move.damage < 0)
        {
            textMeshProUGUI.color = new Color(0, 1, 0, textMeshProUGUI.color.a);
        }
        else
        {
            textMeshProUGUI.color = new Color(1, 1, 0, textMeshProUGUI.color.a);
        }

        foreach (StatusEffect statusEffect in move.statusEffects)
        {
            target.statusEffects.Add(new StatusEffect(statusEffect.name, statusEffect.dot, statusEffect.element, statusEffect.stun, statusEffect.maxTurnsRemaining, statusEffect.currentTurnsRemaining));
        }

        RefreshHealth(target);

    }

    private void UseItem(Item item, Character target)
    {
        target.currentHealth -= item.damage;

        //for damageIndicator
        GameObject image;
        int index;
        if (target.team == 0) //friendly
        {
            index = friendlyParty.IndexOf(target);
            image = friendlyImages["FriendlyImage" + (index + 1)];
        }
        else //enemy
        {
            index = enemyParty.IndexOf(target);
            image = enemyImages["EnemyImage" + (index + 1)];
        }
        GameObject damageIndicator = Instantiate(damageIndicatorPrefab, image.transform.position, new Quaternion(0, 0, 0, 0));
        damageIndicator.transform.SetParent(canvas.transform);
        TextMeshProUGUI textMeshProUGUI = damageIndicator.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = System.Math.Abs(item.damage).ToString();
        if (item.damage > 0)
        {
            textMeshProUGUI.color = new Color(1, 0, 0, textMeshProUGUI.color.a);
        }
        else if (item.damage < 0)
        {
            textMeshProUGUI.color = new Color(0, 1, 0, textMeshProUGUI.color.a);
        }
        else
        {
            textMeshProUGUI.color = new Color(1, 1, 0, textMeshProUGUI.color.a);
        }

        foreach (StatusEffect statusEffect in item.statusEffects)
        {
            target.statusEffects.Add(new StatusEffect(statusEffect.name, statusEffect.dot, statusEffect.element, statusEffect.stun, statusEffect.maxTurnsRemaining, statusEffect.currentTurnsRemaining));
        }

        RefreshHealth(target);

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
            character.statusEffects = new List<StatusEffect>();
            character.isDead = true;
        }
        else
        {
            character.isDead = false;
        }

        int i;
        // Refreshing health panels

        i = 1;
        foreach (Character c in friendlyParty)
        {
            TextMeshProUGUI[] nameAndNumericalHealth = friendlyStatuses["FriendlyStatus" + i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in nameAndNumericalHealth)
            {
                if (text.text.Contains("/"))
                {
                    text.text = c.currentHealth + " / " + c.maxHealth;
                }
            }
            Image[] healthAndHealthBackground = friendlyStatuses["FriendlyStatus" + i].GetComponentsInChildren<Image>();
            foreach (Image image in healthAndHealthBackground)
            {
                if (image.type == Image.Type.Filled)
                {
                    image.fillAmount = (float)c.currentHealth / c.maxHealth;
                }
            }
            if (c.isDead)
            {
                friendlyImages["FriendlyImage" + i].transform.rotation = new Quaternion(0, 0, 0.25f, 0);
            }
            else
            {
                friendlyImages["FriendlyImage" + i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            i++;
        }

        i = 1;
        foreach (Character c in enemyParty)
        {
            TextMeshProUGUI[] nameAndNumericalHealth = enemyStatuses["EnemyStatus" + i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in nameAndNumericalHealth)
            {
                if (text.text.Contains("/"))
                {
                    text.text = c.currentHealth + " / " + c.maxHealth;
                }
            }
            Image[] healthAndHealthBackground = enemyStatuses["EnemyStatus" + i].GetComponentsInChildren<Image>();
            foreach (Image image in healthAndHealthBackground)
            {
                if (image.type == Image.Type.Filled)
                {
                    image.fillAmount = (float)c.currentHealth / c.maxHealth;
                }
            }
            if (c.isDead)
            {
                enemyImages["EnemyImage" + i].transform.rotation = new Quaternion(0, 0, 0.25f, 0);
            }
            else
            {
                enemyImages["EnemyImage" + i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            i++;
        }
        int index;
        if (character.team == 0) //friendly
        {
            index = friendlyParty.IndexOf(character);
        }
        else //enemy
        {
            index = enemyParty.IndexOf(character);
        }

        //Displaying status effects
        string statusEffectString = "";
        foreach (StatusEffect statusEffect in character.statusEffects)
        {
            if (statusEffect.currentTurnsRemaining > 0)
            {
                statusEffectString += statusEffect.name + ":" + statusEffect.currentTurnsRemaining + " ";
            }
        }

        if (character.team == 0) //friendly
        {
            TextMeshProUGUI[] nameAndNumericalHealth = friendlyStatuses["FriendlyStatus" + (index + 1)].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in nameAndNumericalHealth)
            {
                if (text.raycastTarget == false)
                {
                    text.text = statusEffectString;
                }
            }
        }
        else //enemy
        {
            TextMeshProUGUI[] nameAndNumericalHealth = enemyStatuses["EnemyStatus" + (index + 1)].GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in nameAndNumericalHealth)
            {
                if (text.raycastTarget == false)
                {
                    text.text = statusEffectString;
                }
            }
        }
        //end displaying status effects

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
        if (!currentlyStunned)
        {
            textMeshProUGUI.SetText(playerTakingTurnName + " used " + currentlySelectedMoveOrItemName + " on " + chosenTargetName);
        }
        else
        {
            textMeshProUGUI.SetText(playerTakingTurnName + " is stunned");
        }
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
