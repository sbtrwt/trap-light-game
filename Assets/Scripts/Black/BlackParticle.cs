using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackParticle : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        rb2D.MovePosition(rb2D.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed);
        ValidatePosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    private void ValidatePosition()
    {
        if (GlobalConstant.MAX_HEIGHT < rb2D.position.y)
        {
            rb2D.position = new Vector2(rb2D.position.x, GlobalConstant.MIN_HEIGHT);
        }
        if (GlobalConstant.MIN_HEIGHT > rb2D.position.y)
        {
            rb2D.position = new Vector2(rb2D.position.x, GlobalConstant.MAX_HEIGHT);
        }
        if (GlobalConstant.MAX_WIDTH < rb2D.position.x)
        {
            rb2D.position = new Vector2(GlobalConstant.MIN_WIDTH, rb2D.position.y);
        }
        if (GlobalConstant.MIN_WIDTH > rb2D.position.x)
        {
            rb2D.position = new Vector2(GlobalConstant.MAX_WIDTH, rb2D.position.y);
        }
    }
}
