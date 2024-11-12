using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUpps/SpeedBuff")]
public class SpeedBuff : PowerUppEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().moveSpeed += amount;
    }
}
