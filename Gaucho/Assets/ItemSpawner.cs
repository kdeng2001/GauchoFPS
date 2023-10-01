using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasSpawned = false;
    public float despawnTime = 30f;
    public GameObject health;
    public GameObject ammo;
    public float minWaitTime = 10f;
    public float maxWaitTime = 60f;
    //public GameObject ammo;

    private float waitTime = 20f;
    private bool waitTimerOver = false;
    private float spawnTime;
    private bool init = true;
    public bool Consumed = false;
    void Start()
    {
        hasSpawned = true;
        StartCoroutine(InitWaitTimer());
    }
    // init don't spawn, wait some time between min and max waittime
    // hasSpawned = false
    // 1) spawn powerup and hasSpawned = true
    // 2) wait despawnTime
    // 3) powerup despawns -> waitTimer -> hasSpawned = false -> go back to 1)

    // Update is called once per frame
    void Update()
    {
        if(waitTimerOver)
        {
            if(hasSpawned == false)
            {
                spawnTime = Time.time;
                hasSpawned = true;
                SpawnItem();
            }
            else if ((!init && ((Time.time - spawnTime) > despawnTime)) || Consumed)
            {
                hasSpawned=false;
                StartCoroutine(WaitTimer());
            }
        }

        
    }

    public void SpawnItem()
    {
        hasSpawned = true;
        float num = Random.Range(0f, 1f);
        if(num >= 0.5)
        {
            health.SetActive(true);
            //Debug.Log("health on");
        }
        else
        {
            ammo.SetActive(true);
            //Debug.Log("ammo on");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.layer == 3 && hasSpawned == true)
    //    {
    //        StartCoroutine(WaitTimer());
    //    }
    //}

    // called when powerup despawns or is taken
    IEnumerator WaitTimer()
    {
        waitTimerOver = false;
        Debug.Log("called waittimer");
        health.SetActive(false);
        ammo.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("waittimer over");
        waitTimerOver = true;

    }

    IEnumerator InitWaitTimer()
    {
        health = transform.GetChild(0).gameObject;
        ammo = transform.GetChild(1).gameObject;
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        hasSpawned = false;
        init = false;
        waitTimerOver = true;

    }
}
