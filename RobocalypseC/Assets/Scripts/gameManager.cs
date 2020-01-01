
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Player;
    public static float Gravity;
    public static GameObject player;
    public Color shieldColour_, healthColour_, healthDepletion_, shieldDepletion_, poisionColour_;
    public static Color shieldColour, healthColour, healthDepletion, shieldDepletion, poisionColour;
    public float GravityC;
    public GameObject[] PrimaryPowerups;
    public GameObject[] SecondaryPowerups;
    public GameObject[] GrenadePowerups;
    public GameObject HealthPowerup;
    public GameObject ShieldPowerup;
    public static bool Paused=false;
    private void Awake()
    {
        gameManager.Gravity = GravityC;
        gameManager.player = Player;
        gameManager.shieldColour = shieldColour_;
        gameManager.healthColour = healthColour_;
        gameManager.healthDepletion = healthDepletion_;
        gameManager.shieldDepletion = shieldDepletion_;
        gameManager.poisionColour = poisionColour_;
    }
}
