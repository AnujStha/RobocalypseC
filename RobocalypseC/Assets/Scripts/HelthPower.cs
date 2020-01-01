
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
        gameManager.player.GetComponent<HealthAndShield>().recharge(health, 0);
        Destroy(gameObject);
    }
}
