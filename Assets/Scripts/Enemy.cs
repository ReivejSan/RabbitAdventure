using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attack;
    public int souls;
    public float moveSpeed;
    public float delay = 3f;
    public float attackRange = 0.5f;
    public float timer;
    public float setTimer;

    public Player player;
    public HealthBar healthBar;
    public int currentHealth;

    public Transform attackPoint;
    public LayerMask playerLayers, eggLayers;
    public Animator animator;

    Rigidbody2D rb;
    SpriteRenderer sp;

    Transform target;
    Transform targetPlayer;
    Transform targetEgg;
    Vector3 localScale;
    Vector3 distance;
    bool OnPoint = false;

    float playerPosition;
    float EggPosition;
    float dirX, dirY;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        distance = new Vector3(3,0,0);

        ///TARGET = GoddesEgg
        target = targetEgg;
        
        ///ENEMY ATTACK TIMER
        setTimer = timer;

        ///HEALTHBAR SET
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);

        targetPlayer = GameObject.Find("Player").GetComponent<Transform>();
        targetEgg = GameObject.Find("GoddesEgg").GetComponent<Transform>();
        
        sp = GetComponent<SpriteRenderer>();
    }

    void Update(){
        ///Cek apakah posisi enemy = target
        OnTargetPoint();

        ///Enemy Attack timer
        setTimer -= Time.deltaTime;

        player.souls = PlayerPrefs.GetInt("Souls");

        ///Enemy bergerak
        Move();
        playerPosition = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);
        EggPosition = Vector3.Distance(gameObject.transform.position, GameObject.Find("GoddesEgg").transform.position);

        Debug.Log("Player = " + playerPosition);
        Debug.Log("Egg = " + EggPosition);
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(dirX, dirY);
    }

    void LateUpdate() {
        if(rb.velocity.x > 0) {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if(rb.velocity.x < 0) {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    public bool Running() {
        return animator.GetBool("Run");
    }

    public void OnTargetPoint(){
        if(target != null){
            if(target == targetEgg){
                if((transform.position.x <= target.position.x + distance.x &&  transform.position.y == target.position.y + distance.y) ||
                    (transform.position.x >= target.position.x - distance.x && transform.position.y == target.position.y - distance.y)){
                    OnPoint = true;
                }
                else{
                    OnPoint = false;
                }
            }
            else{
                if((transform.position.x == target.position.x &&  transform.position.y == target.position.y)){
                    OnPoint = true;
                }
                else{
                    OnPoint = false;
                }
            }
            
        }
        else{
            OnPoint = false;
        }
    }

    public void Move(){
        ///Jika target = Player
        if(playerPosition < EggPosition && OnPoint == false){
            target = targetPlayer;
            Debug.Log("Target = Player");
            if(transform.position.x > target.position.x){
                transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed*Time.deltaTime);
            }
            else{
                transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed*Time.deltaTime);
            }
            animator.SetBool("Run", true);
            TurnDirection();
        }
        ///Jika target = Egg
        else if(playerPosition > EggPosition && OnPoint == false){
            target = targetEgg;
            Debug.Log("Target = Egg");
            if(transform.position.x > target.position.x)
                transform.position = Vector2.MoveTowards(transform.position, targetEgg.position + distance, moveSpeed*Time.deltaTime);
            else{
                transform.position = Vector2.MoveTowards(transform.position, targetEgg.position - distance, moveSpeed*Time.deltaTime);
            }
            animator.SetBool("Run", true);
            TurnDirection();
        }
        ///Jika Enemy sampai ke target
        else if(OnPoint == true){
            Debug.Log("Target = Attack Target");
            if(!Running()){
                AttackTimer();
            }
            else{
                animator.SetBool("Run", false);
            }
            
            TurnDirection();
        }
    }

    ///Cek arah wajah enemy ke player
    public void TurnDirection(){
        if(transform.position.x > target.position.x){
            sp.flipX = true;
        }
        else{
            sp.flipX = false;
        }
    }

    ///ATTACK ANIMATION
    public void Attack(){
        animator.SetTrigger("Attack");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        Collider2D[] hitEgg = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, eggLayers);
        foreach(Collider2D player in hitPlayer) {
            player.GetComponent<playerCombat>().TakeDamage(attack);
        }
        foreach(Collider2D egg in hitEgg) {
            egg.GetComponent<goddesEgg>().TakeDamage(attack);
        }
    }


    public void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.name == "Player" || coll.gameObject.name == "GoddesEgg"){
             AttackTimer();
         }
    }

    ///ENEMY ATTACK TIMER
    public void AttackTimer(){
        if(setTimer <= 0){
            Attack();
            setTimer = timer;
        }
    }

    ///CHECKING IF ENEMY DIE
    public void Die(){
        animator.SetBool("IsDead", true);
        player.souls += souls;
        PlayerPrefs.SetInt("Souls", player.souls);
        // Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        Destroy(gameObject);
    }

    ///Menerima damage
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        ///Cek jika healt = 0 (DEAD)
        if(currentHealth <= 0) {
            Die();
        }
    }
}
