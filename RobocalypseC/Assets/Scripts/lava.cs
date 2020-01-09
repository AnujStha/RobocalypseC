using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : TouchDamage1 {
    private HealthAndShield playerHealthAndShield;
    public float damagePerSecond,healthRatio,shieldRatio;
    private void Start()
    {
        playerHealthAndShield = gameManager.player.GetComponent<HealthAndShield>();
    }
    public override void enter()
    {
        playerHealthAndShield.damage(damagePerSecond * Time.fixedDeltaTime, healthRatio, shieldRatio);
    }
}
