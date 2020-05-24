using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static StaticStorage;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private float horiz;
    private float vert;
    public float speed;
    public GameObject mainCamera;
    public GameObject depetrificationWindow;
    public GameObject depetrificationCount;
    public Dictionary<string, GameObject> characters = new Dictionary<string, GameObject>();
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        //Depetrification window

        depetrificationCount = GameObject.Find("DepetrificationCount");

        character1 = GameObject.Find("Character1");
        character2 = GameObject.Find("Character2");
        character3 = GameObject.Find("Character3");
        character4 = GameObject.Find("Character4");
        characters.Add("Character1", character1);
        characters.Add("Character2", character2);
        characters.Add("Character3", character3);
        characters.Add("Character4", character4);

        depetrificationWindow = GameObject.Find("DepetrificationWindow");
        depetrificationWindow.SetActive(false);

        //Depetrification window end
    }

    // Update is called once per frame
    void Update()
    {
        horiz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        anim.SetFloat("HorizontalSpeed", speed*horiz);
        anim.SetFloat("VerticalSpeed", speed*vert);

        //Depetrification window

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!inCombat)
            {
                UpdateDepetrificationWindow();
                if (!depetrificationWindow.active)
                {
                    depetrificationWindow.SetActive(true);
                }
                else
                {
                    depetrificationWindow.SetActive(false);
                }
            }
        }

        //Depetrification window end
    }

    void FixedUpdate()
    {
        rb2d.velocity = speed * (new Vector2(horiz, vert)).normalized * Mathf.Max(Mathf.Abs(vert), Mathf.Abs(horiz));
    }

    public void UpdateDepetrificationWindow()
    {
        depetrificationCount.GetComponent<TextMeshProUGUI>().text = "Crystals: " + StaticStorage.GetItemCount(StaticStorage.allItems["Depetrification Crystal"]);
        int i = 1;
        foreach (Character character in playerParty)
        {
            if (character.isDead)
            {
                TextMeshProUGUI textMeshProUGUI = characters["Character" + i].GetComponentInChildren<TextMeshProUGUI>();
                textMeshProUGUI.SetText(character.name);
                i++;
            }
        }
        for (int j = i; j <= 4; j++) //getting rid of buttons for characters we dont have
        {
            characters["Character" + j].SetActive(false);
        }
    }
}
