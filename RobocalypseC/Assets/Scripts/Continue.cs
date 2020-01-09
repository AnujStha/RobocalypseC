using UnityEngine;
public class Continue: MonoBehaviour
{
    public bool continueLevel=false;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
