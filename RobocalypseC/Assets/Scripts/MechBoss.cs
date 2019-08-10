
using UnityEngine;

public class MechBoss : MonoBehaviour
{
    public gameManager GameManager;
    private GameObject player;
    public Animator anim;
    [Range(0,1)]
    public float rightRotation;
    [Range(0, 1)]
    public float leftRotation;
    [Range(0, 1)]
    public float rightGunRotation;
    [Range(0, 1)]
    public float leftGunRotation;
    public float rotationsmoother;
    [Header("hand and guns")]
    public Transform handL;
    public Transform handR;
    public Transform gunL;
    public Transform gunR;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Player;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("handR", rightRotation);
        anim.SetFloat("handL", leftRotation);
        anim.SetFloat("gunR", rightGunRotation);
        pointGunLToPlayer();
        pointGunRToPlayer();
    }

    void pointGunRToPlayer() {
        float angle = Vector3.Angle(player.transform.position - gunR.transform.position, new Vector3(0, 0, 1));
        angle /= 180;
        if (player.transform.position.y < gunR.position.y) {
            angle *= -1;
        }
        angle += .5f;
        
    }
    void pointGunLToPlayer() {
        float angle = Vector3.Angle(player.transform.position - gunL.transform.position, new Vector3(0, 0, 1));
        if (player.transform.position.y < gunL.position.y)
        {
            angle *= -1;
        }
        angle /= 180;
        angle += .5f;
        anim.SetFloat("gunL", Mathf.Lerp(anim.GetFloat("gunL"),angle,rotationsmoother));
    }

    void pointRightMessileToPlayer() {

    }

    void pointLeftMessileToPlayer() {

    }
}
