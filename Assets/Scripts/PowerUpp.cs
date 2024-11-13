using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpp : MonoBehaviour
{
    public float speedMultiplier = 2f;
    public float jumpMultiplier = 1.5f;
    public float sizeMultiplier = 1.2f;
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            AdjustSpeed(playerMovement, speedMultiplier);
            AdjustJump(playerMovement, jumpMultiplier);
            AdjustSize(player, sizeMultiplier);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(duration);

            AdjustSpeed(playerMovement, 1 / speedMultiplier);
            AdjustJump(playerMovement, 1 / jumpMultiplier);
            AdjustSize(player, 1 / sizeMultiplier);
        }

        Destroy(gameObject);
    }

    private void AdjustSpeed(PlayerMovement playerMovement, float multiplier)
    {
        playerMovement.moveSpeed *= multiplier;
    }

    private void AdjustJump(PlayerMovement playerMovement, float multiplier)
    {
        playerMovement.jumpForce *= multiplier;
    }

    private void AdjustSize(Collider2D player, float multiplier)
    {
        player.transform.localScale *= multiplier;
    }
}
