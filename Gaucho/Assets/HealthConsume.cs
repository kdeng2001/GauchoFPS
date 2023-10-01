using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsume : MonoBehaviour
{
    // Start is called before the first frame update
    public int healthAmount = 50;
    private ItemSpawner spawner;
    private playerStats playerHealth;

    void Start()
    {
        spawner = GetComponentInParent<ItemSpawner>();
        playerHealth = GameObject.Find("First Person Player").GetComponent<playerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision occured");
        if (other.gameObject.layer == 3)
        {
            Debug.Log("consume health");
            spawner.Consumed = true;
            playerHealth.Heal(healthAmount);
            // give ammo
        }
    }
}
