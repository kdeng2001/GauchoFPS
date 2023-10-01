using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public GameObject explosion;
    public int damage = 10;

    private playerStats player;
    private float initTime;
    private void Start()
    {
        player = GameObject.Find("First Person Player").GetComponent<playerStats>();
        initTime = Time.time;
  
    }
    // Update is called once per frame
    void Update()
    {
        if((Time.time - initTime) > lifetime)
        {
            Debug.Log("destroy projectile");
            Destroy(gameObject);
        }
        // each frame we move the projectile forward in the direction its facing
        // transform.forward is a normalized vector
        // time.deltatime is the time the last frame took to render, we multiply by this
        // to make the speed frame independent 

        // speed is a scalar
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    // event that triggers when the collider hits another object with a collider
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Enemy"))
        //{
        //    other.gameObject.GetComponent<CharacterStats>().takeDamage(damage);

        //}
        // if we hadn't checked 'Trigger', we woulkd use OnCollisionEnteR(Collision collision)
        if (other.CompareTag("Player")) // make sure we did not hit the player (will pass through them)
        {
            player.TakeDamage(damage);

        }

    }
}
