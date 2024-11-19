using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool canMove;
    public Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Camera cam;

    public float moveSpeed;
    private Vector2 movement;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        bc = this.GetComponent<BoxCollider2D>();
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        GameManager.current.onDialogueStart += disableMove;
        GameManager.current.onDialogueEnd += enableMove;

        GameManager.current.onToggleUIOn += disableMove;
        GameManager.current.onToggleUIOff += enableMove;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = this.transform.position;

        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            moveNormal();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    void moveNormal()
    {
        movement.Normalize();

        if (movement != Vector2.zero)
        {
            bc.offset = new Vector2(movement.x, movement.y);

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void enableMove()
    {
        canMove = true;
    }

    void disableMove()
    {
        canMove = false;
        movement = Vector2.zero;
        moveNormal();
    }
}
