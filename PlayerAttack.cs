//Created by: Deven Greenlee
//Date Last Modified: 4/25/19
//Languange: C#
//Purpose: This script controls the player different attack methods as well as the animations for each attack 





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public int Health;
    public int damDid;
    public float attackCooldownTime;

    public float timeBetweenStances;
    public float stanceCooldownTime;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;




    //Variables that control the stats foreach player stance
    public int currentStance;
    private int agressiveDam = 2;
    private int agressiveSpeed = 3;
    private int agressiveDif = 2;
    private float agressiveSped = 0.2f;


    private int deffensiveDam = 3;
    private int deffensiveSpeed = 1;
    private int deffensiveDif = 3;
    private float defensiveSped = 1f;


    private int rangedDam = 1;
    private int rangedSpeed = 2;
    private int rangedDif = 1;
    private float rangedSped =0.5f;



    //variables that are related to ranged attacking 
    public Projectile projectScript;
    public GameObject arrowSpawner;
    public GameObject playerBulletUp;
    public GameObject playerBulletRight;
    public GameObject playerBulletDown;
    public GameObject playerBulletLeft;


    public bool isAgresive = false;
    public bool isDefesnisve = false;
    public bool isRanged = false;

    public AudioSource stanceSound;
    public AudioSource swordHitSound;
    public AudioSource swordMissSound;

    private bool flashActive;
    private float flashLength;
    public float flashCounter;

    private SpriteRenderer playerSprite;

    Animator anim;
    private Rigidbody2D rb2d;
    PlayerMovement moveScript;

    public float thrust;
    public float thrustTime;

    public GameObject Camera; 

   




    public PlayerMovement MovementScript;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        flashLength = .5f;
        Health = 30;

        currentStance = 0;
        
        isAgresive = true;

        anim.SetBool("isAgressive", true);
        MovementScript = GetComponent<PlayerMovement>();

        playerSprite = GetComponent<SpriteRenderer>();
        

        

       

    }

    // Update is called once per frame
    void Update()
    {





        //changing of the hitbox direction 
        if (moveScript.moveVelocity.x == 0)
        {
            attackPos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if (moveScript.moveVelocity.x > 0)
        {
            attackPos.position = new Vector3(transform.position.x + .4f, transform.position.y, transform.position.z);
        }

        if (moveScript.moveVelocity.x < 0)
        {
            attackPos.position = new Vector3(transform.position.x - .4f, transform.position.y, transform.position.z);
        }

        if (moveScript.moveVelocity.y > 0)
        {
            attackPos.position = new Vector3(transform.position.x , transform.position.y + .4f, transform.position.z);
        }

        if (moveScript.moveVelocity.y < 0)
        {
            attackPos.position = new Vector3(transform.position.x, transform.position.y - .4f, transform.position.z);
        }



        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.B))
        {
            Application.Quit();
        }

        //code for ranged attack 
        // attack looking to the top of the screen
        if(attackCooldownTime<= 0)
        {
            if ((Input.GetKeyDown("space") && Input.GetKey(KeyCode.W)) && isRanged == true)
            {

                GameObject bullet1 = (GameObject)Instantiate(playerBulletUp);
                bullet1.transform.position = arrowSpawner.transform.position;
                anim.SetTrigger("Attack");
                attackCooldownTime = rangedSped;

            }

            // attack looking toward the right 
            if ((Input.GetKeyDown("space") && Input.GetKey(KeyCode.D)) && isRanged == true)
            {


                GameObject bullet1 = (GameObject)Instantiate(playerBulletRight);
                bullet1.transform.position = arrowSpawner.transform.position;
                anim.SetTrigger("Attack");
                attackCooldownTime = rangedSped;

            }

            //attack looking at the bottom of the screen
            if ((Input.GetKeyDown("space") && Input.GetKey("s")) && isRanged == true)
            {


                GameObject bullet1 = (GameObject)Instantiate(playerBulletDown);
                bullet1.transform.position = arrowSpawner.transform.position;
                anim.SetTrigger("Attack");
                attackCooldownTime = rangedSped;

            }

            //attack looking toward the left of the screen 
            if ((Input.GetKeyDown("space") && Input.GetKey(KeyCode.A)) && isRanged == true)
            {


                GameObject bullet1 = (GameObject)Instantiate(playerBulletLeft);
                bullet1.transform.position = arrowSpawner.transform.position;
                anim.SetTrigger("Attack");
                attackCooldownTime = rangedSped;

            }

        }









        // code for stance changing 
        if (timeBetweenStances <= 0)
        {
            if (Input.GetKeyDown("q"))
            {
                anim.SetTrigger("StanceChange");
                changeStance();
                stanceSound.Play();
                
            }
        }



        //Controls the damage taken by the enemy based on what stance and position of the player
        if (Input.GetKeyDown("space") && isRanged == false)
        {

           
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

            if(isAgresive)
            {
                if(attackCooldownTime <= 0)
                {

                    if (enemiesToDamage.Length == 0)
                    {
                        anim.SetTrigger("Attack");
                        swordMissSound.Play();
                        attackCooldownTime = agressiveSped;
                        

                    }
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if(enemiesToDamage[i].gameObject.CompareTag("Dragon"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<DragonEnemy>().TakeDamage(agressiveDam);
                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;





                        }
                        else if (enemiesToDamage[i].gameObject.CompareTag("Cloud"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<TurrentEnemy>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;
                           
                        }
                        else if (enemiesToDamage[i].gameObject.CompareTag("Head"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<FloatingHeadEnemy>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;
                          
                        }

                        else if (enemiesToDamage[i].gameObject.CompareTag("Hashi"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<HashiController>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;

                        }
                        else
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;
                            Camera.GetComponent<CameraFollow>().shakeCamera(.9f, .1f);
                          

                        }

                      
                    }

                }
               
            }

            if (isDefesnisve)
            {
                if (attackCooldownTime <= 0)
                {
                    if (enemiesToDamage.Length == 0)
                    {
                        anim.SetTrigger("Attack");
                        swordMissSound.Play();
                        attackCooldownTime = defensiveSped;

                    }
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].gameObject.CompareTag("Dragon"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<DragonEnemy>().TakeDamage(agressiveDam);
                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;



                        }
                        else if (enemiesToDamage[i].gameObject.CompareTag("Cloud"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<TurrentEnemy>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;
                        }
                        else if (enemiesToDamage[i].gameObject.CompareTag("Head"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<FloatingHeadEnemy>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;
                        }

                        else if (enemiesToDamage[i].gameObject.CompareTag("Hashi"))
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<HashiController>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;

                        }
                        else
                        {
                            anim.SetTrigger("Attack");
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(agressiveDam);

                            swordHitSound.Play();
                            attackCooldownTime = agressiveSped;
                        }
                    }
                }
            }
            
           

        }
        else
        {
            attackCooldownTime -= Time.deltaTime;
            if (attackCooldownTime <= 0f)
            {
                attackCooldownTime = 0;
            }
        }







        //flash when taking damage


        if (flashCounter > flashLength * .66f)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);

        }
        else if (flashCounter > flashLength * .33f)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
        }
        else if (flashCounter > 0)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
        }
        else
        { 
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            flashActive = false;
        }

        flashCounter -= Time.deltaTime;



    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //This function controls the transitions of astats from one stance to another
    void changeStance()
    {
        bool sChanged = false;


        if (isAgresive == true && sChanged == false)
        {
            anim.SetBool("isAgressive", false);
            anim.SetBool("isDefensive", true);
            damage = deffensiveDam;
            MovementScript.setSpeed(deffensiveSpeed);
            attackRange = attackRange * 2;
            isAgresive = false;
            isDefesnisve = true;
            sChanged = true;
        }

        if (isDefesnisve == true && sChanged == false)
        {
            anim.SetBool("isDefensive", false);
            anim.SetBool("isRanged", true);
            damage = rangedDam;
            MovementScript.setSpeed(rangedSpeed);
            isDefesnisve = false;
            attackRange = attackRange / 2;
            isRanged = true;
            sChanged = true;


        }

        if (isRanged == true && sChanged == false)
        {
            anim.SetBool("isRanged", false);
            anim.SetBool("isAgressive", true);
            damage = agressiveDam;
            MovementScript.setSpeed(agressiveSpeed);
            isRanged = false;
            isAgresive = true;
            sChanged = true;


            
        }



       
    }

    //This function controls the damage taken by the player depending on the stance
    public void takeDamage(int d)
    {
        

        if (isDefesnisve == true)
        {
            d = d - deffensiveDif;
            Health = Health - d;
        }
        if (isAgresive == true)
        {
            d = d - agressiveDif;
            Health = Health - d;
            damDid = d;

        }
        if (isRanged == true)
        {
            d = d - rangedDif;
            Health = Health - d;
        }

        flashActive = true;
        flashCounter = flashLength;


    }

    

}
