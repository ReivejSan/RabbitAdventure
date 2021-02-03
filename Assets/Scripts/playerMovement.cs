using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rb;
    private float dirX, dirY;
    public float speed = 0f;
    private Vector3 localScale;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player.animator = GetComponent<Animator>();
    
        player = GetComponent<Player>();
        localScale = transform.localScale;
    }

    void Update() {
        dirX = Input.GetAxisRaw("Horizontal") * player.speed;
        dirY = Input.GetAxisRaw("Vertical") * player.speed;

        AnimationControl();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(dirX, dirY);
    }

    private void LateUpdate() {
        if(rb.velocity.x > 0) {
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if(rb.velocity.x < 0) {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    private void AnimationControl() {
        if(rb.velocity.y == 0 && rb.velocity.x == 0) {
            player.animator.SetBool("run", false);
        }
        else{
            player.animator.SetBool("run", true);
        }
    }
}
