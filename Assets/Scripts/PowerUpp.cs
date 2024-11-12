using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpp : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 5f;
    public GameObject pickupEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other) );
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        
        //If we make pickup effect
        //Instantiate(pickupEffect, player.transform.position,transform.rotation);
        
        
        // speedbuff/debuff
        PlayerMovement speedStat = player.GetComponent<PlayerMovement>();
        speedStat.moveSpeed *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        yield return new WaitForSeconds(duration);
        
        speedStat.moveSpeed /= multiplier;
        
        Destroy(gameObject);
    }
}
