//Created by: Deven Greenlee
//Date Last Modified: 4/25/19
//Languange: C#
//Purpose: This script controls the knockback of the enemies when theytake damage


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knokback : MonoBehaviour
{
    public bool isDamage = false;
    public float thrust;
    public float kTime;

    //when they player enters the area and attacks...
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
        if (enemy.CompareTag("Enemy"))
        {
            isDamage = enemy.GetComponent<Enemy>().takingDamage;
           
        }
        else if (enemy.CompareTag("Cloud"))
        {
            isDamage = enemy.GetComponent<HashiController>().takingDamage;
        }

        if (isDamage)
        {
            thrust = 5;
        }
        else
        {
            thrust = 1 ;
        }

        if (enemy!= null)
        {
            //push the enemy back
            enemy.isKinematic = false;
            Vector2 difference = enemy.transform.position -  transform.position;
            difference = difference.normalized * thrust;
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(knockCo(enemy));
            enemy.GetComponent<Enemy>().takingDamage = false;
        }
    }

    //stop the enemy
    private IEnumerator knockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(kTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;

        }
    }

}
