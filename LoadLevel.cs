//Created by: Deven Greenlee
//Date Last Modified: 4/25/19
//Languange: C#
//Purpose: This script controls the changing of levels 



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public string Level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //when the player enters the area the level changes to that specific level
    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(Level);
    }
}
