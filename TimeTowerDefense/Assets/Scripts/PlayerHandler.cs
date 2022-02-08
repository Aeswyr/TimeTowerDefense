using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    [SerializeField] private InputHandler input;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private float MAX_SPEED;
    [SerializeField] private float JUMP_SPEED;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (input.dir != 0) {
            speed = MAX_SPEED;
            sprite.flipX = input.dir < 0;
            rbody.drag = 0;
            rbody.velocity = new Vector2(speed * input.dir, rbody.velocity.y);
            animator.SetBool("running", true);
        } else {
            animator.SetBool("running", false);
            rbody.drag = 7;
        }

        if (input.jump.pressed) {
            Debug.Log("BOING");
            animator.SetTrigger("jumping");
            rbody.velocity = new Vector2(rbody.velocity.x, JUMP_SPEED);
        }

        //animator.SetFloat("speed", Mathf.Abs(rbody.velocity.x));
    }
}
