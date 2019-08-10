﻿
using UnityEngine;

public class jumpPoint : MonoBehaviour
{
    public bool PositiveDirection=true;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.tag == "Enemy")
        {
            AIObjects ai = other.GetComponent<AIObjects>();
            if (ai != null) {
              if(ai.Heading()==PositiveDirection)ai.Jump();
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AIObjects ai = other.GetComponent<AIObjects>();
            if (ai != null)
            {
                if(ai.Heading()==PositiveDirection)ai.Jump();
            }

        }
    }
}
