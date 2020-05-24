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
    public int currentStrength;

    public Vector2 spawnPosFriendly;
    public Vector2 spawnPosEnemy;
    public Vector2 spawnPosVerticalChange;
    public Vector2 spawnPosOffset;
    public float offsetMultiplier;

    public string currentInterruptTargetName;

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

    public Dictionary<string, GameObject> sprites = new Dictionary<string, GameObject>();
    public GameObject spriteHero;
    public GameObject spritePrincess;
    public GameObject spriteOldMan;
    public GameObject spriteGirl;
    public GameObject spriteGuy;
    public GameObject spriteTreant;
    public GameObject spriteMole;
    public GameObject spriteStranger;

    public Dictionary<string, GameObject> friendlySprites = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> enemySprites = new Dictionary<string, GameObject>();

    public GameObject damageIndicatorPrefab;

    public TextMeshProUGUI turnMenuText;
    public TextMeshProUGUI turnOrderText;

    public List<GameObject> cloneList = new List<GameObject>();

    DialogueController dialogueController;

    public GameObject mainCameraCombat;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosFriendly = new Vector2(-2.08f, 6.5f);
        spawnPosEnemy = new Vector2(10.11f, 6.5f);
        spawnPosVerticalChange = new Vector2(0, -2.75f);
        spawnPosOffset = new Vector2(400, 225);
        offsetMultiplier = 1.25f;

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

        spriteHero = GameObject.Find("SpriteHero");
        spritePrincess = GameObject.Find("SpritePrincess");
        spriteOldMan = GameObject.Find("SpriteOldMan");
        spriteGirl = GameObject.Find("SpriteGirl");
        spriteGuy = GameObject.Find("SpriteGuy");
        spriteTreant = GameObject.Find("SpriteTreant");
        spriteMole = GameObject.Find("SpriteMole");
        spriteStranger = GameObject.Find("SpriteStranger");
        sprites.Add("SpriteHero", spriteHero);
        sprites.Add("SpritePrincess", spritePrincess);
        sprites.Add("SpriteOldMan", spriteOldMan);
        sprites.Add("SpriteGirl", spriteGirl);
        sprites.Add("SpriteGuy", spriteGuy);
        sprites.Add("SpriteTreant", spriteTreant);
        sprites.Add("SpriteMole", spriteMole);
        sprites.Add("SpriteStranger", spriteStranger);

        turnMenuText = GameObject.Find("PlayerTakingTurnText").GetComponent<TextMeshProUGUI>();
        turnOrderText = GameObject.Find("TurnOrderText").GetComponent<TextMeshProUGUI>();

        dialogueController = GameObject.Find("DialogueCombat").GetComponent<DialogueController>();

        mainCameraCombat = GameObject.Find("Main Camera Combat");

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

            GameObject friendlySprite = Instantiate(sprites[character.spriteName], spawnPosFriendly + (spawnPosVerticalChange * (i - 1)), new Quaternion(0, 0, 0, 0));
            cloneList.Add(friendlySprite);
            friendlySprite.transform.localScale = new Vector3(friendlySprite.transform.localScale.x * -2, friendlySprite.transform.localScale.y * 2, friendlySprite.transform.localScale.y * 2);
            SpriteRenderer friendlySpriteSR = friendlySprite.GetComponent<SpriteRenderer>();
            friendlySpriteSR.sortingOrder = 101;
            friendlySprites.Add("FriendlySprite" + i, friendlySprite);

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

            GameObject enemySprite = Instantiate(sprites[character.spriteName], spawnPosEnemy + (spawnPosVerticalChange * (i - 1)), new Quaternion(0, 0, 0, 0));
            cloneList.Add(enemySprite);
            enemySprite.transform.localScale = new Vector3(enemySprite.transform.localScale.x * -2, enemySprite.transform.localScale.y * 2, enemySprite.transform.localScale.y * 2);
            SpriteRenderer enemySpriteSR = enemySprite.GetComponent<SpriteRenderer>();
            enemySpriteSR.sortingOrder = 101;
            enemySprites.Add("EnemySprite" + i, enemySprite);

            i++;
        }
        for (int j = i; j <= 4; j++) //getting rid of status panels for characters that aren't there
        {
            enemyStatuses["EnemyStatus" + j].SetActive(false);
        }

        foreach (Character character in combatParticipantsSortList)
        {
            RefreshHealth(character);
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "end": //end of battle
                //SceneManager.LoadScene("Overworld");
                foreach (GameObject clone in cloneList)
                {
                    Destroy(clone);
                }
                mainCameraCombat.SetActive(false);
                SceneManager.UnloadSceneAsync("CombatScene");
                player.mainCamera.SetActive(true);
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                inCombat = false;
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
                    
                    if (!enemyAlive && friendlyAlive)
                    {
                        if (allCharacters["Toa"].isDead == true)
                        {
                            if (GetItemCount(allItems["Depetrification Crystal"]) > 0)
                            {
                                StaticStorage.UsePlayerItem("Depetrification Crystal");
                                StaticStorage.allCharacters["Toa"].isDead = false;
                                StaticStorage.allCharacters["Toa"].currentHealth = StaticStorage.allCharacters["Toa"].maxHealth;
                                state = "end";
                                Debug.Log("You won");
                            }
                            else
                            {
                                restorePlayerToSave();
                                state = "end";
                                Debug.Log("You lost");
                            }
                        }
                        else
                        {
                            state = "end";
                            Debug.Log("You won");
                        }
                    }
                    else if (!friendlyAlive)
                    {
                        restorePlayerToSave();
                        state = "end";
                        Debug.Log("You lost");
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

                        if (playerTakingTurn.isInterrupt)
                        {
                            turnOrder.Dequeue(); //dont put them at the back of the queue
                        }
                        else
                        {
                            turnOrder.Enqueue(turnOrder.Dequeue()); //put them at the back of the queue
                        }

                        if (!playerTakingTurn.isDead)
                        {
                            //check for status effects
                            bool stunned = false; //stun wears off each turn
                            currentlyStunned = false;
                            List<StatusEffect> statusEffects = playerTakingTurn.statusEffects;
                            currentStrength = 0;
                            foreach (StatusEffect statusEffect in statusEffects)
                            {
                                if (!(statusEffect.currentTurnsRemaining <= 0))
                                {
                                    playerTakingTurn.currentHealth -= statusEffect.dot;

                                    currentStrength = statusEffect.strength;

                                    //for damageIndicator
                                    if (!(statusEffect.strength != 0 && statusEffect.dot == 0 && statusEffect.stun == false)) {
                                        GameObject target;
                                        if (playerTakingTurn.team == 0) //friendly
                                        {
                                            int targetIndex = friendlyParty.IndexOf(playerTakingTurn);
                                            target = friendlyTargets["FriendlyTarget" + (targetIndex + 1)];
                                        }
                                        else //enemy
                                        {
                                            int targetIndex = enemyParty.IndexOf(playerTakingTurn);
                                            target = enemyTargets["EnemyTarget" + (targetIndex + 1)];
                                        }
                                        GameObject damageIndicator = Instantiate(damageIndicatorPrefab, target.transform.position, new Quaternion(0, 0, 0, 0));
                                        cloneList.Add(damageIndicator);
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
                                        int randomMoveNum = Random.Range(0, playerTakingTurn.moves.Count);
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
                                    else if (friendlyParty.Contains(playerTakingTurn)) //player is given a selection of their possible actions
                                    {
                                        turnMenuText.text = playerTakingTurn.name;
                                        //Adding the the player's items to their selection
                                        int i = 1;
                                        List<Item> seenItems = new List<Item>();
                                        foreach (Item item in playerItems)
                                        {
                                            if (!seenItems.Contains(item))
                                            {
                                                TextMeshProUGUI textMeshProUGUI = items["Item" + i].GetComponentInChildren<TextMeshProUGUI>();
                                                textMeshProUGUI.SetText(item.name + " (" + StaticStorage.GetItemCount(item) + ")");
                                                seenItems.Add(item);
                                                i++;
                                            }
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
                                    else if (playerTakingTurn.isInterrupt)
                                    {
                                        currentlySelectedMoveOrItem = "move";
                                        currentlySelectedMoveOrItemName = playerTakingTurn.moves[0].name;
                                        SetChosenTargetName(currentInterruptTargetName);
                                        StartCoroutine("WaitEndTurn");
                                        if (!allCharacters[chosenTargetName].isDead)
                                        {
                                            StartCoroutine("WaitUseMoveOrItem");
                                        }
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

    private void UseMove(Move move, Character targetCharacter)
    {
        int damage;
        if (move.damage > 0)
        {
            if (move.damage + currentStrength >= 0)
            {
                damage = move.damage + currentStrength;
            }
            else
            {
                damage = 0;
            }
        }
        else
        {
            damage = move.damage;
        }
        
        targetCharacter.currentHealth -= damage;

        //for damageIndicator
        GameObject target;
        if (targetCharacter.team == 0) //friendly
        {
            int targetIndex = friendlyParty.IndexOf(targetCharacter);
            target = friendlyTargets["FriendlyTarget" + (targetIndex + 1)];
        }
        else //enemy
        {
            int targetIndex = enemyParty.IndexOf(targetCharacter);
            target = enemyTargets["EnemyTarget" + (targetIndex + 1)];
        }
        GameObject damageIndicator = Instantiate(damageIndicatorPrefab, target.transform.position, new Quaternion(0, 0, 0, 0));
        cloneList.Add(damageIndicator);
        damageIndicator.transform.SetParent(canvas.transform);
        TextMeshProUGUI textMeshProUGUI = damageIndicator.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = System.Math.Abs(damage).ToString();
        if (damage > 0)
        {
            textMeshProUGUI.color = new Color(1, 0, 0, textMeshProUGUI.color.a);
        }
        else if (damage < 0)
        {
            textMeshProUGUI.color = new Color(0, 1, 0, textMeshProUGUI.color.a);
        }
        else
        {
            textMeshProUGUI.color = new Color(1, 1, 0, textMeshProUGUI.color.a);
        }

        foreach (StatusEffect statusEffect in move.statusEffects)
        {
            targetCharacter.statusEffects.Add(new StatusEffect(statusEffect.name, statusEffect.dot, statusEffect.element, statusEffect.stun, statusEffect.strength, statusEffect.maxTurnsRemaining, statusEffect.currentTurnsRemaining));
        }

        RefreshHealth(targetCharacter);

    }

    private void UseItem(Item item, Character targetCharacter)
    {
        targetCharacter.currentHealth -= item.damage;

        //for damageIndicator
        GameObject target;
        if (targetCharacter.team == 0) //friendly
        {
            int targetIndex = friendlyParty.IndexOf(targetCharacter);
            target = friendlyTargets["FriendlyTarget" + (targetIndex + 1)];
        }
        else //enemy
        {
            int targetIndex = enemyParty.IndexOf(targetCharacter);
            target = enemyTargets["EnemyTarget" + (targetIndex + 1)];
        }
        GameObject damageIndicator = Instantiate(damageIndicatorPrefab, target.transform.position, new Quaternion(0, 0, 0, 0));
        cloneList.Add(damageIndicator);
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
            targetCharacter.statusEffects.Add(new StatusEffect(statusEffect.name, statusEffect.dot, statusEffect.element, statusEffect.stun, statusEffect.strength, statusEffect.maxTurnsRemaining, statusEffect.currentTurnsRemaining));
        }

        RefreshHealth(targetCharacter);

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
                friendlySprites["FriendlySprite" + i].transform.rotation = new Quaternion(0, 0, 0.25f, 0);
            }
            else
            {
                friendlySprites["FriendlySprite" + i].transform.rotation = new Quaternion(0, 0, 0, 0);
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
                enemySprites["EnemySprite" + i].transform.rotation = new Quaternion(0, 0, 0.25f, 0);
            }
            else
            {
                enemySprites["EnemySprite" + i].transform.rotation = new Quaternion(0, 0, 0, 0);
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

        //Allowing interrupting dialogue
        if (StoryStaticStorage.testInterrupt)
        {
            if (enemyParty[0].currentHealth <= 25)
            {
                StoryStaticStorage.testInterrupt = false;
                currentInterruptTargetName = "EnemyTarget1";
                dialogueController.SetCurrentDialogue(DialogueStaticStorage.testInterruptDialogue);
                AddToFrontOfTurnOrder(allCharacters["King Jebediah Interrupt"]);
            }
        }

        Debug.Log(character.name+"'s current health: "+character.currentHealth);
    }

    public void AddToFrontOfTurnOrder(Character character)
    {
        Character[] charactersInQueue = turnOrder.ToArray();
        turnOrder.Clear();
        turnOrder.Enqueue(character);
        foreach (Character characterInQueue in charactersInQueue)
        {
            turnOrder.Enqueue(characterInQueue);
        }
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

    public void SetInterruptTargetName(string chosenTargetName)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (chosenTargetName == "FriendlyTarget" + i)
            {
                this.currentInterruptTargetName = friendlyParty[i - 1].name;
                break;
            }
            else if (chosenTargetName == "EnemyTarget" + i)
            {
                this.currentInterruptTargetName = enemyParty[i - 1].name;
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
