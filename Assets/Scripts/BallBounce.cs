using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private bool ballLaunched = false;
    private Rigidbody2D rb;
    public Vector2[] startDirections;
    private int randomNumber;
    public float ballForce;
    private Vector3 paddleOffset = new Vector3(0.2f, 0.3f, 0);
    private GameObject paddle;
    private GameManagerScript manager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddle = GameObject.Find("Paddle");
        manager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballLaunched) {
            transform.position = paddle.transform.position + paddleOffset;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //launch ball
                randomNumber = Random.Range(0, startDirections.Length);
                rb.AddForce(Vector3.Normalize(startDirections[randomNumber])* ballForce, ForceMode2D.Impulse);
                ballLaunched = true;
            }
        } else {
            //Jank code for avoiding bad situations
            if (Mathf.Abs(rb.velocity.x) < 1) rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * 2, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < 1) rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * 2);
            rb.velocity = Vector3.Normalize(rb.velocity) * ballForce;
        }

        //Cheats
        if (Input.GetKeyDown(KeyCode.R)) {
            ballLaunched = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "defeat")
        {
            rb.velocity = Vector3.zero;
            ballLaunched = false;
            manager.loseBall();
        }
    }
}
