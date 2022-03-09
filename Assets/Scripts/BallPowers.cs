using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowers : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject paddle;
    private MovePaddle paddleController;
    private BallBounce ballBounce;
    private bool ballLaunched;
    private Transform magnet;
    private float magnetForce = 10;
    private GameObject brickTarget;
    private SpriteRenderer magnetSprite;
    private Transform fireball;
    private float fireballSpeed = 1.5f;
    private SpriteRenderer fireballSprite;
    private Vector3 prevPos;

    //Power Ups
    private bool hasMagnet = false;
    private bool hasFireball = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddle = GameObject.Find("Paddle");
        paddleController = paddle.GetComponent<MovePaddle>();
        ballBounce = GetComponent<BallBounce>();
        magnet = gameObject.transform.Find("Magnet");
        magnetSprite = magnet.GetComponent<SpriteRenderer>();
        magnet.gameObject.SetActive(false);
        fireball = gameObject.transform.Find("Fireball");
        fireballSprite = fireball.GetComponent<SpriteRenderer>();
        fireball.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMagnet) {
            //Find nearby target
            GameObject[] targets = GameObject.FindGameObjectsWithTag("magnetTarget");
            brickTarget = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject b in targets)
            {
                float curDistance = (b.transform.position - position).sqrMagnitude;
                if (curDistance < distance)
                {
                    brickTarget = b;
                    distance = curDistance;
                }
            }
        }

        if (hasFireball) {
            //Go faster with webassembly
            if (!ballLaunched) fireball.up = Vector3.down;
            else fireball.up = rb.velocity;
        }

        prevPos = transform.position;
    }

    public void FixedUpdate() {
        if (hasMagnet && brickTarget != null) {
            //Move towards target brick
            magnet.up = brickTarget.transform.position - magnet.position;
            rb.AddForce(new Vector2(magnet.up.x, magnet.up.y).normalized * magnetForce);
        }
    }

    public void usePowerUp(PowerUpController power) {

        if (power.isActive()) {
            StartCoroutine(powerUpCoroutine(power.type));
            power.usePowerUp(); //Destroy powerup
        }
    }

    private IEnumerator powerUpCoroutine(PowerUpController.Type type) {
        switch(type) {
            case PowerUpController.Type.LongPaddle:
                paddleController.setLongPaddle(true);
                yield return new WaitForSeconds(20);
                paddleController.setLongPaddle(false);
            break;
            case PowerUpController.Type.Magnet:
                hasMagnet = true;
                magnet.gameObject.SetActive(true);
                yield return new WaitForSeconds(20);
                hasMagnet = false;
                magnet.gameObject.SetActive(false);
            break;
            case PowerUpController.Type.FireBall:
                hasFireball = true;
                fireball.gameObject.SetActive(true);
                ballBounce.setSpeedAdjustment(fireballSpeed);
                yield return new WaitForSeconds(10);
                hasFireball = false;
                fireball.gameObject.SetActive(false);
                ballBounce.setSpeedAdjustment(1f);
            break;
            default:
                yield return new WaitForSeconds(0);
            break;
        }
    }
    
    public void setBallLaunched(bool launched) {
        ballLaunched = launched;
    }
}
