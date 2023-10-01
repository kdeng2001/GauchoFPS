using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float minWaitTime = 5f;
    public float maxWaitTime = 60f;
    public float waitTime = 20f;

    private bool init = true;
    private bool waitTimerOver = false;
    private bool hasSpawned;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        hasSpawned = true;
        StartCoroutine(InitWaitTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTimerOver)
        {
            if (hasSpawned == false)
            {
                spawnTime = Time.time;
                hasSpawned = true;
                StartCoroutine(WaitTimer());
            }
        }

    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    IEnumerator WaitTimer()
    {
        SpawnEnemy();
        waitTimerOver = false;
        Debug.Log("called waittimer");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("waittimer over");
        waitTimerOver = true;
        hasSpawned = false;

    }
    IEnumerator InitWaitTimer()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        hasSpawned = false;
        init = false;
        waitTimerOver = true;

    }
}
