using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class playerStats : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public Volume damaged;
    private int currentHealth = 0;
    private Image healthSlider;
    private float time = 0.5f;
    public GameObject gameOver;
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider = GameObject.Find("PlayerHealthUI").transform.GetChild(0).GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("player damage taken");
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = healthPercentage;
        // change volume
        damaged.weight = (1 / (healthPercentage + 0.01f));
        
        if (currentHealth <= 0)
        {
            // Game over
            //damaged.weight = 1;
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameOver.SetActive(true);
            return;
        }
        Invoke(nameof(VolumeChange), time);
    }

    private void VolumeChange()
    {
        damaged.weight = 0;
        // change back volume
    }

    public void Heal(int health)
    {
        currentHealth += health;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = healthPercentage;
    }
}
