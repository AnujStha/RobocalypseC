
using UnityEngine;

public class PickupsGrenades : Interactable
{
    public int id;
    public int count;
    public GameObject PUPanimation;
    public override void Start()
    {
        PUPanimation = GameObject.Find("GameManager").GetComponent<gameManager>().GrenadePowerups[id % 10];
        base.Start();
        Instantiate(PUPanimation, transform);
    }
    public override void InRange()
    {
        int type = id % 100;
        base.interact();
        GunPlaceHolderPlayer holder = gameManager.player.GetComponentInChildren<GunPlaceHolderPlayer>();
        holder.grenadeCount[type] += count;
        Destroy(gameObject);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
