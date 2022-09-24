using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    Animator animator;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("moveY", rb.velocity.y);


        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1
                || Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Vertical") == 1)
        {
            if (canMove)
            {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        bottomLeftLimit = bottomLeft + new Vector3(0.5f, 0.5f, 0);
        topRightLimit = topRight + new Vector3(-0.5f, -0.5f, 0);
    }
}
