using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public float damage = 10;
    public float range = 100;
    public GameObject hitMarker;
    public ObjectPool hitMarkers;
    public GunSettings g;
    private AudioPlay audio;

    private TextMeshProUGUI ammoText;

    private int ammoAmount;

    private void Start()
    {
        ammoText = GameObject.Find("Ammo").GetComponent<TextMeshProUGUI>();
        ammoAmount = g.ammoCapacity;
        UpdateAmmoUI(ammoAmount);
        audio = gameObject.GetComponent<AudioPlay>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        //if (Input.GetButton("Fire1"))
        {
            //Debug.Log(ammoAmount);
            if (ammoAmount > 0)
            {
                audio.playClip();
                Shoot();
                UpdateAmmoUI(-1);
            }
            
        }
        
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range) && hit.transform.gameObject.layer != LayerMask.GetMask("Player"))
        {
            //Debug.Log(hit.transform.name);
            //Debug.Log(hit.transform.gameObject.layer);
            //Debug.Log(hit.transform.position);

            hitMarker = ObjectPool.SharedInstance.GetPooledObject();
            hitMarker.SetActive(true);
            hitMarker.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("deal damage");
                Debug.Log((int)g.damage);
                hit.transform.gameObject.GetComponent<CharacterStats>().TakeDamage((int) g.damage);
                // deal damage to enemy
                // call takeDamage
            }
            //var marker = Instantiate(hitMarker, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
        }
    }

    public void UpdateAmmoUI(int ammo)
    {
        //Debug.Log(ammo);
        ammoAmount += ammo;
        Debug.Log(ammoAmount);
        ammoText.text = ammoAmount.ToString("000");
    }
}
