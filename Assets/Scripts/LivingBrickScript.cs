using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBrickScript : HitBrick
{
    public GameObject livingBrickPrefab;
    private Sprite originalSprite;

    public override void Start()
    {
        base.Start();
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public override void onHit(Collision2D other, int numberOfHits)
    {
        //Find neighboring bricks that can be infected
        Collider2D[] neighbors = Physics2D.OverlapBoxAll(transform.position, new Vector2(4, 2), 0f);
        for (int i = 0; i < neighbors.Length; i++) {
            if (!neighbors[i].gameObject) continue;
            GameObject neighbor = neighbors[i].gameObject;
            if (neighbor.CompareTag("infectableBrick")) {
                GameObject b = Instantiate(livingBrickPrefab, neighbor.transform.position, neighbor.transform.rotation);
                neighbor.GetComponent<HitBrick>().destroy();
                b.GetComponent<SpriteRenderer>().sprite =  originalSprite;
            }
        }
    }
}
