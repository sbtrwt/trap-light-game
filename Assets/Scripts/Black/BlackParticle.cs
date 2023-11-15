using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackParticle : MonoBehaviour
{
    [SerializeField] private float speed = 50;
    private Rigidbody2D rb2D;

    private LineRenderer line; // Reference to LineRenderer
    private Vector3 mousePos;
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        rb2D.MovePosition(rb2D.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed);
       
        LineInput();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
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

    void LineInput()
    {

        // On mouse down new line will be created 
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (line == null)
               CreateLine();
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos.z = 0;
            //line.SetPosition(0, mousePos);
            //startPos = mousePos;
            line.SetPosition(0, rb2D.position);
            startPos = rb2D.position;
        }
        //else if (Input.GetMouseButtonUp(0))
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (line)
            {
                //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //mousePos.z = 0;
                //line.SetPosition(1, mousePos);
                //endPos = mousePos;
                line.SetPosition(1, rb2D.position);
                endPos = rb2D.position;
                AddColliderToLine();
                line = null;
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (line)
            {
                //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //mousePos.z = 0;
                //line.SetPosition(1, mousePos);
                line.SetPosition(1, rb2D.position);
            }
        }
    }
    // Following method creates line runtime using Line Renderer component
    private void CreateLine()
    {
        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
        //line.SetVertexCount(2);
        //line.SetWidth(0.1f, 0.1f);
        //line.SetColors(Color.black, Color.black);
        line.useWorldSpace = true;
    }
    // Following method adds collider to created line
    private void AddColliderToLine()
    {
        BoxCollider2D col = new GameObject("Collider").AddComponent<BoxCollider2D>();
        col.transform.parent = line.transform; // Collider is added as child object of line
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
       
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (startPos + endPos) / 2;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between startPos and endPos
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);
    }
}
