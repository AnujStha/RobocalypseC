﻿using System.Collections;
using UnityEngine;

public class pod : MonoBehaviour,IKillable
{
    private Animator anim;
    public float spawnInterval;
    public float detectDistance;
    public GameObject spawnObject;
    private float spawnCounter;
    public float openTime;
    public Transform spawnPoint;
    private bool isAlive=true;
    private GameObject player;
    // Start is called before the first frame update

    void Start()
    {
        player = gameManager.player;
        spawnCounter = spawnInterval;
        anim = GetComponent<Animator>();    
    }
    void Open() {
        anim.SetTrigger("open");

    }
    void Close() {
        anim.SetTrigger("close");
    }
    IEnumerator StartSpawn() {
        Open();
        yield return new WaitForSeconds(openTime);
        Instantiate(spawnObject, spawnPoint.position, transform.rotation);
        Close();
    }
    void Update()
    {
        if (isAlive&&Vector3.Distance(player.transform.position,transform.position)<detectDistance)
        {
            if (spawnCounter < 0)
            {

                StartCoroutine(StartSpawn());
                spawnCounter = spawnInterval+openTime;
            }
            else
            {
                spawnCounter -= Time.deltaTime;
            }
        }
    }
    void IKillable.dead()
    {
        isAlive = false;
        anim.SetTrigger("dead");
    }
    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
}
