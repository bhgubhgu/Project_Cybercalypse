using UnityEngine;
using System.Collections;

public class vampireScript : MonoBehaviour {

    [SerializeField] float      speed = 1.0f;
    [SerializeField] float      jumpForce = 5.0f;
    [SerializeField] bool       attackAnticipation = true;

    private float               inputX;
    private Animator            animator;
    private Rigidbody2D         body2d;
    private bool                flipflop = true;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // -- Handle input and movement --
        inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        // Move
        body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

        // -- Handle Animations --
        animator.SetBool("Grounded", IsGrounded());

        //Death
        if (Input.GetKeyDown("k"))
            animator.SetTrigger("Death");
        //Hurt
        else if (Input.GetKeyDown("h"))
            animator.SetTrigger("Hurt");

        //Dodge
        else if(Input.GetKeyDown("left shift"))
            animator.SetTrigger("Dodge");

        //Attack
        else if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("AttackAnticipation", attackAnticipation);

            //Change between Light and Heavy attack each strike
            if (flipflop)
                animator.SetTrigger("AttackLight");
            else
                animator.SetTrigger("AttackHeavy");

            flipflop = !flipflop;
        }

        //Jump
        else if(Input.GetKeyDown("space") && IsGrounded())
        {
            animator.SetTrigger("JumpStart");
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
        }

        //Walk
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            animator.SetInteger("AnimState", 1);
        //Idle
        else
            animator.SetInteger("AnimState", 0);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.05f);
    }
}
