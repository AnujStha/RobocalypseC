
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Player;
    public static float Gravity;
    public static GameObject player;
    public float GravityC;
    private void Awake()
    {
        gameManager.Gravity = GravityC;
        gameManager.player = Player;
    }
}
