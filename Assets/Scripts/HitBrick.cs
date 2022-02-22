using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBrick : MonoBehaviour
{
    private int numberOfHits = 0;
    private int timer = 0;
    private int cooldownTimer = 15;
    public int maxHits;
    private SpriteRenderer brickSprite;
    public Sprite[] phases;
    private GameManagerScript manager;

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        manager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        manager.numBricks++;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer--;

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (timer == 0) {
            timer = cooldownTimer;
            if (numberOfHits < phases.Length)
                brickSprite.sprite = phases[numberOfHits];

            numberOfHits++;
            if (numberOfHits >= maxHits) {
                manager.destroyBrick();
                Destroy(this.gameObject);
            }
        }
    }
}
