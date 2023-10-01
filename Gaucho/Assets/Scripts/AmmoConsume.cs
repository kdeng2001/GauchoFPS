using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConsume : MonoBehaviour
{
    public int ammoGiven = 100;
    private ItemSpawner spawner;
    private GameObject[] guns;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponentInParent<ItemSpawner>();
        guns = GameObject.FindGameObjectsWithTag("Gun");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision occured");
        if (other.gameObject.layer == 3)
        {
            Debug.Log("consume ammo");
            spawner.Consumed = true;
            foreach (GameObject gun in guns)
            {
                if (gun.activeSelf)
                {
                    Debug.Log("updateAmmoUI");
                    gun.gameObject.GetComponent<Gun>().UpdateAmmoUI(ammoGiven);
                    break;
                }
            }
            // give ammo
        }
    }
}
