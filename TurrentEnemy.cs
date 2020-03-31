//Created by: Deven Greenlee
//Date Last Modified: 4/25/19
//Languange: C#
//Purpose: This script controls this enemies ability to shoot projectiles at the enemy



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentEnemy : MonoBehaviour
{

    public int health;
    public float speed;
    public int randomSpot;

    
    private bool isFollowing;
    private Transform target;

    public Transform enemyPos;
    public float attackRange;
    public LayerMask whatIsPlayer;

    // variables for the projectile speed and position
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletTimer;
    public float shootInterval;
    public float stopTime = 2f;
    public bool isConfused;

    public ParticleSystem guts;
    public AudioSource hitSound;
    public AudioSource dieSound;
    public bool takingDamage;

    void Start()
    {
        shootInterval = 1f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isFollowing = false;
        
        speed = .5f;
        health = 6;
    }


    // Update is called once per frame
    void Update()
    {

        if (isConfused == true)
        {
            stopTime -= Time.deltaTime;
        }

        if (stopTime <= 0)
        {
            isConfused = false;
        }

        //code for enemy death 
        if (health <= 0)
        {
            dieSound.Play();
            Destroy(gameObject);
        }
        // if the player enters the area the enemy begins to attack
        Collider2D[] playersTofollow = Physics2D.OverlapCircleAll(enemyPos.position, attackRange, whatIsPlayer);

        if (playersTofollow.Length >= 1)
        {
            isFollowing = true;
            attack();
        }


       if(isConfused == false)
        {
            if (isFollowing == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
  
        }






    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyPos.position, attackRange);
    }

    //controls the enemy shooting 
    public void attack()
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            GameObject bulletClone;
            bulletClone = Instantiate(bullet, enemyPos.transform.position, enemyPos.transform.rotation) as GameObject;
            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            bulletTimer = 0;
        }

        
    }

    //allows the enemy to take damage from the player 
    public void TakeDamage(int damage)
    {
        takingDamage = true;
        guts.Play();
        hitSound.Play();
        health = health - damage;




    }
}
