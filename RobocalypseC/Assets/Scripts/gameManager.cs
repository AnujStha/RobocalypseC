
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Player;
    public static float Gravity;
    public float GravityC;
    private void Awake()
    {
        gameManager.Gravity = GravityC;
        Debug.Log(gameManager.Gravity);
    }
}
