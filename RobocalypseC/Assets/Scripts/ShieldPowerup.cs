
public class ShieldPowerup :Interactable
{
    public float shield;
    public override void Start()
    {
        base.Start();
    }
    public override void InRange()
    { HealthAndShield player = gameManager.player.GetComponent<HealthAndShield>();
        if (player.Shield < player.MaxShield) 
         player.recharge(0,shield );
        Destroy(gameObject);
    }
}
