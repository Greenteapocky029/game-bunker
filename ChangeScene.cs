//Created By: Deven Greenlee 
//Last Modified: 4/15/20
//Language:C#
//Description: this script allows for the changing/ trnsition of scenes in game 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public Animator transition;
    private GameObject player;

    private void Start()
    {
        //finds the player game object 
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void changeLevel(string index)
    {
        //starts corotine that will change the level 
        StartCoroutine(loadLevel(index));
    }

    IEnumerator loadLevel(string lindex)
    {
        //starts animation 
        transition.SetTrigger("Start");
        // waits a second for things to catch up 
        yield return new WaitForSeconds(1);
        //changes scene 
        SceneManager.LoadScene(lindex);
    }

}
