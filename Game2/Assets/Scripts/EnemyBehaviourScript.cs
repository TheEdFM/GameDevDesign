using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        StaticStorage.newEncounter(new string[]{ this.name, player.name, "Mole Slasher", "Treant", "Daisy" });//needs to be change to append all party members. 
        //this needs to change for the new storage
        SceneManager.LoadScene("CombatScene"); // 

    }
}
