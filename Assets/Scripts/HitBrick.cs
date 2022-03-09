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
    private AudioManager audioManager;
    public GameObject brickExplosion;

    // Start is called before the first frame update
    public virtual void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        manager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        manager.numBricks++;
        timer = 200;
        audioManager = manager.GetAudioManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer--;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball")) {
            if (timer == 0) {
                timer = cooldownTimer;
                if (numberOfHits < phases.Length)
                    brickSprite.sprite = phases[numberOfHits];

                numberOfHits++;
                
                //for special brick types
                onHit(other, numberOfHits);

                if (numberOfHits >= maxHits) {
                    audioManager.PlayClip(AudioManager.Clip.HardHit);
                    destroy();
                } else audioManager.PlayClip(AudioManager.Clip.BrickBreak);
            }
        }
    }

    public void destroy() {
        GameObject part = Instantiate(brickExplosion, transform.position, brickExplosion.transform.rotation);
        var main = part.GetComponent<ParticleSystem>().main;
        main.startColor = brickSprite.color;
        manager.destroyBrick();
        Destroy(this.gameObject);
    }

    public virtual void onHit(Collision2D other, int numberOfHits) {
    }
}
