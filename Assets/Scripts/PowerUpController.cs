using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public enum Type {
        LongPaddle,
        Magnet,
        FireBall
    }

    public Type type;
    private bool active = true;
    private SpriteRenderer sprite;
    private SpriteRenderer[] stars;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        stars = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Fade out powerup
        if (!active) {
            float alpha = sprite.color.a - 0.5f * Time.deltaTime;
            if (alpha <= 0) Destroy(gameObject);

            sprite.color = new Color(1f, 1f, 1f, alpha);
            foreach (SpriteRenderer star in stars) {
                alpha = star.color.a - 0.5f * Time.deltaTime;
                star.color = new Color(1f, 1f, 1f, alpha);
            }
        }
    }

    public bool isActive() {
        return active;
    }

    public void usePowerUp() {
        active = false;
    }
}
