//Created by: Deven Greenlee
//Date Last Modified: 4/25/19
//Languange: C#
//Purpose: This script controls the player character movement on the screen



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Vector2 moveVelocity;
    public Rigidbody2D rb;
    Animator anim;
    public AudioSource walkSound;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        anim.SetInteger("MoveX", (int)moveVelocity.x);
        anim.SetInteger("MoveY", (int)moveVelocity.y);
        if (anim.GetInteger("MoveX") != 0 || anim.GetInteger("MoveY")  != 0)
        {
            if (timer <= 0)
            {
                walkSound.Play();
                timer = .3f;
            }

        }
        timer -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        //rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        rb.velocity = new Vector2(moveVelocity.x, moveVelocity.y);
     ;
    }

    public void setSpeed(int num)
    {
        speed = num;
    }
}
