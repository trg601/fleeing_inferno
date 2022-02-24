using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrickScript : MonoBehaviour
{
    private float time = 0;
    private float timescale = 0.7f;
    private SpriteRenderer brickSprite;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        brickSprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += timescale * Time.deltaTime;
        if (time > 5000f) time -= 5000f;

        float alpha = (Mathf.Sin(time) + 1) / 2;
        brickSprite.color = new Color(1f, 1f, 1f, alpha);
        if (alpha < 0.6f) box.isTrigger = true;
        else box.isTrigger = false;
    }
}
