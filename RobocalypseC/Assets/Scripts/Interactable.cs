﻿
using UnityEngine;
public class Interactable : MonoBehaviour
{
    private float playerDistance;
    protected GameObject player;
    public float engageRange;
    public bool inRange;
    public virtual void Start()
    {
        player = gameManager.player;    
    }
    private void Update()
    {
        if (inRange) {
            if (Input.GetButtonDown("Interact")) {
                interact();
            }
        }
    }
    private void FixedUpdate()
    {

        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < engageRange && !inRange)
        {
            inRange = true;
            InRange();
        }
        else if (playerDistance > engageRange && inRange) {
            inRange = false;
            OutOfRange();
        }
    }
    public virtual void interact()
    {

    }
    public virtual void InRange()
    {

    }
    public virtual void OutOfRange()
    {

    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, engageRange);
    }
}