using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCombat : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rb;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage;

    public int healthBuff;
    public int attackBuff;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public int currentHealth;
    public HealthBar healthBar;

    void Start () {
        player.souls = PlayerPrefs.GetInt("Souls");
        GetHealthFromShop();
        GetAttackFromShop();
        currentHealth = player.health + healthBuff;
        attackDamage = player.attack + attackBuff;
        
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Time.time >= nextAttackTime) {
            if(Input.GetKeyDown(KeyCode.Z)) {
                if(!player.Running()){
                    Attack();
                }
                else{
                    RunAttack();
                }
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }  
    }

    public void RunAttack() {
        player.RunAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void Attack() {
        player.StandAttack();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }


    void OnDrawGizmosSelected() {
        if(attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            SceneManager.LoadScene("Main");
        }
    }

    public int GetHealthFromShop(){
        return healthBuff = PlayerPrefs.GetInt("PlayerHealth");
    }

    public int GetAttackFromShop(){
        return attackBuff = PlayerPrefs.GetInt("attack");
    }
}
