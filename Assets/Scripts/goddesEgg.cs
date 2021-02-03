using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class goddesEgg : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public int healthBuff;

    public HealthBar healthBar;
    public bool ded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GetHealthFromShop();
        currentHealth = maxHealth + healthBuff;
    }

    // Update is called once per frame
    void Update()
    {
        if(ded == true) {
            SceneManager.LoadScene("Main");
        }
    }
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            mati(true);
        }
    }

    public bool mati(bool ded) {
        return this.ded = ded;
    }

    public int GetHealthFromShop(){
        return healthBuff = PlayerPrefs.GetInt("EggHealth");
    }
}
