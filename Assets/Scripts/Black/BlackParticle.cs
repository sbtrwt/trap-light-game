using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TrapLight.Light
{

    public class BlackParticle : MonoBehaviour
    {
        [SerializeField] private float speed = 50;
        private Rigidbody2D rb2D;

        private LineRenderer line; // Reference to LineRenderer
        private Vector3 startPos;    // Start position of line
        private Vector3 endPos;    // End position of line
        [SerializeField] private GameObject explosiveItemPrefab;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();

        }
        private void Update()
        {
            rb2D.MovePosition(rb2D.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed);

            HandleInput();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log(collision.gameObject.tag);
        }

        void HandleInput()
        {


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (line == null)
                    CreateLine();

                line.SetPosition(0, rb2D.position);
                startPos = rb2D.position;
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                if (line)
                {

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

                    line.SetPosition(1, rb2D.position);
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                CreateExplosive();
            }
        }

        private void CreateLine()
        {
            line = new GameObject("Line").AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Diffuse"));
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

        private void CreateExplosive()
        {
            if (explosiveItemPrefab != null)
                Instantiate(explosiveItemPrefab, transform.position, Quaternion.identity);
        }
    }
}