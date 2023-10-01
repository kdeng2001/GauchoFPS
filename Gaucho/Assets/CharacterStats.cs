using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterStats : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public int damage;
    private HealthUI healthUI;
    private float dieTime = 1f;
    private Animator anim;
    private ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = maxHealth;
        healthUI = GetComponent<HealthUI>();
        anim = GetComponent<Animator>();
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    public void TakeDamage(int rawDamage)
    {
        damage = rawDamage;
        // other factors that influence damage, ie armor, matchup typings, etc...
        currentHealth -= damage;
        healthUI.OnHealthChanged(maxHealth, currentHealth);


        if (currentHealth <= 0)
        {
            anim.SetTrigger("Die");
            Invoke(nameof(Die), dieTime);
        }
        else
        {
            anim.SetTrigger("Take Damage");
        }

    }

    public void Die()
    {
        scoreKeeper.UpdateScore(10);
        Debug.Log(transform.name + " died");
        //if (transform.name == "Toon Wizard-Red")
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //}
        Destroy(gameObject);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}