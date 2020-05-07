//Created By: Deven Greenlee 
//Last Modified: 4/15/20
//Language:C#
//Desciption: This script signals the end of the level 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_End : MonoBehaviour
{
    GameObject player;
    General_Level_Manager level;

    private void Start()
    {
        //finds player and level manager game object 
        player = GameObject.FindGameObjectWithTag("Player");
        level = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<General_Level_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //moves to the next scene
        level.hasBeenCompleted = true;
        // saves the player and level
        Save_System.savePlayer(player.GetComponent<Inventory>());
        Save_System.saveLevel(level);
        // loads the data for the level complete screen 
        SceneManager.LoadScene("LevelComplete");

    }
}
