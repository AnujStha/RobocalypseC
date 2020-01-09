
public class HelthPower : Interactable
{
    public float health;
    // Start is called before the first frame update
     public override void Start()
    {
        base.Start();    
    }
    public override void InRange()
    {
        HealthAndShield player= gameManager.player.GetComponent<HealthAndShield>();
        if(player.health<player.MaxHealth)
        player.recharge(health, 0);
        Destroy(gameObject);
    }
}
