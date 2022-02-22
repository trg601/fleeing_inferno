using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{
    public float speed = 10;
    private float playerInput;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * Time.deltaTime * speed * playerInput);
    }
}
