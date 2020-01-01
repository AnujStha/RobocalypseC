
public class ShieldPowerup :Interactable
{
    public float shield;
    public override void Start()
    {
        base.Start();
    }
    public override void InRange()
    {
        gameManager.player.GetComponent<HealthAndShield>().recharge(0,shield );
        Destroy(gameObject);
    }
}
