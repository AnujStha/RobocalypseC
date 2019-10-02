
using UnityEngine;

public class Lock : Button
{
    public int key;
    public override void interact()
    {
        if (player.GetComponent<PlayerController>().CheckKey(key))
        {
            Debug.Log("unlocked with " + key + " key");
            foreach (Switchable s in switchables)
            {
                s.Switch();
            }
        }
    }
  
}
