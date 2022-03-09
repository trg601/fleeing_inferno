using System.Collections;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private bool ballLaunched = false;
    private Rigidbody2D rb;
    public Vector2[] startDirections;
    private int randomNumber;
    public float ballForce;
    public float originalBallForce;
    private Vector3 paddleOffset = new Vector3(0.2f, 0.3f, 0);
    private GameObject paddle;
    private MovePaddle paddleController;
    private GameManagerScript manager;
    private AudioManager audioManager;
    private BallPowers ballPowers;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballPowers = GetComponent<BallPowers>();
        paddle = GameObject.Find("Paddle");
        paddleController = paddle.GetComponent<MovePaddle>();
        manager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        audioManager = manager.GetAudioManager();
        originalBallForce = ballForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballLaunched) {
            transform.position = paddle.transform.position + paddleOffset;
            rb.velocity = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //launch ball
                randomNumber = Random.Range(0, startDirections.Length);
                rb.AddForce((startDirections[randomNumber]).normalized * ballForce, ForceMode2D.Impulse);
                setBallLaunched(true);
            }
        } else {
            //Jank code for avoiding bad situations
            if (Mathf.Abs(rb.velocity.x) < 1) rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * 2, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < 1) rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * 2);
            rb.velocity = rb.velocity.normalized * ballForce;
        }

        //Cheats
        if (Input.GetKeyDown(KeyCode.R)) {
            ballLaunched = false;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("powerUp")) {
            ballPowers.usePowerUp(other.GetComponent<PowerUpController>());
        }
        else if(other.gameObject.CompareTag("defeat"))
        {
            rb.velocity = Vector3.zero;
            setBallLaunched(false);
            manager.loseBall();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("paddle") && rb.velocity != Vector2.zero) {
            audioManager.PlayClip(AudioManager.Clip.HardHit);
        } else if (other.gameObject.CompareTag("wall")) {
            audioManager.PlayClip(AudioManager.Clip.SoftHit);
        }
    }
    
    public void setSpeedAdjustment(float factor) {
        ballForce = originalBallForce * factor;
    }

    private void setBallLaunched(bool launched) {
        ballLaunched = launched;
        ballPowers.setBallLaunched(launched);
    }
}
