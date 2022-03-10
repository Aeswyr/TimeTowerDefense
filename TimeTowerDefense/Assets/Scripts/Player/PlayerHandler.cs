using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{

    [SerializeField] private InputHandler input;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private JumpHandler jump;
    [SerializeField] private MovementHandler move;
    [SerializeField] private GroundedCheck ground;
    [SerializeField] private GameObject towerIndicatorPrefab;
    private GameObject towerIndicator;


    private bool grounded;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Gamemode = Mode.MOVE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool prevGrounded = grounded;
        grounded = ground.CheckGrounded();
        if (grounded && !prevGrounded)
            animator.SetTrigger("land");

        if (input.dir != 0) {
            sprite.flipX = input.dir < 0;
            move.UpdateMovement(input.dir);
        } else {
            animator.SetBool("running", false);
        }

        if (input.move.pressed) {
            move.StartAcceleration(input.dir);
            animator.SetBool("running", true);
        }

        if (input.move.released) {
            move.StartDeceleration();
        }

        if (input.jump.pressed) {
            animator.SetTrigger("jump");
            jump.StartJump();
            animator.SetBool("grounded", false);
        } else {
            animator.SetBool("grounded", grounded && rbody.velocity.y < 1);
        }

        if (input.mode.pressed) {
            if (towerIndicator == null) {
                towerIndicator = Instantiate(towerIndicatorPrefab);
            } else {
                Destroy(towerIndicator);
                towerIndicator = null;
            }
        }

        if (towerIndicator != null && !grounded) {
            Destroy(towerIndicator);
            towerIndicator = null;
        }

        if (towerIndicator != null) {
            Grid levelGrid = GameController.Instance.GetLevelGrid();
            towerIndicator.transform.position = levelGrid.CellToWorld(levelGrid.WorldToCell(this.transform.position)) + new Vector3(0, 1, 0);
        }
        
    }
}
