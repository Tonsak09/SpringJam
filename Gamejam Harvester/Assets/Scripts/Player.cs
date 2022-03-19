using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    private Vector2 lookDir;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        lookDir = new Vector2();
        lookDir = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Collection();
    }

    private void Collection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir, 2, mask);

            if (hit.collider != null)
            {
                print(hit.collider);
            }
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else
        {
            rb.velocity *= Vector2.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, 0);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity *= Vector2.up;
        }

        if(rb.velocity != Vector2.zero)
        {
            lookDir = rb.velocity.normalized;
        }
    }
}
