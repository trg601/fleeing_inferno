using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrickScript : MonoBehaviour
{
    int time = 0;
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
        time++;
        float alpha = (Mathf.Sin(time / 200f) + 1)/2;
        brickSprite.color = new Color(1f, 1f, 1f, alpha);
        if (alpha < 0.6) box.isTrigger = true;
        else box.isTrigger = false;
    }
}
