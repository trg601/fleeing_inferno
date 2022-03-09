using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{
    public float speed = 10;
    private float playerInput;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Sprite defaultPaddle;
    public Sprite longPaddle;
    private BoxCollider2D boxCollider;
    private float defaultWidth;
    private float longWidth = 4.2f;
    private Transform character;
    private Vector3 charPos;
    private float charOffsetX = 0.85f;
    private Animator charAnim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        defaultWidth = boxCollider.size.x;
        defaultPaddle = sprite.sprite;
        character = gameObject.transform.Find("Character");
        charPos = character.localPosition;
        charAnim = character.gameObject.GetComponent<Animator>();
        charAnim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(playerInput) > 0.1) {
            charAnim.Play("Run");
        } else {
            charAnim.Play("Idle");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector2.right * Time.deltaTime * speed * playerInput);
    }

    public void setLongPaddle(bool hasLongPaddle) {
        if (hasLongPaddle) {
            sprite.sprite = longPaddle;
            boxCollider.size = new Vector2(longWidth, boxCollider.size.y);
            character.localPosition = new Vector3(charPos.x - charOffsetX, charPos.y, 0);
        } else {
            sprite.sprite = defaultPaddle;
            boxCollider.size = new Vector2(defaultWidth, boxCollider.size.y);
            character.localPosition = charPos;
        }
    }
}
