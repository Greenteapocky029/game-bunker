//Created By: Deven Greenlee 
//Last Modified: 4/15/20
//Language:C#
//Description: This script hadles the level complete scene and all the data that is presented 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    Level_Data data;
    public Text timeText;
    public Text mineralText;
    public Text payText;
    public bool hasBeenC;

    // Start is called before the first frame update
    void Start()
    {
        //finds the player game object 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // loads level data 
        data = Save_System.loadLevel();
        // gets the game timer, amount of mineral destroyed, amountof pay 
        timeText.text = data.levelTime.ToString();
        mineralText.text = data.destroyedMiningNodes.ToString();
        payText.text = data.payAmount.ToString();
        player.GetComponent<Inventory>().money += data.payAmount;
        // saves player inventory 
        Save_System.savePlayer(player.GetComponent<Inventory>());

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
