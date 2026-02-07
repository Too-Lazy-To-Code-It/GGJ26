using UnityEngine;
using Code.Scriptable_Objects;
using Code.Managers;
using System.Collections;
using System;
[CreateAssetMenu(menuName = "PowerUps/Dephase Player")]
public class DepashingPowerUp : PowerUpSo
{
    public override void Apply()
    {
        GameManager.Instance.player.playerCollider.isTrigger = true;
        GameManager.Instance.player.playerRigidbody2D.constraints= RigidbodyConstraints2D.FreezePositionY;
    }

  

   
    public override void Revert()
    {
        GameManager.Instance.player.playerCollider.isTrigger = false;
        GameManager.Instance.player.playerRigidbody2D.constraints = RigidbodyConstraints2D.None;
        GameManager.Instance.player.playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
